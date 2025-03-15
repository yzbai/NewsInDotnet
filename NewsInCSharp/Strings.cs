
using HandlebarsDotNet;

using NewsInCSharp.Accessories;

using Scriban;

namespace NewsInCSharp
{
    /// <summary>
    /// 各种字符串： https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/tokens/interpolated
    /// </summary>
    public class Strings : IRunable
    {
        public void Run()
        {
            LiteralStringStyle1();
            LiteralStringStyle2();
            VariousStringStyles();
        }

        public void VariousStringStyles()
        {
            //$: 内插 { } 表达式, { }内可以跨行
            //@：原义
            //"""三个以上: 原始
            string str1 = $"Let's learn {string.Join(',', new string[] { "a", "b", "c" })}!";
            string str2 = $"""
                <Html>
                He said "Hello!"
                You said "{str1}"
                </Htm>
                """;

            Console.WriteLine(str1);
            Console.WriteLine(str2);
        }

        /// <summary>
        /// Literal String Style for template using HandleBars.Net
        /// </summary>
        public void LiteralStringStyle1()
        {
            //This can be useful when use a template handler/engine
            string literalStr = """
                <div class="entry">
                  <h1>{{title}}</h1>
                  <div class="body">
                    {{body}}
                  </div>
                </div>
                """;

            //Use HandleBars
            HandlebarsTemplate<object, object> template = Handlebars.Compile(literalStr);

            string rt = template(new { title = "My Title", body = "This is the body" });

            Console.WriteLine(rt);
        }

        /// <summary>
        /// Literal String Style for template using Scriban
        /// </summary>
        private void LiteralStringStyle2()
        {
            string templateLiteral = """
                 <ul id='products'>
                    {{ for product in products }}
                        <li>
                            <h2>{{ product.name }}</h2>
                            Price: {{ product.price }}
                        </li>
                    {{ end }}
                 </ul>
                """;

            Template template = Template.Parse(templateLiteral);
            var products = Mocker.GetRandomProducts();

            string rt = template.Render(new { Products = products });

            Console.WriteLine(rt);
        }
    }
}
