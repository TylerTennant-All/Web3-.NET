﻿using CSharpInWeb3SmartContracts.Enumerations;
using CSharpInWeb3SmartContracts.Utilities;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace CSharpInWeb3SmartContracts.Controllers
{
    public class SmartContractController : ControllerBase
    {
        public EnumHelper EnumHelper { get; set; }

        public SmartContractController(IConfiguration configuration)
        {
            EnumHelper = new EnumHelper(configuration);
        }

        [Consumes("application/json")]
        [HttpPost("DeployWithoutParameters")]
        public async Task<ActionResult> DeployWithoutParameters(Chain chain, BlockchainNetwork blockchainNetwork, string privateKey, string metamaskAddress, string byteCode, [FromBody] object abi)
        {
            try
            {
                Account? account = new Account(privateKey, chain);
                Web3? web3 = new Web3(account, EnumHelper.GetStringBasedOnEnum(blockchainNetwork));

                HexBigInteger estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(abi.ToString(),
                                                                                            byteCode,
                                                                                            metamaskAddress,
                                                                                            null);

                TransactionReceipt? deployContract = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi.ToString(),
                                                                                                                    byteCode,
                                                                                                                    metamaskAddress,
                                                                                                                    estimatedGas,
                                                                                                                    null, null, null, null);

                return Ok(deployContract);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}