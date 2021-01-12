using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLoanContract.Data;
using WebApiLoanContract.Models;

namespace WebApiLoanContract.Controllers
{
    [ApiController]
    [Route("v1/installments")]
    public class InstallmentController : ControllerBase
    {
        /// <summary>
        /// List all installments
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Installment>>> Get([FromServices] DataContext context)
        {
            var installments = await context.Installments.ToListAsync();
            return installments;
        }
    }
}