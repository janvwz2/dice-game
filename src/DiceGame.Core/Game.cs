namespace DiceGame.Core;

public class Game
{
    private readonly List<GameRound> _rounds = [];

    private readonly List<string> _logs = [];

    private int _activeRoundNumber = 0;

    public int? PlayerNumberToStart { get; set; }

    public Player Player1 { get; set; } = new()
    {
        PlayerNumber = 1
    };

    public Player Player2 { get; set; } = new()
    {
        PlayerNumber = 2
    };

    public Player ActivePlayer =>
        Player1.IsActive
            ? Player1
            : Player2.IsActive
                ? Player2
                : throw new InvalidOperationException("No active player.");

    public int ActiveRoundNumber => _activeRoundNumber;

    public bool IsActive => _activeRoundNumber > 0;

    public bool IsFinished { get; set; }

    public IEnumerable<string> Logs => _logs;

    public Player GetPlayer(int playerNumber) =>
        playerNumber switch
        {
            1 => Player1,
            2 => Player2,
            _ => throw new ArgumentException("Invalid player number.")
        };

    public GameRound StartGame()
    {
        if (_rounds.Count > 0) throw new InvalidOperationException("Game already started.");

        _logs.Add("The game has started.");

        return StartNewRound();
    }

    public GameRound StartNewRound()
    {
        Player1.AssignDice();
        Player2.AssignDice();

        _activeRoundNumber++;

        var playerNumberToStart = GetPlayerNumberToStart();

        switch (playerNumberToStart)
        {
            case 1: Player1.Activate(); break;
            case 2: Player2.Activate(); break;
            default:
                throw new InvalidOperationException("Invalid player number to start.");
        }

        _logs.Add($"Starting round {_activeRoundNumber} ({ActivePlayer.Name} has the first move).");

        var gameRound = new GameRound
        {
            RoundNumber         = _activeRoundNumber,
            PlayerNumberToStart = playerNumberToStart,
            IsFinished          = false
        };

        gameRound.StartRound();

        _rounds.Add(gameRound);

        return gameRound;
    }

    private int GetPlayerNumberToStart()
    {
        if (_rounds.Count == 0)
        {
            return PlayerNumberToStart ?? new Random().Next(1, 3); // Use null-coalescing operator
        }

        var lastRound = _rounds.LastOrDefault();

        return lastRound?.WinningPlayerNumber ?? throw new InvalidOperationException("Unable to determine the next player.");
    }
}