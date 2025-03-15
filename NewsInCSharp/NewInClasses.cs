using System.Text.Json;

using NewsInCSharp.Accessories;

namespace NewsInCSharp
{
    public class NewInClasses : IRunable
    {
        public void Run()
        {
            MainConstructor();
        }

        /// <summary>
        /// 主构造函数
        /// </summary>
        private void MainConstructor()
        {
            Student student = new Student("John", 10);

            string json = JsonSerializer.Serialize(student, DefaultJsonContext.Default.Student);

            Console.WriteLine(json);
        }
    }
}
