using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition
{


    public partial class HumanAvatarOwnerDeployment : HumanAvatarOwnerDeploymentBase
    {
        public HumanAvatarOwnerDeployment() : base(BYTECODE) { }
        public HumanAvatarOwnerDeployment(string byteCode) : base(byteCode) { }
    }

    public class HumanAvatarOwnerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public HumanAvatarOwnerDeploymentBase() : base(BYTECODE) { }
        public HumanAvatarOwnerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CreatePrimeAvatarFunction : CreatePrimeAvatarFunctionBase { }

    [Function("createPrimeAvatar")]
    public class CreatePrimeAvatarFunctionBase : FunctionMessage
    {

    }

    public partial class MakeAnOfferFunction : MakeAnOfferFunctionBase { }

    [Function("makeAnOffer")]
    public class MakeAnOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "avatarId", 1)]
        public virtual BigInteger AvatarId { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class OffersForAvatarFunction : OffersForAvatarFunctionBase { }

    [Function("offersForAvatar", typeof(OffersForAvatarOutputDTO))]
    public class OffersForAvatarFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }

    public partial class OffersMadeByClientFunction : OffersMadeByClientFunctionBase { }

    [Function("offersMadeByClient", typeof(OffersMadeByClientOutputDTO))]
    public class OffersMadeByClientFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }





    public partial class OffersForAvatarOutputDTO : OffersForAvatarOutputDTOBase { }

    [FunctionOutput]
    public class OffersForAvatarOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
        [Parameter("uint256", "futureExpirationTime", 2)]
        public virtual BigInteger FutureExpirationTime { get; set; }
        [Parameter("uint256", "avatarId", 3)]
        public virtual BigInteger AvatarId { get; set; }
        [Parameter("uint256", "amount", 4)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class OffersMadeByClientOutputDTO : OffersMadeByClientOutputDTOBase { }

    [FunctionOutput]
    public class OffersMadeByClientOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
        [Parameter("uint256", "futureExpirationTime", 2)]
        public virtual BigInteger FutureExpirationTime { get; set; }
        [Parameter("uint256", "avatarId", 3)]
        public virtual BigInteger AvatarId { get; set; }
        [Parameter("uint256", "amount", 4)]
        public virtual BigInteger Amount { get; set; }
    }
}
