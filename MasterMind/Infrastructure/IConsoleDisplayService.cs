namespace Mastermind.Infrastructure
{
    public interface IConsoleDisplayService
    {
        void DisplayOutput(string message);
        string GetConsoleInput();
        void ExitApplication();
    }
}