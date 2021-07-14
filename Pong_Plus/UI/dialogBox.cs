using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Needler.Graphics;

namespace Needler.UI
{
    public enum dialogState
    {
        Hidden = 0,
        AwaitingProceed = 1,
        Printing = 2,
        Paused = 3,
        Finish = 4
    }
    public class DialogBox: InteractableUIElement
    {
        public static Texture2D menus;
        public static SpriteFont font;
        private MenuBox mbox;
        private double updateTime;
        private const double updateSpeed = 0.02f;
        private int height;
        private int width;
        private int x;
        private int y;
        public dialogState state;
        public string text;
        private int writeUpTo;
        public bool hidden = true;



        public static void initialize(SpriteFont sprfnt)
        {
            font = sprfnt;
        }
        public DialogBox(int x, int y, int width, int height)
        {
            this.height = height;
            this.width = width;
            this.x = x;
            this.y = y;
            this.mbox = new MenuBox(x, y, width, height);
            text = "";
            writeUpTo = 0;
            this.state = dialogState.Paused;
        }
        public void Update(GameTime gametime, KeyboardState kbstate)
        {
            if (state == dialogState.Printing)
            {
                updateTime -= gametime.ElapsedGameTime.TotalSeconds;
                if (updateTime <= 0)
                {
                    writeUpTo++;
                    if (text[writeUpTo-1] == ' ') { writeUpTo++; }
                    updateTime = updateSpeed;
                }
                if (writeUpTo >= text.Length)
                {
                    state = dialogState.AwaitingProceed;
                }
            }
            ProcessInputs(kbstate);
        }
        public void ProcessInputs(KeyboardState kbstate)
        {
            if (KeyboardManager.HasBeenPressed(Keys.C))
            {
                if (state == dialogState.Printing || state == dialogState.Paused)
                {
                    writeUpTo = text.Length;
                    state = dialogState.AwaitingProceed;
                }
                else if (state == dialogState.AwaitingProceed)
                {
                    text = "";
                    writeUpTo = 0;
                    state = dialogState.Finish;
                }
            }
        }
        public void Write(string text)
        {
            if (state != dialogState.Printing)
            {
                this.text = text;
                this.writeUpTo = 0;
                this.state = dialogState.Printing;
            }
        }
        public void Draw(SpriteBatch spritebatch, float scale)
        {
            if (hidden)
            {
                return;
            }

            mbox.Draw(spritebatch, new Vector2(x, y), scale);

            spritebatch.DrawString(font, text.Substring(0, writeUpTo), new Vector2(x + 5, y + 5), Color.Black);
        }
    }
}
