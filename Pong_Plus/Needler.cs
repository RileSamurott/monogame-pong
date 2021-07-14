using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Needler.UI;
using Needler.Graphics;
using Needler.BattleSystem;

namespace Needler
{
    public class Needler : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Texture2D systemTextures;
        public static Texture2D menuIconTextures;
        public static Texture2D pixelLiteral;
        public static Texture2D healEffect; // test graphic
        public static Texture2D cerublue;
        public static SpriteFont unifont;
        public static Texture2D counterflip;
        public static int scrht;
        public static int scrwd;
        private Texture2D character;
        private Battle battle;
        private AllyCharacter ceru;
        private EnemyCharacter echar;
        public int framerate;
        public static Random rnd;
        public static MultiSprite heal;

        public Needler()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            scrht = GraphicsDevice.Viewport.Height;
            scrwd = GraphicsDevice.Viewport.Width;
            // TODO: Add your initialization logic here
            rnd = new Random();
            
            base.Initialize();
            

           
        }

        protected override void LoadContent()
        {
            cerublue = Content.Load<Texture2D>("ceruspr");
            Console.Out.WriteLine(cerublue.ToString());
            menuIconTextures = Content.Load<Texture2D>("menus");
            systemTextures = Content.Load<Texture2D>("System");
            unifont = Content.Load<SpriteFont>("File");

            MenuBox.initialize(Content.Load<Texture2D>("dialogBG"));
            DialogBox.initialize(unifont);
            DamageText.initialize(systemTextures, scrwd, scrht);


            character = Content.Load<Texture2D>("tile");
            healEffect = Content.Load<Texture2D>("battlegfx");
            counterflip = Content.Load<Texture2D>("scrollingctr");


            pixelLiteral = new Texture2D(GraphicsDevice, 1, 1);
            pixelLiteral.SetData<Color>(new Color[] { Color.White });
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            heal = new MultiSprite(healEffect, 0, 0, 15, 15, Color.White, 9, 0.05f, false, true);
            ceru = new AllyCharacter("Ceru",
                new MultiSprite(cerublue, 0, 0, 10, 10, Color.White, 4, -0.05f, false, false),
                new StatSet(1, 100, 10,10, 10, 60));
            var k = new AllyCharacter("Two",
               new MultiSprite(cerublue, 0, 0, 10, 10, Color.White, 4, -0.05f, false, false),
               new StatSet(1, 50, 10, 10, 10, 60));
            var l = new AllyCharacter("Three",
               new MultiSprite(cerublue, 0, 0, 10, 10, Color.White, 4, -0.05f, false, false),
               new StatSet(1, 999, 10, 10, 10, 60));
            echar = new EnemyCharacter("Enemy", character, new SpriteData(), new StatSet(1, 100, 10, 15, 15, 80));
            battle = new Battle(new List<AllyCharacter>() { ceru, k, l }, new List<EnemyCharacter>() { echar });
            battle.decideTurnOrderInitial();
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            pixelLiteral.Dispose();
            menuIconTextures.Dispose();
            systemTextures.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            
            KeyboardState kbstate = KeyboardManager.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbstate.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //combatText.Update(gameTime);
            battle.Update(gameTime);
            battle.bRenderer.Update(gameTime, kbstate);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlueViolet);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend, SamplerState.PointClamp,null,null,null,null);
            battle.bRenderer.Draw(_spriteBatch, new Vector2(0));
            //menutest.Draw(_spriteBatch, 3.5f);
            //dialogTest.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);

            
        }
    }
}
