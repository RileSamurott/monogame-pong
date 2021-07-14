using System;
using Needler.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Needler.UI
{
    public class UIOption: CameraDrawable
    {
        public string name;
        public Sprite? highlight;
        public Sprite? nohighlight;
        public bool selected = false;
        public bool disabled = false;
        public bool drawText;
        public MenuAction onSelection; // Menu action can open another menu,
                                       // declare an action for a particular character in the current turn order, etc.

        public UIOption(string name, bool drawName, Sprite hl, Sprite nhl, MenuAction onsel)
        {
            this.name = name;
            this.drawText = drawName;
            this.highlight = hl;
            this.nohighlight = nhl;
            this.onSelection = onsel;
        }

        public UIOption(Sprite hl, Sprite nhl, MenuAction onsel)
        {
            this.name = "";
            this.drawText = false;
            this.highlight = hl;
            this.nohighlight = nhl;
            this.onSelection = onsel;
        }

        public void choose(MenuManager caller)
        {
            if (onSelection == null)
            {
                return;
            }
            onSelection.menuExecute(caller);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 position, float scale)
        {
            if (selected)
            {
                if (drawText) spritebatch.DrawString(
                    Needler.unifont,
                    name,
                    position + (highlight == null ? new Vector2(0) : new Vector2((highlight.width / 2) * scale, (highlight.height / 2) * scale)),
                    Color.White
                );
                if (highlight != null)
                    highlight.Draw(spritebatch, position, scale);
            }
            else
            {
                if (drawText) spritebatch.DrawString(
                    Needler.unifont,
                    name,
                    position + (nohighlight == null ? new Vector2(0) : new Vector2((nohighlight.width / 2) * scale, (nohighlight.height / 2) * scale)),
                    Color.White
                );
                if (nohighlight != null) {
                    nohighlight.Draw(spritebatch, position, scale);
                }
            }
        }
    }
}
