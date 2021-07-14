using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Needler.UI;
namespace Needler.BattleSystem
{
    public class Damage: TurnActionObject
    {
        private Battle battle;
        private Character target;
        private int amount;
        private DamageText dtxt;
        private bool suspend;
        private Shake shk;


        public Damage(Battle battle, Character target, int amount)
        {
            this.battle = battle;
            this.target = target;
            this.amount = amount;
            this.state = tacState.inactive;
            this.suspend = true;
            this.shk = new Shake(battle, target);
        }

        public Damage(Battle battle, Character target, int amount, bool suspend)
        {
            this.battle = battle;
            this.target = target;
            this.amount = amount;
            this.state = tacState.inactive;
            this.suspend = suspend;
            this.shk = new Shake(battle, target);
        }

        public override void init()
        {
            this.state = tacState.initialized;
            this.dtxt = battle.damage(target, amount);
            if (suspend) shk.init();

        }

        public override bool Update(GameTime gametime)
        {
            if (!suspend)
            {
                state = tacState.finished;
                battle.cTurn.Insert(1,this.shk);
                return true;
            }
            shk.Update(gametime);
            if ( dtxt.state == dtState.dead && shk.state == tacState.finished) { state = tacState.finished;  return true; }
            else { return false; }
        }
    }
}
