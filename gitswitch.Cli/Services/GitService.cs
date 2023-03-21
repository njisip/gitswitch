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
        /// Starts a git process with specified commands.
        /// </summary>
        /// <param name="args">The git commands and arguments.</param>
        /// <returns>The command output.</returns>
        internal string Start(string args = "")
        {
            _info.Arguments = args;
            return StartGit();
        }

        /// <summary>
        /// Starts a git process with specified commands.
        /// </summary>
        /// <param name="args">The git commands and arguments.</param>
        /// <returns>The command output.</returns>
        internal string Start(string[] args)
        {
            _info.Arguments = string.Join(' ', args);
            return StartGit();
        }

        /// <summary>
        /// Starts the git process.
        /// </summary>
        /// <returns>The command output.</returns>
        private string StartGit()
        {
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
        /// Checks if git is installed on the machine.
        /// </summary>
        /// <returns><see langword="true"/> if git is installed, else <see langword="false"/>.</returns>
        internal bool IsExist()
        {
            if (Start("--version").StartsWith("git version"))
                return true;
            return false;
        }
    }
}
