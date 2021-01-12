using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLoanContract.Models;

namespace WebApiLoanContract.Services
{
    public interface IContractService
    {
        Task<List<Contract>> GetContracts();
    }
}