namespace SummonersTale
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using SummonersTale.Components;
    using SummonersTale.GameStates;
    using SummonersTale.StateManager;

    public class Game1 : Game
    {
        static Rectangle screenRectangle;

        protected GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        GameStateManager gameStateManager;

        ITitleIntroState titleIntroState;
        IMainMenuState mainMenuState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Defining the game window size
            screenRectangle = new Rectangle(0, 0, 1280, 720);
            graphics.PreferredBackBufferWidth = ScreenRectangle.Width;
            graphics.PreferredBackBufferHeight = ScreenRectangle.Height;

            gameStateManager = new GameStateManager(this);
            Components.Add(gameStateManager);

            this.IsMouseVisible = true;

            titleIntroState = new TitleIntroState(this);
            mainMenuState = new MainMenuState(this);

            gameStateManager.ChangeState((TitleIntroState)titleIntroState, PlayerIndex.One);
        }

        public static Rectangle ScreenRectangle
        {
            get { return screenRectangle; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public ITitleIntroState TitleIntroState
        {
            get { return titleIntroState; }
        }

        public IMainMenuState MainMenuState
        {
            get { return mainMenuState; }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Initialize()
        {
            Components.Add(new InputState(this));

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            base.Draw(gameTime);
        }
    }
}