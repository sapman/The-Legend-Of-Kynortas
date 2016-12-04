using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public class Animate
    {
        Texture2D texture;

        public Point frameSize { get; private set; }
        protected Point numberOfFrames;
        protected int frameX, frameY;
        protected Rectangle source;

        float timer, interval;

        public Direction LookingDirection { get { return (Direction)frameY; } }

        public Animate(Texture2D text, int coloumns, int rows, float interval)
        {
            this.texture = text;
            numberOfFrames = new Point(coloumns, rows);
            frameSize = new Point(text.Width / coloumns,text.Height / rows);
            this.interval = interval;
        }

        public void Update(GameTime gameTime, Direction dir)
        {
            frameY = (int)dir;
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                timer = 0;
                frameX++;
                if (frameX >= numberOfFrames.X)
                    frameX = 0;
            }

            source = new Rectangle(frameX * frameSize.X, frameY * frameSize.Y,
                frameSize.X, frameSize.Y);
        }

        public void Stand(Direction dir)
        {
            frameY = (int)dir;
            frameX = 0;
            source = new Rectangle(frameX * frameSize.X, frameY * frameSize.Y,
                            frameSize.X, frameSize.Y);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 p)
        {
            spriteBatch.Draw(texture, new Rectangle((int)p.X, (int)p.Y, source.Width, source.Height), source,Color.White);
        }


    }
}
