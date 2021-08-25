using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Needler.Graphics;
using System.Collections.Generic;
namespace Needler.UI
{
    public enum dtState
    {
        alive = 1,
        dead = 0
    }
    public class DamageText
    {
        public static Sprite nums;
        private int number;
        private Vector2 velocity;
        private int life;
        public static int destroyX;
        public static int destroyY;
        private Vector2 position;
        private bool special;
        public dtState state;
        private int xOffset;
        private Color color;
        private static Dictionary<char, int> digits = new Dictionary<char, int>(){
            {'0',0},
            {'1',1},
            {'2',2},
            {'3',3},
            {'4',4},
            {'5',5},
            {'6',6},
            {'7',7},
            {'8',8},
            {'9',9}
        };

        public static void initialize(Texture2D src, int xLimit, int yLimit)
        {
            nums = new Sprite(src, 0, 0, 9, 9, Color.White);
            destroyX = xLimit;
            destroyY = yLimit;

        }
        public DamageText(int number, Vector2 position, Color color, Vector2 velocity, int life)
        {
            this.velocity = velocity;
            this.position = position;
            this.number = number;
            this.life = life;
            special = false;
            state = dtState.alive;
            this.xOffset = number.ToString().Length;
            this.color = color;
        }
        public DamageText(int number, Vector2 position, Color color, Vector2 velocity)
        {
            this.velocity = velocity;
            this.position = position;
            this.number = number;
            this.life = -1;
            special = false;
            state = dtState.alive;
            this.xOffset = number.ToString().Length;
            this.color = color;
        }
        public DamageText(int number, Vector2 position, Color color) { // Special Damage
            this.velocity = new Vector2(0,-6);
            this.position = position;
            this.number = number;
            this.life = 60;
            special = true;
            state = dtState.alive;
            this.xOffset = number.ToString().Length;
            this.color = color;
        }
        public bool Update(GameTime gameTime)
        {
            life--;
            position += velocity;
            if (special)
            {
                velocity /= 1.1f;
            }
            else
            {
                velocity.Y += 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (life == 0 || position.Y > destroyY)
            {
                state = dtState.dead;
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spritebatch, float scale)
        {
            nums.color = this.color;
            string dgts = number.ToString();
            for (int i = 0; i < dgts.Length; i++)
            {
                nums.xOffset = digits[dgts[i]] * 9;
                nums.Draw(spritebatch, position + new Vector2((2 * i - dgts.Length) * 4 * scale, 0), scale);
            }
        }
    }
}
