using HumanAvatarContract.Contracts.HumanAvatarOwner;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class Wallet {
        static Wallet instance = null;
        string activePrivateKey;
        Web3 web3;
        HumanAvatarOwnerService service;


        private Wallet() { }

        public static Wallet GetInstance() {
            if (instance == null) {
                instance = new Wallet();
                Properties.Secret.Default.PropertyChanged += instance.Settings_PropertyChanged;
            }
            return instance;
        }

        public void SetWalletData(string privateKey) {
            activePrivateKey = privateKey;
            InitServices();
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            InitServices();
        }

        private void InitServices() {
            web3 = new Web3(new Account(activePrivateKey, Properties.Secret.Default.ChainId), $"https://kovan.infura.io/v3/{Properties.Secret.Default.APIKey}");
            service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);
        }

        public Web3 GetWeb3() {
            return web3;
        }

        public HumanAvatarOwnerService GetService() {
            return service;
        }
    }
}
