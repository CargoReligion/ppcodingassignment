using Autofac;
using Autofac.Extensions.DependencyInjection;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Repositories.Organizations;
using Documentmanager.Core.Domain.Services.Organizations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Use Autofac as the ServiceProvider
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Register your services here
    containerBuilder.RegisterType<OrganizationRepository>().As<IRepository<Organization>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrganizationService>().InstancePerLifetimeScope();


    // You can also register entire assemblies
    // containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    //     .AsImplementedInterfaces();
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