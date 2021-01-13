using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Search for contract by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> List one contract with installments
        public async Task<Contract> GetContractsById(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            contract.Installments = await _context.Installments
                .Where(x => contract.ContractId == x.ContractId)
                .ToListAsync();
            return contract;
        }

        /// <summary>
        /// Include installments according to the number set before post it all
        /// and also verify data annotations
        /// </summary>
        /// <param name="Contract model"></param>
        /// <returns> Create and post all contracts with installments
        public async Task<ActionResult<Contract>> InsertInstallments(
            [FromBody] Contract model)
        {
                for (var i=1; i<model.NumberInstallments; i++)
                {
                    model.Installments.Add(new Installment());
                }
                _context.Contracts.Add(model);
                await _context.SaveChangesAsync();
                return model;
        }

        /// <summary>
        /// Include installments according to the number set in the patch to update the contract
        /// </summary>
        /// <param name="ContractPatchRequest request, Contract model"></param>
        /// <returns> Update contract with new installments
        public async Task<ActionResult<Contract>> InsertInstallmentsPatch(
            [FromBody] ContractPatchRequest request,
            [FromBody] Contract model)
        {
                for (var i=1; i<request.NumberInstallments; i++)
                {
                    request.Installments.Add(new Installment());
                }
                model.Installments = request.Installments;
                await _context.SaveChangesAsync();
                return model;
        }

        /// <summary>
        /// Search for the contract by id and remove it
        /// </summary>
        /// <param name="Contract model"></param>
        /// <returns> Remove the contract
        public async Task<Contract> RemoveContract(int id)
        {            
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);            
            contract = await RemoveInstallments(id);
            await _context.SaveChangesAsync();
            return contract;
        }

        /// <summary>
        /// Search by the contract id and remove the installments
        /// </summary>
        /// <param name="Contract model"></param>
        /// <returns> Remove the contract with installments
        public async Task<Contract> RemoveInstallments(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            var installments = await _context.Installments            
                .Where(x => contract.ContractId == x.ContractId)
                .ToArrayAsync();
            _context.Installments.RemoveRange(installments);
            return contract;
        }
    }
}