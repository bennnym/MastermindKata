using System;

namespace Mastermind.Infrastructure
{
    public class ConsoleIoService : IConsoleDisplayService
    {
        public void DisplayOutput(string message)
        {
            Console.WriteLine(message);
        }

        public string GetConsoleInput()
        {
            return Console.ReadLine();
        }

        public void ExitApplication() 
        {
            Environment.Exit(0); 
        }
    }
}