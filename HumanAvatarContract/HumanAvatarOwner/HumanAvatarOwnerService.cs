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

        public Task<string> MakeAnOfferRequestAsync(MakeAnOfferFunction makeAnOfferFunction)
        {
             return ContractHandler.SendRequestAsync(makeAnOfferFunction);
        }

        public Task<TransactionReceipt> MakeAnOfferRequestAndWaitForReceiptAsync(MakeAnOfferFunction makeAnOfferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(makeAnOfferFunction, cancellationToken);
        }

        public Task<string> MakeAnOfferRequestAsync(BigInteger avatarId, BigInteger amount)
        {
            var makeAnOfferFunction = new MakeAnOfferFunction();
                makeAnOfferFunction.AvatarId = avatarId;
                makeAnOfferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(makeAnOfferFunction);
        }

        public Task<TransactionReceipt> MakeAnOfferRequestAndWaitForReceiptAsync(BigInteger avatarId, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var makeAnOfferFunction = new MakeAnOfferFunction();
                makeAnOfferFunction.AvatarId = avatarId;
                makeAnOfferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(makeAnOfferFunction, cancellationToken);
        }

        public Task<OffersForAvatarOutputDTO> OffersForAvatarQueryAsync(OffersForAvatarFunction offersForAvatarFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<OffersForAvatarFunction, OffersForAvatarOutputDTO>(offersForAvatarFunction, blockParameter);
        }

        public Task<OffersForAvatarOutputDTO> OffersForAvatarQueryAsync(BigInteger returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
        {
            var offersForAvatarFunction = new OffersForAvatarFunction();
                offersForAvatarFunction.ReturnValue1 = returnValue1;
                offersForAvatarFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryDeserializingToObjectAsync<OffersForAvatarFunction, OffersForAvatarOutputDTO>(offersForAvatarFunction, blockParameter);
        }

        public Task<OffersMadeByClientOutputDTO> OffersMadeByClientQueryAsync(OffersMadeByClientFunction offersMadeByClientFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<OffersMadeByClientFunction, OffersMadeByClientOutputDTO>(offersMadeByClientFunction, blockParameter);
        }

        public Task<OffersMadeByClientOutputDTO> OffersMadeByClientQueryAsync(string returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
        {
            var offersMadeByClientFunction = new OffersMadeByClientFunction();
                offersMadeByClientFunction.ReturnValue1 = returnValue1;
                offersMadeByClientFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryDeserializingToObjectAsync<OffersMadeByClientFunction, OffersMadeByClientOutputDTO>(offersMadeByClientFunction, blockParameter);
        }
    }
}
