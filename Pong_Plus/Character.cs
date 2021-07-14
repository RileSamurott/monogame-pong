using System;
namespace Needler
{
    public enum cStatus
    {
        Alive = 1,
        Dead = 0,
        Mortal = 2
    }
    public abstract class Character
    {
        public StatSet characterStats;
        public string name;
        public int temporaryHPChange = 0;
        public int temporaryPPChange = 0;
        public int temporaryOffenseChange = 0;
        public int temporaryDefenseChange = 0;
        public int temporarySpeedChange = 0;

        public int currentHP;

        public cStatus status;

        public Character(string name, StatSet statset)
        {
            this.characterStats = statset;
            this.name = name;
            this.status = cStatus.Alive;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
