using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonersTale.Components;
using SummonersTale.StateManager;

namespace SummonersTale.GameStates
{
    public interface IMainMenuState : IGameState { }

    public class MainMenuState : BaseGameState, IMainMenuState
    {
        MenuComponent menuComponent;
        Texture2D backgroundTexture;
        SpriteFont menuFont;

        public MainMenuState(Game game) : base(game)
        {
            game.Services.AddService<IMainMenuState>(this);
        }
    }
}
