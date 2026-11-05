using System;
using System.Diagnostics;
using System.Linq;

namespace Lab05
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Задание 1");
            using (var cur = Process.GetCurrentProcess())
            {
                Console.WriteLine($"Имя: {cur.ProcessName}");
                Console.WriteLine($"PID: {cur.Id}");
                Console.WriteLine($"Старт: {cur.StartTime}");
                Console.WriteLine($"Память: {cur.WorkingSet64 / 1024 / 1024} МБ");
                Console.WriteLine($"Потоков: {cur.Threads.Count}");
            }

            Console.WriteLine("\nТоп-5 по памяти:");
            var top5 = Process.GetProcesses()
                .Select(p => (p, mem: SafeMem(p)))
                .OrderByDescending(x => x.mem)
                .Take(5);
            int i = 1;
            foreach (var (p, mem) in top5)
                Console.WriteLine($"{i++}. {SafeName(p)} — {mem / 1024 / 1024} МБ");

            Console.WriteLine("\nЗадание 2");
            var psi = new ProcessStartInfo("cmd.exe", "/c ping 8.8.8.8 -n 4")
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            using (var p = Process.Start(psi))
            {
                Console.WriteLine(p.StandardOutput.ReadToEnd());
                p.WaitForExit();
            }

            Console.WriteLine("\nЗадание 3");
            var psi3 = new ProcessStartInfo("cmd.exe", "/c timeout /t 60 /nobreak")
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var p = Process.Start(psi3))
            {
                Console.WriteLine($"Процесс запущен (PID {p.Id}), ждём 3 сек...");
                if (!p.WaitForExit(3000))
                {
                    Console.WriteLine("Не завершился — Kill()");
                    p.Kill();
                    p.WaitForExit();
                }
                Console.WriteLine("Готово.");
            }

            Console.WriteLine("\nЗадание 4 ");
            Console.WriteLine("AppDomain использовались для логической изоляции кода внутри процесса:");
            Console.WriteLine("ASP.NET (изоляция сайтов), плагины с возможностью выгрузки.");
            Console.WriteLine("Отличие от процесса: AppDomain — внутри одного процесса, без своего PID.");
            Console.WriteLine("Замена в .NET 5+: AssemblyLoadContext (collectible) и отдельные процессы.");

            Console.ReadKey();
        }

        static long SafeMem(Process p)
        {
            try { return p.WorkingSet64; } catch { return 0; }
        }

        static string SafeName(Process p)
        {
            try { return p.ProcessName; } catch { return "?"; }
        }
    }
}
