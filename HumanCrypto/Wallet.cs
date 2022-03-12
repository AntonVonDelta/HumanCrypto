﻿using HumanAvatarContract.Contracts.HumanAvatarOwner;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class Wallet {
        string activePrivateKey="";
        List<string> allAccounts;
        Web3 web3;
        HumanAvatarOwnerService service;


        public Wallet(List<string> allAccounts) {
            this.allAccounts = allAccounts;
            activePrivateKey = allAccounts[0];

            Properties.Secret.Default.PropertyChanged += Settings_PropertyChanged;

            InitServices();
        }

        public void LoadAccount(int id) {
            activePrivateKey = allAccounts[id];
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            InitServices();
        }

        private void InitServices() {
            if (activePrivateKey == string.Empty) return;

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
