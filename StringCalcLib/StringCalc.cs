namespace StringCalcLib;

using System.Text.RegularExpressions;

public static class MeSpecialExtensions {
    public static int SumAsIntButNoNegativeAndIgnoreOverAThousand(this int[] numbers) {
        int sum = 0;

        foreach (int num in numbers) {
            if (num <= 1000) {
                sum += num;
            }

            if (num < 0) {
                throw new ArgumentException("Negative values not allowed!");
            }
        }

        return sum;
    }

    public static (string, string[]) SeparateDelimsFromString(this string input, string[] defaultDelims) {
        List<string> delims = new List<string>(defaultDelims);
        foreach (Match match in Regex.Matches(input, @"\[(.*?)\]"))
        {
            delims.Add(match.Groups[1].Value);
        }

        foreach (Match match in Regex.Matches(input, @"\/\/(.)"))
        {
            delims.Add(match.Groups[1].Value);
        }

        return (Regex.Replace(input, @"\[.*?\]|\/\/.", ""), delims.ToArray());
    }
}

public class StringCalc
{	
    public int Add(string input)
    {
        if (input == null)
        {
            return 0;
        }

        string mainInput;
        string[] delims;
        (mainInput, delims) = input.SeparateDelimsFromString(new string[]{",", "\n"});
        return Array.ConvertAll(mainInput.Split(delims, StringSplitOptions.RemoveEmptyEntries), int.Parse).SumAsIntButNoNegativeAndIgnoreOverAThousand();
    }
}
