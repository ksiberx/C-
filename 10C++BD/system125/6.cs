using System;
using System.Threading;

class Program
{
    private static AutoResetEvent event1 = new AutoResetEvent(true);
    private static AutoResetEvent event2 = new AutoResetEvent(false);

    static void OddNumbers()
    {
        for (int i = 1; i <= 9; i += 2)
        {
            event1.WaitOne();
            Console.WriteLine(i);

            event2.Set();
        }
    }

    static void EvenNumbers()
    {
        for (int i = 2; i <= 10; i += 2)
        {
            event2.WaitOne();
            Console.WriteLine(i);

            event1.Set();
        }
    }

    static void Main()
    {
        Thread odd = new Thread(OddNumbers);
        Thread even = new Thread(EvenNumbers);

        odd.Start();
        even.Start();

        odd.Join();
        even.Join();

        event1.Dispose();
        event2.Dispose();

        Console.ReadKey();
    }
}