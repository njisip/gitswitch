using gitswitch.Cli.Services;
using System.CommandLine;

namespace gitswitch.Cli.Commands
{
    internal class SwitchUserCommand : Command
    {
        /// <summary>
        /// The git service.
        /// </summary>
        private readonly GitService _git;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly UserService _userService;

        /// <summary>
        /// The key argument.
        /// </summary>
        private Argument<string> _keyArg;

        /// <summary>
        /// Creates switch user command.
        /// </summary>
        public SwitchUserCommand(GitService git, UserService userService) 
            : base("switch", "Switch user to the specified user")
        {
            _git = git;
            _userService = userService;

            // Initialize arguments
            _keyArg = new Argument<string>("key", "The key used to identify the user");
            AddArgument(_keyArg);

            this.SetHandler((key) =>
            {
                // Get user from collection
                var user = _userService.GetUser(key);

                // Check if user exists
                if (user is null)
                    Console.WriteLine($"The user with key '{key}' does not exist.");
                else
                {
                    // Switch user
                    _git.LocalUser = user;
                    Console.WriteLine("The local user was switched to:");
                    _userService.ShowUser(user);
                }

            }, _keyArg);
        }
    }
}
