using System;
using System.Collections.Concurrent;


namespace Zawodnicy.Library
{
     public class Tracker
    {
        private readonly ConcurrentDictionary<int, Location> _currentPositions = new ConcurrentDictionary<int, Location>();

        public void RegisterPlayer(Player player)
        {
            _currentPositions[player.Id] = player.CurrentLocation;
            player.LocationChanged += OnLocationChange;
        }

        public void OnLocationChange(object sender, LocationEventArgs e)
        {
            _currentPositions[e.PlayerId] = e.NewLocation;
        }

        public Location GetPlayerLocation(int playerId)
        {
            return _currentPositions.TryGetValue(playerId, out var loc) ? loc : null;
        }
    }
}
