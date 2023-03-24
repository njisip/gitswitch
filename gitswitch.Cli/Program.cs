using gitswitch.Cli.Commands;
using gitswitch.Cli.Services;
using System.CommandLine;

namespace gitswitch.Cli
{
    internal class Program
    {
        /// <summary>
        /// The git service.
        /// </summary>
        private static readonly GitService _git = GitService.Create();

        /// <summary>
        /// The user service.
        /// </summary>
        private static readonly UserService _userService = UserService.Create();

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
            // Check if git is installed
            if (!_git.IsInstalled)
            {
                Console.WriteLine("git is not installed on your system");
                return 1;
            }

            // Load users
            _userService.LoadUsers();

            // Create root commnand
            _rootCommand = new RootCommand("A tool to easily switch between users in a repository");

            // Add commands
            _rootCommand.AddCommand(new UserCommand(_git, _userService));
            _rootCommand.AddCommand(new InitializeRepositoryCommand(_git, _userService));
            _rootCommand.AddCommand(new CloneRepositoryCommand(_git, _userService));

            // Show help if no commands or arguments given
            if (args == null || !args.Any())
                args = new string[] { "-h" };

            // Pass arguments
            return await _rootCommand.InvokeAsync(args);
        }
    }
}