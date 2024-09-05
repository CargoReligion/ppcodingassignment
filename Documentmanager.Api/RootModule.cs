using Autofac;
using AutoMapper;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Repositories.Organizations;

namespace Documentmanager.Api
{
    public class RootModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            #region Repositories
            builder.RegisterType<OrganizationRepository>().As<IRepository<Organization>>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
