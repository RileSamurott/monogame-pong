using System;
using Microsoft.Xna.Framework;
namespace Needler.BattleSystem
{
    public interface ProgressiveChanges
    {
        public bool ProgressiveUpdate(GameTime gametime);
    }
}
