namespace Mastermind.StringMethods
{
    public static class StringFormatter 
    {
        public static string CapitalizeWord(this string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1, word.Length - 1).ToLower();
        }
    }
}