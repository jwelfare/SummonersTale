namespace SummonersTale.GameStates
{
    using System;
    using Microsoft.Xna.Framework;
    using SummonersTale.StateManager;

    public class BaseGameState : GameState
    {
        protected static Random random = new Random();

        protected Game1 GameRef;

        public BaseGameState(Game game)
            : base(game)
        {
            this.GameRef = (Game1)game;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
