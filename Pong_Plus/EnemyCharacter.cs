using System;
using Microsoft.Xna.Framework;
using Needler.Graphics;
using Microsoft.Xna.Framework.Graphics;
namespace Needler
{
    public class EnemyCharacter: Character
    {
        public Sprite frontsprite;
        public Sprite backsprite;
        protected StatSet stats;
        public new int currentHP;
        public bool facing;


        public EnemyCharacter(string name, Texture2D sprite,  SpriteData spriteData, StatSet statset): base(name, statset)
        {
            this.name = name;
            this.frontsprite = new Sprite(
                sprite,
                0,
                0,
                spriteData.width,
                spriteData.height,
                Color.White
            );
            this.backsprite = new Sprite(
                sprite,
                spriteData.width + 1,
                0,
                spriteData.width,
                spriteData.height,
                Color.White
            );
            this.stats = statset;
            this.currentHP = statset.maxHP;
            this.facing = true;
        }
        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale)
        {
            if (facing)
                frontsprite.Draw(spritebatch, position, scale);
            else
                backsprite.Draw(spritebatch, position, scale);
        }
    }
}
