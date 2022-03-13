using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
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

        public string Address() {
            return wallet.GetWeb3().TransactionManager.Account.Address;
        }

        public Task<BigInteger> GetAvatarsCountAsync() {
            return wallet.GetService().GetAvatarsCountQueryAsync();
        }

        public Task<AvatarsOutputDTO> AvatarsQueryAsync(BigInteger index) {
            return wallet.GetService().AvatarsQueryAsync(index);
        }

        public Task<BigInteger> GetAvatarIdsOfAddressCountAsync() {
            return wallet.GetService().GetAvatarIdsOfAddressCountQueryAsync();
        }

        public Task<BigInteger> AvatarIdsOfAddressQueryAsync(BigInteger index) {
            return wallet.GetService().AvatarIdsOfAddressQueryAsync(Address(),index);
        }
    }
}
