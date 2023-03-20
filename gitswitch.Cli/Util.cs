namespace gitswitch.Cli
{
    internal static class Util
    {
        /// <summary>
        /// Shows a user name and email.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="email">The user email.</param>
        internal static void ShowUser(string name, string email)
        {
            Console.WriteLine($"Name:\t{name}");
            Console.WriteLine($"Email:\t{email}");
        }
    }
}
