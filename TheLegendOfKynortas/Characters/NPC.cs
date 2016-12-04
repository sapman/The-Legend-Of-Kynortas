using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public enum NPCWalkingStyle    {
        Circling, Patrol, Standing 
    }
    public class NPC : Character
    {
        public static float NPC_SPEED = 0.1f; 

        string routinesSpeak;
        string dialogue;

        private bool isDrawName = false;

        public NPC(string name, Texture2D walking, Vector2 startingPosition, Direction dir, Vector2 headOffset, Vector2 headSize)
            : base(name, walking, startingPosition, NPC_SPEED, dir, headOffset, headSize)
        {
            dialogue = "";
            routinesSpeak = "";
        }

        public bool CheckLookAt(Vector2 lookAtPosition, Direction playerDir)
        {
            if (Vector2.Distance(lookAtPosition, Position) < 64)
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

        public bool LookAt(Vector2 lookAtPosition, Direction playerDir)
        {
            if (Vector2.Distance(lookAtPosition, Position) < 64)
            {
                Vector2 directionVector = lookAtPosition - Position;

                if (Math.Abs(directionVector.X) > Math.Abs(directionVector.Y))
                {
                    //Look at x axis
                    if (directionVector.X > 0 && playerDir == Direction.Left)
                    {
                        LookingDirection = Direction.Right;
                        return true;
                    }
                    else if (playerDir == Direction.Right)
                    {
                        LookingDirection = Direction.Left;
                        return true;
                    }
                }
                else
                {
                    //Look at y axis
                    if (directionVector.Y > 0 && playerDir == Direction.Up)
                    {
                        LookingDirection = Direction.Down;
                        return true;
                    }
                    else if (playerDir == Direction.Down)
                    {
                        LookingDirection = Direction.Up;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            BuildCollisionRectangle();
            CurrentAnimation.Stand(LookingDirection);
        }

        public bool HasDialogue { get { return dialogue == null || dialogue == ""; } }

        public string Speak()
        {
            if (dialogue != "")
            {
                string st = dialogue;
                dialogue = "";
                return st;
            }
            return routinesSpeak;
        }
        public void SetDialouge(string msg)
        {
            dialogue = msg;
        }
        public void SetRoutine(string msg)
        {
            routinesSpeak = msg;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isDrawName)
            {
                DrawName(spriteBatch);
                isDrawName = false;
            }
            base.Draw(spriteBatch);
        }

        private void DrawName(SpriteBatch spriteBatch)
        {
            int margins = 10;
            Vector2 textSize = Game1.font12.MeasureString(Name);
            Vector2 pos = new Vector2(CollisionRectangle.Center.X - textSize.X / 2, CollisionRectangle.Center.Y - textSize.Y);
            spriteBatch.Draw(Game1.boxTexture, new Rectangle((int)(pos.X - margins * 1.25), (int)(pos.Y - margins * 1.25), (int)(textSize.X + 2.75 * margins), (int)(textSize.Y / 2 + 1.75 * margins)), Color.Black);
            spriteBatch.Draw(Game1.boxTexture, new Rectangle((int)(pos.X - margins), (int)pos.Y - margins, (int)textSize.X + 2 * margins, (int)textSize.Y /2  + margins), Color.White);
            
            spriteBatch.DrawString(Game1.font12, Name, pos - Vector2.UnitY * textSize.Y / 2, Color.Black);
        }
    }
}
