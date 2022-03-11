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
        public static string BYTECODE = "60006001556101806040526004608081815260a082905260c082905260e08290526101008290526101208290526101408290526101609190915261004790600590600861006c565b5034801561005457600080fd5b50600080546001600160a01b03191633179055610127565b82805482825590600052602060002090601f016020900481019282156101025791602002820160005b838211156100d357835183826101000a81548160ff021916908360ff1602179055509260200192600101602081600001049283019260010302610095565b80156101005782816101000a81549060ff02191690556001016020816000010492830192600103026100d3565b505b5061010e929150610112565b5090565b5b8082111561010e5760008155600101610113565b6106e8806101366000396000f3fe608060405234801561001057600080fd5b50600436106100575760003560e01c80631cc321d31461005c5780634a819b1c1461007157806379391ce9146100ab578063834d5fac146100b3578063fd967f9014610100575b600080fd5b61006f61006a3660046105be565b610113565b005b61008461007f3660046105be565b610285565b60408051941515855260208501939093529183015260608201526080015b60405180910390f35b61006f6102cf565b6100c66100c13660046105e0565b6103e2565b6040805163ffffffff95861681529490931660208501526001600160a01b039091169183019190915261ffff1660608201526080016100a2565b61008461010e3660046105f9565b610436565b60025482101561015d5760405162461bcd60e51b815260206004820152601060248201526f105d985d185c881b9bdd08199bdd5b9960821b60448201526064015b60405180910390fd5b80156101ab5760405162461bcd60e51b815260206004820152601c60248201527f4e6f207a65726f20616d6f756e74206f6666657220616c6c6f776564000000006044820152606401610154565b604080516080810190915260018152600090602081016101ce4262015180610647565b81526020808201869052604091820194909452336000908152600380865282822080546001818101835591845287842086516004928302909101805491151560ff19928316178155878a01805182860155888801805160028085019190915560608b018051948901949094559c8852848c52978720805480870182559088529a9096209751999092029096018054981515989096169790971785559151918401919091559051948201949094559151919092015550565b600460205281600052604060002081815481106102a157600080fd5b6000918252602090912060049091020180546001820154600283015460039093015460ff9092169450925084565b6000546001600160a01b031633146102e657600080fd5b6040805160a08101825260008082526020820181905280546001600160a01b031692820192909252606081019190915260029060808101610325610452565b9052815460018181018455600093845260209384902083516002909302018054858501516040860151606087015161ffff16600160e01b0261ffff60e01b196001600160a01b03909216600160401b029190911668010000000000000000600160f01b031963ffffffff9384166401000000000267ffffffffffffffff19909516939097169290921792909217949094169390931792909217825560808301518051939492936103dd93928501929190910190610503565b505050565b600281815481106103f257600080fd5b600091825260209091206002909102015463ffffffff808216925064010000000082041690600160401b81046001600160a01b031690600160e01b900461ffff1684565b600360205281600052604060002081815481106102a157600080fd5b60608060005b81518110156104ac5761010061046c6104b2565b610476919061065f565b82828151811061048857610488610681565b60ff90921660209283029190910190910152806104a481610697565b915050610458565b50919050565b6001805460009144914291846104c783610697565b9091555060408051602081019490945283019190915260608201526080016040516020818303038152906040528051906020012060001c905090565b82805482825590600052602060002090601f016020900481019282156105995791602002820160005b8382111561056a57835183826101000a81548160ff021916908360ff160217905550926020019260010160208160000104928301926001030261052c565b80156105975782816101000a81549060ff021916905560010160208160000104928301926001030261056a565b505b506105a59291506105a9565b5090565b5b808211156105a557600081556001016105aa565b600080604083850312156105d157600080fd5b50508035926020909101359150565b6000602082840312156105f257600080fd5b5035919050565b6000806040838503121561060c57600080fd5b82356001600160a01b038116811461062357600080fd5b946020939093013593505050565b634e487b7160e01b600052601160045260246000fd5b6000821982111561065a5761065a610631565b500190565b60008261067c57634e487b7160e01b600052601260045260246000fd5b500690565b634e487b7160e01b600052603260045260246000fd5b60006000198214156106ab576106ab610631565b506001019056fea26469706673582212204c4fd8b36ae617bc2878c374551cd740ae69c3d5635246900390f2a29be1754f64736f6c634300080c0033";
        public HumanAvatarOwnerDeploymentBase() : base(BYTECODE) { }
        public HumanAvatarOwnerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AvatarsFunction : AvatarsFunctionBase { }

    [Function("avatars", typeof(AvatarsOutputDTO))]
    public class AvatarsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
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

    public partial class AvatarsOutputDTO : AvatarsOutputDTOBase { }

    [FunctionOutput]
    public class AvatarsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "momId", 1)]
        public virtual uint MomId { get; set; }
        [Parameter("uint32", "dadId", 2)]
        public virtual uint DadId { get; set; }
        [Parameter("address", "avatarOwner", 3)]
        public virtual string AvatarOwner { get; set; }
        [Parameter("uint16", "generation", 4)]
        public virtual ushort Generation { get; set; }
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
