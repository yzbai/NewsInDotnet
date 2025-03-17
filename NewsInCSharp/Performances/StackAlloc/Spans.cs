
using System.Runtime.InteropServices;

using NewsInCSharp.Accessories;

namespace NewsInCSharp.Performances.StackAlloc
{
    /// <summary>
    /// Span配合 stackalloc 可以改造很多原始类，达到性能提升
    /// 比如Task，StringBuilder，List
    /// 
    /// scoped 关键字配合 ref struct，防止这个引用存储到堆上。
    /// ref struct的本质就是栈上的引用类型。 
    /// </summary>
    public class Spans : IRunable
    {
        public void Run()
        {
            SpanIsRefStruct();
            ModifyStringWithoutReAlloc();
            JoinNumbers();
        }

        //this is a method that use span to speed up string modification
        private void SpeedUpStringReplace()
        {
            string bigString = Utils.GetNaughtyString();
            char[] buffer = bigString.ToCharArray();
            Span<char> span = buffer.AsSpan();

            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] == 'a')
                {
                    span[i] = 'x';
                }
            }

            Console.WriteLine(span.ToString());
        }

        private void ModifyStringWithoutReAlloc()
        {
            string bigString = Utils.GetNaughtyString();

            char[] buffer = bigString.ToCharArray();

            Span<char> span = buffer.AsSpan();

            span[0] = 'x';

            Console.WriteLine(span.ToString());
        }

        /// <summary>
        /// 使用string.Create 和 span
        /// </summary>
        private void JoinNumbers()
        {
            int[] numbers = { 1, 2, 3 };
            string result = string.Create(numbers.Length * 2 - 1, numbers, (span, nums) =>
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (i > 0)
                    {
                        span[0] = ',';
                        //可以通过Slice让span指向 前移
                        span = span.Slice(1);
                    }
                    if (nums[i].TryFormat(span, out int written))
                    {
                        span = span.Slice(written);
                    }
                }
            });
        }

        private void SpanIsRefStruct()
        {
            //allocate on stack
            Span<int> intSpan = stackalloc int[5];

            //allocate on heap
            Span<int> intSpan2 = new int[5];

            unsafe
            {
                int* memo = (int*)NativeMemory.Alloc(sizeof(int), 16);
            }
        }

        /// <summary>
        /// ref struct 是一种特殊的结构体，它只能在栈上分配内存，并且不能逃逸到堆上。
        /// 可以在 ref struct 中声明 ref 字段，这使得结构体能够直接持有对其他变量的引用，
        /// 就像 System.Span<T> 类型一样，Span<T> 就是一个 ref struct，它通过 ref 字段来管理一段连续的内存区域。
        /// </summary>
        ref struct StructOnStack
        {
            public ref int Age;

            public ref ProductRecord PR;
        }
    }
}
