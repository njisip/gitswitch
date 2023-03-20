namespace gitswitch.Cli
{
    internal static class Util
    {
        private static string _notSet = "<not set>";

        /// <summary>
        /// Git arguments to get or set local name.
        /// </summary>
        internal static string LocalNameArguments = "config user.name";

        /// <summary>
        /// Git arguments to get or set local email.
        /// </summary>
        internal static string LocalEmailArguments = "config user.email";

        /// <summary>
        /// Git arguments to get or set global name.
        /// </summary>
        internal static string GlobalNameArguments = "config --global user.name";

        /// <summary>
        /// Git arguments to get or set global email.
        /// </summary>
        internal static string GlobalEmailArguments = "config --global user.email";

        /// <summary>
        /// Shows a user information.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="email">The user email.</param>
        /// <param name="key">The user key.</param>
        internal static void ShowUser(string name, string email, string? key = null)
        {
            name = string.IsNullOrEmpty(name) ? _notSet : name;
            email = string.IsNullOrEmpty(email) ? _notSet : email;

            if (!string.IsNullOrEmpty(key))
                Console.WriteLine($"[{key}]");
            Console.WriteLine($"Name:\t{name}");
            Console.WriteLine($"Email:\t{email}");
        }
    }
}
