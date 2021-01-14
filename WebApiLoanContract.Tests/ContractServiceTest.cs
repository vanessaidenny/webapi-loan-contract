using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLoanContract.Data;
using WebApiLoanContract.Services;
using WebApiLoanContract.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLoanContract.Tests
{
    public class ContractServiceTest
    {
        [Fact]
        public async Task GetContractsTest_ReturnSpecificData()
        {
            //ARRANGE - Create InMemoryDatabase
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockDatabase")
                .Options;

            //Create mocked Context by seeding Data
            using (var context = new DataContext(options))
            {
                context.Contracts.Add(new Contract
                {
                    NumberInstallments = 2,
                    AmountFinanced = 20
                });
                context.SaveChanges();
            }

            //ACT - Context instance with Data
            using (var context = new DataContext(options))
            {
                ContractService contractService = new ContractService(context);
                var result = await contractService.GetContracts();

                //ASSERT
                Assert.NotEmpty(result);
                foreach (var data in result)
                {
                    Assert.Equal(data.NumberInstallments, 2);
                    Assert.Equal(data.AmountFinanced, 20);
                }
            }
        }        
        
        [Fact]
        public async Task GetContractsByIdTest_ValidateContractId1()
        {
            //ARRANGE - Create InMemoryDatabase
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockDatabase")
                .Options;

            //Create mocked Context by seeding Data
            using (var context = new DataContext(options))
            {
                context.Contracts.Add(new Contract
                {
                    NumberInstallments = 2,
                    AmountFinanced = 20
                });
                context.SaveChanges();
            }

            //ACT - Context instance with Data
            using (var context = new DataContext(options))
            {
                ContractService contractService = new ContractService(context);
                var result = await contractService.GetContractsById(1);

                //ASSERT
                Assert.Equal(result.ContractId, 1);
            }
        }
    }
}
