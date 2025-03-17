using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http.Json;

namespace NewsInAspnet;

public class Program
{
    /// <summary>
    /// 1. NativeAOT
    ///     - Use System.Text.Json SourceGenerator
    /// 2. HybridCache ?
    /// </summary>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });

        //builder.Services.ConfigureHttpJsonOptions(options =>
        //{
        //    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        //});

        var app = builder.Build()
            .AddTodoApis();

        app.Run();
    }
}

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
