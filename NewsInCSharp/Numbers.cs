
using System.Numerics;

namespace NewsInCSharp
{
    public class Numbers : IRunable
    {
        public void Run()
        {
            INumberExample();
        }

        /// <summary>
        /// 新增INumber类型
        /// </summary>
        private void INumberExample()
        {
            Console.WriteLine(Add(1, 2));
            Console.WriteLine(Add(1.121f, 2.22f));
            Console.WriteLine(Add(1.121, 2.22f));
            Console.WriteLine(Add(1.121, 2.22));
        }

        private T Add<T>(T x, T y) where T : INumber<T>
        {
            return x + y;
        }

    }
}
