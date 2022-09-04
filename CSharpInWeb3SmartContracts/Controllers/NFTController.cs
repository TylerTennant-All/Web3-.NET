﻿using CSharpInWeb3SmartContracts.Models;
using CSharpInWeb3SmartContracts.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInWeb3SmartContracts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NFTController : ControllerBase
    {
        private readonly string _byteCode = "60806040523480156200001157600080fd5b50600060208190527f67be87c3ff9960ca1e9cfac5cab2ff4747269cf9ed20c9b7306235ac35a491c58054600160ff1991821681179092557ff7815fccbf112960a73756e185887fedcb9fc64ca0a16cc5923b7960ed7808008054821683179055635b5e139f60e01b9092527f9562381dfbc2d8b8b66e765249f330164b73e329e5f01670660643571d1974df8054909216179055620000b8620000b23390565b62000112565b604080518082019091526005808252641394d3919560da1b602083015290620000e2908262000209565b506040805180820190915260028152614e5360f01b60208201526006906200010b908262000209565b50620002d5565b600880546001600160a01b038381166001600160a01b0319831681179093556040519116919082907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e090600090a35050565b634e487b7160e01b600052604160045260246000fd5b600181811c908216806200018f57607f821691505b602082108103620001b057634e487b7160e01b600052602260045260246000fd5b50919050565b601f8211156200020457600081815260208120601f850160051c81016020861015620001df5750805b601f850160051c820191505b818110156200020057828155600101620001eb565b5050505b505050565b81516001600160401b0381111562000225576200022562000164565b6200023d816200023684546200017a565b84620001b6565b602080601f8311600181146200027557600084156200025c5750858301515b600019600386901b1c1916600185901b17855562000200565b600085815260208120601f198616915b82811015620002a65788860151825594840194600190910190840162000285565b5085821015620002c55787850151600019600388901b60f8161c191681555b5050505050600190811b01905550565b61166380620002e56000396000f3fe608060405234801561001057600080fd5b506004361061010b5760003560e01c8063715018a6116100a2578063b88d4fde11610071578063b88d4fde14610235578063c87b56dd14610248578063d3fc98641461025b578063e985e9c51461026e578063f2fde38b146102aa57600080fd5b8063715018a6146102015780638da5cb5b1461020957806395d89b411461021a578063a22cb4651461022257600080fd5b806323b872dd116100de57806323b872dd146101a757806342842e0e146101ba5780636352211e146101cd57806370a08231146101e057600080fd5b806301ffc9a71461011057806306fdde0314610152578063081812fc14610167578063095ea7b314610192575b600080fd5b61013d61011e366004611192565b6001600160e01b03191660009081526020819052604090205460ff1690565b60405190151581526020015b60405180910390f35b61015a6102bd565b60405161014991906111f5565b61017a610175366004611208565b61034f565b6040516001600160a01b039091168152602001610149565b6101a56101a036600461123d565b6103d1565b005b6101a56101b5366004611267565b610572565b6101a56101c8366004611267565b61072d565b61017a6101db366004611208565b61074d565b6101f36101ee3660046112a3565b6107a5565b604051908152602001610149565b6101a561080d565b6008546001600160a01b031661017a565b61015a610821565b6101a56102303660046112be565b610830565b6101a5610243366004611343565b61089c565b61015a610256366004611208565b6108e5565b6101a56102693660046113b2565b610951565b61013d61027c36600461140c565b6001600160a01b03918216600090815260046020908152604080832093909416825291909152205460ff1690565b6101a56102b83660046112a3565b6109a9565b6060600580546102cc9061143f565b80601f01602080910402602001604051908101604052809291908181526020018280546102f89061143f565b80156103455780601f1061031a57610100808354040283529160200191610345565b820191906000526020600020905b81548152906001019060200180831161032857829003601f168201915b5050505050905090565b6000818152600160209081526040808320548151808301909252600682526518181998181960d11b9282019290925283916001600160a01b03166103af5760405162461bcd60e51b81526004016103a691906111f5565b60405180910390fd5b506000838152600260205260409020546001600160a01b031691505b50919050565b60008181526001602052604090205481906001600160a01b03163381148061041c57506001600160a01b038116600090815260046020908152604080832033845290915290205460ff165b6040518060400160405280600681526020016530303330303360d01b815250906104595760405162461bcd60e51b81526004016103a691906111f5565b50600083815260016020908152604091829020548251808401909352600683526518181998181960d11b918301919091528491906001600160a01b03166104b35760405162461bcd60e51b81526004016103a691906111f5565b50600084815260016020908152604091829020548251808401909352600683526506060666060760d31b918301919091526001600160a01b039081169190871682036105125760405162461bcd60e51b81526004016103a691906111f5565b5060008581526002602052604080822080546001600160a01b0319166001600160a01b038a811691821790925591518893918516917f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b92591a4505050505050565b60008181526001602052604090205481906001600160a01b0316338114806105b057506000828152600260205260409020546001600160a01b031633145b806105de57506001600160a01b038116600090815260046020908152604080832033845290915290205460ff165b604051806040016040528060068152602001650c0c0ccc0c0d60d21b8152509061061b5760405162461bcd60e51b81526004016103a691906111f5565b50600083815260016020908152604091829020548251808401909352600683526518181998181960d11b918301919091528491906001600160a01b03166106755760405162461bcd60e51b81526004016103a691906111f5565b50600084815260016020908152604091829020548251808401909352600683526530303330303760d01b918301919091526001600160a01b039081169190881682146106d45760405162461bcd60e51b81526004016103a691906111f5565b5060408051808201909152600681526530303330303160d01b60208201526001600160a01b0387166107195760405162461bcd60e51b81526004016103a691906111f5565b506107248686610a22565b50505050505050565b61074883838360405180602001604052806000815250610aad565b505050565b600081815260016020908152604091829020548251808401909352600683526518181998181960d11b918301919091526001600160a01b031690816103cb5760405162461bcd60e51b81526004016103a691906111f5565b60408051808201909152600681526530303330303160d01b60208201526000906001600160a01b0383166107ec5760405162461bcd60e51b81526004016103a691906111f5565b506001600160a01b0382166000908152600360205260409020545b92915050565b610815610d4c565b61081f6000610da6565b565b6060600680546102cc9061143f565b3360008181526004602090815260408083206001600160a01b03871680855290835292819020805460ff191686151590811790915590519081529192917f17307eab39ab6107e8899845ad3d59bd9653f200f220920489ca2b5937696c31910160405180910390a35050565b6108de85858585858080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250610aad92505050565b5050505050565b600081815260016020908152604091829020548251808401909352600683526518181998181960d11b9183019190915260609183916001600160a01b03166109405760405162461bcd60e51b81526004016103a691906111f5565b5061094a83610df8565b9392505050565b610959610d4c565b6109638484610e9a565b6109a38383838080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250610f7d92505050565b50505050565b6109b1610d4c565b6001600160a01b038116610a165760405162461bcd60e51b815260206004820152602660248201527f4f776e61626c653a206e6577206f776e657220697320746865207a65726f206160448201526564647265737360d01b60648201526084016103a6565b610a1f81610da6565b50565b600081815260016020908152604080832054600290925290912080546001600160a01b03191690556001600160a01b0316610a5d8183610fef565b610a678383611098565b81836001600160a01b0316826001600160a01b03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef60405160405180910390a4505050565b60008281526001602052604090205482906001600160a01b031633811480610aeb57506000828152600260205260409020546001600160a01b031633145b80610b1957506001600160a01b038116600090815260046020908152604080832033845290915290205460ff165b604051806040016040528060068152602001650c0c0ccc0c0d60d21b81525090610b565760405162461bcd60e51b81526004016103a691906111f5565b50600084815260016020908152604091829020548251808401909352600683526518181998181960d11b918301919091528591906001600160a01b0316610bb05760405162461bcd60e51b81526004016103a691906111f5565b50600085815260016020908152604091829020548251808401909352600683526530303330303760d01b918301919091526001600160a01b03908116919089168214610c0f5760405162461bcd60e51b81526004016103a691906111f5565b5060408051808201909152600681526530303330303160d01b60208201526001600160a01b038816610c545760405162461bcd60e51b81526004016103a691906111f5565b50610c5f8787610a22565b610c71876001600160a01b0316611140565b15610d4257604051630a85bd0160e11b81526000906001600160a01b0389169063150b7a0290610cab9033908d908c908c90600401611473565b6020604051808303816000875af1158015610cca573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250810190610cee91906114b0565b60408051808201909152600681526530303330303560d01b60208201529091506001600160e01b03198216630a85bd0160e11b14610d3f5760405162461bcd60e51b81526004016103a691906111f5565b50505b5050505050505050565b6008546001600160a01b0316331461081f5760405162461bcd60e51b815260206004820181905260248201527f4f776e61626c653a2063616c6c6572206973206e6f7420746865206f776e657260448201526064016103a6565b600880546001600160a01b038381166001600160a01b0319831681179093556040519116919082907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e090600090a35050565b6000818152600760205260409020805460609190610e159061143f565b80601f0160208091040260200160405190810160405280929190818152602001828054610e419061143f565b8015610e8e5780601f10610e6357610100808354040283529160200191610e8e565b820191906000526020600020905b815481529060010190602001808311610e7157829003601f168201915b50505050509050919050565b60408051808201909152600681526530303330303160d01b60208201526001600160a01b038316610ede5760405162461bcd60e51b81526004016103a691906111f5565b50600081815260016020908152604091829020548251808401909352600683526518181998181b60d11b918301919091526001600160a01b031615610f365760405162461bcd60e51b81526004016103a691906111f5565b50610f418282611098565b60405181906001600160a01b038416906000907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef908290a45050565b600082815260016020908152604091829020548251808401909352600683526518181998181960d11b918301919091528391906001600160a01b0316610fd65760405162461bcd60e51b81526004016103a691906111f5565b5060008381526007602052604090206109a38382611531565b600081815260016020908152604091829020548251808401909352600683526530303330303760d01b918301919091526001600160a01b0384811691161461104a5760405162461bcd60e51b81526004016103a691906111f5565b506001600160a01b0382166000908152600360205260408120805460019290611074908490611607565b9091555050600090815260016020526040902080546001600160a01b031916905550565b600081815260016020908152604091829020548251808401909352600683526518181998181b60d11b918301919091526001600160a01b0316156110ef5760405162461bcd60e51b81526004016103a691906111f5565b50600081815260016020818152604080842080546001600160a01b0319166001600160a01b03881690811790915584526003909152822080549192909161113790849061161a565b90915550505050565b6000813f7fc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a47081158015906111745750808214155b949350505050565b6001600160e01b031981168114610a1f57600080fd5b6000602082840312156111a457600080fd5b813561094a8161117c565b6000815180845260005b818110156111d5576020818501810151868301820152016111b9565b506000602082860101526020601f19601f83011685010191505092915050565b60208152600061094a60208301846111af565b60006020828403121561121a57600080fd5b5035919050565b80356001600160a01b038116811461123857600080fd5b919050565b6000806040838503121561125057600080fd5b61125983611221565b946020939093013593505050565b60008060006060848603121561127c57600080fd5b61128584611221565b925061129360208501611221565b9150604084013590509250925092565b6000602082840312156112b557600080fd5b61094a82611221565b600080604083850312156112d157600080fd5b6112da83611221565b9150602083013580151581146112ef57600080fd5b809150509250929050565b60008083601f84011261130c57600080fd5b50813567ffffffffffffffff81111561132457600080fd5b60208301915083602082850101111561133c57600080fd5b9250929050565b60008060008060006080868803121561135b57600080fd5b61136486611221565b945061137260208701611221565b935060408601359250606086013567ffffffffffffffff81111561139557600080fd5b6113a1888289016112fa565b969995985093965092949392505050565b600080600080606085870312156113c857600080fd5b6113d185611221565b935060208501359250604085013567ffffffffffffffff8111156113f457600080fd5b611400878288016112fa565b95989497509550505050565b6000806040838503121561141f57600080fd5b61142883611221565b915061143660208401611221565b90509250929050565b600181811c9082168061145357607f821691505b6020821081036103cb57634e487b7160e01b600052602260045260246000fd5b6001600160a01b03858116825284166020820152604081018390526080606082018190526000906114a6908301846111af565b9695505050505050565b6000602082840312156114c257600080fd5b815161094a8161117c565b634e487b7160e01b600052604160045260246000fd5b601f82111561074857600081815260208120601f850160051c8101602086101561150a5750805b601f850160051c820191505b8181101561152957828155600101611516565b505050505050565b815167ffffffffffffffff81111561154b5761154b6114cd565b61155f81611559845461143f565b846114e3565b602080601f831160018114611594576000841561157c5750858301515b600019600386901b1c1916600185901b178555611529565b600085815260208120601f198616915b828110156115c3578886015182559484019460019091019084016115a4565b50858210156115e15787850151600019600388901b60f8161c191681555b5050505050600190811b01905550565b634e487b7160e01b600052601160045260246000fd5b81810381811115610807576108076115f1565b80820180821115610807576108076115f156fea26469706673582212207a232e35628dad90cdf9b3a771d0f5c94a0ac7c5039cf85e546a2fdde901a9af64736f6c63430008100033";

        private readonly string _abi = @" [{""inputs"":[],""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""_owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""_approved"",""type"":""address""},{""indexed"":true,""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""_owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""_operator"",""type"":""address""},{""indexed"":false,""internalType"":""bool"",""name"":""_approved"",""type"":""bool""}],""name"":""ApprovalForAll"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""previousOwner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""newOwner"",""type"":""address""}],""name"":""OwnershipTransferred"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""_from"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""_to"",""type"":""address""},{""indexed"":true,""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""inputs"":[{""internalType"":""address"",""name"":""_approved"",""type"":""address""},{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""approve"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""getApproved"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_owner"",""type"":""address""},{""internalType"":""address"",""name"":""_operator"",""type"":""address""}],""name"":""isApprovedForAll"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_to"",""type"":""address""},{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""},{""internalType"":""string"",""name"":""_uri"",""type"":""string""}],""name"":""mint"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""name"":""name"",""outputs"":[{""internalType"":""string"",""name"":""_name"",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""owner"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""ownerOf"",""outputs"":[{""internalType"":""address"",""name"":""_owner"",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""renounceOwnership"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_from"",""type"":""address""},{""internalType"":""address"",""name"":""_to"",""type"":""address""},{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""safeTransferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_from"",""type"":""address""},{""internalType"":""address"",""name"":""_to"",""type"":""address""},{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""},{""internalType"":""bytes"",""name"":""_data"",""type"":""bytes""}],""name"":""safeTransferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_operator"",""type"":""address""},{""internalType"":""bool"",""name"":""_approved"",""type"":""bool""}],""name"":""setApprovalForAll"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""bytes4"",""name"":""_interfaceID"",""type"":""bytes4""}],""name"":""supportsInterface"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""symbol"",""outputs"":[{""internalType"":""string"",""name"":""_symbol"",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""tokenURI"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_from"",""type"":""address""},{""internalType"":""address"",""name"":""_to"",""type"":""address""},{""internalType"":""uint256"",""name"":""_tokenId"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""newOwner"",""type"":""address""}],""name"":""transferOwnership"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""}] ";

        private readonly User _user = new User();

        public EnumHelper EnumHelper { get; set; }

        public NFTController(IConfiguration configuration)
        {
            EnumHelper = new EnumHelper(configuration);
            _user = configuration.GetSection("User").Get<User>();
        }



    }
}
