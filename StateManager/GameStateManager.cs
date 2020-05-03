namespace SummonersTale.StateManager
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    // The GameStateManager inherits from GameComponent so it can be registered as a service and have its Update method called automatically
    public class GameStateManager : GameComponent, IStateManager
    {
        private readonly Stack<GameState> gameStates = new Stack<GameState>();

        // DrawableGameComponents have a DrawOrder associated with them. Components with higher values will be drawn before components with lower values.
        // (GameState implements DrawableGameComponent)
        // There are constants that hold the initial draw order of a component and an increment that will be added when a new state is pushed onto the
        // stack or decremented when a state is popped off the stack.
        private const int startDrawOrder = 5000;
        private const int drawOrderInc = 50;
        private int drawOrder;

        // If a component has subscribed to the event its internal handler will be called when this event is raised.
        public event EventHandler StateChanged;

        public IGameState CurrentState
        {
            get
            {
                return this.gameStates.Peek();
            }
        }

        public GameStateManager(Game game)
            : base(game)
        {
            this.Game.Services.AddService(typeof(IStateManager), this);
        }

        public void PushState(GameState state, PlayerIndex? index)
        {
            this.drawOrder += drawOrderInc;
            this.AddState(state, index);
            this.OnStateChanged();
        }

        private void AddState(GameState state, PlayerIndex? index)
        {
            this.gameStates.Push(state);
            state.PlayerIndexInControl = index;
            this.Game.Components.Add(state);
            this.StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (this.gameStates.Count != 0)
            {
                this.RemoveState();
                this.drawOrder -= drawOrderInc;
                this.OnStateChanged();
            }
        }

        private void RemoveState()
        {
            GameState state = this.gameStates.Peek();
            this.StateChanged -= state.StateChanged;
            this.Game.Components.Remove(state);
            this.gameStates.Pop();
        }

        public void ChangeState(GameState state, PlayerIndex? index)
        {
            while (this.gameStates.Count > 0)
            {
                this.RemoveState();
            }

            this.drawOrder = startDrawOrder;
            state.DrawOrder = drawOrder;
            this.drawOrder += drawOrderInc;
            this.AddState(state, index);
            this.OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return this.gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, null);
            }
        }
    }
}
