using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;

namespace HumanAvatarContract.Contracts.HumanAvatarOwner
{
    public partial class HumanAvatarOwnerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, HumanAvatarOwnerDeployment humanAvatarOwnerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<HumanAvatarOwnerDeployment>().SendRequestAndWaitForReceiptAsync(humanAvatarOwnerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, HumanAvatarOwnerDeployment humanAvatarOwnerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<HumanAvatarOwnerDeployment>().SendRequestAsync(humanAvatarOwnerDeployment);
        }

        public static async Task<HumanAvatarOwnerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, HumanAvatarOwnerDeployment humanAvatarOwnerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, humanAvatarOwnerDeployment, cancellationTokenSource);
            return new HumanAvatarOwnerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public HumanAvatarOwnerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AcceptOfferRequestAsync(AcceptOfferFunction acceptOfferFunction)
        {
             return ContractHandler.SendRequestAsync(acceptOfferFunction);
        }

        public Task<TransactionReceipt> AcceptOfferRequestAndWaitForReceiptAsync(AcceptOfferFunction acceptOfferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptOfferFunction, cancellationToken);
        }

        public Task<string> AcceptOfferRequestAsync(BigInteger avatarId)
        {
            var acceptOfferFunction = new AcceptOfferFunction();
                acceptOfferFunction.AvatarId = avatarId;
            
             return ContractHandler.SendRequestAsync(acceptOfferFunction);
        }

        public Task<TransactionReceipt> AcceptOfferRequestAndWaitForReceiptAsync(BigInteger avatarId, CancellationTokenSource cancellationToken = null)
        {
            var acceptOfferFunction = new AcceptOfferFunction();
                acceptOfferFunction.AvatarId = avatarId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptOfferFunction, cancellationToken);
        }

        public Task<AvatarOfferOutputDTO> AvatarOfferQueryAsync(AvatarOfferFunction avatarOfferFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<AvatarOfferFunction, AvatarOfferOutputDTO>(avatarOfferFunction, blockParameter);
        }

        public Task<AvatarOfferOutputDTO> AvatarOfferQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var avatarOfferFunction = new AvatarOfferFunction();
                avatarOfferFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<AvatarOfferFunction, AvatarOfferOutputDTO>(avatarOfferFunction, blockParameter);
        }

        public Task<AvatarsOutputDTO> AvatarsQueryAsync(AvatarsFunction avatarsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<AvatarsFunction, AvatarsOutputDTO>(avatarsFunction, blockParameter);
        }

        public Task<AvatarsOutputDTO> AvatarsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var avatarsFunction = new AvatarsFunction();
                avatarsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<AvatarsFunction, AvatarsOutputDTO>(avatarsFunction, blockParameter);
        }

        public Task<string> CancelOfferRequestAsync(CancelOfferFunction cancelOfferFunction)
        {
             return ContractHandler.SendRequestAsync(cancelOfferFunction);
        }

        public Task<TransactionReceipt> CancelOfferRequestAndWaitForReceiptAsync(CancelOfferFunction cancelOfferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelOfferFunction, cancellationToken);
        }

        public Task<string> CancelOfferRequestAsync(BigInteger avatarId)
        {
            var cancelOfferFunction = new CancelOfferFunction();
                cancelOfferFunction.AvatarId = avatarId;
            
             return ContractHandler.SendRequestAsync(cancelOfferFunction);
        }

        public Task<TransactionReceipt> CancelOfferRequestAndWaitForReceiptAsync(BigInteger avatarId, CancellationTokenSource cancellationToken = null)
        {
            var cancelOfferFunction = new CancelOfferFunction();
                cancelOfferFunction.AvatarId = avatarId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelOfferFunction, cancellationToken);
        }

        public Task<string> CreateOfferRequestAsync(CreateOfferFunction createOfferFunction)
        {
             return ContractHandler.SendRequestAsync(createOfferFunction);
        }

        public Task<TransactionReceipt> CreateOfferRequestAndWaitForReceiptAsync(CreateOfferFunction createOfferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createOfferFunction, cancellationToken);
        }

        public Task<string> CreateOfferRequestAsync(BigInteger avatarId, BigInteger amount)
        {
            var createOfferFunction = new CreateOfferFunction();
                createOfferFunction.AvatarId = avatarId;
                createOfferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(createOfferFunction);
        }

        public Task<TransactionReceipt> CreateOfferRequestAndWaitForReceiptAsync(BigInteger avatarId, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var createOfferFunction = new CreateOfferFunction();
                createOfferFunction.AvatarId = avatarId;
                createOfferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createOfferFunction, cancellationToken);
        }

        public Task<string> CreatePrimeAvatarRequestAsync(CreatePrimeAvatarFunction createPrimeAvatarFunction)
        {
             return ContractHandler.SendRequestAsync(createPrimeAvatarFunction);
        }

        public Task<string> CreatePrimeAvatarRequestAsync()
        {
             return ContractHandler.SendRequestAsync<CreatePrimeAvatarFunction>();
        }

        public Task<TransactionReceipt> CreatePrimeAvatarRequestAndWaitForReceiptAsync(CreatePrimeAvatarFunction createPrimeAvatarFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPrimeAvatarFunction, cancellationToken);
        }

        public Task<TransactionReceipt> CreatePrimeAvatarRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<CreatePrimeAvatarFunction>(null, cancellationToken);
        }
    }
}
