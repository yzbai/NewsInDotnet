using NewsInCSharp.Accessories;

namespace NewsInCSharp
{
    public class Deconstructions : IRunable
    {
        public void Run()
        {
            TupleDeconstruction();
            ObjectDeConstruction();
            RecordDeConstruction();
            ArrayDeconstruction();

            VariousBrackets();
        }

        private void ArrayDeconstruction()
        {
            int[] numbers = [1, 2, 3];

            _ = numbers is [int x, var y, var z];

            //Error
            //var (x, y, z) = numbers;

            // 列表模式解构, 这是[] ，不是 ( )
            if (numbers is [var a, var b, var c])
            {
                Console.WriteLine($"a={a}, b={b}, c={c}"); // 输出: a=1, b=2, c=3
            }

            // 或者在 switch 表达式中使用
            var result = numbers switch
            {
                [1, 2, 3] => "匹配数组 [1,2,3]",
                [var xx, ..] => $"第一个元素是 {xx}，剩余长度 ≥1",
                _ => "未知"
            };
        }

        private void RecordDeConstruction()
        {
            ProductRecord p = new ProductRecord("X", 1);

            ProductRecord other = p with { Name = "Y" };
            ProductRecord d = p;
        }

        private void VariousBrackets()
        {
            //[ ]的本质是 数组、列表
            //( )的本质是 元组、解构
            //{ }的本质是 对象、属性集

            int[] numbers = [1, 2, 3];
            var person = (Name: "Alice", 30, "Female");
            var person1 = new { Name = "Alice", Age = 30, Gender = "Female" };

        }

        private void ObjectDeConstruction()
        {
            //Student 需要有Deconstruct()方法。
            //否则，Student应该是个Record
            Student student = new Student("Alice", 30);

            var (x, y) = student;
            Console.WriteLine($"Deconstructe Object:  {x}, {y}");

            (var xx, var yy) = student;
            Console.WriteLine($"Deconstructe Object:  {xx}, {yy}");
        }

        private void TupleDeconstruction()
        {
            var person = (Name: "Alice", Age: 30);

            var (x, y) = person;

            Console.WriteLine($"Deconstructe Tuple:  {x}, {y}");
        }
    }
}
