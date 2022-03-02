using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanCrypto {
    public partial class Form1 : Form {
        Web3 web3;
        Account web3Account;

        public Form1() {
            InitializeComponent();

            web3 = new Web3("https://kovan.infura.io/v3/9aa3d95b3bc440fa88ea12eaa4456161");
            InitWalletAccount();

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKeyTxt, networkChainTxt };
            foreach (Control control in settingsBoundedControls) {
                control.TextChanged += genericControl_TextChanged;
            }
        }

        private async void Form1_Load(object sender, EventArgs e) {
            var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            Console.WriteLine($"Latest Block Number is: {latestBlockNumber}");
        }


        private void InitWalletAccount() {
            web3Account = new Account(Properties.Secret.Default.PrivateKey, Properties.Secret.Default.ChainId);
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
            updatingControls = false;
        }

        private void genericControl_TextChanged(object sender, EventArgs e) {
            if (updatingControls) return;
            saveSettingsBtn.Enabled = true;
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e) {
            Properties.Secret.Default.APIKey = apiKeyTxt.Text;
            Properties.Secret.Default.PrivateKey = privateKeyTxt.Text;
            Properties.Secret.Default.ChainId =Convert.ToInt32(networkChainTxt.Text);

            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }


        #endregion


    }

}
