using System;
using System.Collections.Generic;
using Needler.Graphics;
using Microsoft.Xna.Framework;

namespace Needler.BattleSystem
{
    public abstract class BattleAction
    {
        protected int numOfTargets;
        protected Character user;
        protected List<Character> targets;
        public abstract List<TurnActionObject> battleExecute(Battle battle);
    }
    /*
    public class BashAction: BattleAction
    {
        protected new int numOfTargets = 1;
        public BashAction(Character user, List<Character> targets)
        {
            this.user = user;
            this.targets = targets;
        }
        public override void battleExecute(Battle battle)
        {
            battle.damage(targets[0], 1);
            battle.finishAction();
        }
    }
    */

    public class BashAction : BattleAction
    {
        protected new int numOfTargets = 1;
        public BashAction(Character user, List<Character> targets)
        {
            this.user = user;
            this.targets = targets;
        }
        public override List<TurnActionObject> battleExecute(Battle battle)
        {
            return new List<TurnActionObject>() {
                new Damage(battle, battle.allyCharacters[0], 10, false)
            };
        
        }
    }
    public class SpriteAnimationAction : BattleAction
    {
        protected new int numOfTargets = 1;
        public SpriteAnimationAction()
        {
        }
        public override List<TurnActionObject> battleExecute(Battle battle)
        {
            var ret = new List<TurnActionObject>();
            ret.Add(new MoveObject(battle, battle.allyCharacters[0], new Vector2(Needler.rnd.Next(500), Needler.rnd.Next(500)), 1, true, true));
            return ret;
        }
    }
}
