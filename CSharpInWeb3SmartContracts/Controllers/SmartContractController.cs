﻿using Application.Interfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    public class SmartContractController : ControllerBase
    {
        private readonly User _user = new User();
        public EnumHelper EnumHelper { get; set; }

        private readonly ISmartContractService _smartContractService;

        public SmartContractController(IConfiguration configuration, ISmartContractService smartContractService)
        {
            EnumHelper = new EnumHelper(configuration);
            _user = configuration.GetSection("User").Get<User>()!;
            _smartContractService = smartContractService;
        }

        [HttpGet("GetAllSmartContracts")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllSmartContractsAsync()
        {
            var allSmartContracts = await _smartContractService.GetSmartContractsAsync();
            return Ok(allSmartContracts);
        }

        [HttpGet("GetAllSmartContracts")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSmartContractAsync(string id)
        {
            var allSmartContracts = await _smartContractService.GetSmartContractAsync(id);
            return Ok(allSmartContracts);
        }

        [Consumes("application/json")]
        [HttpPost("DeployAnyContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeployContractAsync([FromBody] SmartContractDTO smartContractDto)
        {
            var deployedSmartContract = await _smartContractService.DeploySmartContractAsync(smartContractDto);
            return Ok(deployedSmartContract);
        }

        [Consumes("application/json")]
        [HttpPost("CallContractVariable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CallContractVariableAsync(string variableName, [FromBody] SmartContract smartContractModel)
        {
            var variableResult = await _smartContractService.ReadContractFunctionVariableAsync(variableName, smartContractModel);
            return Ok(variableResult.ToString());
        }

        [Consumes("application/json")]
        [HttpPost("CallReadFunction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ReadContractFunctionAsync(string variableName, [FromBody] SmartContract smartContractModel)
        {
            var variableResult = await _smartContractService.ReadContractFunctionVariableAsync(variableName, smartContractModel);
            return Ok(variableResult.ToString());
        }

        [Consumes("application/json")]
        [HttpPost("CallWriteFunction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CallWriteFunctionAsync(string functionName, long sendAsEth, [FromBody] SmartContract smartContractModel)
        {
            var functionResult = await _smartContractService.WriteContractFunctionVariableAsync(functionName, sendAsEth, smartContractModel);
            return Ok(functionResult.ToString());
        }

        [Consumes("application/json")]
        [HttpPost("TrackAnyEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> TrackEventAsync(string eventName, SmartContract smartContractJson)
        {
            var eventLogs = await _smartContractService.TrackEventAsync(eventName, smartContractJson);
            return Ok(eventLogs.ToString());
        }

    }
}
