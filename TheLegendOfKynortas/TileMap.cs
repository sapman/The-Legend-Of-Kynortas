using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Legend_Of_Kynortas
{
    public class TileMap
    {
        public string Name { get; private set; }
        private Tile[,] tiles;
        private List<Character> NPCS;
        private List<MapObject> objects;

        public TileMap(string name, int[,] map) : this(name)
        {
            CreateMap(map);
        }
        public TileMap(string name)
        {
            this.Name = name;
            objects = new List<MapObject>();
            NPCS = new List<Character>();
        }

        protected void CreateMap(int[,] map)
        {

            tiles = new Tile[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] != 0)
                    {
                        switch (map[i, j])
                        {
                            case 1: tiles[i, j] = Tile.CreateWoodenFloor(i, j); break;
                            case 2: tiles[i, j] = Tile.CreateWoodenWallUpDown(i, j); break;
                            case 3: tiles[i, j] = Tile.CreateWoodenWallDown(i, j); break;
                            case 4: tiles[i, j] = Tile.CreateWoodenWallFull(i, j); break;
                            case 5: tiles[i, j] = Tile.CreateWoodenWallTop(i, j); break;
                            //case 5: tiles[i, j] = Tile.CreateWoodenWallTopHorizontal(i, j); break;
                            //case 6: tiles[i, j] = Tile.CreateWoodenWallTopVertical(i, j); break;
                            //case 7: tiles[i, j] = Tile.CreateWoodenWallTopCornerTopLeft(i, j); break;
                            //case 8: tiles[i, j] = Tile.CreateWoodenWallTopCornerTopRight(i, j); break;
                            //case 9: tiles[i, j] = Tile.CreateWoodenWallTopCornerBottomLeft(i, j); break;
                            //case 10: tiles[i, j] = Tile.CreateWoodenWallTopCornerBottomRight(i, j); break;
                            default: tiles[i, j] = Tile.CreateWoodenWallTop(i, j);
                                break;
                        }

                    }
                }
            }
        }

        public void GetIndexByPosition(Vector2 position, out int i, out int j)
        {
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            Intersects(rect, out i, out j);
        }
        public bool Intersects(Rectangle rect)
        {
            int i, j;
            return Intersects(rect, out i, out j);
        }
        public bool Intersects(Rectangle rect, out int i, out int j)
        {
            for (int I = 0; I < tiles.GetLength(0); I++)
            {
                for (int J = 0; J < tiles.GetLength(1); J++)
                {
                    if (tiles[I,J] != null  && tiles[I,J].Intersects(rect))
                    {
                        i = I;
                        j = J;
                        return true;
                    }
                }
            }
            i = -1;
            j = -1;

            return IntersectsWithObjects(rect);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var npc in NPCS)
            {
                npc.Update(gameTime);
            }
        }

        public void AddObject(MapObject obj)
        {
            objects.Add(obj);
        }
        public void AddCharacter(Character chara)
        {
            NPCS.Add(chara);
        }
        private bool IntersectsWithObjects(Rectangle col)
        {
            foreach (var obj in objects)
            {
                if (col.Intersects(obj.CollisionRectangle)) return true ;
            }
            return false;
        }

        public MapObject GetObjectByName(string name)
        {
            foreach (var obj in objects)
            {
                if(obj.Name.CompareTo(name) == 0) return obj;
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] != null) tiles[i, j].Draw(spriteBatch);
                }
            }

            foreach (var npc in NPCS)
            {
                npc.DrawByY();
            }
            foreach (var obj in objects)
            {
                obj.Draw(spriteBatch);
            }
        }

        public List<Character> GetNPCs()
        {
            return NPCS;
        }
        public List<MapObject> GetObjects()
        {
            return objects;
        }
    }
}
