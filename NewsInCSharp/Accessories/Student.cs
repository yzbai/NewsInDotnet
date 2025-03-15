
namespace NewsInCSharp.Accessories
{
    /// <summary>
    /// Student class with 
    /// MainConstructor
    /// DeConstructor
    /// </summary>
    public class Student(string name, int age)
    {
        public string Name { get; } = name;
        public int Age { get; } = age;

        public void Deconstruct(out string name, out int age)
        {
            name = Name;
            age = Age;
        }
    }
}
