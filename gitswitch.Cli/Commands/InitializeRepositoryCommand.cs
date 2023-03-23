using gitswitch.Cli.Services;
using System.CommandLine;

namespace gitswitch.Cli.Commands
{
    internal class InitializeRepositoryCommand : Command
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
        /// The user key.
        /// </summary>
        private Option<string?> _userOption;

        /// <summary>
        /// Creates a init repository command.
        /// </summary>
        public InitializeRepositoryCommand(GitService git, UserService userService)
            : base("init", "Create an empty Git repository or reinitialize an existing one")
        {
            _git = git;
            _userService = userService;

            // Initialize options
            _userOption = new Option<string?>("--user", "The key of the user to initialize a repository with");
            _userOption.AddAlias("-u");
            _userOption.ArgumentHelpName = "key";
            AddOption(_userOption);

            this.SetHandler((key) =>
            {
                var hasKey = !string.IsNullOrEmpty(key);

                // Check if is not given
                if (!hasKey)
                {
                    InitializeRepository();
                    return;
                }

                // Get user
                var user = _userService.GetUser(key!);
                if (user is not null)
                {                    
                    InitializeRepository();

                    // Set user
                    _git.LocalUser = user;
                    Console.WriteLine($"Set with user '{user.Name} ({user.Email})'");
                }
                else
                    Console.WriteLine($"The user with key '{key}' does not exist");
            }, _userOption);
        }

        /// <summary>
        /// Initialize or reinitialize a git repository.
        /// </summary>
        private void InitializeRepository()
        {
            Console.WriteLine(_git.InitializeRepository());
        }
    }
}
