using AutoMapper;
using System;

namespace Core.Infrastructure.Mapper
{
    /// <summary>
    /// 映射器配置注册器接口
    /// </summary>
    public interface IMapperConfiguration
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>Mapper configuration action</returns>
        Action<IMapperConfigurationExpression> GetConfiguration();

        /// <summary>
        /// 映射器实现的顺序
        /// </summary>
        int Order { get; }
    }
}
