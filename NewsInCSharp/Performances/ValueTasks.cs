
namespace NewsInCSharp.Performances
{
    /// <summary>
    /// 优先使用 Task：如果方法几乎总是异步完成，直接使用 Task 更简单、安全。
    /// 谨慎使用 ValueTask：仅在以下情况考虑：
    /// 同步完成是常见情况（如缓存命中）。
    /// 需要与同步/异步混合的接口兼容。
    /// 编写高性能基础库，且能严格管理生命周期。
    /// 避免过度优化：不要为了“可能”的优化而牺牲代码可读性。在应用程序代码中，Task 通常是更安全的选择。
    /// </summary>
    public class ValueTasks : IRunable
    {
        public void Run()
        {
            BasicValueTaskUse();
        }

        private void BasicValueTaskUse()
        {
            ValueTask<int> GetCached(int key)
            {
                if (key % 2 == 0)
                {
                    //就是为了这个假异步，才需要使用ValueTask来提高性能
                    //不需要再在堆里创建个Task来包裹值了，直接返回一个struct包裹值。
                    return ValueTask.FromResult(key / 2);
                }

                //这里是特别微小的牺牲
                return new ValueTask<int>(GetFromNetwork(key));
            }

            async Task<int> GetFromNetwork(int key)
            {
                await Task.Delay(1000);
                return key * 2;
            }
        }
    }
}
