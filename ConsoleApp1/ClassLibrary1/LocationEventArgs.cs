using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawodnicy.Library
{
    public class LocationEventArgs : EventArgs
    {
        public int PlayerId { get; }
        public Location NewLocation { get; }

        public LocationEventArgs(int playerId, Location newLocation)
        {
            PlayerId = playerId;
            NewLocation = newLocation;
        }
    }
}
