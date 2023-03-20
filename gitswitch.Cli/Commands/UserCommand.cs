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
        /// Creates a user command.
        /// </summary>
        public UserCommand()
            : base("user", "Show user information")
        {
            // Initialize options
            _globalOption = new Option<bool>("--global", "Flag to show global user information");
            _globalOption.SetDefaultValue(false);
            _globalOption.AddAlias("-g");
            AddOption(_globalOption);

            // Initialize handler
            this.SetHandler((isGlobal) =>
            {
                // Check if global user
                if (isGlobal)
                    ShowGlobalUser();
                else
                    ShowLocalUser();
            }, _globalOption);
        }

        /// <summary>
        /// Shows the local user.
        /// </summary>
        private void ShowLocalUser()
        {
            var name = Git.Start(_localNameArguments);
            var email = Git.Start(_localEmailArguments);
            Util.ShowUser(name, email);
        }

        /// <summary>
        /// Shows the global user.
        /// </summary>
        private void ShowGlobalUser()
        {
            var name = Git.Start(_globalNameArguments);
            var email = Git.Start(_globalEmailArguments);
            Util.ShowUser(name, email);;
        }
    }
}
