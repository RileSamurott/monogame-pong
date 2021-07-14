using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Needler.UI
{
    public abstract class MenuGeneric: InteractableUIElement
    {
        public MenuManager caller;
        public MenuBox mbox;
        private List<Vector2> drawCoords;
        public List<UIOption> options;
        public int menuStart;
        public int menuEnd;
        public int menuSelected;
        public UIOption cselected
        {
            get { return options[menuSelected]; }
        }

        public MenuGeneric(int x, int y, int width, int height, List<UIOption> options, MenuManager caller)
        {
            this.caller = caller;
            this.mbox = new MenuBox(x, y, width, height);
            this.options = options;
        }

        public virtual void Update(GameTime gametime, KeyboardState kbstate)
        {
            ProcessInputs(kbstate);
        }

        public abstract void ProcessInputs(KeyboardState kbstate);

        public virtual void Draw(SpriteBatch spritebatch, float scale)
        {
            for (int i = menuStart; i < menuEnd; i++)
            {
                options[i].Draw(spritebatch, drawCoords[i - menuStart], scale);
            }
        }
    }
}
