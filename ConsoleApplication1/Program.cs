using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Child instance = new Child();
            //instance.Increment();
            //DateTime dt = GetTime();
            //if (dt == null)
            //{
            //    Console.WriteLine("nn");
            //}
            //else
            //{
            //    Console.WriteLine("aa");
            //}
            int[] arr = { 2, 4, 1, 5, 6 };
            int result = arr.Where(n => n % 2 == 0).Sum();
            Console.WriteLine(result);
        }

        private static DateTime GetTime()
        {
            //Omitted
            return new DateTime();
        }
    }

    class Parent
    {
        protected int Count;

        public Parent()
        {
            Count = 0;
        }
    }

    class Child : Parent
    {
        public void Increment()
        {
            Count = Count + 1;
            Console.WriteLine(Count);
        }
    }
}
