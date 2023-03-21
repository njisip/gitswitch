using gitswitch.Cli.Commands;
using System.CommandLine;
using System.Text.Json;

namespace gitswitch.Cli
{
    internal class Program
    {
        private static string _usersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");

        /// <summary>
        /// The root command: gitsw.
        /// </summary>
        private static RootCommand? _rootCommand;

        /// <summary>
        /// Gets the collection of users.
        /// </summary>
        internal static Dictionary<string, User>? Users { get; private set; }

        /// <summary>
        /// The starting point of the application.
        /// </summary>
        /// <param name="args">The commandline arguments.</param>
        /// <returns>The exit code.</returns>
        static async Task<int> Main(string[] args)
        {
            // Check if git is installed
            if (!Git.IsExist())
            {
                Console.WriteLine("git is not installed on your system");
                return 1;
            }

            // Load users
            var json = File.ReadAllText(_usersPath);
            Users = JsonSerializer.Deserialize<Dictionary<string, User>>(json)!;

            // Create root commnand
            _rootCommand = new RootCommand("A tool to easily switch between users in a repository");

            // Add commands
            _rootCommand.AddCommand(new UserCommand());

            // Show help if no commands or arguments given
            if (args == null || !args.Any())
                args = new string[] { "-h" };

            // Pass arguments
            return await _rootCommand.InvokeAsync(args);
        }

        /// <summary>
        /// Saves the users collection to a file.
        /// </summary>
        internal static void SaveUsers()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(Users, options);
            File.WriteAllText(_usersPath, json);
        }
    }
}