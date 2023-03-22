using gitswitch.Cli.Services;
using System.CommandLine;

namespace gitswitch.Cli.Commands
{
    internal class AddUserCommand : Command
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
        /// The user name argument.
        /// </summary>
        private Argument<string> _nameArg;

        /// <summary>
        /// The email argument.
        /// </summary>
        private Argument<string> _emailArg;

        /// <summary>
        /// The option to switch to user after adding.
        /// </summary>
        private Option<bool> _switchOption;

        /// <summary>
        /// Creates a add user command.
        /// </summary>
        public AddUserCommand(GitService git, UserService userService)
            : base("add", "Add new user")
        {
            _git = git;
            _userService = userService;

            // Initialize arguments
            _keyArg = new Argument<string>("key", "The key used to identify the user");
            _nameArg = new Argument<string>("name", "The user name");
            _emailArg = new Argument<string>("email", "The user email");
            AddArgument(_keyArg);
            AddArgument(_nameArg);
            AddArgument(_emailArg);

            // Initialize options
            _switchOption = new Option<bool>("--switch", "Switch to user after adding");
            _switchOption.SetDefaultValue(false);
            _switchOption.AddAlias("-s");
            AddOption(_switchOption);

            this.SetHandler((key, name, email, isSwitch) =>
            {
                var userToAdd = new User(key, name, email);

                // Try to add user
                if (!_userService.TryAddUser(userToAdd))
                {
                    Console.WriteLine($"The key '{key}' already exists.");
                    return;
                }

                // Save users to file
                _userService.SaveUsers();
                Console.WriteLine($"User '{name} ({email})' has been added with key '{key}'");

                // Check if needed to switch to the added user
                if (isSwitch)
                    _git.LocalUser = userToAdd;

            }, _keyArg, _nameArg, _emailArg, _switchOption);
        }
    }
}
