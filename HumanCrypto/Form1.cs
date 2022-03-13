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
        Wallet wallet;
        Web3Controller controller;
        int page1 = 0;
        int page2 = 0;
        CachedImages cachedImages;

        AvatarOfferOutputDTO selectedAvatarOffer;
        BigInteger selectedOwnAvatar;

        public Form1(Wallet wallet) {
            InitializeComponent();

            this.wallet = wallet;
            this.controller = new Web3Controller(wallet);
            this.cachedImages = new CachedImages(controller);

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKey1Txt,privateKey2Txt, networkChainTxt, contractKeyTxt, priorityFeeTxt };
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
            }
        }
        private async void pictureBox2_MouseClick(object sender, MouseEventArgs e) {
            int iconsPerRow = 3;
            int padding = 20;
            Size resizedImageSize = new Size { Width = (pictureBox2.Width - 2 * padding) / iconsPerRow, Height = (pictureBox2.Width - 2 * padding) / iconsPerRow };
            int iconsPerColumn = (pictureBox2.Height - 2 * padding) / resizedImageSize.Height;

            int itemRow = (e.Y-padding) / resizedImageSize.Height;
            int itemCol = (e.X - padding) / resizedImageSize.Width;
            int itemIndex = itemRow * iconsPerRow + itemCol;
            int absoluteItemIndex = itemIndex + page1 * iconsPerRow * iconsPerColumn;

            BigInteger avatarsCount = await controller.GetAvatarsCountAsync();
            if (absoluteItemIndex>= avatarsCount) {
                priceLbl.Text = "Price: No offer";
                acceptOfferBtn.Enabled = false;
                return;
            }

            AvatarOfferOutputDTO offer = await controller.GetAvatarOfferAsync(absoluteItemIndex);
            if (!offer.Active) {
                priceLbl.Text = "Price: No offer";
                acceptOfferBtn.Enabled = false;
                return;
            }

            priceLbl.Text = $"Price: {offer.Amount} Wei";
            acceptOfferBtn.Enabled = true;
            selectedAvatarOffer = offer;
        }
        private async void acceptOfferBtn_Click(object sender, EventArgs e) {
            if (selectedAvatarOffer == null) return;
            
            acceptOfferBtn.Enabled = false;

            try {
                await controller.AcceptOfferAsync(selectedAvatarOffer.AvatarId);
            }catch(Exception ex) {
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

            selectedOwnAvatar = avatarId;
            makeOfferBtn.Enabled = true;
        }
        private async void makeOfferBtn_Click(object sender, EventArgs e) {
            makeOfferBtn.Enabled = false;

            try {
                await controller.MakeOfferAsync(selectedOwnAvatar, BigInteger.Parse(offerAmountTxt.Text));
            } catch (Exception ex) {
                notifyControl.ShowBalloonTip(5000, "Make Offer transaction", ex.Message, ToolTipIcon.Error);
                return;
            }
            notifyControl.ShowBalloonTip(5000, "Make Offer transaction", "An offer was just created", ToolTipIcon.None);
        }



        #endregion


    }

}
