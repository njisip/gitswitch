using System.CommandLine;

namespace gitswitch.Cli.Commands
{
    internal class UserCommand : Command
    {
        private string _localNameArguments = "config user.name";
        private string _localEmailArguments = "config user.email";
        private string _globalNameArguments = "config --global user.name";
        private string _globalEmailArguments = "config --global user.email";

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
        public UserCommand()
            : base("user", "Show user information")
        {
            // Initialize options
            _globalOption = new Option<bool>("--global", "Show global user information");
            _globalOption.SetDefaultValue(false);
            _globalOption.AddAlias("-g");
            AddOption(_globalOption);

            _allUsersOption = new Option<bool>("--all", "Show all existing users");
            _allUsersOption.SetDefaultValue(false);
            _allUsersOption.AddAlias("-a");
            AddOption(_allUsersOption);

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
            var name = Git.Start(_localNameArguments);
            var email = Git.Start(_localEmailArguments);
            Console.WriteLine("Local user");
            Util.ShowUser(name, email);
        }

        /// <summary>
        /// Shows the global user.
        /// </summary>
        private void ShowGlobalUser()
        {
            var name = Git.Start(_globalNameArguments);
            var email = Git.Start(_globalEmailArguments);
            Console.WriteLine("Global user");
            Util.ShowUser(name, email);;
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
