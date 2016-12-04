using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public abstract class GameObject : IComparable
    {
        public Vector2 Position;
        public Rectangle CollisionRectangle;
        public abstract void Draw(SpriteBatch spriteBatch);

        public void DrawByY()
        {
            CharacterManager.Add(this);
        }

        public int CompareTo(Object obj)
        {
            return (this.CollisionRectangle.Bottom - ((GameObject)obj).CollisionRectangle.Bottom);
        }
    }
}
