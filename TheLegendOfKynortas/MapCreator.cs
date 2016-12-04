using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public static class MapCreator
    {
        public static TileMap CreatePlayerHome()
        {
            int[,] tmpMap = new int[,] { {5,5,5,0,5,5,5,5},
                                         {5,3,3,1,3,3,3,5},
                                         {5,1,1,1,1,1,1,5},
                                         {5,1,1,1,1,5,5,5},
                                         {5,1,1,1,1,3,3,5},
                                         {5,1,1,1,1,1,1,5},
                                         {5,5,5,5,5,5,5,5}};

            TileMap map = new TileMap("Player home",tmpMap);
            map.AddObject(ObjectCreator.CreateWoodenWindow(1, 2));
            map.AddObject(ObjectCreator.CreateWoodenWindow(1, 4));
            map.AddObject(ObjectCreator.CreateWoodenCupboard(1.25f,0.75f));
            map.AddObject(ObjectCreator.CreateWoodeTable(1,4,2));
            map.AddObject(ObjectCreator.CreateWoodenStairs("Stairs to bedroom", Direction.Left,map, 2, 6, 2, 5));
            map.AddCharacter(ObjectCreator.GET_TEMPORAL_NPC());
            return map;
        }
        public static TileMap CreatePlayerBedRoom()
        {
            int[,] tmpMap = new int[,] { {5,5,5,5,5,5},
                                         {5,3,3,3,3,5},
                                         {5,1,1,1,1,5},
                                         {5,1,1,1,5,5},
                                         {5,5,5,5,5,0}};

            TileMap map = new TileMap("Player bedroom", tmpMap);
            map.AddObject(ObjectCreator.CreateWoodeTable(1, 3, 0.75f));
            map.AddObject(ObjectCreator.CreateWoodenCloset(1.25f, 3));
            map.AddObject(ObjectCreator.CreateWoodenWindow(1, 2));
            map.AddObject(ObjectCreator.CreateWoodenBed(2, 4));
            map.AddObject(ObjectCreator.CreateWoodenStairsTop("Stairs to living room",Direction.Right, map, 2, 1, 2, 2));

            return map;
        }
    }
}
