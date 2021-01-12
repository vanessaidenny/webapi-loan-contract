using System.Collections.Generic;
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
            return contracts;
        }

        /// <summary>
        /// Create and post all contracts
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Contract>> Post(
            [FromServices] DataContext context,
            [FromBody] Contract model)
        {
            if (ModelState.IsValid)
            {
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