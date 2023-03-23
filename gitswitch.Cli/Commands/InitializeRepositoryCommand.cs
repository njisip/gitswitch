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
        /// Creates a init repository command.
        /// </summary>
        public InitializeRepositoryCommand(GitService git) 
            : base("init", "Create an empty Git repository or reinitialize an existing one")
        {
            _git = git;

            this.SetHandler(() =>
            {
                Console.WriteLine(_git.InitializeRepository());
            });
        }
    }
}
