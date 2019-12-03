using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SummonersTale.StateManager
{
    public interface IGameState
    {
        IGameState Tag { get; }
        PlayerIndex? PlayerIndexInControl { get; set; }
    }

    // DrawableGameComponent adds LoadContent, Update and Draw methods to the game state so they can be called from other classes
    public abstract partial class GameState : DrawableGameComponent, IGameState
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
            get { return indexInControl; }
            set { indexInControl = value; }
        }

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        public IGameState Tag
        {
            get { return tag; }
        }

        public GameState(Game game) : base(game)
        {
            tag = this;
            childComponents = new List<GameComponent>();
            content = Game.Content;

            // Dependency injection
            manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
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

            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                {
                    ((DrawableGameComponent)component).Draw(gameTime);
                }
            }
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (manager.CurrentState == tag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach (GameComponent component in childComponents)
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
            Enabled = false;
            Visible = false;

            foreach (GameComponent component in childComponents)
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