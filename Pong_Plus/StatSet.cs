using System;
namespace Needler
{
    public class StatSet
    {
        public int level;
        public int maxHP;
        public int maxPP;
        public int offense;
        public int defense;
        public int speed;
        

        public StatSet(int level, int maxHP, int maxPP, int offense, int defense, int speed)
        {
            this.level = level;
            this.maxHP = maxHP;
            this.maxPP = maxPP;
            this.offense = offense;
            this.defense = defense;
            this.speed = speed;
        }
    }
}
