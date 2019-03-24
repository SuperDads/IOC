using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.Infrastructure;
using Core.Infrastructure.DependencyManagement;
using System;
using System.Linq;
using System.Reflection;

namespace API.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            #region 方式一

            //Assembly assemblies = typeFinder.GetAssemblies().First(p => p.ManifestModule.Name == "BLL.dll");

            //foreach (var t in assemblies.GetTypes())
            //{
            //    if (t.IsClass && !t.IsAbstract && t.Name.LastIndexOf("BLL") == t.Name.Length - 7)
            //    {
            //        builder.RegisterType(t).InstancePerLifetimeScope();
            //    }
            //}
            //builder.RegisterAssemblyTypes(assemblies).InstancePerLifetimeScope();

            #endregion

            // 获取已加载到此应用程序域的执行上下文中的程序集
            Assembly[] assemblies = typeFinder.GetAssemblies().ToArray();

            Type[] dependencyTypes = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && !p.IsAbstract).ToArray();

            builder.RegisterTypes(dependencyTypes)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}