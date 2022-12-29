﻿using Domain.Models;
using Infrastructure.Persistence.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    public class SmartContractRepository : GenericRepository<SmartContract>, ISmartContractRepository
    {
        public SmartContractRepository(ApplicationDbContext smartContractDbContext) : base(smartContractDbContext) { }

        public async Task<IEnumerable<SmartContract>> GetSmartContracts()
        {
            var allSmartContracts = await GetAll();
            return allSmartContracts;
        }

        public async Task<SmartContract> GetSmartContractAsync(long id)
        {
            var smartContract = await GetById(id);
            return smartContract;
        }


    }
}
