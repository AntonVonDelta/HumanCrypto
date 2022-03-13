using HumanAvatarContract.Contracts.HumanAvatarOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class Web3Controller {
        Wallet wallet;

        public Web3Controller(Wallet wallet) {
            this.wallet = wallet;
        }

        public Task<BigInteger> GetAvatarsCountAsync() {
            return wallet.GetService().GetAvatarsCountQueryAsync();
        }


    }
}
