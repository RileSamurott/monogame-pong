using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Needler.Graphics
{
    public class Camera
    {
        public float xOffset;
        public float yOffset;

        public SpriteBatch spritebatch
        {
            get { return spritebatch; }
            set { spritebatch = value; }
        }
        /*
        public void Render(CameraDrawable thing, Vector2 position)
        {
            position.X -= xOffset;
            position.Y -= yOffset;
            thing.Draw(spritebatch, position);
        }
        */

    }
}
