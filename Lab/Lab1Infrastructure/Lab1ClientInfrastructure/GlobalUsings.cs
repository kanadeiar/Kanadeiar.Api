// dotnet ef --startup-project ../../Lab1ClientApi/ migrations add init --context ClientDbContext

global using Kanadeiar.Api.Repositories;
global using Lab1ClientApplication.Interfaces.Repositories;
global using Lab1ClientDomain.Entites;
global using Lab1ClientInfrastructure.Data;
global using Lab1ClientInfrastructure.Repositories;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
