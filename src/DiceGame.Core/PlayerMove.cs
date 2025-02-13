namespace DiceGame.Core;

public class PlayerMove
{
    private readonly Dice _keptDice = [];

    public required int PlayerNumber { get; set; }

    public Dice KeptDice => _keptDice.OrderBy(d => d.Position);

    public int Score { get; set; }

    public void KeepDie(Die die)
    {
        _keptDice.Add(die);
    }
}