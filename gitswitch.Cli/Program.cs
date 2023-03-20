using gitswitch.Cli.Commands;
using System.CommandLine;

namespace gitswitch.Cli
{
    internal class Program
    {
        /// <summary>
        /// The root command: gitsw.
        /// </summary>
        private static RootCommand? _rootCommand;

        /// <summary>
        /// The starting point of the application.
        /// </summary>
        /// <param name="args">The commandline arguments.</param>
        /// <returns>The exit code.</returns>
        static async Task<int> Main(string[] args)
        {
            if (!Git.IsExist())
            {
                Console.WriteLine("git is not installed on your system");
                return 1;
            }

            // Create root commnand
            _rootCommand = new RootCommand("A CLI tool to easily switch between users in a repository");

            // Add commands
            _rootCommand.AddCommand(new UserCommand());

            // Pass arguments
            return await _rootCommand.InvokeAsync(args);
        }
    }
}