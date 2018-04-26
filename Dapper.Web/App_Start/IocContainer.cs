using Autofac;
using System.Reflection;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace Dapper.Web
{
    public class IocContainer
    {
        public static void Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.Load("Dapper.Repository"))
               .Where(x => x.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerDependency();
            builder.RegisterAssemblyTypes(Assembly.Load("Dapper.Service"))
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}