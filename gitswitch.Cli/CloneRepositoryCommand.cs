using gitswitch.Cli.Services;
using System.CommandLine;

namespace gitswitch.Cli
{
    internal class CloneRepositoryCommand : Command
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
        /// The repository url.
        /// </summary>
        private Argument<string> _repoArg;

        /// <summary>
        /// Creates a command to clone a repository.
        /// </summary>
        public CloneRepositoryCommand(GitService git, UserService userService) 
            : base("clone", "Clone a repository into a new directory")
        {
            _git = git;
            _userService = userService;

            // Initialize arguments
            _repoArg = new Argument<string>("repo", "The url of the repository to clone");            
            AddArgument(_repoArg);

            this.SetHandler((repo) =>
            {
                if (string.IsNullOrEmpty(repo))
                {
                    Console.WriteLine("No repository url given");
                    return;
                }

                Console.WriteLine(_git.CloneRepository(repo));

            }, _repoArg);
        }
    }
}
