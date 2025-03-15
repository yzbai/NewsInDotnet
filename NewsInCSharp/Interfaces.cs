
namespace NewsInCSharp
{
    public class Interfaces : IRunable
    {
        public void Run()
        {
            StaticAbstractMethodInInterface();
        }

        /// <summary>
        /// Interface can have static abstract method now.
        /// </summary>
        private void StaticAbstractMethodInInterface()
        {
            IHaveStaticMethod.StaticMethodWithImplement();

            HaveStaticMethodClass.StaticAbstractMethod();

            HaveStaticMethodClass.StaticVirtualMethod();

            HaveStaticMethodClass.StaticMethodWithImplement();
        }

        interface IHaveStaticMethod
        {
            static void StaticMethodWithImplement()
            {
                Console.WriteLine("Interface can have static method with Implement.");
            }

            /// <summary>
            /// 必须提供
            /// </summary>
            static abstract void StaticAbstractMethod();

            /// <summary>
            /// 可被覆盖
            /// </summary>
            static virtual void StaticVirtualMethod()
            {
                Console.WriteLine("Static virtual method in Interface.");
            }
        }

        class HaveStaticMethodClass : IHaveStaticMethod
        {
            public static void StaticAbstractMethod()
            {
                Console.WriteLine("Interface can have static abstract method!");
            }

            public static void StaticVirtualMethod()
            {
                //IHaveStaticMethod.StaticVirtualMethod();

                Console.WriteLine("Static virtual method in Implements.");
            }

            public static void StaticMethodWithImplement()
            {
                Console.WriteLine("Static method in Implements.");
            }
        }
    }
}
