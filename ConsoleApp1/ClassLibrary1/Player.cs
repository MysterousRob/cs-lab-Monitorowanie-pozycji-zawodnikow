using System;


namespace Zawodnicy.Library
{
    public class Player
    {
        private readonly Random _random = new Random();
        private readonly double _boardWidth;
        private readonly double _boardHeight;

        private int id;
        private double distance;

        public int Id => id;
        public double Distance => distance;
        public Location CurrentLocation { get; private set; }

        public event EventHandler<LocationEventArgs> LocationChanged;

        public Player(int id, double initialX, double initialY, double boardWidth, double boardHeight)
        {
            this.id = id;
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            CurrentLocation = new Location(initialX, initialY);
            distance = 0;
        }

        public void ChangeLocation()
        {
            if (_random.Next(0, 100) < 15)
            {
                return;
            }

            double moveX = (_random.NextDouble() * 30) - 15;
            double moveY = (_random.NextDouble() * 30) - 15;

            double newX = CurrentLocation.X + moveX;
            double newY = CurrentLocation.Y + moveY;

            newX = Math.Clamp(newX, 0, _boardWidth);
            newY = Math.Clamp(newY, 0, _boardHeight);

            double actualDeltaX = newX - CurrentLocation.X;
            double actualDeltaY = newY - CurrentLocation.Y;
            double deltaDistance = Math.Sqrt(actualDeltaX * actualDeltaX + actualDeltaY * actualDeltaY);

            distance += deltaDistance;

            CurrentLocation = new Location(newX, newY);

            OnLocationChanged(new LocationEventArgs(id, CurrentLocation));
        }

        protected virtual void OnLocationChanged(LocationEventArgs e)
        {
            LocationChanged?.Invoke(this, e);
        }

    }
}
