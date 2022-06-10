﻿using CSharpInWeb3SmartContracts.Models;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Contracts;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;

namespace CSharpInWeb3SmartContracts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniswapV3Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly User _user = new User();

        private readonly string _uniswapV3factoryAddress = "0x1F98431c8aD98523631AE4a59f267346ea31F984";

        private readonly string _uniswapV3factoryAbi = @" [{""inputs"":[],""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""uint24"",""name"":""fee"",""type"":""uint24""},{""indexed"":true,""internalType"":""int24"",""name"":""tickSpacing"",""type"":""int24""}],""name"":""FeeAmountEnabled"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""oldOwner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""newOwner"",""type"":""address""}],""name"":""OwnerChanged"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""token0"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""token1"",""type"":""address""},{""indexed"":true,""internalType"":""uint24"",""name"":""fee"",""type"":""uint24""},{""indexed"":false,""internalType"":""int24"",""name"":""tickSpacing"",""type"":""int24""},{""indexed"":false,""internalType"":""address"",""name"":""pool"",""type"":""address""}],""name"":""PoolCreated"",""type"":""event""},{""inputs"":[{""internalType"":""address"",""name"":""tokenA"",""type"":""address""},{""internalType"":""address"",""name"":""tokenB"",""type"":""address""},{""internalType"":""uint24"",""name"":""fee"",""type"":""uint24""}],""name"":""createPool"",""outputs"":[{""internalType"":""address"",""name"":""pool"",""type"":""address""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""uint24"",""name"":""fee"",""type"":""uint24""},{""internalType"":""int24"",""name"":""tickSpacing"",""type"":""int24""}],""name"":""enableFeeAmount"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""uint24"",""name"":"""",""type"":""uint24""}],""name"":""feeAmountTickSpacing"",""outputs"":[{""internalType"":""int24"",""name"":"""",""type"":""int24""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""},{""internalType"":""address"",""name"":"""",""type"":""address""},{""internalType"":""uint24"",""name"":"""",""type"":""uint24""}],""name"":""getPool"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""owner"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""parameters"",""outputs"":[{""internalType"":""address"",""name"":""factory"",""type"":""address""},{""internalType"":""address"",""name"":""token0"",""type"":""address""},{""internalType"":""address"",""name"":""token1"",""type"":""address""},{""internalType"":""uint24"",""name"":""fee"",""type"":""uint24""},{""internalType"":""int24"",""name"":""tickSpacing"",""type"":""int24""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_owner"",""type"":""address""}],""name"":""setOwner"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""}] ";


        private readonly string USDT = "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48";

        private readonly string DAI = "0x6B175474E89094C44Da98b954EedeAC495271d0F";


        private readonly string _tokenERC20Abi = @" [{""inputs"":[{""internalType"":""uint256"",""name"":""chainId_"",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""src"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""guy"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":true,""inputs"":[{""indexed"":true,""internalType"":""bytes4"",""name"":""sig"",""type"":""bytes4""},{""indexed"":true,""internalType"":""address"",""name"":""usr"",""type"":""address""},{""indexed"":true,""internalType"":""bytes32"",""name"":""arg1"",""type"":""bytes32""},{""indexed"":true,""internalType"":""bytes32"",""name"":""arg2"",""type"":""bytes32""},{""indexed"":false,""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""LogNote"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""src"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""dst"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""constant"":true,""inputs"":[],""name"":""DOMAIN_SEPARATOR"",""outputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""PERMIT_TYPEHASH"",""outputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""},{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""allowance"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""usr"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""usr"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""burn"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""internalType"":""uint8"",""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""guy"",""type"":""address""}],""name"":""deny"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""usr"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""mint"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""src"",""type"":""address""},{""internalType"":""address"",""name"":""dst"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""move"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""nonces"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""holder"",""type"":""address""},{""internalType"":""address"",""name"":""spender"",""type"":""address""},{""internalType"":""uint256"",""name"":""nonce"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""expiry"",""type"":""uint256""},{""internalType"":""bool"",""name"":""allowed"",""type"":""bool""},{""internalType"":""uint8"",""name"":""v"",""type"":""uint8""},{""internalType"":""bytes32"",""name"":""r"",""type"":""bytes32""},{""internalType"":""bytes32"",""name"":""s"",""type"":""bytes32""}],""name"":""permit"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""usr"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""pull"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""usr"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""push"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""guy"",""type"":""address""}],""name"":""rely"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""dst"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""src"",""type"":""address""},{""internalType"":""address"",""name"":""dst"",""type"":""address""},{""internalType"":""uint256"",""name"":""wad"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""version"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""wards"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}] ";

        private readonly string _uniswapV3PoolAbi = @" [{""inputs"":[],""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""indexed"":true,""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""},{""indexed"":false,""internalType"":""uint128"",""name"":""amount"",""type"":""uint128""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""name"":""Burn"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":false,""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""indexed"":true,""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""indexed"":true,""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""},{""indexed"":false,""internalType"":""uint128"",""name"":""amount0"",""type"":""uint128""},{""indexed"":false,""internalType"":""uint128"",""name"":""amount1"",""type"":""uint128""}],""name"":""Collect"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""indexed"":false,""internalType"":""uint128"",""name"":""amount0"",""type"":""uint128""},{""indexed"":false,""internalType"":""uint128"",""name"":""amount1"",""type"":""uint128""}],""name"":""CollectProtocol"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""paid0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""paid1"",""type"":""uint256""}],""name"":""Flash"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""uint16"",""name"":""observationCardinalityNextOld"",""type"":""uint16""},{""indexed"":false,""internalType"":""uint16"",""name"":""observationCardinalityNextNew"",""type"":""uint16""}],""name"":""IncreaseObservationCardinalityNext"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""uint160"",""name"":""sqrtPriceX96"",""type"":""uint160""},{""indexed"":false,""internalType"":""int24"",""name"":""tick"",""type"":""int24""}],""name"":""Initialize"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""indexed"":true,""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""},{""indexed"":false,""internalType"":""uint128"",""name"":""amount"",""type"":""uint128""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""name"":""Mint"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""uint8"",""name"":""feeProtocol0Old"",""type"":""uint8""},{""indexed"":false,""internalType"":""uint8"",""name"":""feeProtocol1Old"",""type"":""uint8""},{""indexed"":false,""internalType"":""uint8"",""name"":""feeProtocol0New"",""type"":""uint8""},{""indexed"":false,""internalType"":""uint8"",""name"":""feeProtocol1New"",""type"":""uint8""}],""name"":""SetFeeProtocol"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""indexed"":false,""internalType"":""int256"",""name"":""amount0"",""type"":""int256""},{""indexed"":false,""internalType"":""int256"",""name"":""amount1"",""type"":""int256""},{""indexed"":false,""internalType"":""uint160"",""name"":""sqrtPriceX96"",""type"":""uint160""},{""indexed"":false,""internalType"":""uint128"",""name"":""liquidity"",""type"":""uint128""},{""indexed"":false,""internalType"":""int24"",""name"":""tick"",""type"":""int24""}],""name"":""Swap"",""type"":""event""},{""inputs"":[{""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""},{""internalType"":""uint128"",""name"":""amount"",""type"":""uint128""}],""name"":""burn"",""outputs"":[{""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""},{""internalType"":""uint128"",""name"":""amount0Requested"",""type"":""uint128""},{""internalType"":""uint128"",""name"":""amount1Requested"",""type"":""uint128""}],""name"":""collect"",""outputs"":[{""internalType"":""uint128"",""name"":""amount0"",""type"":""uint128""},{""internalType"":""uint128"",""name"":""amount1"",""type"":""uint128""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""internalType"":""uint128"",""name"":""amount0Requested"",""type"":""uint128""},{""internalType"":""uint128"",""name"":""amount1Requested"",""type"":""uint128""}],""name"":""collectProtocol"",""outputs"":[{""internalType"":""uint128"",""name"":""amount0"",""type"":""uint128""},{""internalType"":""uint128"",""name"":""amount1"",""type"":""uint128""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""name"":""factory"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""fee"",""outputs"":[{""internalType"":""uint24"",""name"":"""",""type"":""uint24""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""feeGrowthGlobal0X128"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""feeGrowthGlobal1X128"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""},{""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""flash"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""uint16"",""name"":""observationCardinalityNext"",""type"":""uint16""}],""name"":""increaseObservationCardinalityNext"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""uint160"",""name"":""sqrtPriceX96"",""type"":""uint160""}],""name"":""initialize"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""name"":""liquidity"",""outputs"":[{""internalType"":""uint128"",""name"":"""",""type"":""uint128""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""maxLiquidityPerTick"",""outputs"":[{""internalType"":""uint128"",""name"":"""",""type"":""uint128""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""},{""internalType"":""uint128"",""name"":""amount"",""type"":""uint128""},{""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""mint"",""outputs"":[{""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""name"":""observations"",""outputs"":[{""internalType"":""uint32"",""name"":""blockTimestamp"",""type"":""uint32""},{""internalType"":""int56"",""name"":""tickCumulative"",""type"":""int56""},{""internalType"":""uint160"",""name"":""secondsPerLiquidityCumulativeX128"",""type"":""uint160""},{""internalType"":""bool"",""name"":""initialized"",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint32[]"",""name"":""secondsAgos"",""type"":""uint32[]""}],""name"":""observe"",""outputs"":[{""internalType"":""int56[]"",""name"":""tickCumulatives"",""type"":""int56[]""},{""internalType"":""uint160[]"",""name"":""secondsPerLiquidityCumulativeX128s"",""type"":""uint160[]""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""name"":""positions"",""outputs"":[{""internalType"":""uint128"",""name"":""liquidity"",""type"":""uint128""},{""internalType"":""uint256"",""name"":""feeGrowthInside0LastX128"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""feeGrowthInside1LastX128"",""type"":""uint256""},{""internalType"":""uint128"",""name"":""tokensOwed0"",""type"":""uint128""},{""internalType"":""uint128"",""name"":""tokensOwed1"",""type"":""uint128""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""protocolFees"",""outputs"":[{""internalType"":""uint128"",""name"":""token0"",""type"":""uint128""},{""internalType"":""uint128"",""name"":""token1"",""type"":""uint128""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint8"",""name"":""feeProtocol0"",""type"":""uint8""},{""internalType"":""uint8"",""name"":""feeProtocol1"",""type"":""uint8""}],""name"":""setFeeProtocol"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""name"":""slot0"",""outputs"":[{""internalType"":""uint160"",""name"":""sqrtPriceX96"",""type"":""uint160""},{""internalType"":""int24"",""name"":""tick"",""type"":""int24""},{""internalType"":""uint16"",""name"":""observationIndex"",""type"":""uint16""},{""internalType"":""uint16"",""name"":""observationCardinality"",""type"":""uint16""},{""internalType"":""uint16"",""name"":""observationCardinalityNext"",""type"":""uint16""},{""internalType"":""uint8"",""name"":""feeProtocol"",""type"":""uint8""},{""internalType"":""bool"",""name"":""unlocked"",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""int24"",""name"":""tickLower"",""type"":""int24""},{""internalType"":""int24"",""name"":""tickUpper"",""type"":""int24""}],""name"":""snapshotCumulativesInside"",""outputs"":[{""internalType"":""int56"",""name"":""tickCumulativeInside"",""type"":""int56""},{""internalType"":""uint160"",""name"":""secondsPerLiquidityInsideX128"",""type"":""uint160""},{""internalType"":""uint32"",""name"":""secondsInside"",""type"":""uint32""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""recipient"",""type"":""address""},{""internalType"":""bool"",""name"":""zeroForOne"",""type"":""bool""},{""internalType"":""int256"",""name"":""amountSpecified"",""type"":""int256""},{""internalType"":""uint160"",""name"":""sqrtPriceLimitX96"",""type"":""uint160""},{""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""swap"",""outputs"":[{""internalType"":""int256"",""name"":""amount0"",""type"":""int256""},{""internalType"":""int256"",""name"":""amount1"",""type"":""int256""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""int16"",""name"":"""",""type"":""int16""}],""name"":""tickBitmap"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""tickSpacing"",""outputs"":[{""internalType"":""int24"",""name"":"""",""type"":""int24""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""int24"",""name"":"""",""type"":""int24""}],""name"":""ticks"",""outputs"":[{""internalType"":""uint128"",""name"":""liquidityGross"",""type"":""uint128""},{""internalType"":""int128"",""name"":""liquidityNet"",""type"":""int128""},{""internalType"":""uint256"",""name"":""feeGrowthOutside0X128"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""feeGrowthOutside1X128"",""type"":""uint256""},{""internalType"":""int56"",""name"":""tickCumulativeOutside"",""type"":""int56""},{""internalType"":""uint160"",""name"":""secondsPerLiquidityOutsideX128"",""type"":""uint160""},{""internalType"":""uint32"",""name"":""secondsOutside"",""type"":""uint32""},{""internalType"":""bool"",""name"":""initialized"",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""token0"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""token1"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""}] ";

        public UniswapV3Controller(IConfiguration configuration)
        {
            _configuration = configuration;
            _user.BlockchainProvider = _configuration["BlockchainProviderMainnet"];
            _user.MetamaskAddress = _configuration["MetamaskAddress"];
            _user.PrivateKey = _configuration["PrivateKey"];
        }

        [HttpGet("GetReserves")]
        public async Task<ActionResult> GetPoolAndBalances(string addressToken0, string addressToken1, long fee)
        {
            try
            {
                Account? account = new Account(_user.PrivateKey, Chain.MainNet);
                Web3? web3 = new Web3(account, _user.BlockchainProvider);

                Contract? smartContract = web3.Eth.GetContract(_uniswapV3factoryAbi, _uniswapV3factoryAddress);
                Function? getPool = smartContract.GetFunction("getPool");

                object[] parameters = new object[3] { addressToken0, addressToken1, fee };
                string poolAddress = await getPool.CallAsync<string>(parameters);

                if (poolAddress == "0x0000000000000000000000000000000000000000")
                {
                    return NotFound();
                }

                Contract? smartContractPool = web3.Eth.GetContract(_uniswapV3PoolAbi, poolAddress);
                Function? getToken0FromPool = smartContractPool.GetFunction("token0");
                string token0 = await getToken0FromPool.CallAsync<string>();

                Function? getToken1FromPool = smartContractPool.GetFunction("token1");
                string token1 = await getToken1FromPool.CallAsync<string>();


                Contract? smartContractToken0 = web3.Eth.GetContract(_tokenERC20Abi, token0);
                Function? balanceOfToken0 = smartContractToken0.GetFunction("balanceOf");
                BigInteger balanceOfToken0Result = await balanceOfToken0.CallAsync<BigInteger>(poolAddress);

                Contract? smartContractToken1 = web3.Eth.GetContract(_tokenERC20Abi, token1);
                Function? balanceOfToken1 = smartContractToken1.GetFunction("balanceOf");
                BigInteger balanceOfToken1Result = await balanceOfToken1.CallAsync<BigInteger>(poolAddress);

                decimal balanceInEthToken0 = Web3.Convert.FromWei(balanceOfToken0Result);
                decimal balanceInEthToken1 = Web3.Convert.FromWei(balanceOfToken1Result);

                //balanceInEthToken1 has 6 decimals and not 18
                decimal adjusted_price = balanceInEthToken1 / (10 ^ (18 - 6));
                decimal inverted_price = 1 / adjusted_price;

                decimal price = balanceInEthToken0 / inverted_price;

                return Ok($"Token 1 balance {balanceInEthToken0} and balance of token 2 {inverted_price}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
