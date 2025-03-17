namespace NewsInCSharp.Performances.StackAlloc
{
    class StructRecords : IRunable
    {
        public void Run()
        {
            UseStructRecord();
        }

        private void UseStructRecord()
        {
            throw new NotImplementedException();
        }

        public record struct Point(int X, int Y);
    }
}
