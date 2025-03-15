
using NewsInCSharp.Accessories;

namespace NewsInCSharp
{
    /// <summary>
    /// https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/patterns#list-patterns
    /// </summary>
    public class PatternMatch : IRunable
    {
        public void Run()
        {
            PropertyPatternMatch();
            TuplePatternMatch();
        }

        /// <summary>
        /// Property Pattern Match
        /// </summary>
        private void PropertyPatternMatch()
        {
            Mocker.GetRandomProducts(10)
                .Where(p => p is { Name.Length: > 5, Price: > 10 })
                .ToList()
                .ForEach(Console.WriteLine);
        }

        /// <summary>
        /// is 除了判断，还有赋值的作用
        /// </summary>
        private void TuplePatternMatch()
        {
            Mocker.GetRandomProducts(10)
                //Product is a Record Type, So can be deconstruct automactlly.
                //Others should set a Deconstruct method.
                .Where(p => p is (var name, var price) && name.Length > 5 && price > 10)
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
