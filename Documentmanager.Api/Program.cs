using Autofac;
using Autofac.Extensions.DependencyInjection;
using Documentmanager.Core.Domain.Models.Document;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Models.Users;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Repositories.Organizations;
using Documentmanager.Core.Domain.Repositories.Users;
using Documentmanager.Core.Domain.Services.Common;
using Documentmanager.Core.Domain.Services.Documents;
using Documentmanager.Core.Domain.Services.Organizations;
using Documentmanager.Core.Domain.Services.OrganizationUser;
using Documentmanager.Core.Domain.Services.Users;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Organization), typeof(User), typeof(Document));

// Use Autofac as the ServiceProvider
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DocumentManager API", Version = "v1" });
});

// Register services directly with Autofac here
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Register your services here
    containerBuilder.RegisterType<OrganizationRepository>().As<IRepository<Organization>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserRepository>().As<IRepository<User>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DocumentRepository>().As<IRepository<Document>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DocumentRepository>().As<IDocumentRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationUserService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DocumentService>().InstancePerLifetimeScope();

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();