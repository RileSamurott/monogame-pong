using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Needler.Graphics;
namespace Needler.UI
{
    public class BattleMenuBar: InteractableUIElement, MenuAction
    {
        private int cselect;
        private List<UIOption> options;
        private MenuManager caller;
        private bool hidden;
        public BattleMenuBar(List<UIOption> options, MenuManager caller) // Simple implementation first: only two options: bash and guard
        {
            this.caller = caller;
            this.options = options;
            this.cselect = 0;
        }

        public void Update(GameTime gametime, KeyboardState kbstate)
        {
            ProcessInputs(kbstate);
        }

        private void move(int val)
        {
            cselect += val;
            if (cselect > options.Count - 1)
                cselect = 0;
            else if (cselect < 0)
                cselect = options.Count - 1;
        }

        public void ProcessInputs(KeyboardState kbstate)
        {
            options[cselect].selected = false;
            if (KeyboardManager.HasBeenPressed(Keys.C)) {
                options[cselect].choose(caller);
            }
            else if (KeyboardManager.HasBeenPressed(Keys.Left)) {
                move(-1);
            }
            else if (KeyboardManager.HasBeenPressed(Keys.Right)) {
                move(1);
            }
            else if (KeyboardManager.HasBeenPressed(Keys.X))
            {
                caller.battle.undoLastAction();
            }
            options[cselect].selected = true;



        }
        public void Draw(SpriteBatch spritebatch, float scale)
        {
            if (hidden) { return; }
            // Draw black rectangle

            spritebatch.Draw(Needler.pixelLiteral, new Rectangle(0,0,Needler.scrwd,(int)(12*scale)),Color.Black);

            // Options
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Draw(spritebatch, new Vector2((1 + 12 * i) * scale, 1), scale);
            }

        }

        public void menuExecute(MenuManager menumgr)
        {
            menumgr.cActiveMenus.Push(this);
        }
    }
}
