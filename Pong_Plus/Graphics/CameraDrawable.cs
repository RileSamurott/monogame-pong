using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Needler.Graphics
{
    public interface CameraDrawable
    {
        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale);
    }
}
