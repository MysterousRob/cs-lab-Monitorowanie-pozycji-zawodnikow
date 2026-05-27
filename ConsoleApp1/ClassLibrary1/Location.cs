using System;


namespace Zawodnicy.Library
{
    public class Location
    {
        public double X { get; }
        public double Y { get; }
        public DateTime Timestamp { get; }

        public Location(double x, double y)
        {
            X = x;
            Y = y;
            Timestamp = DateTime.Now;
        }
    }
}
