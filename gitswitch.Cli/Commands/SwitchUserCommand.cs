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
        /// Flag to switch global user.
        /// </summary>
        private Option<bool> _globalOption;

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

            // Initialize options
            _globalOption = new Option<bool>("--global", "Switch global user");
            _globalOption.SetDefaultValue(false);
            _globalOption.AddAlias("-g");
            AddOption(_globalOption);

            this.SetHandler((key, isGlobal) =>
            {
                // Get user from collection
                var user = _userService.GetUser(key);

                // Check if user exists
                if (user is null)
                {
                    Console.WriteLine($"The user with key '{key}' does not exist");
                    return;
                }

                if (!isGlobal)
                {
                    // Switch local user
                    _git.LocalUser = user;
                    Console.WriteLine("The local user was switched to:");                    
                }
                else
                {
                    // Switch global user
                    _git.GlobalUser = user;
                    Console.WriteLine("The global user was switched to:");
                }
                _userService.ShowUser(user);

            }, _keyArg, _globalOption);
        }
    }
}
