using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public class CombatCharacter : Character
    {
        Animate Attack, Die, Defence;

        public CombatCharacter(string name, Texture2D walking, Texture2D Attack, Texture2D Die, Texture2D Defence, Vector2 startingPosition, float speed, Direction dir, Vector2 headOffset, Vector2 headSize)
            : base(name, walking, startingPosition, speed, dir, headOffset, headSize)
        {
        }
    }
}
