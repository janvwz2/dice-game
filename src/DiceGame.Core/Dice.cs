namespace DiceGame.Core;

public class Dice : List<Die>
{
    public int Score
    {
        get
        {
            var faces = this.Select(d => d.ActiveFace)
               .Where(d => d != ActiveDieFaceType.NotThrown)
               .ToList();

            if (faces.Count == 0) return 0;

            var score = 0;

            if (FindSequenceAndPurgeIt(ActiveDieFaceType.One, ActiveDieFaceType.Six))
            {
                score += 1500;
            }
            else if (FindSequenceAndPurgeIt(ActiveDieFaceType.Two, ActiveDieFaceType.Six))
            {
                score += 750;
            }
            else if (FindSequenceAndPurgeIt(ActiveDieFaceType.One, ActiveDieFaceType.Five))
            {
                score += 500;
            }

            var faceCounts = faces
               .GroupBy(face => face)
               .ToDictionary(group => group.Key, group => group.Count());

            // TODO it can't count the ones used for the sequence
            foreach (var (face, amountOfDiceWithFace) in faceCounts)
            {
                var scoreForFace = face switch
                {
                    ActiveDieFaceType.One   => 100,
                    ActiveDieFaceType.Two   => 20,
                    ActiveDieFaceType.Three => 30,
                    ActiveDieFaceType.Four  => 40,
                    ActiveDieFaceType.Five  => 50,
                    ActiveDieFaceType.Six   => 60,
                    _                       => 0
                };
                score += amountOfDiceWithFace switch
                {
                    < 3 => face is ActiveDieFaceType.One or ActiveDieFaceType.Five
                        ? scoreForFace * amountOfDiceWithFace
                        : 0,
                    3   => scoreForFace * 10,
                    > 3 => scoreForFace * 10 * (amountOfDiceWithFace - 2)
                };
            }

            return score;

            bool FindSequenceAndPurgeIt(
                ActiveDieFaceType startFace,
                ActiveDieFaceType endFace)
            {
                for (var faceNumber = (int)startFace; faceNumber <= (int)endFace; faceNumber++)
                {
                    var face = (ActiveDieFaceType)faceNumber;

                    if (!faces.Contains(face)) return false;
                }
                // The sequence is found

                faces.RemoveAll(face => face >= startFace && face <= endFace);

                return true;
            }
        }
    }
}