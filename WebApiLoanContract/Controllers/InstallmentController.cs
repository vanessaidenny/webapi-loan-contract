using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLoanContract.Data;
using WebApiLoanContract.Models;

namespace WebApiLoanContract.Controllers
{
    [ApiController]
    [Route("installments")]
    public class InstallmentController : ControllerBase
    {
        /// <param name="DataContext context"></param>
        /// <returns> List all installments
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Installment>>> Get([FromServices] DataContext context)
        {
            var installments = await context.Installments.ToListAsync();
            return installments;
        }
    }
}