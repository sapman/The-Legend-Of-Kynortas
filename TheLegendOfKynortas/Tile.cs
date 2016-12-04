using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public class Tile : GameObject
    {
        private static Texture2D woodenFloor;
        private static Texture2D woodenWall;
        private static Texture2D woodenWallTop;
        private static Texture2D woodenWallTopHorizontal;
        private static Texture2D woodenWallTopVertical;
        private static Texture2D woodenWallTopCornerTopLeft;
        private static Texture2D woodenWallTopCornerTopRight;
        private static Texture2D woodenWallTopCornerBottomLeft;
        private static Texture2D woodenWallTopCornerBottomRight;

        public static Point TileSize = new Point(64, 64);

        Texture2D texture;
        Rectangle rectangle;
        public bool Standable { get; private set; }
        private bool inQueue = false;
        private bool drawByY = false;
        private Tile(Texture2D tileTexture, bool standable, int i, int j, bool drawByY, Vector2 collisionOffset, Vector2 collisionSize)
        {
            this.drawByY = drawByY;
            texture = tileTexture;
            Standable = standable;
            rectangle = new Rectangle(j * TileSize.X, i * TileSize.Y, TileSize.X,TileSize.Y);

            if (!standable)
            {
                if (collisionSize == Vector2.Zero) CollisionRectangle = rectangle;
                else
                {
                    CollisionRectangle = new Rectangle(rectangle.X + (int)collisionOffset.X, rectangle.Y + (int)collisionOffset.Y,
                        (int)collisionSize.X, (int)collisionSize.Y);
                }
            }
            else CollisionRectangle = Rectangle.Empty;
        }

        private Tile(Texture2D tileTexture, bool standable, int i, int j)
            : this(tileTexture, standable, i, j, false , Vector2.Zero, Vector2.Zero)
        {

        }

        public static void Initialize(ContentManager Content)
        {
            #region Wooden Tiles
            woodenFloor = Content.Load<Texture2D>("Tiles/WoodenFloor2");
            woodenWall = Content.Load<Texture2D>("Tiles/WoodenWall2");
            woodenWallTop = Content.Load<Texture2D>("Tiles/WoodenWallTop2");
            woodenWallTopHorizontal = Content.Load<Texture2D>("Tiles/WoodenWallTopHorizontal");
            woodenWallTopVertical = Content.Load<Texture2D>("Tiles/WoodenWallTopVertical");
            woodenWallTopCornerTopLeft = Content.Load<Texture2D>("Tiles/WoodenWallTopCornerTopLeft");
            woodenWallTopCornerTopRight = Content.Load<Texture2D>("Tiles/WoodenWallTopCornerTopRight");
            woodenWallTopCornerBottomLeft = Content.Load<Texture2D>("Tiles/WoodenWallTopCornerBottomLeft");
            woodenWallTopCornerBottomRight = Content.Load<Texture2D>("Tiles/WoodenWallTopCornerBottomRight");
            #endregion
        }

        public static Tile CreateWoodenFloor(int i, int j)
        {
            return new Tile(woodenFloor, true, i, j);
        }
        public static Tile CreateWoodenWallUpDown(int i, int j)
        {
            return new Tile(woodenWall, false, i, j,true , new Vector2(-10, 20), new Vector2(79, 12));
        }
        public static Tile CreateWoodenWallDown(int i, int j)
        {
            return new Tile(woodenWall, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 32));
        }
        public static Tile CreateWoodenWallFull(int i, int j)
        {
            return new Tile(woodenWall, false, i, j,false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTop(int i, int j)
        {
            return new Tile(woodenWallTop, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTopHorizontal(int i, int j)
        {
            return new Tile(woodenWallTopHorizontal, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTopVertical(int i, int j)
        {
            return new Tile(woodenWallTopVertical, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTopCornerTopLeft(int i, int j)
        {
            return new Tile(woodenWallTopCornerTopLeft, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTopCornerTopRight(int i, int j)
        {
            return new Tile(woodenWallTopCornerTopRight, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTopCornerBottomLeft(int i, int j)
        {
            return new Tile(woodenWallTopCornerBottomLeft, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }
        public static Tile CreateWoodenWallTopCornerBottomRight(int i, int j)
        {
            return new Tile(woodenWallTopCornerBottomRight, false, i, j, false, new Vector2(-10, 0), new Vector2(79, 64));
        }

        public bool Intersects(Rectangle rect)
        {
            return CollisionRectangle.Intersects(rect) && !Standable;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (inQueue || !drawByY)
            {
                inQueue = false;
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
            else
            {
                inQueue = true;
                DrawByY();
            }
        }
    }
}
