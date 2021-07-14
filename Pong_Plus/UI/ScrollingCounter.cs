using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Needler.Graphics;
namespace Needler.UI
{
    public class ScrollingCounter
    {
        public bool hasSecondary;
        public int potentialHealth;
        public int potentialSecondary;
        public int currentHealth;
        public int currentSecondary;
        protected int ctrOffsetPrimary;
        protected int ctrOffsetSecondary;

        public const double decreaseSpeedPrimary = 0.012f;
        public const double decreaseSpeedSecondary = 0.012f;
        protected double timer;
        protected double timerSecondary;


        protected static Sprite numScrolling = new Sprite(
            Needler.systemTextures,
            60,
            9,
            7,
            7,
            Color.Black
        );

        protected static Sprite counterSpr = new Sprite(
            Needler.systemTextures,
            0,
            9,
            60,
            35,
            Color.White
        );

        public ScrollingCounter(int potential1, int current1, int potential2, int current2)
        {
            this.potentialHealth = potential1;
            this.currentHealth = current1;
            this.potentialSecondary = potential2;
            this.currentSecondary = current2;

            ctrOffsetPrimary = 0;
            ctrOffsetSecondary = 0;
            timer = decreaseSpeedPrimary;
            timerSecondary = decreaseSpeedSecondary;
        }

        public static int join(int[] number)
        {
            int ret = 0;
            for (int i = 0; i < number.Length; i++)
            {
                ret += number[i] * (int) Math.Pow((double)10, (double)i);
            }
            return ret;
        }

        public int[] getPrimaryYOffsets()
        {
            // From the right side of the number, how many 9s are there?
            string temp = currentHealth.ToString();
            int[] ret;
            int numSigs = temp.Length-1; // Amount of unchanging digits
            for (int i = temp.Length-1; i > 0; i--)
            {
                if (temp[i] == '9')
                    numSigs--;
                else
                    break;
            }
            ret = new int[temp.Length];
            for (int i = 0; i < numSigs; i++)
                ret[i] = 9 + (temp[i] - '0') * 7;
            for (int i = numSigs; i < temp.Length; i++)
                ret[i] = 9 + (temp[i] - '0') * 7 + ctrOffsetPrimary;

            return ret;
        }

        public int[] getSecondaryYOffsets()
        {
            // From the right side of the number, how many 9s are there?
            string temp = currentSecondary.ToString();
            int[] ret;
            int numSigs = temp.Length - 1; // Amount of unchanging digits
            for (int i = temp.Length - 1; i > 0; i--)
            {
                if (temp[i] == '9')
                    numSigs--;
                else
                    break;
            }
            ret = new int[temp.Length];
            for (int i = 0; i < numSigs; i++)
                ret[i] = 9 + (temp[i] - '0') * 7;
            for (int i = numSigs; i < temp.Length; i++)
                ret[i] = 9 + (temp[i] - '0') * 7 + ctrOffsetSecondary;
            return ret;
        }

        public bool Update(GameTime gametime)
        {
            double a = gametime.ElapsedGameTime.TotalSeconds;
            timer -= a;
            timerSecondary -= a;

            if (timer < 0)
            {
                timer = Math.Abs(decreaseSpeedPrimary);
                int temp1 = Math.Sign(currentHealth-potentialHealth);
                shiftPrimaryOffset(temp1 != 0 ? temp1 : Math.Sign(ctrOffsetPrimary));
            }
            if (timerSecondary < 0)
            {
                timerSecondary = Math.Abs(decreaseSpeedSecondary);
                int temp2 = Math.Sign(currentSecondary - potentialSecondary);
                shiftSecondaryOffset(temp2 != 0 ? temp2 : Math.Sign(ctrOffsetSecondary)); 
            }
            return currentHealth <= 0;
        }

        public void shiftPrimaryOffset(int amt)
        {
            
            ctrOffsetPrimary -= amt;
            if (ctrOffsetPrimary < 0)
            {
                ctrOffsetPrimary = 6;
                currentHealth -= 1;
            }
            else if (ctrOffsetPrimary > 6)
            {
                ctrOffsetPrimary = 0;
                currentHealth += 1;
            }
        }

        public void shiftSecondaryOffset(int amt)
        {
            ctrOffsetSecondary -= amt;
            if (ctrOffsetSecondary < 0)
            {
                ctrOffsetSecondary = 6;
                currentSecondary -= 1;
            }
            else if (ctrOffsetSecondary > 6)
            {
                ctrOffsetSecondary = 0;
                currentSecondary += 1;
            }
        }
        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale)
        {
            int[] offsets = getPrimaryYOffsets();
            counterSpr.Draw(spritebatch, position, scale);
            for (int i = 0; i < offsets.Length; i++)
            {
                numScrolling.yOffset = offsets[i];
                numScrolling.Draw(spritebatch, position + (new Vector2(29,13)*scale + new Vector2(8 * scale* (i + 3 - offsets.Length), 0)), scale);
            }
            offsets = getSecondaryYOffsets();
            for(int i = 0; i < offsets.Length; i++)
            {
                numScrolling.yOffset = offsets[i];
                numScrolling.Draw(spritebatch, position + (new Vector2(29, 23)*scale + new Vector2(8 * scale*(i + 3 - offsets.Length), 0)), scale);
            }
        }
    }
}
