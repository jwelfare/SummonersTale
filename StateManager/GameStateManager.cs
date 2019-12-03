using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SummonersTale.StateManager
{
    public interface IStateManager
    {
        IGameState CurrentState { get; }
        event EventHandler StateChanged;                        // This event will trigger the event handler in all active games states
        void PushState(GameState state, PlayerIndex? index);
        void ChangeState(GameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(GameState state);
    }

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
                return gameStates.Peek();
            }
        }

        public GameStateManager(Game game) : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this);
        }

        public void PushState(GameState state, PlayerIndex? index)
        {
            drawOrder += drawOrderInc;
            AddState(state, index);
            OnStateChanged();
        }

        private void AddState(GameState state, PlayerIndex? index)
        {
            gameStates.Push(state);
            state.PlayerIndexInControl = index;
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (gameStates.Count != 0)
            {
                RemoveState();
                drawOrder -= drawOrderInc;
                OnStateChanged();
            }
        }

        private void RemoveState()
        {
            GameState state = gameStates.Peek();
            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            gameStates.Pop();
        }

        public void ChangeState(GameState state, PlayerIndex? index)
        {
            while (gameStates.Count > 0)
            {
                RemoveState();
            }

            drawOrder = startDrawOrder;
            state.DrawOrder = drawOrder;
            drawOrder += drawOrderInc;
            AddState(state, index);
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            if (StateChanged != null)
            {
                StateChanged(this, null);
            }
        }
    }
}
