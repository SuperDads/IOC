using AutoMapper;
using System;
using System.Collections.Generic;

namespace Core.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration
    /// </summary>
    public static class AutoMapperConfiguration
    {
        //private static MapperConfiguration _mapperConfiguration;
        //private static IMapper _mapper;

        /// <summary>
        /// 初始化映射器
        /// </summary>
        /// <param name="configurationActions">Configuration actions</param>
        public static void Init(List<Action<IMapperConfigurationExpression>> configurationActions)
        {
            if (configurationActions == null)
                throw new ArgumentNullException("configurationActions");

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                foreach (var ca in configurationActions)
                    ca(cfg);
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper { get; private set; }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration { get; private set; }

        /// <summary>
        /// Mapper
        /// </summary>
        //public static IMapper Mapper
        //{
        //    get
        //    {
        //        return _mapper;
        //    }
        //}

        /// <summary>
        /// Mapper configuration
        /// </summary>
        //public static MapperConfiguration MapperConfiguration
        //{
        //    get
        //    {
        //        return _mapperConfiguration;
        //    }
        //}
    }
}