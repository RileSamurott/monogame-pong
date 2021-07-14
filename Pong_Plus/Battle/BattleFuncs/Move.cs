using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Needler.UI;
using Needler.Utilities;
using Needler.Graphics;
namespace Needler.BattleSystem
{
    public enum objectID
    {
        character = 0,
        sprite = 1
    }
    public class MoveObject : TurnActionObject
    {
        private Battle battle;
        private Character chtarget = null;
        private MultiSprite msprtarget = null;
        private int amount;
        private readonly bool suspend;
        private Vector2 initialpos;
        private Vector2 targetpos;
        private Smoothstep smoother;
        private bool useSmooth;
        private objectID type;
        private double fintime;
        private double sttime = 0;
        private bool pause;

        public MoveObject(Battle battle, object target, Vector2 targetPos, float useTime, bool useSmooth, bool pause)
        {
            this.battle = battle;
            if (target is Character)
            {
                this.chtarget = (Character)target;
                type = objectID.character;
            }
            else if (target is MultiSprite)
            {
                this.msprtarget = (MultiSprite)target;
                type = objectID.sprite;
            }
            else
            {
                throw new NotImplementedException("Error: Cannot move this type of object around on the battle screen.");
            }
            this.state = tacState.inactive;
            this.suspend = true;
            this.targetpos = targetPos;
            this.useSmooth = useSmooth;
            this.fintime = useTime;
            this.pause = pause;
        }

        public override void init()
        {
            this.state = tacState.initialized;
            if (type == objectID.character)
                this.initialpos = battle.bRenderer.getCharPosition(chtarget);
            else
                this.initialpos = battle.bRenderer.getSpritePosition(msprtarget);
            
            if (useSmooth)
            {
                this.smoother = new Smoothstep(initialpos, targetpos);
            }
            battle.bRenderer.movingInProgress.Add(this);
            Console.Out.WriteLine("There are now " + battle.bRenderer.movingInProgress.Count.ToString() + " movements in progress.");

        }

        public override bool Update(GameTime gametime)
        {
            if (pause && state != tacState.finished)
            {
                return false;
            }
            state = tacState.finished;
            return true;
        }

        public bool MovementUpdate(GameTime gametime)
        {
            sttime += gametime.ElapsedGameTime.TotalSeconds;
            float x = (float)(sttime / fintime);
            if (type == objectID.character) {
                battle.bRenderer.setCharPosition(chtarget, useSmooth ? smoother.getPosition(x) : initialpos + (targetpos - initialpos) * x);
            }
            else
            {
                battle.bRenderer.setSpritePosition(msprtarget, useSmooth ? smoother.getPosition(x) : initialpos + (targetpos - initialpos) * x);
            }
            if (x >= 1)
            {
                state = tacState.finished;
                if (type == objectID.character) { battle.bRenderer.setCharPosition(chtarget, targetpos); }
                else { battle.bRenderer.setSpritePosition(msprtarget, targetpos); }
                return true;
            }
            return false;
        }
    }
}
