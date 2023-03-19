using System.Diagnostics;

namespace gitswitch.Cli
{
    public static class Git
    {
        /// <summary>
        /// The git process information.
        /// </summary>
        private static ProcessStartInfo _info;

        /// <summary>
        /// Initializes the fields.
        /// </summary>
        static Git()
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
        /// Starts a git process with specified commands.
        /// </summary>
        /// <param name="args">The git commands and arguments.</param>
        /// <returns>The command output.</returns>
        public static string Start(string args = "")
        {
            _info.Arguments = args;
            return StartGit();
        }

        /// <summary>
        /// Starts a git process with specified commands.
        /// </summary>
        /// <param name="args">The git commands and arguments.</param>
        /// <returns>The command output.</returns>
        public static string Start(string[] args)
        {
            _info.Arguments = string.Join(' ', args);
            return StartGit();
        }

        /// <summary>
        /// Starts the git process.
        /// </summary>
        /// <returns>The command output.</returns>
        static string StartGit()
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
        public static bool IsExist()
        {
            if (Start("--version").StartsWith("git version"))
                return true;
            return false;
        }
    }
}
