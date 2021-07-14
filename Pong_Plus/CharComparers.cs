using System;
using System.Collections.Generic;
namespace Needler
{
    public class speedEvaluate: IComparer<Character>
    {
        int IComparer<Character>.Compare(Character x, Character y)
        {
            return x.characterStats.speed + x.temporarySpeedChange - y.characterStats.speed - y.temporarySpeedChange;
        }

    }
}
