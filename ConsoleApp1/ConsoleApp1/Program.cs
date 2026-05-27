using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Zawodnicy.Library;

namespace Zawodnicy.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SYMULATOR MONITOROWANIA ZAWODNIKÓW ===");

            int K = 5;                        
            double boardWidth = 500;          
            double boardHeight = 1000;        
            int matchDuration = 5000;         

            Tracker tracker = new Tracker();
            List<Player> players = new List<Player>();
            Random random = new Random();

            for (int i = 1; i <= K; i++)
            {
                double startX = random.NextDouble() * boardWidth;
                double startY = random.NextDouble() * boardHeight;

                Player player = new Player(i, startX, startY, boardWidth, boardHeight);
                players.Add(player);
                tracker.RegisterPlayer(player);
            }

            Console.WriteLine($"Zarejestrowano {K} zawodników w systemie monitoringu.");
            Console.WriteLine("Uruchamianie symulacji ruchu w oddzielnych wątkach...");

            bool isRunning = true;
            List<Thread> threads = new List<Thread>();

            foreach (Player player in players)
            {
                Thread playerThread = new Thread(() =>
                {
                    while (isRunning)
                    {
                        player.ChangeLocation();

                        int sleepTime = random.Next(30, 150);
                        Thread.Sleep(sleepTime);
                    }
                });

                threads.Add(playerThread);
                playerThread.Start();
            }

            Thread.Sleep(matchDuration);

            isRunning = false;

            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.WriteLine("\n=== WYNIKI KOŃCOWE SYMULACJI ===");
            foreach (Player player in players)
            {
                Console.WriteLine($"Zawodnik ID: {player.Id} | Pokonany dystans: {player.Distance:F2} m | Pozycja końcowa: [{player.CurrentLocation.X:F2}; {player.CurrentLocation.Y:F2}]");
            }

            Player maxPlayer = players.OrderByDescending(p => p.Distance).First();

            Console.WriteLine("\n------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Zwycięzca: Zawodnik {maxPlayer.Id} pokonał największy dystans: {maxPlayer.Distance:F2} jednostek!");
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć program...");
            Console.ReadKey();
        }
    }
}