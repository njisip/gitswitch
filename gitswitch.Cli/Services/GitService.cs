using System.Diagnostics;

namespace gitswitch.Cli.Services
{
    internal class GitService
    {
        /// <summary>
        /// The git singleton instance.
        /// </summary>
        private static readonly GitService _git = new GitService();

        /// <summary>
        /// The git process information.
        /// </summary>
        private ProcessStartInfo _info;

        #region Git command arguments

        private readonly string _gitVersionArg = "--version";
        private readonly string _localNameArg = "config user.name";
        private readonly string _localEmailArg = "config user.email";
        private readonly string _globalNameArg = "config --global user.name";
        private readonly string _globalEmailArg = "config --global user.email";
        private readonly string _gitInitArg = "init";

        #endregion

        /// <summary>
        /// Gets or sets the local git user.
        /// </summary>
        internal User LocalUser
        {
            get
            {
                return new User
                {
                    Name = Run(_localNameArg),
                    Email = Run(_localEmailArg)
                };
            }
            set
            {
                Run($"{_localNameArg} \"{value.Name}\"");
                Run($"{_localEmailArg} \"{value.Email}\"");
            }
        }

        /// <summary>
        /// Gets or sets the global git user.
        /// </summary>
        internal User GlobalUser
        {
            get
            {
                return new User
                {
                    Name = Run(_globalNameArg),
                    Email = Run(_globalEmailArg)
                };
            }
            set
            {
                Run($"{_globalNameArg} \"{value.Name}\"");
                Run($"{_globalEmailArg} \"{value.Email}\"");
            }
        }

        /// <summary>
        /// Gets the value indicating whether git is installed on the machine.
        /// </summary>
        internal bool IsInstalled
        {
            get
            {
                if (Run(_gitVersionArg).StartsWith("git version"))
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Initializes the fields.
        /// </summary>
        private GitService()
        {
            _info = new ProcessStartInfo
            {
                FileName = "git",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
        }

        /// <summary>
        /// Creates the git service.
        /// </summary>
        /// <returns>The git instance.</returns>
        internal static GitService Create()
        {
            return _git;
        }

        /// <summary>
        /// Starts the git process.
        /// </summary>
        /// <returns>The command output.</returns>
        private string Run(string arguments = "")
        {
            // Set arguments
            _info.Arguments = arguments;

            var output = "";
            using (var process = new Process { StartInfo = _info })
            {
                // Start the process
                process.Start();

                // Compile the output
                while (!process.StandardOutput.EndOfStream)
                    output += $"{process.StandardOutput.ReadLine()}\n";
            }
            return output.Trim();
        }

        /// <summary>
        /// Initializes a git repository.
        /// </summary>
        internal string InitializeRepository()
        {
            return Run(_gitInitArg);
        }
    }
}
