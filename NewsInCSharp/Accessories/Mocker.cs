namespace NewsInCSharp.Accessories
{
    public static class Mocker
    {
        public static IEnumerable<ProductRecord> GetRandomProducts(int count = 5)
        {
            return Enumerable.Range(0, count)
                .Select(_ => new ProductRecord(Utils.GetRandomWords(), Utils.GetRandomPrice()));
        }
    }
}
