using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Legend_Of_Kynortas
{
    class PlayerHome : TileMap
    {
        public PlayerHome(string name) : base(name)
        {
            //int[,] tmpMap = new int[,] { {5,5,5,0,5,5,5,5},
            //                             {5,3,3,1,3,3,3,5},
            //                             {5,1,1,1,1,1,1,5},
            //                             {5,1,1,1,1,5,5,5},
            //                             {5,1,1,1,1,3,3,5},
            //                             {5,1,1,1,1,1,1,5},
            //                             {5,5,5,5,5,5,5,5}};

            //CreateMap(tmpMap);
            //AddObject(MapObject.CreateWoodenBad(4, 4));
            ////AddObject(new MapObject("Table", Content.Load<Texture2D>("Objects/table2"), true, 4, 2, false, new Vector2(10, 10), new Vector2(40, 40)));
            ////AddObject(new MapObject("cupboard1", Content.Load<Texture2D>("Objects/cupboard1"), true, 1.25f, 1, true, new Vector2(5, 40), new Vector2(50, 5)));
            ////AddObject(new MapObject("colset1", Content.Load<Texture2D>("Objects/colset1"), true, 1.25f, 5, true, new Vector2(0, 0), new Vector2(50, 30)));
            ////AddObject(new MapObject("Window", Content.Load<Texture2D>("Objects/WoodenWindow"), 1, 2));
            ////AddObject(new MapObject("Window", Content.Load<Texture2D>("Objects/WoodenWindow"), 1, 4));
            //AddObject(Portal.CreateWoodenStairs("Stairs to bedroom",2,6,1,6));
        }
    }
}
