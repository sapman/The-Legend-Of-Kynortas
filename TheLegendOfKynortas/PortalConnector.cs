using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Legend_Of_Kynortas
{
    public static class PortalConnector
    {
        public static void ConnectAll()
        {
            Connect("Player home", "Stairs to bedroom", "Player bedroom", "Stairs to living room");
        }

        private static void Connect(string map1Name, string portal1Name, string map2Name, string portal2Name)
        {
            TileMap map1 = GetMapByName(map1Name), map2 = GetMapByName(map2Name);
            Portal p1 = (Portal)map1.GetObjectByName(portal1Name), p2 = (Portal)map2.GetObjectByName(portal2Name);

            p1.Destination = p2;
            p2.Destination = p1;
        }

        public static TileMap GetMapByName(string name)
        {
            foreach (var map in Game1.maps)
            {
                if (map.Name.CompareTo(name) == 0) return map;
            }
            return null;
        }
    }
}
