using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanCrypto {
    class Web3Controller {
        Wallet wallet;
        CancellationTokenSource principalSource = new CancellationTokenSource();

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
            return wallet.GetService().AvatarIdsOfAddressQueryAsync(Address(), index);
        }

        public async Task<string> DeployContract() {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var deployParams = new HumanAvatarOwnerDeployment {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei)
                    };
                    receipt = await HumanAvatarOwnerService.DeployContractAndWaitForReceiptAsync(wallet.GetWeb3(), deployParams, source);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null) throw new Exception(errorMessage);
            return receipt.ContractAddress;
        }
    }
}
