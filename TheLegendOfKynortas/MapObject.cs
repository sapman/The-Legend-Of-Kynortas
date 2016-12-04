using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using The_Legend_Of_Kynortas.Objects;

namespace The_Legend_Of_Kynortas
{
    public class MapObject : GameObject
    {

        public string Name { get; protected set; }
        Texture2D texture;
        Rectangle rectangle;
        private bool drawByY = false;
        private bool inQueue = false;
        bool isCollision;
        protected bool usable;
        public MapObject(string name, Texture2D texture, bool IsCollision, float i, float j, bool DrawByY, Vector2 colOffset, Vector2 colSize)
        {
            usable = false;
            isCollision = IsCollision;
            this.texture = texture;
            rectangle = new Rectangle((int)(j * Tile.TileSize.X),(int)(i * Tile.TileSize.Y), Tile.TileSize.X, Tile.TileSize.Y);
            CollisionRectangle = new Rectangle((int)(rectangle.X + colOffset.X), (int)(rectangle.Y + colOffset.Y),
                (int)colSize.X, (int)colSize.Y);
            drawByY = DrawByY;
            this.Position = new Vector2(rectangle.X, rectangle.Y);
            Name = name;
        }
        public MapObject(string name, Texture2D texture, float i, float j)
            : this(name, texture, false, i, j, false, Vector2.Zero, Vector2.Zero)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (inQueue || !drawByY)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
                inQueue = false;
            }
            else
            {
                DrawByY();
                inQueue = true;
            }
        }

        public bool IsUsable { get { return usable; } }
    }
}
