using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using The_Legend_Of_Kynortas.Objects;

namespace The_Legend_Of_Kynortas
{
    public static class ObjectCreator
    {
        private static Texture2D woodenBed, woodenWindow, woodenCloset, WoodenCupboard, woodenTable1, woodenTable2;
        private static Texture2D woodenStairs, woodenStairsTop;
        private static Texture2D TEMPORAL;
        public static void Initialize(ContentManager Content)
        {
            woodenBed = Content.Load<Texture2D>("Objects/WoodenBed");
            woodenStairs = Content.Load<Texture2D>("Objects/WoodenStairs");
            woodenWindow = Content.Load<Texture2D>("Objects/WoodenWindow");
            woodenCloset = Content.Load<Texture2D>("Objects/colset1");
            WoodenCupboard = Content.Load<Texture2D>("Objects/cupboard1");
            woodenTable1 = Content.Load<Texture2D>("Objects/table1");
            woodenTable2 = Content.Load<Texture2D>("Objects/table2");
            woodenStairsTop = Content.Load<Texture2D>("Objects/WoodenStairsDown");
            TEMPORAL = Content.Load<Texture2D>("ElderRed");
        }

        public static MapObject CreateWoodenWindow(float i, float j)
        {
            return new MapObject("Wooden Window", woodenWindow , i, j);
        }
        public static MapObject CreateWoodenBed(float i, float j)
        {
            return new Bed("Wooden Bed", woodenBed, true, i, j, new Vector2(0, 0), new Vector2(64, 30));
        }
        public static MapObject CreateWoodenCloset(float i, float j)
        {
            return new MapObject("Wooden Closet", woodenCloset, true, i, j, true, new Vector2(0, 0), new Vector2(55, 30));
        }
        public static MapObject CreateWoodeTable(int index, float i, float j)
        {
            Texture2D txt = woodenTable1;
            if (index == 2) txt = woodenTable2;
            return new MapObject("Wooden Table", txt, true, i, j, false, new Vector2(10, 10), new Vector2(40, 40));
        }
        public static MapObject CreateWoodenCupboard(float i, float j)
        {
            return new MapObject("Wooden Cupboard", WoodenCupboard, true, i, j, true, new Vector2(5, 40), new Vector2(50, 5));
        }
        public static MapObject CreateWoodenStairs(string name, Direction portalDir, TileMap Map, float i, float j, float desti, float destj)
        {
            return new Portal(name, woodenStairs, true, i, j, false, new Vector2(10, 10), new Vector2(40, 40), desti, destj, null, portalDir, Map);
        }
        public static MapObject CreateWoodenStairsTop(string name, Direction portalDir, TileMap Map, float i, float j, float desti, float destj)
        {
            return new Portal(name, woodenStairsTop, true, i, j, false, new Vector2(10, 10), new Vector2(40, 40), desti, destj, null,portalDir, Map);
        }
        public static NPC GET_TEMPORAL_NPC()
        {
            NPC tempNpc = new NPC("Head Of The Couriors", TEMPORAL, new Vector2(100, 100), Direction.Down, new Vector2(22, 0), new Vector2(25, 22));
            tempNpc.SetDialouge(tempNpc.Name + " : this is your home! here you can save the game, eat to regain food or just hang out! I got some work for you, I wrote it down for you in this piece of paper.");
            tempNpc.SetRoutine(tempNpc.Name + " : Hurry up i need this job done fast!");
            return tempNpc;
        }
        

    }
}
