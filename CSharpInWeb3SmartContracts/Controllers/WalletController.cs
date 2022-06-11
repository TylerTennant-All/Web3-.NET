﻿using CSharpInWeb3SmartContracts.Models;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace CSharpInWeb3SmartContracts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly User _user = new User();

        public WalletController(IConfiguration configuration)
        {
            _user.BlockchainProvider = configuration["BlockchainProviderKovan"];
            _user.MetamaskAddress = configuration["MetamaskAddress"];
            _user.PrivateKey = configuration["PrivateKey"];
        }

        [HttpGet("GetBalance")]
        public async Task<ActionResult> GetBalance(Chain chain)
        {
            Account? account = new Account(_user.PrivateKey, chain);
            Web3? web3 = new Web3(account, _user.BlockchainProvider);

            HexBigInteger? balance = await web3.Eth.GetBalance.SendRequestAsync(_user.MetamaskAddress);
            decimal etherAmount = Web3.Convert.FromWei(balance.Value);

            return Ok($"Ethereum balance {etherAmount}");
        }

        [HttpGet("SendEthereum")]
        public async Task<ActionResult> SendEthereum(Chain chain, string toAddress, decimal etherAmount)
        {
            try
            {
                Account? account = new Account(_user.PrivateKey, chain);
                Web3? web3 = new Web3(account, _user.BlockchainProvider);

                TransactionReceipt? transaction = await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(toAddress, etherAmount);

                return Ok($"Transaction hash {transaction.TransactionHash}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);

            }
        }


        [HttpGet("TransferTokens")]
        public async Task<ActionResult> SendERC20Token() //Chain chain,string toAddress, string contractAddress, 
        {
            try
            {
                Account? account = new Account(_user.PrivateKey, Chain.Kovan);
                Web3? web3 = new Web3(account, _user.BlockchainProvider);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);

            }
        }



    }
}
