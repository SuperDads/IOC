using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace API
{
    public class Containers
    {
        //private static IContainer _Container { get; set; }

        public void Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}