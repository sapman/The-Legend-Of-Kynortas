using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using The_Legend_Of_Kynortas.Objects;

namespace The_Legend_Of_Kynortas
{
    public enum Direction
    {
        Down = 0, Left = 1, Right = 2, Up = 3
    };

    public class Character : GameObject
    {
        protected Animate Walking;
        protected Animate CurrentAnimation;

        public string Name { get; protected set; }
        public float Speed { get; protected set; }

        public Direction LookingDirection { get; protected set; }

        public Vector2 HeadOffset, HeadSize;

        public Character(string name, Texture2D walking, Vector2 startingPosition, float speed, Direction dir, Vector2 headOffset, Vector2 headSize)
        {
            Name = name;
            Speed = speed;
            Position = startingPosition;
            Walking = new Animate(walking, 4, 4, 175);
            CurrentAnimation = Walking;

            HeadOffset = headOffset;
            HeadSize = headSize;
        }

        public Rectangle BuildCollisionRectangle()
        {
            CollisionRectangle = new Rectangle((int)(Position.X + HeadOffset.X), (int)(Position.Y + HeadOffset.Y),
                (int)HeadSize.X, (int)HeadSize.Y);
            return CollisionRectangle;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                movement.Y += (float)(Speed);
                LookingDirection = Direction.Down;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movement.X -= (float)(Speed );
                LookingDirection = Direction.Left;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movement.X += (float)(Speed);
                LookingDirection = Direction.Right;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                movement.Y -= (float)(Speed);
                LookingDirection = Direction.Up;
            }

            movement = BuildMovement(movement);

            Position += movement;
            if (movement != Vector2.Zero)
            {
                CurrentAnimation.Update(gameTime, LookingDirection);
                if (movement.X > 0) LookingDirection = Direction.Right;
                else if (movement.X < 0) LookingDirection = Direction.Left;
                else if (movement.Y > 0) LookingDirection = Direction.Down;
                else if (movement.Y < 0) LookingDirection = Direction.Up;
            }
            else CurrentAnimation.Stand(LookingDirection);
        }

        public void UseNearest()
        {
            //npc.LookAt(this.Position, LookingDirection);
            SortByDistance(Game1.currentMap);
            int objIndex = 0, npcIndex = 0;
            while (objIndex != Game1.currentMap.GetObjects().Count || npcIndex != Game1.currentMap.GetNPCs().Count)
            {
                if (objIndex != Game1.currentMap.GetObjects().Count && (npcIndex == Game1.currentMap.GetNPCs().Count || Vector2.Distance(Game1.currentMap.GetObjects()[objIndex].Position, this.Position) < Vector2.Distance(Game1.currentMap.GetNPCs()[npcIndex].Position, this.Position)))
                {
                    if (Game1.currentMap.GetObjects()[objIndex].IsUsable &&
                        ((UsableObject)Game1.currentMap.GetObjects()[objIndex]).LookAt(Position, this.LookingDirection))
                    {
                        ((UsableObject)Game1.currentMap.GetObjects()[objIndex]).Use();
                        break;
                    }
                    objIndex++;
                }
                else
                {
                    if (Game1.currentMap.GetNPCs()[npcIndex] is NPC && ((NPC)Game1.currentMap.GetNPCs()[npcIndex]).LookAt(Position, this.LookingDirection))
                    {
                        Game1.currentSpeaker = (NPC)Game1.currentMap.GetNPCs()[objIndex];
                        Game1.state = GameState.Dialog;
                        break;
                    }
                    npcIndex++;
                }
            }
            //foreach (var np in Game1.check)
            //{
            //    if (np.LookAt(this.Position, LookingDirection))
            //    {
            //        Game1.currentSpeaker = np;
            //        Game1.state = GameState.Dialog;
            //        break;
            //    }
            //    //else if (Vector2.Distance(this.Position, np.Position) > ) break;
            //}
        }

        public void CheckIfCanLook()
        {
            SortByDistance(Game1.currentMap);
            int objIndex = 0, npcIndex = 0;
            while (objIndex != Game1.currentMap.GetObjects().Count || npcIndex != Game1.currentMap.GetNPCs().Count)
            {
                if (objIndex != Game1.currentMap.GetObjects().Count && (npcIndex == Game1.currentMap.GetNPCs().Count || Vector2.Distance(Game1.currentMap.GetObjects()[objIndex].Position, this.Position) < Vector2.Distance(Game1.currentMap.GetNPCs()[npcIndex].Position, this.Position)))
                {
                    if (Game1.currentMap.GetObjects()[objIndex].IsUsable &&
                        ((UsableObject)Game1.currentMap.GetObjects()[objIndex]).CheckLookAt(Position, this.LookingDirection))
                    {
                        break;
                    }
                    objIndex++;
                }
                else
                {
                    if (Game1.currentMap.GetNPCs()[npcIndex] is NPC && ((NPC)Game1.currentMap.GetNPCs()[npcIndex]).CheckLookAt(Position, this.LookingDirection))
                    {
                        break;
                    }
                    npcIndex++;
                }
            }
        }

        public void SortByDistance(TileMap map)
        {
            Character temp;

            for (int write = 0; write < map.GetNPCs().Count; write++)
            {
                for (int sort = 0; sort < map.GetNPCs().Count - 1; sort++)
                {
                    if (Vector2.Distance(this.Position, map.GetNPCs()[sort].Position) < Vector2.Distance(this.Position, map.GetNPCs()[sort + 1].Position))
                    {
                        temp = map.GetNPCs()[sort + 1];
                        map.GetNPCs()[sort + 1] = map.GetNPCs()[sort];
                        map.GetNPCs()[sort] = temp;
                    }
                }
            }

            MapObject obj;
            for (int write = 0; write < map.GetObjects().Count; write++)
            {
                for (int sort = 0; sort < map.GetObjects().Count - 1; sort++)
                {
                    if (Vector2.Distance(this.Position, map.GetObjects()[sort].Position) < Vector2.Distance(this.Position, map.GetObjects()[sort + 1].Position))
                    {
                        obj = map.GetObjects()[sort + 1];
                        map.GetObjects()[sort + 1] = map.GetObjects()[sort];
                        map.GetObjects()[sort] = obj;
                    }
                }
            }
        }

        public Vector2 BuildMovement(Vector2 movement)
        {
            Rectangle nextCol = BuildCollisionRectangle();
            nextCol.X += (int)movement.X;

            if (movement.X != 0 && Intersects(nextCol))
                movement.X = 0;

            nextCol = BuildCollisionRectangle();
            nextCol.Y += (int)movement.Y;

            if (movement.Y != 0 && Intersects(nextCol))
                movement.Y = 0;

            return movement;
        }

        public bool Intersects(Rectangle col)
        {
            foreach (var npc in Game1.currentMap.GetNPCs())
            {
                if (npc.CollisionRectangle.Intersects(col)) return true;
            }
            col.Width = 16;
            col.Height = 64;
            if (Game1.currentMap.Intersects(col)) return true;
            return false;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch, Position);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
