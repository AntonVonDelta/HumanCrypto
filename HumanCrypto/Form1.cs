﻿using Nethereum.Web3;
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
        GenomeProcessing genomeProcessing;




        public Form1() {
            InitializeComponent();

            web3 = new Web3(new Account(Properties.Secret.Default.PrivateKey, Properties.Secret.Default.ChainId), "https://kovan.infura.io/v3/9aa3d95b3bc440fa88ea12eaa4456161");

            // Init genome
            genomeProcessing = new GenomeProcessing(new byte[] { 4, 4, 4, 4, 4, 4, 4, 4 });

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKeyTxt, networkChainTxt, contractKeyTxt };
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
            genomeProcessing.Reset();

            PicassoConstruction picasso = new PicassoConstruction(genomeProcessing);
            e.Graphics.DrawImage(picasso.GetBitmap(), new Rectangle(0, 0, 500, 500));
        }



        private void button1_Click(object sender, EventArgs e) {
            genomeProcessing.Randomize();
            pictureBox1.Invalidate();
        }

        private async void button2_Click(object sender, EventArgs e) {
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);
            string errorMessage = "Transaction failed";
            TransactionReceipt receipt = null;

            try {
                receipt = await service.CreatePrimeAvatarRequestAndWaitForReceiptAsync();
            } catch (Exception ex) {
                errorMessage = ex.Message;
            }

            if (receipt.Failed()) {
                notifyControl.Text = errorMessage;
                notifyControl.ShowBalloonTip(5000);
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
            updatingControls = false;
        }

        private void genericControl_TextChanged(object sender, EventArgs e) {
            if (updatingControls) return;
            saveSettingsBtn.Enabled = true;
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e) {
            Properties.Secret.Default.APIKey = apiKeyTxt.Text;
            Properties.Secret.Default.PrivateKey = privateKeyTxt.Text;
            Properties.Secret.Default.ChainId = Convert.ToInt32(networkChainTxt.Text);
            Properties.Secret.Default.ContractKey = contractKeyTxt.Text;

            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }

        private async void button3_Click(object sender, EventArgs e) {
            var deployParams = new HumanAvatarOwnerDeployment {
                MaxPriorityFeePerGas = 100000000
            };
            CancellationTokenSource source = new CancellationTokenSource(20000);
            TransactionReceipt deploy = null;

            try {
                deploy = await HumanAvatarOwnerService.DeployContractAndWaitForReceiptAsync(web3, deployParams, source);
            } catch (TaskCanceledException ex) {
                notifyControl.ShowBalloonTip(2000, "HumanCrypto", "Contract deploy timedout", ToolTipIcon.Error);
                return;
            }

            if (deploy.Status.Value == 0) {
                notifyControl.ShowBalloonTip(2000, "HumanCrypto", "Contract failed", ToolTipIcon.Error);
                return;
            }

            notifyControl.ShowBalloonTip(2000, "HumanCrypto", "New contract deployed", ToolTipIcon.None);
            contractKeyTxt.Text = deploy.ContractAddress;
        }
        #endregion

        #region AllAvatars
        private void tabAllAvatars_Enter(object sender, EventArgs e) {
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);

            var transactionFunction = new AvatarsFunction { }
            service.AvatarsQueryAsync()
        }
        #endregion
    }

}
