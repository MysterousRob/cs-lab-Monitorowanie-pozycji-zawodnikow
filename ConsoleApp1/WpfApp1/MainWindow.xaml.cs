using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Zawodnicy.Library; 

namespace Zawodnicy.WPFApp
{
    public partial class MainWindow : Window
    {
        private Tracker _tracker = new Tracker();
        private List<Player> _players = new List<Player>();
        private Dictionary<int, Ellipse> _playerEllipses = new Dictionary<int, Ellipse>();
        private Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            SetupSimulation();
        }

        private void SetupSimulation()
        {
            int K = 5;
            double boardWidth = 500;
            double boardHeight = 1000;

            for (int i = 1; i <= K; i++)
            {
                Player player = new Player(i, _random.NextDouble() * boardWidth, _random.NextDouble() * boardHeight, boardWidth, boardHeight);
                _players.Add(player);
                _tracker.RegisterPlayer(player);

                player.LocationChanged += OnPlayerLocationChanged;

                Ellipse ellipse = new Ellipse { Width = 15, Height = 15, Fill = Brushes.Red };
                _playerEllipses[i] = ellipse;
                Field.Children.Add(ellipse); 

                Canvas.SetLeft(ellipse, player.CurrentLocation.X);
                Canvas.SetTop(ellipse, player.CurrentLocation.Y);

                System.Threading.Thread t = new System.Threading.Thread(() =>
                {
                    while (true)
                    {
                        player.ChangeLocation();
                        System.Threading.Thread.Sleep(_random.Next(50, 200));
                    }
                })
                { IsBackground = true };
                t.Start();
            }
        }

        private void OnPlayerLocationChanged(object sender, LocationEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (_playerEllipses.TryGetValue(e.PlayerId, out Ellipse ellipse))
                {
                    Canvas.SetLeft(ellipse, e.NewLocation.X);
                    Canvas.SetTop(ellipse, e.NewLocation.Y);
                }
            });
        }
    }
}