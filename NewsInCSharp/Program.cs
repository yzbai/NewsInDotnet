namespace NewsInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //使用SourceGenerator 生成，避免使用Reflect发现类.
            RunableClassCollector.RunableInstances.ForEach(runable => runable.Run());
        }
    }
}
