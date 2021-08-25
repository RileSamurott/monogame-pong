using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Needler.Graphics;

namespace Needler.UI
{
    public class MenuBox: CameraDrawable
    {
        public static Texture2D menus;
        public static Sprite tlCorner;
        public static Sprite trCorner;
        public static Sprite blCorner;
        public static Sprite brCorner;
        public static Sprite top;
        public static Sprite bottom;
        public static Sprite left;
        public static Sprite right;
        public static Sprite blank;
        public int height;
        public int width;
        public int x;
        public int y;
        public bool hidden = false;



        public static void initialize(Texture2D src)
        {
            menus = src;
            tlCorner = new Sprite(src, 0, 0, 5, 5, Color.White);
            trCorner = new Sprite(src, 10, 0, 5, 5, Color.White);
            blCorner = new Sprite(src, 0, 10, 5, 5, Color.White);
            brCorner = new Sprite(src, 10, 10, 5, 5, Color.White);
            top = new Sprite(src, 5, 0, 5, 5, Color.White);
            bottom = new Sprite(src, 5, 10, 5, 5, Color.White);
            left = new Sprite(src, 0, 5, 5, 5, Color.White);
            right = new Sprite(src, 10, 5, 5, 5, Color.White);
            blank = new Sprite(src, 5, 5, 5, 5, Color.White);
        }

        public MenuBox(int x, int y, int width, int height)
        {
            this.height = height;
            this.width = width;
            this.x = x;
            this.y = y;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale) // TODO: Redundant position parameter, need to remove
        {
            if (hidden)
            {
                return;
            }
            tlCorner.Draw(spritebatch, new Vector2(x, y), scale);
            blCorner.Draw(spritebatch, new Vector2(x, y + height - 5*scale), scale);
            top.Draw(spritebatch, new Rectangle((int)(x + 5*scale), y, (int)(width - 10*scale), (int)(5*scale)));
            bottom.Draw(spritebatch, new Rectangle((int)(x + 5*scale), (int)(y + height - 5*scale), (int)(width - 10*scale), (int)(5*scale)));
            trCorner.Draw(spritebatch, new Vector2(x + width - 5*scale, y), scale);
            brCorner.Draw(spritebatch, new Vector2(x + width - 5*scale, y + height - 5*scale), scale);
            left.Draw(spritebatch, new Rectangle(x, (int)(y + 5*scale), (int)(5*scale), (int)(height - 10*scale)));
            right.Draw(spritebatch, new Rectangle((int)(x + width - 5*scale), (int)(y + 5*scale), (int)(5*scale), (int)(height - 10*scale)));
            blank.Draw(spritebatch, new Rectangle((int)(x + 5 * scale), (int)(y + 5 * scale), (int)(width - 10 * scale), (int)(height - 10 * scale)));
        }
    }
}
