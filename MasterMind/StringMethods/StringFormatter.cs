namespace Mastermind.StringMethods
{
    public class StringFormatter
    {
        public static string Capitalize(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1, input.Length - 1);
        }
    }
}