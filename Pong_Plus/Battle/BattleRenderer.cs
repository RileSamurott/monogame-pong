using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Needler.Graphics;
using Needler.UI;
using Microsoft.Xna.Framework.Graphics;
namespace Needler.BattleSystem
{
    public class BattleRenderer
    {
        private Battle battle;
        private List<DamageText> inProgressDamage;
        private List<MultiSprite> spritesToDraw;
        public List<Vector2> aCharPos;
        public List<Vector2> eCharPos;
        public List<MoveObject> movingInProgress;
        
        public float globalScale;

        public BattleRenderer(Battle battle)
        {
            this.battle = battle;
            this.inProgressDamage = new List<DamageText>();
            this.globalScale = 3;
            this.spritesToDraw = new List<MultiSprite>();
            this.aCharPos = new List<Vector2>(new Vector2[battle.allyCharacters.Count]);
            this.eCharPos = new List<Vector2>(new Vector2[battle.enemyCharacters.Count]);
            this.movingInProgress = new List<MoveObject>();
        }


        public void calculatePositions()
        {
            for(int i = 0; i < battle.allyCharacters.Count; i++) {
                //aCharPos[i] = new Vector2(
                var a = new Vector2(
                    Needler.scrwd / (battle.allyCharacters.Count + 1) * (i+1) - 27 * globalScale, // Hardcoded
                    Needler.scrht - 35 * globalScale
                );
                (new MoveObject(battle, battle.allyCharacters[i], a, 0.2f, true, false)).init();
            }
            for (int i = 0; i < battle.enemyCharacters.Count; i++)
            {
                //eCharPos[i] = new Vector2(
                var e = new Vector2(
                    Needler.scrwd / 2 - globalScale,
                    Needler.scrht / 2
                );
                Console.Out.WriteLine(i.ToString());
                (new MoveObject(battle, battle.enemyCharacters[i], e, 0.2f, true, false)).init();
            }
        }

        public Vector2 getCharPosition(Character character)
        {
            if (character is AllyCharacter)
            {
                return aCharPos[(battle.allyCharacters.IndexOf((AllyCharacter)character))];
            }
            else
            {
                return eCharPos[(battle.enemyCharacters.IndexOf((EnemyCharacter)character))];
            }
        }
        public void setCharPosition(Character character, Vector2 pos)
        {
            if (character is AllyCharacter)
            {
                aCharPos[(battle.allyCharacters.IndexOf((AllyCharacter)character))] = pos;
            }
            else
            {
                eCharPos[(battle.enemyCharacters.IndexOf((EnemyCharacter)character))] = pos;
            }
        }

        public Vector2 getSpritePosition(MultiSprite sprite)
        {
            var ind = spritesToDraw.IndexOf(sprite);
            if (ind >= 0)
                return spritesToDraw[ind].position;
            return new Vector2(-1, -1);
        }
        public void setSpritePosition(MultiSprite sprite, Vector2 newpos)
        {
            var ind = spritesToDraw.IndexOf(sprite);
            if (ind >= 0)
                spritesToDraw[ind].position = newpos;
        }

        public DamageText showDamage(int dmg, Character reciever, Color color)
        {
            Vector2 position = new Vector2();
            if (reciever is AllyCharacter)
            {
                position = aCharPos[battle.allyCharacters.IndexOf((AllyCharacter)reciever)];
                position.X += 35*globalScale;
            }
            else if (reciever is EnemyCharacter)
            {
                position = eCharPos[battle.enemyCharacters.IndexOf((EnemyCharacter)reciever)];
            }
            var a = new DamageText(dmg, position, color);
            inProgressDamage.Add(a);
            Console.Out.WriteLine("There are now " + inProgressDamage.Count.ToString() + " Damagetexts.");
            return a;
        }

        public void displaySprite(MultiSprite sprite)
        {
            spritesToDraw.Add(sprite);
        }
        public void removeSprite(MultiSprite sprite)
        {
            spritesToDraw.Remove(sprite);
        }

        public void Update(GameTime gametime, KeyboardState kbstate)
        {
            foreach(AllyCharacter p in battle.allyCharacters)
            {
                p.Update(gametime);
            }

            for (int i = 0; i < inProgressDamage.Count; i++)
            {
                if (inProgressDamage[i].Update(gametime))
                {
                    inProgressDamage.RemoveAt(i);
                    i--;
                }
            }

            battle.dialog.Update(gametime, kbstate);

            battle.menuManager.ProcessInputs(kbstate);

            for (int i = 0; i < spritesToDraw.Count; i++)
            {
                if (spritesToDraw[i].Update(gametime))
                {
                    spritesToDraw.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < movingInProgress.Count; i++)
            {
                if (movingInProgress[i].MovementUpdate(gametime))
                {
                    movingInProgress.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch, Vector2 offset)
        {
            for (int i = 0; i < battle.allyCharacters.Count; i++)
            {
                battle.allyCharacters[i].Draw(spritebatch, aCharPos[i], globalScale);
            }

            battle.menuManager.Draw(spritebatch, globalScale);

            battle.dialog.Draw(spritebatch, globalScale);

            for (int i = 0; i < battle.enemyCharacters.Count; i++)
            {
                battle.enemyCharacters[i].Draw(spritebatch, eCharPos[i], globalScale);
            }
            for (int i = 0; i < spritesToDraw.Count; i++)
            {
                spritesToDraw[i].Draw(spritebatch, spritesToDraw[i].position, globalScale);
            }
            for (int i = 0; i < inProgressDamage.Count; i++)
            {
                inProgressDamage[i].Draw(spritebatch, globalScale);
            }
        }
    }
}
