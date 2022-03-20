using System.Text;

namespace NParasiticNumberEndingInN;

public class ParasiticNumberFinder
{
    private readonly int _trailingDigit;
    private readonly int _numberBase;

    public ParasiticNumberFinder(int trailingDigit, int numberBase)
    {
        _trailingDigit = trailingDigit;
        _numberBase = numberBase;
    }

    public string Calculate()
    {
        double dividend = _trailingDigit;
        double divider = _trailingDigit * _numberBase - 1;
        List<int> digitSequence = new();
        List<int>? repeatingSequence = null;

        while (repeatingSequence == null)
        {
            int nextDigit = (int)(dividend / divider);
            if (nextDigit != 0 || digitSequence.Count > 0)
            {
                digitSequence.Add(nextDigit);
            }

            dividend -= nextDigit * divider;
            dividend *= _numberBase;

            if (digitSequence.Count > 1 && digitSequence.Last() == _trailingDigit)
            {
                repeatingSequence = FindFirstRepeatingSequence(digitSequence);
            }
        }

        return ToBaseString(repeatingSequence);
    }

    private List<int>? FindFirstRepeatingSequence(List<int> digitSequence)
    {
        for (int digitIndex = 1; digitIndex < digitSequence.Count / 2; digitIndex++)
        {
            if (digitSequence[digitIndex] == _trailingDigit && VerifyRepeats(digitSequence, digitIndex + 1))
            {
                return digitSequence.GetRange(0, digitIndex + 1);
            }
        }

        return null;
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

    private string ToBaseString(List<int> digitSequence)
    {
        StringBuilder stringBuilder = new();
        foreach (var digit in digitSequence)
        {
            char symbol = digit < 10 
                ? (char)(digit + 48) 
                : (char)(digit + 55);
            stringBuilder.Append(symbol);
        }

        return stringBuilder.ToString();
    }
}