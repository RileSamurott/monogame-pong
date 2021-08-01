using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Needler.Graphics;
namespace Needler.UI
{
    public enum FontChars
    {

    }

    public class 
    public class AtlasFont
    {
        private Texture2D source;

        private Sprite srcspr;

        private int maxchrs;

        private char startingChar;

        private int chrHeight;

        private int chrWidth;

        private int sourceHorzChars;

        private int sourceVertChars;

        public AtlasFont(Texture2D source, int chrHeight, int chrWidth, char? startingChar)
        {
            this.source = source;
            this.srcspr = new Sprite(source, 0, 0, source.Width, source.Height, Color.White);
            this.sourceHorzChars = (int)(source.Width / chrWidth);
            this.sourceVertChars = (int)(source.Height / chrHeight);
            this.maxchrs = sourceVertChars * sourceHorzChars;
            this.startingChar = startingChar == null ? 'a' : (char)startingChar;
            this.chrHeight = chrHeight;
            this.chrWidth = chrWidth;
        }

        public void DrawChar(SpriteBatch spritebatch, Vector2 position, float scale, char c)
        {
            int offset = c - startingChar;
            if (offset < 0 || offset >= maxchrs)
            {
                throw new ArgumentOutOfRangeException("Character <" + c + "> is out of the available range of characters for this AtlasFont.");
            }
            srcspr.xOffset = offset % sourceHorzChars;
            srcspr.yOffset = offset / sourceVertChars;
            srcspr.Draw(spritebatch, position, scale);

        }
    }
}
