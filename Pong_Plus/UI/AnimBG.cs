using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Needler.Graphics;
namespace Needler.UI
{
    public class AnimBG
    {
        private RenderTarget2D renderTarget;
        private static GraphicsDevice gd;
        private static SpriteBatch sprbtch;
        private int rtheight;
        private int rtwidth;
        private static int scrnwd;
        private static int scrnht;

        private int ndh;
        private int ndv;

        public MultiSprite bgsource;
        public int renderScale;
        private int xOffset;
        private int yOffset;

        public int xOff
        {
            get { return xOffset; }
            set { xOffset = value % bgsource.width; }
        }
        public int yOff
        {
            get { return yOffset; }
            set { yOffset = value % bgsource.height; }
        }

        public static bool begin;

        public static void initialize(GraphicsDevice grad)
        {
            gd = grad;
            scrnht = Needler.scrht;
            scrnwd = Needler.scrwd;
            sprbtch = new SpriteBatch(grad);
        }
        public AnimBG(MultiSprite bgsource, int stXOffset, int stYOffset)
        {
            this.bgsource = bgsource;
            this.xOffset = stXOffset;
            this.yOffset = stYOffset;

            ndh = (scrnwd / bgsource.width + 2);
            ndv = (scrnht / bgsource.height + 2);

            rtheight = bgsource.height * (ndv-1);
            rtwidth = bgsource.width * (ndh-1);

            Console.Out.WriteLine(rtwidth + " x " + rtheight);

            renderTarget = new RenderTarget2D(gd,
                rtwidth,
                rtheight
            );
        }

        private void renderStart()
        {
           if(!begin)
            {
                gd.SetRenderTarget(renderTarget);
                gd.Clear(Color.Aquamarine);
                sprbtch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);
                begin = true;
           }
           else { Console.Out.WriteLine("Animated Backgrounds: Rendering already started."); }
        }

        public void render()
        {
            renderStart();
            int startx = - bgsource.width + xOffset;
            int starty = - bgsource.height+ yOffset;
            for (int x = 0; x < ndh; x++)
                for (int y = 0; y < ndv; y++)
                    bgsource.Draw(sprbtch, new Vector2(startx + bgsource.width * x, starty + bgsource.height * y), 1);
            renderConclude();
        }

        private static void renderConclude()
        {
            if (begin)
            {
                sprbtch.End();
                gd.SetRenderTarget(null);
                begin = false;
            }
            else { Console.Out.WriteLine("Animated Backgrounds: Rendering already stopped."); }
        }

        public void Update(GameTime gametime)
        {
            bgsource.Update(gametime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(renderTarget, Vector2.Zero, Color.White);
        }
    }
}
