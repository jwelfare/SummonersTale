namespace SummonersTale.StateManager
{
    using Microsoft.Xna.Framework;

    public interface IGameState
    {
        IGameState Tag { get; }
        PlayerIndex? PlayerIndexInControl { get; set; }
    }
}