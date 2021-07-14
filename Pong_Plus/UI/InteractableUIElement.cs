using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Needler.UI
{
    public interface InteractableUIElement
    {
        void Update(GameTime gametime, KeyboardState kbstate);
        void ProcessInputs(KeyboardState kbstate);
        void Draw(SpriteBatch spritebatch, float scale);

    }
}
