using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Legend_Of_Kynortas.Objects
{
    class Bed : UsableObject
    {
        public Bed(string name, Texture2D texture, bool IsCollision, float i, float j, Vector2 colOffset, Vector2 colSize)
            : base(name, texture, IsCollision, i, j, false, colOffset, colSize)
        {
            isDrawName = true;
        }
        public override void Use()
        {
            Game1.state = GameState.Dialog;
            Game1.currentDialog = "Game Saved! :]";
            Game1.currentSpeaker = null;
        }
    }
}
