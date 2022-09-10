﻿using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CSharpInWeb3SmartContracts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinMarketCapController : ControllerBase
    {
        private string _apiKey { get; }

        public CoinMarketCapController(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("CoinMarketCap:APIKey").Get<string>();
        }

        [HttpGet("GetCoins")]
        public async Task<ActionResult> GetCoins()
        {
            try
            {

            }
            catch (Exception exception)
            {

            }

        }
    }
}
