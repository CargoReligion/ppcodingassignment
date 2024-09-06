using Autofac;
using Autofac.Extensions.DependencyInjection;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Models.Users;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Repositories.Organizations;
using Documentmanager.Core.Domain.Services.Organizations;
using Documentmanager.Core.Domain.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Organization), typeof(User));

// Use Autofac as the ServiceProvider
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Register your services here
    containerBuilder.RegisterType<OrganizationRepository>().As<IRepository<Organization>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserRepository>().As<IRepository<User>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserService>().InstancePerLifetimeScope();
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