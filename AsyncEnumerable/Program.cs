using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncEnumerable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await foreach (int number in GetNumbers())
            {
                WriteLine($"Number: {number}");
            }
        }

        static async IAsyncEnumerable<int> GetNumbers()
        {
            var r = new Random();

            System.Threading.Thread.Sleep(r.Next(1000,2000));
            yield return r.Next(0,100);

            System.Threading.Thread.Sleep(r.Next(500,1000));
            yield return r.Next(101,500);

            System.Threading.Thread.Sleep(r.Next(100,200));
            yield return r.Next(501,1000);
        }
    }
}
