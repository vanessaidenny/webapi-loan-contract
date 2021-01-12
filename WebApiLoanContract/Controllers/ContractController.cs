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
    }
}