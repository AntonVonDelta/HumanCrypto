using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
using System.Threading;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace HumanCrypto {
    public partial class Form1 : Form {
        class AvatarInfo {
            public AvatarsOutputDTO dto;
            public AvatarOfferOutputDTO offer;
            public Rectangle drawRect;
            public bool hasOffer;
        };
        class OwnAvatarInfo {
            public BigInteger avatarId;
            public AvatarOfferOutputDTO offer;
        };

        Wallet wallet;
        Web3Controller controller;
        int page1 = 0;
        int page2 = 0;
        CachedImages cachedImages;

        AvatarInfo selectedAvatar;
        OwnAvatarInfo selectedOwnAvatar;

        public Form1(Wallet wallet) {
            InitializeComponent();

            this.wallet = wallet;
            this.controller = new Web3Controller(wallet);
            this.cachedImages = new CachedImages(controller);

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKey1Txt, privateKey2Txt, networkChainTxt, contractKeyTxt, priorityFeeTxt };
            foreach (Control control in settingsBoundedControls) {
                control.TextChanged += genericControl_TextChanged;
            }

            tabAllAvatars.Enter += tabAllAvatars_Enter;
        }




        private void Form1_Load(object sender, EventArgs e) {
            // Set icon for notification control otherwise it is not displayed
            notifyControl.Icon = this.Icon;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            using (PicassoConstruction picasso = new PicassoConstruction(new GenomeProcessing())) {
                e.Graphics.DrawImage(picasso.GetBitmap(), new Point { X = 0, Y = 0 });
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            pictureBox1.Invalidate();
        }
        private async void button2_Click(object sender, EventArgs e) {

            try {
                await controller.CreatePrimeAvatarAsync();
            } catch (Exception ex) {
                notifyControl.ShowBalloonTip(5000, "Contract function call", ex.Message, ToolTipIcon.Error);
                return;
            }

            notifyControl.ShowBalloonTip(5000, "Contract function call", "Transaction succeded", ToolTipIcon.None);
        }


        #region SettingsTab
        // Variable which signals that controls are updated and so should not raise the Changed events or
        // ignore their effect
        private bool updatingControls = false;

        private void tabSettings_Enter(object sender, EventArgs e) {
            saveSettingsBtn.Enabled = false;

            updatingControls = true;
            apiKeyTxt.Text = Properties.Secret.Default.APIKey;
            privateKey1Txt.Text = Properties.Secret.Default.PrivateKey1;
            privateKey2Txt.Text = Properties.Secret.Default.PrivateKey2;

            networkChainTxt.Text = Properties.Secret.Default.ChainId.ToString();
            contractKeyTxt.Text = Properties.Secret.Default.ContractKey;
            priorityFeeTxt.Text = Properties.Secret.Default.PriorityFeeGwei.ToString();
            updatingControls = false;
        }
        private void genericControl_TextChanged(object sender, EventArgs e) {
            if (updatingControls) return;
            saveSettingsBtn.Enabled = true;
        }
        private void saveSettingsBtn_Click(object sender, EventArgs e) {
            Properties.Secret.Default.APIKey = apiKeyTxt.Text;
            Properties.Secret.Default.PrivateKey1 = privateKey1Txt.Text;
            Properties.Secret.Default.PrivateKey2 = privateKey2Txt.Text;

            Properties.Secret.Default.ChainId = Int32.Parse(networkChainTxt.Text);
            Properties.Secret.Default.ContractKey = contractKeyTxt.Text;
            Properties.Secret.Default.PriorityFeeGwei = Double.Parse(priorityFeeTxt.Text);
            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }
        private async void deployContractBtn_Click(object sender, EventArgs e) {
            string contractAddress = "";

            try {
                contractAddress = await controller.DeployContract();
            } catch (Exception ex) {
                notifyControl.ShowBalloonTip(5000, "Contract deployment", ex.Message, ToolTipIcon.Error);
                return;
            }

            notifyControl.ShowBalloonTip(5000, "Contract deployment", "New contract deployed", ToolTipIcon.None);
            contractKeyTxt.Text = contractAddress;
            Properties.Secret.Default.ContractKey = contractKeyTxt.Text;
            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }
        #endregion


        #region AllAvatars

        private void tabAllAvatars_Enter(object sender, EventArgs e) {
            page1 = 0;
            page2 = 0;

            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
        }
        private void nextAvatarBtn_Click(object sender, EventArgs e) {
            page1 += 1;
            pictureBox2.Invalidate();
        }
        private void prevAvatarBtn_Click(object sender, EventArgs e) {
            if (page1 - 1 < 0) return;

            page1 -= 1;
            pictureBox2.Invalidate();
        }
        private async void pictureBox2_Paint(object sender, PaintEventArgs e) {
            int iconsPerRow = 3;
            int padding = 20;
            Size resizedImageSize = new Size { Width = (pictureBox2.Width - 2 * padding) / iconsPerRow, Height = (pictureBox2.Width - 2 * padding) / iconsPerRow };
            int iconsPerColumn = (pictureBox2.Height - 2 * padding) / resizedImageSize.Height;

            List<Bitmap> availableAvatars = await cachedImages.GetAllAvatars(page1 * iconsPerRow * iconsPerColumn, iconsPerRow * iconsPerColumn);

            using (Graphics g = pictureBox2.CreateGraphics()) {
                for (int i = 0; i < availableAvatars.Count; i++) {
                    Bitmap bmp = availableAvatars[i];
                    Point pos = new Point { X = (i % iconsPerRow) * resizedImageSize.Width, Y = (i / iconsPerRow) * resizedImageSize.Height };

                    pos.X += padding;
                    pos.Y += padding;

                    g.DrawImage(bmp, new Rectangle(pos, resizedImageSize));

                }

                if (selectedAvatar != null) {
                    g.DrawRectangle(Pens.Black, selectedAvatar.drawRect);
                }
            }
        }
        private async void pictureBox2_MouseClick(object sender, MouseEventArgs e) {
            int iconsPerRow = 3;
            int padding = 20;
            Size resizedImageSize = new Size { Width = (pictureBox2.Width - 2 * padding) / iconsPerRow, Height = (pictureBox2.Width - 2 * padding) / iconsPerRow };
            int iconsPerColumn = (pictureBox2.Height - 2 * padding) / resizedImageSize.Height;

            int itemRow = (e.Y - padding) / resizedImageSize.Height;
            int itemCol = (e.X - padding) / resizedImageSize.Width;
            int itemIndex = itemRow * iconsPerRow + itemCol;
            int absoluteItemIndex = itemIndex + page1 * iconsPerRow * iconsPerColumn;
            Point pos = new Point(itemCol * resizedImageSize.Width + padding, itemRow * resizedImageSize.Height + padding);

            BigInteger avatarsCount = await controller.GetAvatarsCountAsync();
            if (absoluteItemIndex >= avatarsCount) {
                priceLbl.Text = "Price: No offer";
                acceptOfferBtn.Enabled = false;
                return;
            }


            AvatarsOutputDTO avatarInfo = await controller.AvatarsQueryAsync(absoluteItemIndex);
            AvatarInfo prevSelectedAvatar = selectedAvatar;
            selectedAvatar = new AvatarInfo { dto = avatarInfo, drawRect = new Rectangle(pos, resizedImageSize),hasOffer=false };

            // Set default image for picturebox4 and trigger a redraw for the current selected avatar
            pictureBox4.Image = Properties.Resources.NoParents;
            pictureBox4.Invalidate();

            // Trigger a redraw for this picturebox in order to show the border
            if (prevSelectedAvatar != null) {
                Rectangle prevRect = prevSelectedAvatar.drawRect;
                prevRect.Offset(5, 5);
                prevRect.Inflate(5, 5);
                pictureBox2.Invalidate(prevRect);
            }
            Rectangle currentRect = selectedAvatar.drawRect;
            currentRect.Offset(5, 5);
            currentRect.Inflate(5, 5);
            pictureBox2.Invalidate(currentRect);

            if (avatarInfo.AvatarOwner == controller.Address()) {
                priceLbl.Text = "You own this avatar";
                acceptOfferBtn.Enabled = false;
                return;
            }

            AvatarOfferOutputDTO offer = await controller.GetAvatarOfferAsync(absoluteItemIndex);
            selectedAvatar.offer = offer;
            if (!offer.Active) {
                priceLbl.Text = "Price: No offer";
                acceptOfferBtn.Enabled = false;
                return;
            }

            priceLbl.Text = $"Price: {offer.Amount} Wei";
            acceptOfferBtn.Enabled = true;
            selectedAvatar.hasOffer = true;
        }
        private async void acceptOfferBtn_Click(object sender, EventArgs e) {
            if (selectedAvatar == null || !selectedAvatar.hasOffer) return;

            acceptOfferBtn.Enabled = false;

            try {
                await controller.AcceptOfferAsync(selectedAvatar.offer.AvatarId, selectedAvatar.offer.Amount);
            } catch (Exception ex) {
                notifyControl.ShowBalloonTip(5000, "Offer transaction", ex.Message, ToolTipIcon.Error);
                return;
            }

            notifyControl.ShowBalloonTip(5000, "Offer transaction", "Offer was accepted", ToolTipIcon.None);
        }



        private void nextOwnAvatarBtn_Click(object sender, EventArgs e) {
            page2 += 1;
            pictureBox3.Invalidate();
        }
        private void prevOwnAvatarBtn_Click(object sender, EventArgs e) {
            if (page2 - 1 < 0) return;

            page2 -= 1;
            pictureBox3.Invalidate();
        }
        private async void pictureBox3_Paint(object sender, PaintEventArgs e) {
            int iconsPerRow = 2;
            int padding = 20;
            Size resizedImageSize = new Size { Width = (pictureBox3.Width - 2 * padding) / iconsPerRow, Height = (pictureBox3.Width - 2 * padding) / iconsPerRow };
            int iconsPerColumn = (pictureBox3.Height - 2 * padding) / resizedImageSize.Height;

            List<Bitmap> availableAvatars = await cachedImages.GetOwnAvatars(page2 * iconsPerRow * iconsPerColumn, iconsPerRow * iconsPerColumn);

            using (Graphics g = pictureBox3.CreateGraphics()) {
                for (int i = 0; i < availableAvatars.Count; i++) {
                    Bitmap bmp = availableAvatars[i];
                    Point pos = new Point { X = (i % iconsPerRow) * resizedImageSize.Width, Y = (i / iconsPerRow) * resizedImageSize.Height };

                    pos.X += padding;
                    pos.Y += padding;

                    g.DrawImage(bmp, new Rectangle(pos, resizedImageSize));
                }
            }
        }
        private async void pictureBox3_MouseClick(object sender, MouseEventArgs e) {
            int iconsPerRow = 2;
            int padding = 20;
            Size resizedImageSize = new Size { Width = (pictureBox3.Width - 2 * padding) / iconsPerRow, Height = (pictureBox3.Width - 2 * padding) / iconsPerRow };
            int iconsPerColumn = (pictureBox3.Height - 2 * padding) / resizedImageSize.Height;

            int itemRow = (e.Y - padding) / resizedImageSize.Height;
            int itemCol = (e.X - padding) / resizedImageSize.Width;
            int itemIndex = itemRow * iconsPerRow + itemCol;
            int absoluteItemIndex = itemIndex + page1 * iconsPerRow * iconsPerColumn;

            bool pressedShift = Form.ModifierKeys == Keys.Shift;

            BigInteger avatarsCount = await controller.GetAvatarIdsOfAddressCountAsync();
            if (absoluteItemIndex >= avatarsCount) {
                makeOfferBtn.Enabled = false;
                return;
            }

            BigInteger avatarId = await controller.GetAvatarIdsOfAddressAsync(absoluteItemIndex);
            AvatarOfferOutputDTO offer = await controller.GetAvatarOfferAsync(avatarId);
            if (offer.Active) {
                offerAmountTxt.Text = offer.Amount.ToString();
            } else {
                offerAmountTxt.Text = "0";
            }

            OwnAvatarInfo prevSelectedOwnAvatar = selectedOwnAvatar;
            selectedOwnAvatar = new OwnAvatarInfo { avatarId = avatarId, offer = offer };
            makeOfferBtn.Enabled = true;

            if (pressedShift && prevSelectedOwnAvatar != null) {
                try {
                    await controller.BreedBetween(prevSelectedOwnAvatar.avatarId, selectedOwnAvatar.avatarId);
                }catch(Exception ex) {
                    notifyControl.ShowBalloonTip(5000, "Breed transaction", ex.Message, ToolTipIcon.Error);
                    return;
                }
                notifyControl.ShowBalloonTip(5000, "Breed transaction", "Avatars succesfully bred", ToolTipIcon.None);
            }
        }
        private async void makeOfferBtn_Click(object sender, EventArgs e) {
            makeOfferBtn.Enabled = false;

            try {
                await controller.MakeOfferAsync(selectedOwnAvatar.avatarId, BigInteger.Parse(offerAmountTxt.Text));
            } catch (Exception ex) {
                notifyControl.ShowBalloonTip(5000, "Make Offer transaction", ex.Message, ToolTipIcon.Error);
                return;
            }
            notifyControl.ShowBalloonTip(5000, "Make Offer transaction", "An offer was just created", ToolTipIcon.None);
        }



        private async void pictureBox4_Paint(object sender, PaintEventArgs e) {
            int iconsPerRow = 2;
            int padding = 0;
            Size resizedImageSize = new Size { Width = (pictureBox4.Width - 2 * padding) / iconsPerRow, Height = (pictureBox4.Width - 2 * padding) / iconsPerRow };
            int iconsPerColumn = (pictureBox4.Height - 2 * padding) / resizedImageSize.Height;

            if (selectedAvatar == null) return;

            if (selectedAvatar.dto.Generation == 0) {
                // This avatar has no parents
                return;
            }

            List<Bitmap> parentAvatars = new List<Bitmap> { await cachedImages.GetAvatarById(selectedAvatar.dto.MomId), await cachedImages.GetAvatarById(selectedAvatar.dto.DadId) };

            using (Graphics g = pictureBox4.CreateGraphics()) {
                for (int i = 0; i < parentAvatars.Count; i++) {
                    Bitmap bmp = parentAvatars[i];
                    Point pos = new Point { X = (i % iconsPerRow) * resizedImageSize.Width, Y = (i / iconsPerRow) * resizedImageSize.Height };

                    pos.X += padding;
                    pos.Y += padding;

                    g.DrawImage(bmp, new Rectangle(pos, resizedImageSize));
                }
            }
        }

        #endregion


    }

}
