using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
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

        public Task<BigInteger> GetAvatarIdsOfAddressAsync(BigInteger index) {
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

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
            return receipt.ContractAddress;
        }

        public async Task CreatePrimeAvatarAsync() {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new CreatePrimeAvatarFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei)
                    };
                    receipt = await wallet.GetService().CreatePrimeAvatarRequestAndWaitForReceiptAsync();
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }

        public Task<AvatarOfferOutputDTO> GetAvatarOfferAsync(BigInteger avatarId) {
            return wallet.GetService().AvatarOfferQueryAsync(avatarId);
        }

        public async Task AcceptOfferAsync(BigInteger avatarId, BigInteger amountToSend) {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new CreatePrimeAvatarFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                        AmountToSend = amountToSend
                    };
                    receipt = await wallet.GetService().AcceptOfferRequestAndWaitForReceiptAsync(avatarId, source);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }

        public async Task MakeOfferAsync(BigInteger avatarId, BigInteger amount) {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new CreatePrimeAvatarFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei)
                    };
                    receipt = await wallet.GetService().CreateOfferRequestAndWaitForReceiptAsync(avatarId, amount, source);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }
    }
}
