namespace SummonersTale.StateManager
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    // DrawableGameComponent adds LoadContent, Update and Draw methods to the game state so they can be called from other classes
    public abstract class GameState : DrawableGameComponent, IGameState
    {
        // Allows for retrieving another state from the State Manager in use in other game states
        protected IGameState tag;

        protected readonly IStateManager manager;

        protected ContentManager content;

        protected readonly List<GameComponent> childComponents;

        // PlayerIndexInControl was added to allow for the use of XBOX 360 controllers
        protected PlayerIndex? indexInControl;

        public PlayerIndex? PlayerIndexInControl
        {
            get { return this.indexInControl; }
            set { this.indexInControl = value; }
        }

        public List<GameComponent> Components
        {
            get { return this.childComponents; }
        }

        public IGameState Tag
        {
            get { return this.tag; }
        }

        protected GameState(Game game)
            : base(game)
        {
            this.tag = this;
            this.childComponents = new List<GameComponent>();
            this.content = Game.Content;

            // Dependency injection
            this.manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in this.childComponents)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (GameComponent component in this.childComponents)
            {
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                {
                    ((DrawableGameComponent)component).Draw(gameTime);
                }
            }
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (this.manager.CurrentState == tag)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;

            foreach (GameComponent component in this.childComponents)
            {
                component.Enabled = true;

                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;

            foreach (GameComponent component in this.childComponents)
            {
                component.Enabled = false;

                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }
    }
}