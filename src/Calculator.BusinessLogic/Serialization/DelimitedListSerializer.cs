using System.Globalization;

namespace Calculator.BusinessLogic.Serialization;

public class DelimitedListSerializer : IListSerializer
{
    public string[] Delimiters { get; set; } = [","];

    public List<double> Deserialize(string serializedNumbers)
    {
        if (string.IsNullOrWhiteSpace(serializedNumbers))
            return new List<double> { 0 };

        var delimiters = Delimiters;
        var numbersPart = serializedNumbers;

        if (serializedNumbers.StartsWith("//["))
        {
            var delimiterList = new List<string>();
            var index = 2; 

            while (index < serializedNumbers.Length && serializedNumbers[index] == '[')
            {
                var end = serializedNumbers.IndexOf(']', index);
                if (end < 0)
                    throw new InvalidOperationException("Invalid custom delimiter format.");

                delimiterList.Add(
                    serializedNumbers.Substring(index + 1, end - index - 1));

                index = end + 1;
            }

            if (index >= serializedNumbers.Length || serializedNumbers[index] != '\n')
                throw new InvalidOperationException("Invalid custom delimiter format.");

            delimiters = delimiterList.ToArray();
            numbersPart = serializedNumbers.Substring(index + 1);
        }

        else if (serializedNumbers.StartsWith("//"))
        {
            if (serializedNumbers.Length < 4 || serializedNumbers[3] != '\n')
                throw new InvalidOperationException("Invalid custom delimiter format.");

            delimiters = new[] { serializedNumbers[2].ToString() };
            numbersPart = serializedNumbers.Substring(4);
        }

        var parts = numbersPart.Split(
            delimiters,
            StringSplitOptions.None);

        var result = new List<double>(parts.Length);

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                result.Add(0);
                continue;
            }

            if (double.TryParse(
                part,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var value))
            {
                result.Add(value);
            }
            else
            {
                result.Add(0);
            }
        }

        return result;
    }
}