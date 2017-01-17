using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive
{
    public class Program
    {
        private static void StartBackgroundWork()
        {
            Console.WriteLine("Shows use of Start to start on a background thread:");
            var o = Observable.Start(() =>
            {
                Console.WriteLine("From background thread. Does not block main thread.");
                Console.WriteLine("Calculating...");
                Thread.Sleep(3000);
                Console.WriteLine("Background work completed.");
            }).Finally(() =>
            {
                Console.WriteLine("Main thread completed.");
            });
            Console.WriteLine("\r\n\t In Main Thread...\r\n");
            o.Wait();    // Wait for completion of background operation.
            Console.WriteLine("After waiting");
        }

        private static async void ParallelExcution()
        {
            var o = Observable.CombineLatest
            (
                Observable.Start(() =>
                {
                    Console.WriteLine("Executing 1st on Thread: {0} at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
                    return "Result A";
                }),
                Observable.Start(() =>
                {
                    Console.WriteLine("Executing 2st on Thread: {0} at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
                    return "Result B";
                }),
                Observable.Start(() =>
                {
                    Console.WriteLine("Executing 3st on Thread: {0} at {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
                    return "Result C";
                })
            ).Finally(() =>
            {
                Console.WriteLine("Done");
            });
            foreach (string item in await o.FirstAsync())
            {
                Console.WriteLine(item);
            }
        }



        public static void Main(string[] args)
        {
            //StartBackgroundWork();
            ParallelExcution();
        }
    }
}
