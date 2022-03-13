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

namespace HumanCrypto {
    public partial class Form1 : Form {
        Wallet wallet;
        Web3Controller controller;
        int page1 = 0;
        int page2 = 0;
        CachedImages cachedImages;


        public Form1(Wallet wallet) {
            InitializeComponent();

            this.wallet = wallet;
            this.controller = new Web3Controller(wallet);
            this.cachedImages = new CachedImages(controller);

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKey1Txt, networkChainTxt, contractKeyTxt, priorityFeeTxt };
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
            PicassoConstruction picasso = new PicassoConstruction(new GenomeProcessing());

            e.Graphics.DrawImage(picasso.GetBitmap(), new Point { X = 0, Y = 0 });
        }
        private void button1_Click(object sender, EventArgs e) {
            pictureBox1.Invalidate();
        }
        private async void button2_Click(object sender, EventArgs e) {
            CancellationTokenSource source = new CancellationTokenSource(60000);
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(wallet.GetWeb3(), Properties.Secret.Default.ContractKey);
            TransactionReceipt receipt = null;
            string errorMessage = "Transaction failed";

            try {
                var transactionFunction = new CreatePrimeAvatarFunction {
                    MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei)
                };
                receipt = await service.CreatePrimeAvatarRequestAndWaitForReceiptAsync(transactionFunction, source);
            } catch (Exception ex) {
                errorMessage = ex.Message;
            }

            if (receipt == null || receipt.Failed()) {
                notifyControl.ShowBalloonTip(5000, "Contract function call", errorMessage, ToolTipIcon.Error);
            } else {
                notifyControl.ShowBalloonTip(5000, "Contract function call", "Transaction succeded", ToolTipIcon.None);
            }
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
            Properties.Secret.Default.ChainId = Int32.Parse(networkChainTxt.Text);
            Properties.Secret.Default.ContractKey = contractKeyTxt.Text;
            Properties.Secret.Default.PriorityFeeGwei = Double.Parse(priorityFeeTxt.Text);
            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }
        private async void deployContractBtn_Click(object sender, EventArgs e) {
            CancellationTokenSource source = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string contractAddress = "Failed";

            try {
                contractAddress = await controller.DeployContract();
            } catch (Exception ex) {
                notifyControl.ShowBalloonTip(5000, "Contract deployment", ex.Message, ToolTipIcon.Error);
                return;
            }

            notifyControl.ShowBalloonTip(5000, "Contract deployment", "New contract deployed", ToolTipIcon.None);
            contractKeyTxt.Text = receipt.ContractAddress;
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
        #endregion

    }

}
