using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLoanContract.Data;
using WebApiLoanContract.Models;

namespace WebApiLoanContract.Controllers
{
    [ApiController]
    [Route("contracts")]
    public class ContractController : ControllerBase
    {
        /// <summary>
        /// List all contracts
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Contract>>> Get(
            [FromServices] DataContext context)
        {            
            var contracts = await context.Contracts.ToListAsync();
            foreach(var contract in contracts)
            {
                contract.Installments = await context.Installments
                    .Where(x => contract.ContractId == x.ContractId)
                    .ToListAsync();
            }
            return contracts;
        }

        /// <summary>
        /// Create and post all contracts with installments
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Contract>> Post(
            [FromServices] DataContext context,
            [FromBody] Contract model)
        {
            if (ModelState.IsValid)
            {
                for (var i=1; i<model.NumberInstallments; i++)
                {
                    model.Installments.Add(new Installment());
                }
                context.Contracts.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}