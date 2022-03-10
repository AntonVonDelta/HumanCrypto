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
        public static string BYTECODE = "608060405234801561001057600080fd5b50600080546001600160a01b0319163317905561042c806100326000396000f3fe608060405234801561001057600080fd5b506004361061004c5760003560e01c80631cc321d3146100515780634a819b1c1461006657806379391ce91461009f578063fd967f90146100a7575b600080fd5b61006461005f366004610376565b6100ba565b005b610079610074366004610376565b610226565b604080519415158552602085019390935291830152606082015260800160405180910390f35b610064610270565b6100796100b5366004610398565b61035a565b6001548210156101045760405162461bcd60e51b815260206004820152601060248201526f105d985d185c881b9bdd08199bdd5b9960821b60448201526064015b60405180910390fd5b80156101525760405162461bcd60e51b815260206004820152601c60248201527f4e6f207a65726f20616d6f756e74206f6666657220616c6c6f7765640000000060448201526064016100fb565b6040805160808101909152600181526000906020810161017542620151806103d0565b81526020808201869052604091820194909452336000908152600280865282822080546001818101835591845287842086516004928302909101805491151560ff19928316178155878a0180518286015588880180518388015560608a0180516003948501559c8852828c52978720805480870182559088529a9096209751999092029096018054981515989096169790971785559151918401919091559051908201559251929091019190915550565b6003602052816000526040600020818154811061024257600080fd5b6000918252602090912060049091020180546001820154600283015460039093015460ff9092169450925084565b6000546001600160a01b0316331461028757600080fd5b6040805160808101825260008082526020820181815281546001600160a01b0390811694840194855260608401838152600180548082018255945293517fb10e2d527612073b26eecdfd717e6a320cf44b4afac2b0732d9fcbe2b7fa0cf6909301805492519551945161ffff16600160e01b0261ffff60e01b199590921668010000000000000000029490941668010000000000000000600160f01b031963ffffffff9687166401000000000267ffffffffffffffff199094169490961693909317919091179390931617919091179055565b6002602052816000526040600020818154811061024257600080fd5b6000806040838503121561038957600080fd5b50508035926020909101359150565b600080604083850312156103ab57600080fd5b82356001600160a01b03811681146103c257600080fd5b946020939093013593505050565b600082198211156103f157634e487b7160e01b600052601160045260246000fd5b50019056fea264697066735822122073c75d334e4aac5f20e6b1b45fa1d1f077e03ed28635030a6b3ff8c0dcdb2e8664736f6c634300080c0033";
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
