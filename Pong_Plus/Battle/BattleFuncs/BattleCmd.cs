using System;
using Microsoft.Xna.Framework;
namespace Needler.BattleSystem
{
    public enum BattleCommands
    {
        PassForCurrentChar = 0
    }
    public class BattleCmd: TurnActionObject
    {
        BattleCommands cmd;
        Battle battle;
        public BattleCmd(Battle battle, BattleCommands cmd)
        {
            this.battle = battle;
            this.cmd = cmd;
            this.state = tacState.inactive;
        }
        public override void init()
        {
            switch (cmd)
            {
                case BattleCommands.PassForCurrentChar:
                    battle.setAction(new Pass());
                    break;

                default:
                    break;
            }
            this.state = tacState.initialized;
        }

        public override bool Update(GameTime gametime)
        {
            this.state = tacState.finished;
            return true;
        }

    }
}
