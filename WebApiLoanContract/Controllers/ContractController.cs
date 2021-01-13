using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;
using WebApiLoanContract.Data;
using WebApiLoanContract.Models;
using WebApiLoanContract.Services;

namespace WebApiLoanContract.Controllers
{
    [ApiController]
    [Route("contracts")]
    public class ContractController : ControllerBase
    {
        private IMemoryCache _cache;       
        private IFeatureManager _featureManager; 
        private readonly DataContext _context;
        private readonly IContractService _service;

        public ContractController(
            IMemoryCache memoryCache,
            IFeatureManager featureManager,
            DataContext context,
            IContractService service
        )
        {
            _cache = memoryCache;
            _featureManager = featureManager;
            _context = context;
            _service = service;
        }

        public static class FeatureFlags
        {
            public const string MemoryCache = "MemoryCache";
        }

        /// <summary>
        /// Check that the feature flag for in memory cache data is enabled
        /// and that the cache is already saved before to list the contracts
        /// </summary>
        /// <returns> List of all contracts with installments
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Contract>>> CacheGetOrCreate()
        {
            var cacheIsEnabled = _featureManager.IsEnabledAsync(FeatureFlags.MemoryCache);
            
            if(!await cacheIsEnabled)
            {
                _cache.Remove(_context.Contracts);
            }
            return await
                _cache.GetOrCreateAsync(_context.Contracts.ToListAsync(), entry => 
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                    return _service.GetContracts();
                });
        }

        /// <summary>
        /// Search for contract by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> List one contract with installments
        [HttpGet]
        [Route("{id:int}")]
        public async Task<Contract> GetById(int id)
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
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Contract>> Post(
            [FromBody] Contract model)
        {
            if (ModelState.IsValid)
            {
                for (var i=1; i<model.NumberInstallments; i++)
                {
                    model.Installments.Add(new Installment());
                }
                _context.Contracts.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Search for the contract by id and remove it
        /// </summary>
        /// <param name="Contract model"></param>
        /// <returns> Remove the contract with installments
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<Contract> Delete(int id)
        {
            // Remove contracts
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);
            
            // Remove installments
            var prestacoes = await _context.Installments            
                .Where(x => contract.ContractId == x.ContractId)
                .ToArrayAsync();
            _context.Installments.RemoveRange(prestacoes);

            // Return contracts
            await _context.SaveChangesAsync();
            return contract;
        }
    }
}