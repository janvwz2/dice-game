using DiceGame.Core;

namespace DiceGame.UnitTests;

public class DiceTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 1500)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 3 }, 500)]
    [InlineData(new[] { 3, 2, 3, 4, 5, 6 }, 750)]
    [InlineData(new[] { 1, 2, 2, 3, 3, 4 }, 100)]
    [InlineData(new[] { 1, 1, 2, 2, 3, 3 }, 200)]
    [InlineData(new[] { 1, 1, 1, 2, 3, 4 }, 1000)]
    [InlineData(new[] { 1, 1, 1, 1, 3, 3 }, 2000)]
    [InlineData(new[] { 1, 1, 1, 1, 1, 3 }, 3000)]
    [InlineData(new[] { 1, 1, 1, 1, 1, 1 }, 4000)]
    public void ShouldCalculateScoreCorrectly(
        int[] numbers,
        int   expectedScore)
    {
        var dice     = new Dice();
        var position = 0;

        foreach (var number in numbers)
        {
            var face = (ActiveDieFaceType)number;

            dice.Add(new Die { ActiveFace = face, Position = ++position });
        }

        var score = dice.Score;

        Assert.Equal(expectedScore, score);
    }
}