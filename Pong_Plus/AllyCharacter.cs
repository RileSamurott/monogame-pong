using System;
using Needler.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Needler.UI;
namespace Needler
{
    public class AllyCharacter: Character, CameraDrawable
    {

        protected ScrollingCounter hpMeter;
        protected MultiSprite sprite;

        public bool guarding;
        public int potentialHP
        {
            get { return hpMeter.potentialHealth; }
            set { if (value < 0) hpMeter.potentialHealth = 0; else hpMeter.potentialHealth = Math.Clamp(value,0,characterStats.maxHP);}
        }
        public new int currentHP
        {
            get { return hpMeter.currentHealth; }
            set { if (value < 0) hpMeter.currentHealth = 0; else hpMeter.currentHealth = Math.Clamp(value, 0, characterStats.maxHP); }
        }

        public bool hasPP;
        public int potentialPP;
        public int currentPP;

        public AllyCharacter(string name, MultiSprite sprite, StatSet statSet) : base(name, statSet)
        {
            this.sprite = sprite;
            this.hpMeter = new ScrollingCounter(statSet.maxHP,0,0,statSet.maxPP);
            this.guarding = false;
        }

        public void ToggleReady()
        {
            
            sprite.playSpeed = -sprite.playSpeed;
            Console.Out.WriteLine(base.ToString() + ": Toggled Playing Speed - Set to " + sprite.playSpeed.ToString());
        }
        public void Update(GameTime gametime)
        {
            status = hpMeter.Update(gametime) ? cStatus.Dead : status;
            sprite.Update(gametime);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale)
        {
            hpMeter.Draw(spritebatch, position, scale);
            sprite.Draw(spritebatch, position + new Vector2(22, -sprite.height)*scale, scale);
        }
        
    }
}
