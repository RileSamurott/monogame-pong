using System;
using Microsoft.Xna.Framework;
using Needler.Graphics;

namespace Needler.BattleSystem
{
    public class FadeCharCol: TurnActionObject, ProgressiveChanges
    {
        private Battle battle;
        private Character target;
        private Vector4 fadeDiff;
        private Vector4 origColor;
        private double time;
        private bool pausing;
        private int index;

        private double sttime = 0;

        public FadeCharCol(Battle battle, Character target, Color fadeTo, double time, bool pausing)
        {
            this.battle = battle;
            this.target = target;
            this.fadeDiff = fadeTo.ToVector4();
            this.time = time;
            this.pausing = pausing;
        }
        public override void init()
        {
            sttime = 0;
            this.state = tacState.initialized;
            battle.bRenderer.changesInProgress.Add(this);
            if (target is AllyCharacter)
            {
                this.index = battle.allyCharacters.IndexOf((AllyCharacter)target);
            }
            else if (target is EnemyCharacter)
            {
                this.index = battle.allyCharacters.Count + battle.enemyCharacters.IndexOf((EnemyCharacter)target);
            }
            this.origColor = battle.bRenderer.shadeCols[index].ToVector4();
            fadeDiff.X -= origColor.X;
            fadeDiff.Y -= origColor.Y;
            fadeDiff.Z -= origColor.Z;
            fadeDiff.W -= origColor.W; // tgt - orig
            Console.Out.WriteLine("FadeCharacterColor: Going from " + origColor.ToString() + " to " + (origColor + fadeDiff).ToString());
        }
        public override bool Update(GameTime gametime)
        {
            if (pausing && state != tacState.finished)
            {
                return false;
            }
            state = tacState.finished;
            return true;
        }
        public bool ProgressiveUpdate(GameTime gametime)
        {
            sttime += gametime.ElapsedGameTime.TotalSeconds;
            if (sttime >= time)
            {
                state = tacState.finished;
                battle.bRenderer.shadeCols[index] = new Color(origColor + fadeDiff); // orig + tgt - orig
                Console.Out.WriteLine("Set the color for index " + index.ToString() + " to " + battle.bRenderer.shadeCols[index].ToString());
                return true;
            }
            float pg = (float)(sttime / time);
            battle.bRenderer.shadeCols[index] = new Color(
                origColor + fadeDiff * pg); // orig + tgt - orig
            // Console.Out.WriteLine(battle.bRenderer.shadeCols[index]);
            return false;

        }
    }
}
