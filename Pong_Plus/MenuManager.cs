using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Needler.UI;
using Needler.BattleSystem;
namespace Needler
{
    /// <summary>
    /// Draws and handles all currently running menus.
    /// Menus are supposed to be able to do any of the following:
    /// - Open another menu(s)
    /// - Declare an action for a specific player character to use such as an attack, guard, item use, etc.
    ///   - Using the bound battle object, the 
    /// </summary>
    public class MenuManager
    {
        public Battle battle;
        public Stack<InteractableUIElement> cActiveMenus;

        public MenuManager(Battle battle)
        {
            this.cActiveMenus = new Stack<InteractableUIElement>();
            this.battle = battle;
        }

        public void openMenu(InteractableUIElement menu)
        {
            cActiveMenus.Push(menu);
        }

        public InteractableUIElement currentMenu()
        {
            return cActiveMenus.Peek();
        }
        public void closeCurrentMenu()
        {
            if (cActiveMenus.Count > 0)
                cActiveMenus.Pop();
        }

        public void closeAllMenus()
        {
            cActiveMenus = new Stack<InteractableUIElement>();
        }

        public void ProcessInputs(KeyboardState kbstate)
        {
            if (cActiveMenus.Count != 0)
            {
                cActiveMenus.Peek().ProcessInputs(kbstate);
            }
        }

        public void Draw(SpriteBatch spritebatch, float scale)
        {
            foreach (InteractableUIElement a in cActiveMenus)
            {
                a.Draw(spritebatch, scale);
            }
        }
    }
}
