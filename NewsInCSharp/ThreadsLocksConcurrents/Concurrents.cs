
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace NewsInCSharp.ThreadsLocksConcurrents
{
    public class Concurrents : IRunable
    {
        public void Run()
        {
            WhyNotNormalList();
            UseConcurrentBagInstead();
            UseImmutableListInstead();
            TestBlockingCollections();
        }

        /// <summary>
        /// 因为List内部实现是线程不安全的
        /// 即List的内部字段是线程不安全的，比如_size，_items
        /// 在扩容时，会引起错误。
        /// 解决办法：
        /// 1，加锁 lock, ReaderWriterLockSlim 
        /// 2，使用ConcurrentBag
        /// 3，使用ImmutableList，适合读多，写少
        /// </summary>
        private void WhyNotNormalList()
        {
            List<int> list = new List<int>();

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10; ++i)
            {
                tasks.Add(Task.Run(() => AddToList(1000)));
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"{nameof(WhyNotNormalList)}: Actually Count:{list.Count}, Wanted Count:{10000}");

            void AddToList(int count)
            {
                for (int i = 0; i < count; ++i)
                {
                    list.Add(i);
                }
            }
        }

        private void UseConcurrentBagInstead()
        {
            ConcurrentBag<int> ints = new ConcurrentBag<int>();

        }

        private void UseImmutableListInstead()
        {
            ImmutableList<int> ints = ImmutableList<int>.Empty;

        }

        /// <summary>
        /// BlockingCollection 厉害在Blocking。即可以阻塞等待.
        /// 内部使用了SemaphoreSlim
        /// </summary>
        private void TestBlockingCollections()
        {
            BlockingCollection<int> buffer = new BlockingCollection<int>(5);

            var produceTask = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine($"Start Produce: {i}");
                    buffer.Add(i);
                    Console.WriteLine($"End Produce: {i}");
                }

                buffer.CompleteAdding();
                Console.WriteLine("CompleteAdding");
            });

            var consumeTask = Task.Run(async () =>
            {
                Random random = new Random();

                foreach (var item in buffer.GetConsumingEnumerable())
                {
                    await Task.Delay(random.Next(1000, 5000));
                    Console.WriteLine($"Consume: {item}");
                }
            });

            Task.WaitAll(produceTask, consumeTask);
        }
    }
}
