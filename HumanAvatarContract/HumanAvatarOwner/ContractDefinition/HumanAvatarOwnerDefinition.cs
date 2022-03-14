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
        public static string BYTECODE = "6080604052600060015534801561001557600080fd5b50600080546001600160a01b03191633179055611439806100376000396000f3fe6080604052600436106100915760003560e01c80638f2ac188116100595780638f2ac18814610169578063910e05dd1461018957806393ee80c5146101e6578063c815729d14610206578063ef706adf1461021957600080fd5b80631afed875146100965780633b59f1b8146100b85780634ce94f9f146100dc57806379391ce9146100fe578063834d5fac14610113575b600080fd5b3480156100a257600080fd5b506100b66100b13660046110d6565b610239565b005b3480156100c457600080fd5b506002545b6040519081526020015b60405180910390f35b3480156100e857600080fd5b50336000908152600460205260409020546100c9565b34801561010a57600080fd5b506100b6610382565b34801561011f57600080fd5b5061013361012e3660046110f8565b610477565b6040805195865260208601949094526001600160a01b03909216928401929092526060830191909152608082015260a0016100d3565b34801561017557600080fd5b506100c9610184366004611111565b6104c2565b34801561019557600080fd5b506101c96101a43660046110f8565b60036020526000908152604090208054600182015460029092015460ff909116919083565b6040805193151584526020840192909252908201526060016100d3565b3480156101f257600080fd5b506100b66102013660046110d6565b6104f3565b6100b66102143660046110f8565b610779565b34801561022557600080fd5b506100b66102343660046110f8565b610a37565b60025482106102635760405162461bcd60e51b815260040161025a90611149565b60405180910390fd5b806102b05760405162461bcd60e51b815260206004820152601c60248201527f4e6f207a65726f20616d6f756e74206f6666657220616c6c6f77656400000000604482015260640161025a565b336001600160a01b0316600283815481106102cd576102cd611173565b60009182526020909120600260059092020101546001600160a01b0316146103375760405162461bcd60e51b815260206004820152601b60248201527f596f7520617265206e6f74206f776e6572206f66206176617461720000000000604482015260640161025a565b604080516060810182526001808252602080830186815283850195865260009687526003909152929094209051815460ff191690151517815590519281019290925551600290910155565b6000546001600160a01b0316331461039957600080fd5b60026040518060a00160405280600081526020016000815260200160008054906101000a90046001600160a01b03166001600160a01b03168152602001600081526020016103e5610af7565b90528154600180820184556000938452602080852084516005909402019283558381015183830155604080850151600280860180546001600160a01b0319166001600160a01b0393841617905560608701516003870155608090960151600495860155865416865292905292209054909161045f9161119f565b81546001810183556000928352602090922090910155565b6002818154811061048757600080fd5b60009182526020909120600590910201805460018201546002830154600384015460049094015492945090926001600160a01b039091169185565b600460205281600052604060002081815481106104de57600080fd5b90600052602060002001600091509150505481565b336001600160a01b03166002838154811061051057610510611173565b60009182526020909120600260059092020101546001600160a01b03161461054a5760405162461bcd60e51b815260040161025a906111b6565b336001600160a01b03166002828154811061056757610567611173565b60009182526020909120600260059092020101546001600160a01b0316146105a15760405162461bcd60e51b815260040161025a906111b6565b6000600283815481106105b6576105b6611173565b9060005260206000209060050201600301549050600283815481106105dd576105dd611173565b9060005260206000209060050201600301546002838154811061060257610602611173565b9060005260206000209060050201600301541115610642576002828154811061062d5761062d611173565b90600052602060002090600502016003015490505b8061064c816111f9565b91505060026040518060a0016040528085815260200184815260200160008054906101000a90046001600160a01b03166001600160a01b031681526020018381526020016106e2600287815481106106a6576106a6611173565b906000526020600020906005020160040154600287815481106106cb576106cb611173565b906000526020600020906005020160040154610b48565b90528154600180820184556000938452602080852084516005909402019283558381015183830155604080850151600280860180546001600160a01b0319166001600160a01b03909316929092179091556060860151600386015560809095015160049485015533865292905292209054909161075e9161119f565b81546001810183556000928352602090922090910155505050565b600254811061079a5760405162461bcd60e51b815260040161025a90611149565b336001600160a01b0316600282815481106107b7576107b7611173565b60009182526020909120600260059092020101546001600160a01b031614156108225760405162461bcd60e51b815260206004820181905260248201527f596f752063616e6e6f742061636365707420796f7572206f776e206f66666572604482015260640161025a565b60008181526003602052604090205460ff166108805760405162461bcd60e51b815260206004820152601f60248201527f4e6f20616374697665206f6666657220666f7220746869732061766174617200604482015260640161025a565b6000818152600360205260409020600201543410156108e15760405162461bcd60e51b815260206004820152601e60248201527f4e6f7420656e6f75676820636f696e7320666f7220746865206f666665720000604482015260640161025a565b6000600282815481106108f6576108f6611173565b60009182526020808320600260059093020182015485845260039091526040909220805460ff1916905580546001600160a01b03909216925033918490811061094157610941611173565b906000526020600020906005020160020160006101000a8154816001600160a01b0302191690836001600160a01b031602179055506109808183610f87565b33600090815260046020908152604080832080546001810182559084528284200185905584835260039091528120600201546109bc903461119f565b6000848152600360205260408082206002015490519293506001600160a01b0385169281156108fc0292818181858888f19350505050158015610a03573d6000803e3d6000fd5b50604051339082156108fc029083906000818181858888f19350505050158015610a31573d6000803e3d6000fd5b50505050565b6002548110610a585760405162461bcd60e51b815260040161025a90611149565b336001600160a01b031660028281548110610a7557610a75611173565b60009182526020909120600260059092020101546001600160a01b031614610adf5760405162461bcd60e51b815260206004820152601b60248201527f596f7520617265206e6f74206f776e6572206f66206176617461720000000000604482015260640161025a565b6000908152600360205260409020805460ff19169055565b600180546000914491429184610b0c836111f9565b9091555060408051602081019490945283019190915260608201526080016040516020818303038152906040528051906020012060001c905090565b604080516101008101825260048082526020820181905291810182905260608082018390526080820183905260a0820183905260c0820183905260e0820192909252600091600783610b98610af7565b90506000805b6008811015610bdf57858160088110610bb957610bb9611173565b6020020151610bcb9060ff1683611214565b915080610bd7816111f9565b915050610b9e565b508067ffffffffffffffff811115610bf957610bf961122c565b604051908082528060200260200182016040528015610c22578160200160208202803683370190505b50935060008282604051602001610c43929190918252602082015260400190565b60408051601f19818403018152919052805160209091012090506000610c6b60086002611326565b610c75908561134f565b905060016000805b6008811015610f0d576000610c93600a8961134f565b90508860ff16811115610d6d5760005b8b8360088110610cb557610cb5611173565b602002015160ff168160ff161015610d135760ff87168b85610cd6816111f9565b965081518110610ce857610ce8611173565b60ff9092166020928302919091019091015260089690961c9580610d0b81611363565b915050610ca3565b508a8260088110610d2657610d26611173565b6020020151610d36906008611383565b60ff168e901c9d508a8260088110610d5057610d50611173565b6020020151610d60906008611383565b60ff168d901c9c50610ee6565b84841661ffff1615610e1c5760005b8b8360088110610d8e57610d8e611173565b602002015160ff168160ff161015610dec5760ff8e168b85610daf816111f9565b965081518110610dc157610dc1611173565b60ff9092166020928302919091019091015260089d909d1c9c80610de481611363565b915050610d7c565b508a8260088110610dff57610dff611173565b6020020151610e0f906008611383565b60ff168e901c9d50610ebb565b60005b8b8360088110610e3157610e31611173565b602002015160ff168160ff161015610e8f5760ff8f168b85610e52816111f9565b965081518110610e6457610e64611173565b60ff9092166020928302919091019091015260089e909e1c9d80610e8781611363565b915050610e1f565b508a8260088110610ea257610ea2611173565b6020020151610eb2906008611383565b60ff168d901c9c505b8a8260088110610ecd57610ecd611173565b6020020151610edd906008611383565b60ff1686901c95505b610ef1600a896113a4565b97505060019290921b9180610f05816111f9565b915050610c7d565b5084885114610f1e57610f1e6113b8565b6000805b8951811015610f7457610f368160086113ce565b8a8281518110610f4857610f48611173565b602002602001015160ff16901b82610f609190611214565b915080610f6c816111f9565b915050610f22565b5099505050505050505050505b92915050565b6000805b6001600160a01b038416600090815260046020526040902054610fb09060019061119f565b811015611093576001600160a01b0384166000908152600460205260409020805484919083908110610fe457610fe4611173565b90600052602060002001541415610ffa57600191505b8115611081576001600160a01b0384166000908152600460205260409020611023826001611214565b8154811061103357611033611173565b906000526020600020015460046000866001600160a01b03166001600160a01b03168152602001908152602001600020828154811061107457611074611173565b6000918252602090912001555b8061108b816111f9565b915050610f8b565b506001600160a01b03831660009081526004602052604090208054806110bb576110bb6113ed565b60019003818190600052602060002001600090559055505050565b600080604083850312156110e957600080fd5b50508035926020909101359150565b60006020828403121561110a57600080fd5b5035919050565b6000806040838503121561112457600080fd5b82356001600160a01b038116811461113b57600080fd5b946020939093013593505050565b60208082526010908201526f105d985d185c881b9bdd08199bdd5b9960821b604082015260600190565b634e487b7160e01b600052603260045260246000fd5b634e487b7160e01b600052601160045260246000fd5b6000828210156111b1576111b1611189565b500390565b60208082526023908201527f596f752063616e206f6e6c7920627265656420796f7572206f776e206176617460408201526261727360e81b606082015260800190565b600060001982141561120d5761120d611189565b5060010190565b6000821982111561122757611227611189565b500190565b634e487b7160e01b600052604160045260246000fd5b600181815b8085111561127d57816000190482111561126357611263611189565b8085161561127057918102915b93841c9390800290611247565b509250929050565b60008261129457506001610f81565b816112a157506000610f81565b81600181146112b757600281146112c1576112dd565b6001915050610f81565b60ff8411156112d2576112d2611189565b50506001821b610f81565b5060208310610133831016604e8410600b8410161715611300575081810a610f81565b61130a8383611242565b806000190482111561131e5761131e611189565b029392505050565b60006113328383611285565b9392505050565b634e487b7160e01b600052601260045260246000fd5b60008261135e5761135e611339565b500690565b600060ff821660ff81141561137a5761137a611189565b60010192915050565b600060ff821660ff84168160ff048111821515161561131e5761131e611189565b6000826113b3576113b3611339565b500490565b634e487b7160e01b600052600160045260246000fd5b60008160001904831182151516156113e8576113e8611189565b500290565b634e487b7160e01b600052603160045260246000fdfea2646970667358221220bdf34d113676bec51a2d47f0d80bba28cdca54885cd384e26c2cdd27d191e03c64736f6c634300080c0033";
        public HumanAvatarOwnerDeploymentBase() : base(BYTECODE) { }
        public HumanAvatarOwnerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AcceptOfferFunction : AcceptOfferFunctionBase { }

    [Function("acceptOffer")]
    public class AcceptOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "avatarId", 1)]
        public virtual BigInteger AvatarId { get; set; }
    }

    public partial class AvatarIdsOfAddressFunction : AvatarIdsOfAddressFunctionBase { }

    [Function("avatarIdsOfAddress", "uint256")]
    public class AvatarIdsOfAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }

    public partial class AvatarOfferFunction : AvatarOfferFunctionBase { }

    [Function("avatarOffer", typeof(AvatarOfferOutputDTO))]
    public class AvatarOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class AvatarsFunction : AvatarsFunctionBase { }

    [Function("avatars", typeof(AvatarsOutputDTO))]
    public class AvatarsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class BreedBetweenFunction : BreedBetweenFunctionBase { }

    [Function("breedBetween")]
    public class BreedBetweenFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "momAvatarId", 1)]
        public virtual BigInteger MomAvatarId { get; set; }
        [Parameter("uint256", "dadAvatarId", 2)]
        public virtual BigInteger DadAvatarId { get; set; }
    }

    public partial class CancelOfferFunction : CancelOfferFunctionBase { }

    [Function("cancelOffer")]
    public class CancelOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "avatarId", 1)]
        public virtual BigInteger AvatarId { get; set; }
    }

    public partial class CreateOfferFunction : CreateOfferFunctionBase { }

    [Function("createOffer")]
    public class CreateOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "avatarId", 1)]
        public virtual BigInteger AvatarId { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class CreatePrimeAvatarFunction : CreatePrimeAvatarFunctionBase { }

    [Function("createPrimeAvatar")]
    public class CreatePrimeAvatarFunctionBase : FunctionMessage
    {

    }

    public partial class GetAvatarIdsOfAddressCountFunction : GetAvatarIdsOfAddressCountFunctionBase { }

    [Function("getAvatarIdsOfAddressCount", "uint256")]
    public class GetAvatarIdsOfAddressCountFunctionBase : FunctionMessage
    {

    }

    public partial class GetAvatarsCountFunction : GetAvatarsCountFunctionBase { }

    [Function("getAvatarsCount", "uint256")]
    public class GetAvatarsCountFunctionBase : FunctionMessage
    {

    }



    public partial class AvatarIdsOfAddressOutputDTO : AvatarIdsOfAddressOutputDTOBase { }

    [FunctionOutput]
    public class AvatarIdsOfAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class AvatarOfferOutputDTO : AvatarOfferOutputDTOBase { }

    [FunctionOutput]
    public class AvatarOfferOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "active", 1)]
        public virtual bool Active { get; set; }
        [Parameter("uint256", "avatarId", 2)]
        public virtual BigInteger AvatarId { get; set; }
        [Parameter("uint256", "amount", 3)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class AvatarsOutputDTO : AvatarsOutputDTOBase { }

    [FunctionOutput]
    public class AvatarsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "momId", 1)]
        public virtual BigInteger MomId { get; set; }
        [Parameter("uint256", "dadId", 2)]
        public virtual BigInteger DadId { get; set; }
        [Parameter("address", "avatarOwner", 3)]
        public virtual string AvatarOwner { get; set; }
        [Parameter("uint256", "generation", 4)]
        public virtual BigInteger Generation { get; set; }
        [Parameter("uint256", "genome", 5)]
        public virtual BigInteger Genome { get; set; }
    }









    public partial class GetAvatarIdsOfAddressCountOutputDTO : GetAvatarIdsOfAddressCountOutputDTOBase { }

    [FunctionOutput]
    public class GetAvatarIdsOfAddressCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetAvatarsCountOutputDTO : GetAvatarsCountOutputDTOBase { }

    [FunctionOutput]
    public class GetAvatarsCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
