![GitHub last commit](https://img.shields.io/github/last-commit/vanessaidenny/webapi-loan-contract?color=blueviolet&style=plastic)
![GitHub contributors](https://img.shields.io/github/contributors/vanessaidenny/webapi-loan-contract?color=brightgreen&style=plastic)
 
# Web Api Loan Contract

Web Api for loan contract to generate all bills according to the number of installments

### Table of Contents

- [Features](#features)
  - [Structure](#structure)
  - [Documentation](#documentation)
  - [Testing](#testing)
- [References](#references)
- [License & Copyright](#license)

<a name="features"></a>
## ⚙️ Features

<a name="structure"></a>
### Structure

*  Contract Entity:  
&ensp;Id - key autoincrement, contract date, number of installments, amount financed, installments  
*  Installment Entity:  
&ensp;Contract, expiration date, payment date, amount, status: Open, Delayed, Marked  
&ensp;The Status field must be calculated based on:  
    1. Open:  
    due date >= current date and has no payment date  
    2. Delayed:  
    due date < current date and has no payment date  
    3. Marked:  
    payment date not null  

<a name="documentation"></a>
### Documentation

- [X] Create an web api with contract and installment entities
- [ ] RESTful actions - get, post, put and delete - for contract entity
- [X] Cache data in memory - Implement InMemoryCache
- [X] Implement feature flags to enable or disable the cache
- [X] Implement Swagger package

<a name="testing"></a>
### Testing

Swagger tools with automated API testing to validate that it works as intended

<a name="references"></a>
## 📚 References

[REST client](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient#processing-the-json-result)  
[Relationship model](https://www.youtube.com/watch?app=desktop&v=but7jqjopKM)  
[Implementing APIs with Swashbuckle.AspNetCore package](https://renatogroffe.medium.com/asp-net-core-swagger-documentando-apis-com-o-package-swashbuckle-aspnetcore-5eef480ba1c0)  
[Tutorial: Use feature flags in an ASP.NET Core app](https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core)

<a name="license"></a>
## 📌 License & Copyright

&copy; 2020 Vanessa Isabela Denny