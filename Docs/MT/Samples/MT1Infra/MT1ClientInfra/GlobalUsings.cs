// dotnet ef --startup-project ../../MT1ClientApi/ migrations add init --context ClientDbContext

global using Kanadeiar.Api.Interfaces;
global using Kanadeiar.Api.Repositories;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using MT1ClientApplication.Interfaces.Repos;
global using MT1ClientDomain.Entites;
global using MT1ClientInfra.BackgroundServices;
global using MT1ClientInfra.Data;
global using MT1ClientInfra.Repos;
