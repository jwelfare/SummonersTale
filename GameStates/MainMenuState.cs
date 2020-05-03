namespace SummonersTale.GameStates
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using SummonersTale.Components;

    public class MainMenuState : BaseGameState, IMainMenuState
    {
        MenuComponent menuComponent;
        Texture2D backgroundTexture;
        SpriteFont menuFont;

        public MainMenuState(Game game)
            : base(game)
        {
            game.Services.AddService<IMainMenuState>(this);
        }

        protected override void LoadContent()
        {
            this.backgroundTexture = Game.Content.Load<Texture2D>("bin/GameScreens/MenuScreen");
            this.menuFont = this.Game.Content.Load<SpriteFont>("bin/Fonts/interfaceFont");

            string[] menuItems = { "Play", "Continue", "Options", "Exit", "Credits" };

            this.menuComponent = new MenuComponent(this.menuFont, menuItems);

            Vector2 position = default;
            position.X = 940;
            position.Y = 170;

            this.menuComponent.Position = position;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.menuComponent.Update();

            if (InputState.CheckKeyReleased(Keys.Space) || InputState.CheckKeyReleased(Keys.Enter) ||
                (this.menuComponent.MouseOver && InputState.CheckMouseReleased(MouseButtons.Left)))
            {
                switch (this.menuComponent.SelectedIndex)
                {
                    case 0:
                        InputState.FlushInput();
                        break;
                    case 1:
                        InputState.FlushInput();
                        break;
                    case 2:
                        InputState.FlushInput();
                        break;
                    case 3:
                        this.Game.Exit();
                        break;
                    case 5:
                        InputState.FlushInput();
                        break;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.GameRef.SpriteBatch.Begin();

            this.GameRef.SpriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            this.GameRef.SpriteBatch.End();

            base.Draw(gameTime);

            this.GameRef.SpriteBatch.Begin();

            this.menuComponent.Draw(this.GameRef.SpriteBatch);

            this.GameRef.SpriteBatch.End();
        }
    }
}
