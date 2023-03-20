﻿using gitswitch.Cli.Commands;
using System.CommandLine;
using System.Text.Json;

namespace gitswitch.Cli
{
    internal class Program
    {
        private static string _usersPath = "users.json";

        /// <summary>
        /// The root command: gitsw.
        /// </summary>
        private static RootCommand? _rootCommand;

        /// <summary>
        /// The list of users.
        /// </summary>
        private static Dictionary<string, User>? _users;

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
            _users = JsonSerializer.Deserialize<Dictionary<string, User>>(json)!;

            // Create root commnand
            _rootCommand = new RootCommand("A CLI tool to easily switch between users in a repository");

            // Add commands
            _rootCommand.AddCommand(new UserCommand());

            // Show help if no commands or arguments given
            if (args == null || !args.Any())
                args = new string[] { "-h" };

            // Pass arguments
            return await _rootCommand.InvokeAsync(args);
        }
    }
}