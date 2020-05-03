namespace SummonersTale.StateManager
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IStateManager
    {
        IGameState CurrentState { get; }
        event EventHandler StateChanged;                        // This event will trigger the event handler in all active games states
        void PushState(GameState state, PlayerIndex? index);
        void ChangeState(GameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(GameState state);
    }
}