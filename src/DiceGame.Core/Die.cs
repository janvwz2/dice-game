namespace DiceGame.Core;

public class Die
{
    public ActiveDieFaceType ActiveFace { get; set; }

    public required int Position { get; set; }

    public void ThrowDie()
    {
        var randomFace = (ActiveDieFaceType)new Random().Next(1, 7);

        ActiveFace = randomFace;
    }
}