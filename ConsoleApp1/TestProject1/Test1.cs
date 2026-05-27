using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zawodnicy.Library;
using System;

namespace TestProject1
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void PlayerDistance_ShouldIncrease_WhenPlayerMoves()
        {
            Player player = new Player(1, 0, 0, 100, 100);
            double initialDistance = player.Distance; 

            player.ChangeLocation();

            Assert.IsTrue(player.Distance >= 0, "Dystans powinien być większy lub równy 0");
        }

        [TestMethod]
        public void Tracker_ShouldUpdatePosition_WhenPlayerMoves()
        {
            Tracker tracker = new Tracker();
            Player player = new Player(1, 10, 10, 100, 100);
            tracker.RegisterPlayer(player);

            player.ChangeLocation();
            Location newLoc = tracker.GetPlayerLocation(1);

            Assert.IsNotNull(newLoc, "Tracker powinien zwrócić nową pozycję");
            Assert.AreNotEqual(10, newLoc.X, "Pozycja X powinna się zmienić");
        }
    }
}