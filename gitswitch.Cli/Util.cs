namespace gitswitch.Cli
{
    internal static class Util
    {
        private static string _notSet = "<not set>";

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
