using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MDTBCollisionSystem;

namespace Pong_Plus
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPos;
        float ballSpd;
        BoundingBox2D ballBoundingBox;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            ballSpd = 200f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("Ball");
            ballBoundingBox = new BoundingBox2D(ballPos.X,
                ballPos.Y,
                ballPos.X + ballTexture.Width,
                ballPos.Y + ballTexture.Height);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                ballPos.Y -= ballSpd* (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                ballPos.Y += ballSpd* (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                ballPos.X -= ballSpd* (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                ballPos.X += ballSpd* (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            ballBoundingBox.X = ballPos.X;
            ballBoundingBox.Y = ballPos.Y;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture,
                ballPos,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                0.5f,
                SpriteEffects.None,
                0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
