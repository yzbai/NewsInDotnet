namespace NewsInCSharp.Accessories
{
    /// <summary>
    /// Record类型，自带
    /// 1. 属性比较重写
    /// 2. 解构方法
    /// 3. 只读
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Price"></param>
    public record ProductRecord(string Name, double Price);
}
