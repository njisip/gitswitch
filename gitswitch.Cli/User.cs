namespace gitswitch.Cli
{
    public class User
    {
        /// <summary>
        /// Gets or sets the key associated with the user.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Creates a new git user.
        /// </summary>
        /// <param name="key">The key associated with the user.</param>
        /// <param name="name">The user name.</param>
        /// <param name="email">The user email.</param>
        public User(string key = "", string name = "", string email = "")
        {
            Key = key;
            Name = name;
            Email = email;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{Key}] {Name} - {Email}";
        }
    }
}
