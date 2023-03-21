using gitswitch.Cli.Services;
using System.CommandLine;

namespace gitswitch.Cli.Commands
{
    internal class UserCommand : Command
    {
        /// <summary>
        /// The git service.
        /// </summary>
        private readonly GitService _git;

        /// <summary>
        /// Flag to show global user information.
        /// </summary>
        private Option<bool> _globalOption;

        /// <summary>
        /// Flag to show all existing users.
        /// </summary>
        private Option<bool> _allUsersOption;

        /// <summary>
        /// Creates a user command.
        /// </summary>
        public UserCommand(GitService git)
            : base("user", "Show user information")
        {
            _git = git;

            // Initialize options
            _globalOption = new Option<bool>("--global", "Show global user information");
            _globalOption.SetDefaultValue(false);
            _globalOption.AddAlias("-g");
            AddOption(_globalOption);

            _allUsersOption = new Option<bool>("--all", "Show all existing users");
            _allUsersOption.SetDefaultValue(false);
            _allUsersOption.AddAlias("-a");
            AddOption(_allUsersOption);

            // Initialize sub-commands
            AddCommand(new AddUserCommand(_git));

            // Initialize handler
            this.SetHandler((isGlobal, isAllUsers) =>
            {
                // Check if all users
                if (isAllUsers)
                {
                    ShowAllUsers();
                    return;
                }

                // Check if global user
                if (isGlobal)
                    ShowGlobalUser();
                else
                    ShowLocalUser();
            }, _globalOption, _allUsersOption);
        }

        /// <summary>
        /// Shows the local user.
        /// </summary>
        private void ShowLocalUser()
        {
            var name = _git.Start(Util.LocalNameArguments);
            var email = _git.Start(Util.LocalEmailArguments);
            Console.WriteLine("Local user");
            Util.ShowUser(name, email);
        }

        /// <summary>
        /// Shows the global user.
        /// </summary>
        private void ShowGlobalUser()
        {
            var name = _git.Start(Util.GlobalNameArguments);
            var email = _git.Start(Util.GlobalEmailArguments);
            Console.WriteLine("Global user");
            Util.ShowUser(name, email);
        }

        /// <summary>
        /// Shows all existing users.
        /// </summary>
        private void ShowAllUsers()
        {
            foreach (var user in Program.Users?.Values!)
                Util.ShowUser(user.Name, user.Email, user.Key);
        }
    }
}
