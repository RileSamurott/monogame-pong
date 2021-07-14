using System;
using Needler.BattleSystem;
using System.Collections.Generic;
namespace Needler.UI
{
    public interface MenuAction
    {
        public abstract void menuExecute(MenuManager menumgr);
    }
    public class Bash: MenuAction
    {
        public void menuExecute(MenuManager menumgr)
        {
            Battle k = menumgr.battle;
            menumgr.closeAllMenus();
            k.setAction(new BashAction(k.getCurrentCharActor(), new List<Character>() { k.enemyCharacters[0] }));
        }
    }
    public class HealAct : MenuAction
    {
        public void menuExecute(MenuManager menumgr)
        {
            Battle k = menumgr.battle;
            menumgr.closeAllMenus();
            k.setAction(new SpriteAnimationAction());
        }
    }
}
