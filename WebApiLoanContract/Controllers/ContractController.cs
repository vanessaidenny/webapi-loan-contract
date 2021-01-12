using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly DataContext _context;
        private readonly IContractService _service;

        public ContractController(
            IMemoryCache memoryCache,
            DataContext context,
            IContractService service
        )
        {
            _cache = memoryCache;
            _context = context;
            _service = service;
        }

        /// <summary>
        /// List all contracts with installments
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Contract>>> CacheGetOrCreate()
        {
            var cacheEntry = await
                _cache.GetOrCreateAsync(_context.Contracts.ToListAsync(), entry => 
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                    return _service.GetContracts();
                });
            return cacheEntry;
        }

        /// <summary>
        /// List one contract with installments
        /// </summary>
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
        /// Create and post all contracts with installments
        /// </summary>
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
    }
}