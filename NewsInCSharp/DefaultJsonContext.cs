using System.Text.Json.Serialization;

using NewsInCSharp.Accessories;

namespace NewsInCSharp
{
    /// <summary>
    /// 使用System.Text.Json 的 SourceGenerator。 避免使用Reflect，影响NativeAOT
    /// </summary>
    [JsonSerializable(typeof(Student))]
    public partial class DefaultJsonContext : JsonSerializerContext
    {

    }
}
