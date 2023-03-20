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

            this.SetHandler((key, name, email) =>
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

            }, _keyArg, _nameArg, _emailArg);
        }
    }
}
