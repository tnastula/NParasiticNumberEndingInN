using System.Text;

namespace NParasiticNumberEndingInN;

public class ParasiticNumberFinder
{
    private readonly int TrailingDigit;
    private readonly int NumberBase;
    private static readonly Dictionary<int, char> DigitToSymbol;

    static ParasiticNumberFinder()
    {
        DigitToSymbol = new()
        {
            { 0, '0' },
            { 1, '1' },
            { 2, '2' },
            { 3, '3' },
            { 4, '4' },
            { 5, '5' },
            { 6, '6' },
            { 7, '7' },
            { 8, '8' },
            { 9, '9' },
            { 10, 'A' },
            { 11, 'B' },
            { 12, 'C' },
            { 13, 'D' },
            { 14, 'E' },
            { 15, 'F' }
        };
    }

    public ParasiticNumberFinder(int trailingDigit, int numberBase)
    {
        TrailingDigit = trailingDigit;
        NumberBase = numberBase;
    }

    public string Calculate()
    {
        double divider = TrailingDigit * NumberBase - 1;
        double valueContainingPeriod = TrailingDigit / divider;

        List<int> digitSequence = new();
        List<int>? repeatingSequence = null;
        double magnitude = 1.0 / NumberBase;

        while (valueContainingPeriod > 0)
        {
            if (magnitude == 0)
                break;

            int digit = (int)(valueContainingPeriod / magnitude);
            valueContainingPeriod -= digit * magnitude;
            magnitude /= NumberBase;

            if (digitSequence.Count == 0 && digit == 0)
                continue;

            digitSequence.Add(digit);

            repeatingSequence = FindFirstRepeatingSequence(digitSequence);
            if (repeatingSequence != null)
                break;
        }

        return ToBaseString(repeatingSequence ?? new List<int>());
    }

    private string ToBaseString(List<int> digitSequence)
    {
        StringBuilder stringBuilder = new();
        foreach (var digit in digitSequence)
        {
            stringBuilder.Append(DigitToSymbol[digit]);
        }

        return stringBuilder.ToString();
    }

    private List<int>? FindFirstRepeatingSequence(List<int> digitSequence)
    {
        if (digitSequence.Count <= 1)
            return null;

        int length = 1;
        int terminatingLength = digitSequence.Count / 2;

        while (!VerifyRepeats(digitSequence, length))
        {
            length++;
            if (length > terminatingLength)
            {
                return null;
            }
        }

        return digitSequence.GetRange(0, length);
    }

    private bool VerifyRepeats(List<int> digitSequence, int length)
    {
        int candidateIndex = -1;

        for (int testedIndex = 0; testedIndex < length * 2; testedIndex++)
        {
            candidateIndex++;
            if (candidateIndex >= length)
            {
                candidateIndex = 0;
            }

            if (digitSequence[testedIndex] != digitSequence[candidateIndex])
            {
                return false;
            }
        }

        return true;
    }
}