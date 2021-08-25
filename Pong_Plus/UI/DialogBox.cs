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
    public class DialogBox: InteractableUIElement, MenuAction
    {
        public static Texture2D menus;
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
        public MenuManager caller;
        private bool autoclose;

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
            this.autoclose = false;
        }

        public DialogBox(int x, int y, int width, int height, bool autoclose)
        {
            this.height = height;
            this.width = width;
            this.x = x;
            this.y = y;
            this.mbox = new MenuBox(x, y, width, height);
            text = "";
            writeUpTo = 0;
            this.state = dialogState.Paused;
            this.autoclose = autoclose;
        }

        public void Update(GameTime gametime, KeyboardState kbstate)
        {
            if (state == dialogState.Printing)
            {
                updateTime -= gametime.ElapsedGameTime.TotalSeconds;
                if (writeUpTo >= text.Length)
                {
                    state = dialogState.AwaitingProceed;
                }
                else if (updateTime <= 0)
                {
                    if (text[writeUpTo] == ' ') { writeUpTo++; }
                    writeUpTo++;
                    updateTime = updateSpeed;
                }
            }
            ProcessInputs(kbstate);
        }

        public void ProcessInputs(KeyboardState kbstate)
        {
            if (KeyboardManager.HasBeenPressed(Keys.C))
            {
                if (state == dialogState.AwaitingProceed)
                {
                    text = "";
                    writeUpTo = 0;
                    state = dialogState.Finish;
                    if (autoclose) caller.closeCurrentMenu();
                }
            }
        }

        public void Write(string text)
        {
            if (state != dialogState.Printing)
            {
                Console.Out.WriteLine("Starting Printing Process!");
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
            if (state == dialogState.AwaitingProceed) MenuGeneric.cursor.Draw(spritebatch, new Vector2(x + width - 10, y + height - 10), (float) (Math.PI/2), scale);
            Needler.systemFont.DrawString(spritebatch, new Vector2(x + 5, y + 5), scale, text.Substring(0, writeUpTo), Color.Black);
        }

        public virtual void menuExecute(MenuManager menumgr)
        {
            menumgr.cActiveMenus.Push(this);
            this.caller = menumgr;
        }
    }
}
