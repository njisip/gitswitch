namespace gitswitch.Cli
{
    internal static class Util
    {
        private static string _notSet = "<not set>";

        /// <summary>
        /// Shows a user name and email.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="email">The user email.</param>
        internal static void ShowUser(string name, string email)
        {
            name = string.IsNullOrEmpty(name) ? _notSet : name;
            email = string.IsNullOrEmpty(email) ? _notSet : email;

            Console.WriteLine($"Name:\t{name}");
            Console.WriteLine($"Email:\t{email}");
        }
    }
}
