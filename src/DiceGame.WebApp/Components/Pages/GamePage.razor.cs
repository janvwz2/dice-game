using DiceGame.Core;

using Microsoft.AspNetCore.Components;

namespace DiceGame.WebApp.Components.Pages;

public partial class GamePage : ComponentBase
{
    private Game Game { get; } = new();

    private void StartGame()
    {
        Game.StartGame();

        StateHasChanged();
    }
}