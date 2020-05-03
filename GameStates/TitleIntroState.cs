namespace SummonersTale.GameStates
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using SummonersTale.Components;

    public class TitleIntroState : BaseGameState, ITitleIntroState
    {
        Texture2D background;

        Rectangle backgroundDestination;

        TimeSpan elapsed;

        Vector2 messagePosition;
        SpriteFont messageFont;
        string message;

        public TitleIntroState(Game game) : base(game)
        {
            game.Services.AddService<ITitleIntroState>(this);
        }

        public override void Initialize()
        {
            backgroundDestination = Game1.ScreenRectangle;
            elapsed = TimeSpan.Zero;

            message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = content.Load<Texture2D>(@"bin/GameScreens/TitleScreen");

            messageFont = content.Load<SpriteFont>(@"bin/Fonts/InterfaceFont");
            Vector2 size = messageFont.MeasureString(message);

            messagePosition = new Vector2(
                (Game1.ScreenRectangle.Width - size.X) / 2,
                Game1.ScreenRectangle.Bottom - 50 - messageFont.LineSpacing);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // indexInControl = PlayerIndex.One;
            // elapsed += gameTime.ElapsedGameTime;
            // base.Update(gameTime);
            PlayerIndex? index = null;
            elapsed += gameTime.ElapsedGameTime;

            if (InputState.CheckKeyReleased(Keys.Space) || InputState.CheckKeyReleased(Keys.Enter) ||
                InputState.CheckMouseReleased(MouseButtons.Left))
            {
                manager.ChangeState((MainMenuState)GameRef.MainMenuState, index);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(background, backgroundDestination, Color.White);

            Color color = new Color(1f, 1f, 1f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));
            GameRef.SpriteBatch.DrawString(messageFont, message, messagePosition, color);

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }   
}
