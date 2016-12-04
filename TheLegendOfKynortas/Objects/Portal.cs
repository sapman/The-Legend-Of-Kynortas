using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using The_Legend_Of_Kynortas.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Legend_Of_Kynortas
{
    public class Portal : UsableObject
    {

        public Portal Destination;

        private float portalI, portalJ;
        private TileMap map;

        public TileMap Map { get { return map; } }
        public float PortalRow { get { return portalI; } }
        public float PortalColumn { get { return portalJ; } }
        public Direction PortalDirection { get; private set; }
        public Portal(string name, Texture2D texture, bool IsCollision, float i, float j, bool DrawByY, Vector2 colOffset, Vector2 colSize, float portalRow, float portalColumn, Portal Destination,Direction portalDir,TileMap Map)
            : base(name, texture, IsCollision, i, j, DrawByY, colOffset, colSize)
        {
            map = Map;
            isDrawName = true;
            portalI = portalRow;
            portalJ = portalColumn;
            PortalDirection = portalDir;
            if (Destination != null)
            {
                this.Destination = Destination;
                this.Destination.Destination = this;
            }
        }

        public override void Use()
        {
            Game1.SetCurrentMap(Destination);
        }

    }
}
