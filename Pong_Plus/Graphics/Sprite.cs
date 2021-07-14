using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Needler.Graphics
{
    public class Sprite: CameraDrawable
    {
        public Texture2D source { get; private set; }

        public int sprHeight
        {
            get
            {
                return source.Height;
            }
        }
        public int sprWidth
        {
            get
            {
                return source.Width;
            }
        }

        public int yOffset
        { get; set; }

        public int xOffset
        { get; set; }

        public int height;
        public int width;
        public Color color;

        public Sprite(Texture2D tex, int xOffset, int yOffset, int width, int height, Color color)
        {
            source = tex;
            this.Initialize(xOffset, yOffset, width, height, color);
            
        }

        public void Initialize(int xOffset, int yOffset, int width, int height, Color color)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.height = height;
            this.width = width;
            this.color = color;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale)
        {
            spritebatch.Draw(
                source,
                position,
                new Rectangle(xOffset, yOffset, width, height),
                color,
                0f,
                new Vector2(),
                scale,
                SpriteEffects.None,
                0f
            );
        }
        public void Draw(SpriteBatch spritebatch, Rectangle destination)
        {
            spritebatch.Draw(
                source,
                destination,
                new Rectangle(xOffset, yOffset, width, height),
                Color.White
            );
        }



    }
}
