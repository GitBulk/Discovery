using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var test = new BaseTest();
            test.ProtoBufTest();
            Console.WriteLine("Enter text...");
            Console.ReadLine();

        }
    }
}
