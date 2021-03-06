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
            string errorMessage = "Error";

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
            string errorMessage = "Error";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new CreatePrimeAvatarFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei)
                    };
                    receipt = await wallet.GetService().CreatePrimeAvatarRequestAndWaitForReceiptAsync(transactionFunction);
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
            string errorMessage = "Error";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new AcceptOfferFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                        AvatarId=avatarId,
                        AmountToSend = amountToSend
                    };
                    receipt = await wallet.GetService().AcceptOfferRequestAndWaitForReceiptAsync(transactionFunction, source);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }

        public async Task MakeOfferAsync(BigInteger avatarId, BigInteger amount) {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "Error";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new CreateOfferFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                        AvatarId=avatarId,
                        Amount=amount
                    };
                    receipt = await wallet.GetService().CreateOfferRequestAndWaitForReceiptAsync(transactionFunction, source);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }

        public async Task CancelOfferAsync(BigInteger avatarId) {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "Error";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new CancelOfferFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                        AvatarId = avatarId
                    };
                    receipt = await wallet.GetService().CancelOfferRequestAndWaitForReceiptAsync(transactionFunction, source);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }

        public async Task BreedBetween(BigInteger momAvatarId,BigInteger dadAvatarId) {
            CancellationTokenSource timedSource = new CancellationTokenSource(60000);
            TransactionReceipt receipt = null;
            string errorMessage = "Error";

            using (CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(principalSource.Token, timedSource.Token)) {
                try {
                    var transactionFunction = new BreedBetweenFunction {
                        MaxPriorityFeePerGas = Web3.Convert.ToWei(Properties.Secret.Default.PriorityFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                        Gas= 280000,
                        MomAvatarId = momAvatarId,
                        DadAvatarId = dadAvatarId
                    };
                    receipt = await wallet.GetService().BreedBetweenRequestAndWaitForReceiptAsync(transactionFunction, source);
                    await Task.Delay(5000);
                } catch (Exception ex) {
                    errorMessage = ex.Message;
                }
            }

            if (receipt == null || receipt.Failed()) throw new Exception(errorMessage);
        }
    }
}
