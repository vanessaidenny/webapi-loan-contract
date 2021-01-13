using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiLoanContract.Models;

namespace WebApiLoanContract.Services
{
    public interface IContractService
    {
        Task<List<Contract>> GetContracts();
        Task<Contract> GetContractsById(int id);
        Task<ActionResult<Contract>> InsertInstallments(
            [FromBody] Contract model);
        Task<ActionResult<Contract>> InsertInstallmentsPatch(
            [FromBody] ContractPatchRequest request,
            [FromBody] Contract model);
        Task<Contract> RemoveContract(int id);
        Task<Contract> RemoveInstallments(int id);
    }
}