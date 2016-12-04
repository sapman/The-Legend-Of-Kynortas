using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas.Objects
{
    public abstract class UsableObject : MapObject
    {
        protected bool isDrawName = false;
        protected bool drawName = false;
        public UsableObject(string name, Texture2D texture, bool IsCollision, float i, float j, bool DrawByY, Vector2 colOffset, Vector2 colSize)
            : base(name, texture, IsCollision, i, j, DrawByY, colOffset, colSize)
        {
            usable = true;
        }

        public bool CheckLookAt(Vector2 lookAtPosition, Direction playerDir)
        {
            if (Vector2.Distance(lookAtPosition, Position) < 64 * 1.5f)
            {
                Vector2 directionVector = lookAtPosition - Position;

                if (Math.Abs(directionVector.X) > Math.Abs(directionVector.Y))
                {
                    //Look at x axis
                    if (directionVector.X > 0 && playerDir == Direction.Left)
                    {
                        drawName = true;
                        return true;
                    }
                    else if (directionVector.X < 0 && playerDir == Direction.Right)
                    {
                        drawName = true;
                        return true;
                    }
                }
                else
                {
                    //Look at y axis
                    if (directionVector.Y > 0 && playerDir == Direction.Up)
                    {
                        drawName = true;
                        return true;
                    }
                    else if (directionVector.Y < 0 && playerDir == Direction.Down)
                    {
                        drawName = true;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool LookAt(Vector2 lookAtPosition, Direction playerDir)
        {
            if (Vector2.Distance(lookAtPosition, Position ) < 64 * 1.5f)
            {
                Vector2 directionVector = lookAtPosition - Position;

                if (Math.Abs(directionVector.X) > Math.Abs(directionVector.Y))
                {
                    //Look at x axis
                    if (directionVector.X > 0 && playerDir == Direction.Left)
                    {
                        isDrawName = true;
                        return true;
                    }
                    else if (directionVector.X < 0 && playerDir == Direction.Right)
                    {
                        isDrawName = true;
                        return true;
                    }
                }
                else
                {
                    //Look at y axis
                    if (directionVector.Y > 0 && playerDir == Direction.Up)
                    {
                        isDrawName = true;
                        return true;
                    }
                    else if (directionVector.Y < 0 && playerDir == Direction.Down)
                    {
                        isDrawName = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract void Use();
        public void DrawName(SpriteBatch spriteBatch)
        {
            drawName = false;
            int margins = 10;
            Vector2 textSize = Game1.font12.MeasureString(Name);
            Vector2 pos = new Vector2(CollisionRectangle.Center.X - textSize.X / 2, CollisionRectangle.Top - textSize.Y);
            spriteBatch.Draw(Game1.boxTexture, new Rectangle((int)(pos.X - margins * 1.25), (int)(pos.Y - margins * 1.25), (int)(textSize.X + 2.75 * margins), (int)(textSize.Y / 2 + 1.75 * margins)), Color.Black);
            spriteBatch.Draw(Game1.boxTexture, new Rectangle((int)(pos.X - margins), (int)pos.Y - margins, (int)textSize.X + 2 * margins, (int)textSize.Y / 2 + margins), Color.White);

            spriteBatch.DrawString(Game1.font12, Name, pos - Vector2.UnitY * textSize.Y / 2, Color.Black);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (isDrawName && drawName) CharacterManager.DrawNameAtTop(this);
        }
    }
}
