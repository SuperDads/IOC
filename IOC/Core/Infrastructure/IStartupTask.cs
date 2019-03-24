namespace Core.Infrastructure
{
    /// <summary>
    /// 应该由启动时运行的任务实现的接口
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        void Execute();

        /// <summary>
        /// 顺序
        /// </summary>
        int Order { get; }
    }
}
