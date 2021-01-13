using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLoanContract.Data;
using WebApiLoanContract.Models;

namespace WebApiLoanContract.Services
{
    public class ContractService : IContractService
    {    
        private readonly DataContext _context;

        public ContractService(DataContext context) {
            _context = context;
        }

        /// <summary>
        /// Apply installment entity in each contract entity
        /// </summary>
        /// <returns> List of all contracts with installments
        public async Task<List<Contract>> GetContracts()
        {            
            var contracts = await _context.Contracts.ToListAsync();
            foreach(var contract in contracts)
            {
                contract.Installments = await _context.Installments
                    .Where(x => contract.ContractId == x.ContractId)
                    .ToListAsync();
            }
            return contracts;
        }
    }
}