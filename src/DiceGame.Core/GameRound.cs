namespace DiceGame.Core;

public class GameRound
{
    private readonly List<PlayerMove> _moves = [];

    public required int RoundNumber { get; set; }

    public required int PlayerNumberToStart { get; set; }

    public IEnumerable<PlayerMove> Player1MovesEnumerable => _moves.Where(move => move.PlayerNumber == 1);

    public IEnumerable<PlayerMove> Player2MovesEnumerable => _moves.Where(move => move.PlayerNumber == 2);

    public bool IsFinished { get; set; }

    public int? WinningPlayerNumber { get; set; }

    public void StartRound()
    {
        if (IsFinished) throw new InvalidOperationException("Round is already finished.");

        _moves.Clear();
    }

    public void PlayRoundForPlayer(PlayerMove playerMove)
    {
        if (playerMove.PlayerNumber != 1 && playerMove.PlayerNumber != 2)
        {
            throw new ArgumentException("Invalid player number.");
        }

        _moves.Add(playerMove);
    }
}