namespace DiceGame.Core;

public class Player
{
    private readonly Dice    _hand = [];
    private          string? _name;
    private readonly Dice    _keptDice = [];

    public required int PlayerNumber { get; init; }

    public string? Name
    {
        get =>
            string.IsNullOrWhiteSpace(_name)
                ? $"Player {PlayerNumber}"
                : _name;
        set => _name = value;
    }

    public Dice Hand => _hand.OrderBy(x => x.Position);

    public Dice KeptDice => _keptDice.OrderBy(x => x.Position);

    public bool IsActive { get; set; }

    public bool IsSelecting => IsActive && _hand.Count > 0;

    public void Activate()
    {
        IsActive = true;
    }

    public void AssignDice()
    {
        _hand.Clear();

        for (var i = 0; i < 6; i++)
        {
            _hand.Add(
                new Die
                {
                    Position = i + 1
                });
        }
    }

    public void ThrowDice()
    {
        if (_hand.Count == 0)
        {
            throw new InvalidOperationException("No dice in hand to throw.");
        }

        foreach (var dice in _hand)
        {
            dice.ThrowDie();
        }
    }

    public void KeepDie(Die die)
    {
        _keptDice.Add(die);
    }

    public void RemoveDie(Die die)
    {
        _keptDice.Remove(die);
    }
}