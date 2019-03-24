using Autofac;
using Autofac.Integration.Mvc;
using Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.Infrastructure
{
    public class IocEngine : IEngine
    {
        /// <summary>
        /// Container manager
        /// </summary>
        public virtual ContainerManager ContainerManager
        {
            get;
            private set;
        }

        /// <summary>
        /// 容器
        /// </summary>
        public virtual IContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Run startup tasks
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            //var typeFinder = _containerManager.Resolve<ITypeFinder>();
            //var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            //var startUpTasks = new List<IStartupTask>();
            //foreach (var startUpTaskType in startUpTaskTypes)
            //    startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            ////sort
            //startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            //foreach (var startUpTask in startUpTasks)
            //    startUpTask.Execute();
        }

        /// <summary>
        /// 注册依赖项
        /// </summary>
        protected virtual void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();

            // dependencies
            WebAppTypeFinder typeFinder = new WebAppTypeFinder();
            //builder.RegisterInstance(config).As<NopConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            // register dependencies provided by other assemblies
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            List<IDependencyRegistrar> drInstances = new List<IDependencyRegistrar>();

            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            // sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);

            Container = builder.Build();
            this.ContainerManager = new ContainerManager(Container);

            // 设置依赖项解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        /// <summary>
        /// 注册映射
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterMapperConfiguration()
        {
            //    //dependencies
            //    var typeFinder = new WebAppTypeFinder();

            //    //register mapper configurations provided by other assemblies
            //    var mcTypes = typeFinder.FindClassesOfType<IMapperConfiguration>();
            //    var mcInstances = new List<IMapperConfiguration>();
            //    foreach (var mcType in mcTypes)
            //        mcInstances.Add((IMapperConfiguration)Activator.CreateInstance(mcType));
            //    //sort
            //    mcInstances = mcInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            //    //get configurations
            //    var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            //    foreach (var mc in mcInstances)
            //        configurationActions.Add(mc.GetConfiguration());
            //    //register
            //    AutoMapperConfiguration.Init(configurationActions);
        }

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        public void Initialize()
        {
            // register dependencies
            RegisterDependencies();

            //register mapper configurations
            //RegisterMapperConfiguration();
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
