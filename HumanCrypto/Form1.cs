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
        Web3 web3;

        private GenomeProcessing GetGenome() {
            return new GenomeProcessing(new byte[] { 4, 4, 4, 4, 4, 4, 4, 4 });
        }


        public Form1() {
            InitializeComponent();

            web3 = new Web3(new Account(Properties.Secret.Default.PrivateKey, Properties.Secret.Default.ChainId), "https://kovan.infura.io/v3/9aa3d95b3bc440fa88ea12eaa4456161");

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKeyTxt, networkChainTxt, contractKeyTxt, priorityFeeTxt };
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
            GenomeProcessing genomeProcessing = GetGenome();
            PicassoConstruction picasso = new PicassoConstruction(genomeProcessing);

            e.Graphics.DrawImage(picasso.GetBitmap(),new Point { X = 0, Y = 0 });
        }

        private void button1_Click(object sender, EventArgs e) {
            pictureBox1.Invalidate();
        }

        private async void button2_Click(object sender, EventArgs e) {
            CancellationTokenSource source = new CancellationTokenSource(60000);
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);
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
            privateKeyTxt.Text = Properties.Secret.Default.PrivateKey;
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
            Properties.Secret.Default.PrivateKey = privateKeyTxt.Text;
            Properties.Secret.Default.ChainId = Int32.Parse(networkChainTxt.Text);
            Properties.Secret.Default.ContractKey = contractKeyTxt.Text;
            Properties.Secret.Default.PriorityFeeGwei = Double.Parse(priorityFeeTxt.Text);
            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }

        private async void deployContractBtn_Click(object sender, EventArgs e) {
            CancellationTokenSource source = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "Failed";

            try {
                var deployParams = new HumanAvatarOwnerDeployment {
                    MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei)
                };
                receipt = await HumanAvatarOwnerService.DeployContractAndWaitForReceiptAsync(web3, deployParams, source);
            } catch (Exception ex) {
                errorMessage = ex.Message;
            }

            if (receipt == null || receipt.Failed()) {
                notifyControl.ShowBalloonTip(5000, "Contract deployment", errorMessage, ToolTipIcon.Error);
            } else {
                notifyControl.ShowBalloonTip(5000, "Contract deployment", "New contract deployed", ToolTipIcon.None);
                contractKeyTxt.Text = receipt.ContractAddress;
            }
        }
        #endregion

        #region AllAvatars
        int currentIndex = 0;
        int maxAvatarsDisplayed = 3;
        int iconsPerRow = 3;
        List<Bitmap> availableAvatars = new List<Bitmap>();

        private async void tabAllAvatars_Enter(object sender, EventArgs e) {
            currentIndex = 0;
            availableAvatars = await LoadAvatarsAsync(currentIndex, maxAvatarsDisplayed);
        }
        private async void nextAvatarBtn_Click(object sender, EventArgs e) {
            if (availableAvatars.Count == 0) return;
            currentIndex += maxAvatarsDisplayed;
            availableAvatars = await LoadAvatarsAsync(currentIndex, maxAvatarsDisplayed);
        }
        private async void prevAvatarBtn_Click(object sender, EventArgs e) {
            if (currentIndex - maxAvatarsDisplayed < 0) return;

            currentIndex -= maxAvatarsDisplayed;
            availableAvatars = await LoadAvatarsAsync(currentIndex, maxAvatarsDisplayed);
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            if (availableAvatars.Count == 0) return;
            int bmpWidth = pictureBox2.Width / iconsPerRow;
            int bmpHeight = bmpWidth;

            for (int i=0;i<availableAvatars.Count;i++) {
                Bitmap bmp = availableAvatars[i];
                Point pos = new Point { X = (i % iconsPerRow)* bmpWidth, Y = (i / iconsPerRow) * bmpHeight };
                Size size = new Size { Width = bmpWidth, Height = bmpHeight };

                g.DrawImage(bmp, new Rectangle(pos, size));
            }
        }


        private async Task<List<Bitmap>> LoadAvatarsAsync(int startingIndex, int count) {
            List<Bitmap> results = new List<Bitmap>();
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);

            for (int i = 0; i < count; i++) {
                AvatarsOutputDTO result = null;

                try {
                    var transactionFunction = new AvatarsFunction {
                        MaxFeePerGas = 0,           // This should not consume any gas
                        ReturnValue1 = startingIndex+i
                    };
                    result = await service.AvatarsQueryAsync(transactionFunction);
                } catch (Exception ex) {
                }
                if (result == null) {
                    break;
                }

                GenomeProcessing genomeProcessing = GetGenome();
                genomeProcessing.ParseGenome(result.Genome.ToByteArray());

                PicassoConstruction picasso = new PicassoConstruction(genomeProcessing);
                results.Add(picasso.GetBitmap());
            }

            return results;
        }



        #endregion
        
    }

}
