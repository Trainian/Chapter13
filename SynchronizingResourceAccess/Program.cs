using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        static Random r = new Random();
        static string Message;
        static object locker = new object();
        static int Counter;
        static void Main(string[] args)
        {
            WriteLine("Please wait fot the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();

            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);

            Task.WaitAll(new Task[] {a, b}); // Первым будем выполняться любой из методов

            WriteLine();
            WriteLine($"Results: {Message}.");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
            WriteLine($"{Counter} string modifications.");
        }

        static void MethodA()
        {
            // lock (locker)
            // {
            //     for(int i = 0; i < 5; i++)
            //     {
            //         Thread.Sleep(r.Next(2000));
            //         Message += "A";
            //         Write(".");
            //     }
            // }
            try
            {
                Monitor.TryEnter(locker, TimeSpan.FromSeconds(15));

                for(int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "A";
                    Interlocked.Increment(ref Counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }

        static void MethodB()
        {
            // lock (locker)
            // {
            //     for (int i = 0; i < 5; i++)
            //     {
            //         Thread.Sleep(r.Next(2000));
            //         Message += "B";
            //         Write(".");
            //     }
            // }
            try
            {
                Monitor.TryEnter(locker, TimeSpan.FromSeconds(15));

                for(int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "B";
                    Interlocked.Increment(ref Counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }
    }
}
