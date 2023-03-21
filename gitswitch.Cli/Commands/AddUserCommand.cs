using System.CommandLine;

namespace gitswitch.Cli.Commands
{
    internal class AddUserCommand : Command
    {
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
        public AddUserCommand()
            : base("add", "Add new user")
        {
            // Initialize arguments
            _keyArg = new Argument<string>("--key", "The key used to identify the user");
            _nameArg = new Argument<string>("--name", "The user name");
            _emailArg = new Argument<string>("--email", "The user email");
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
                // Check if key already exists
                if (Program.Users!.ContainsKey(key))
                {
                    Console.WriteLine($"The key '{key}' already exists.");
                    return;
                }

                // Add and save user
                Program.Users?.Add(key, new User(key, name, email));
                Program.SaveUsers();
                Console.WriteLine($"User '{name} ({email})' has been added with key '{key}'");

                if (isSwitch)
                {
                    Git.Start($"{Util.LocalNameArguments} \"{name}\"");
                    Git.Start($"{Util.LocalEmailArguments} \"{email}\"");
                }

            }, _keyArg, _nameArg, _emailArg, _switchOption);
        }
    }
}
