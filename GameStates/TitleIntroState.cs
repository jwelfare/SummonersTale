using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonersTale.StateManager;

namespace SummonersTale.GameStates
{
    public interface ITitleIntroState : IGameState { }

    public class TitleIntroState : BaseGameState, ITitleIntroState
    {
        Texture2D background;

        Rectangle backgroundDestination;

        TimeSpan elapsed;

        // Vector2 position;
        // SpriteFont font;
        // string message;

        public TitleIntroState(Game game) : base(game)
        {
            game.Services.AddService<ITitleIntroState>(this);
        }

        public override void Initialize()
        {
            backgroundDestination = Game1.ScreenRectangle;
            elapsed = TimeSpan.Zero;

            //message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = content.Load<Texture2D>(@"bin/DesktopGL/GameScreens/TitleScreen");

            //font = content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            //Vector2 size = font.MeasureString(message);
            //position = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2, Game1.ScreenRectangle.Bottom - 50 - font.LineSpacing);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            indexInControl = PlayerIndex.One;
            elapsed += gameTime.ElapsedGameTime;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            GameRef.SpriteBatch.Draw(background, backgroundDestination, Color.White);
            GameRef.SpriteBatch.End();

            //Color color = new Color(1f, 1f, 1f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));
            //GameRef.SpriteBatch.DrawString(font, message, position, color);

            base.Draw(gameTime);
        }
    }   
}
