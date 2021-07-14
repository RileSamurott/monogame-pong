using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Needler.Graphics
{
    public enum msprState
    {
        idle = 0,
        playing = 1,
        paused = 2
    }
    public class MultiSprite : Sprite
    {
        private int totalFrames; // Number of horizontal frames
        public int currentFrame;
        public double playSpeed; // Playback speed. Negative speeds reverse direction
        private double timer;
        private bool looping;
        public msprState state;
        private bool timed;
        private bool destroyAfter;
        public Vector2 position = new Vector2(); // TODO: May convert all sprites to have a default position value

        public MultiSprite(Texture2D tex, int xOffset, int yOffset, int width, int height, Color color, int nFrames, float playSpeed, bool looping, bool destroy) :
            base(tex, xOffset, yOffset, width, height, color)
        {
            this.playSpeed = playSpeed;
            this.totalFrames = nFrames - 1;
            this.currentFrame = 0;
            this.timer = this.playSpeed;
            this.looping = looping;
            this.state = msprState.idle;
            this.destroyAfter = destroy;
        }

        public MultiSprite(MultiSprite toCopy): base(toCopy.source, toCopy.xOffset, toCopy.yOffset, toCopy.width, toCopy.height, toCopy.color)
        {
            this.playSpeed = toCopy.playSpeed;
            this.totalFrames = toCopy.totalFrames;
            this.currentFrame = 0;
            this.timer = toCopy.playSpeed;
            this.looping = toCopy.looping;
            this.state = msprState.idle;
            this.destroyAfter = toCopy.destroyAfter;
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
                xOffset + width * currentFrame, // Only supports horizontal sprites at the moment
                yOffset,
                width,
                height);
        }

        public void Start()
        {
            this.state = msprState.playing;
            this.currentFrame = 0;
        }

        public void Pause()
        {
            this.state = msprState.paused;
            this.playSpeed = 0;
        }

        public bool Update(GameTime gametime)
        {
            if (state == msprState.idle && destroyAfter)
                return true;
            var a = gametime.ElapsedGameTime.TotalSeconds;
            timer -= a;

            if (timer <= 0)
            {
                timer = (double)Math.Abs((decimal)playSpeed);
                Next();
            }
            return false;
        }

        public void Next()
        {
            currentFrame += Math.Sign((decimal)(playSpeed));
            if (currentFrame > totalFrames)
                if (looping)
                    currentFrame = 0;
                else
                {
                    currentFrame = totalFrames;
                    state = msprState.idle;
                }
            else if (currentFrame < 0)
                if (looping)
                    currentFrame = totalFrames;
                else
                {
                    currentFrame = 0;
                    state = msprState.idle;
                }
        }

        public new void Draw(SpriteBatch spritebatch, Vector2 position, float scale)
        {
            spritebatch.Draw(
                    source,
                    position,
                    GetRectangle(),
                    color,
                    0f,
                    new Vector2(),
                    scale,
                    SpriteEffects.None,
                    0f
            );
        }

    }
}
