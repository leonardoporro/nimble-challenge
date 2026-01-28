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

        // Step 7: custom delimiter of any length: //[delimiter]\n
        if (serializedNumbers.StartsWith("//["))
        {
            var endIndex = serializedNumbers.IndexOf("]\n", StringComparison.Ordinal);
            if (endIndex < 0)
                throw new InvalidOperationException("Invalid custom delimiter format.");

            var customDelimiter =
                serializedNumbers.Substring(3, endIndex - 3);

            delimiters = new[] { customDelimiter };
            numbersPart = serializedNumbers.Substring(endIndex + 2);
        }
        // Step 6: custom single-character delimiter: //d\n
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
