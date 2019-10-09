namespace Mastermind.StringMethods
{
    public static class StringFormatter
    {
        public static string CapitalizeWord(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1, input.Length - 1).ToLower();
        }
    }
}