using System.Text.Json;

namespace gitswitch.Cli.Services
{
    internal class UserService
    {
        /// <summary>
        /// The user service singleton instance.
        /// </summary>
        private static readonly UserService _userService = new UserService();

        /// <summary>
        /// The path of the users file.
        /// </summary>
        private readonly string _usersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");

        /// <summary>
        /// The placeholder when a value is not set.
        /// </summary>
        private readonly string _notSetPlaceholder = "<not set>";

        /// <summary>
        /// Gets the mapping of users.
        /// </summary>
        internal Dictionary<string, User> Users { get; private set; }

        /// <summary>
        /// Creates a user service
        /// </summary>
        private UserService()
        {
            Users = new();
        }

        /// <summary>
        /// Creates the user service.
        /// </summary>
        /// <returns>The user service.</returns>
        internal static UserService Create()
        {
            return _userService;
        }

        /// <summary>
        /// Loads the user mapping.
        /// </summary>
        internal void LoadUsers()
        {
            Users = JsonSerializer.Deserialize<Dictionary<string, User>>(File.ReadAllText(_usersPath)) ?? new();
        }

        /// <summary>
        /// Tries to add a new user to the mapping.
        /// </summary>
        /// <param name="user">The new user to add.</param>
        /// <returns><see langword="true"/> if successful, else <see langword="false"/>.</returns>
        internal bool TryAddUser(User user)
        {
            if (Users.ContainsKey(user.Key))
                return false;

            Users.Add(user.Key, user);
            return true;
        }

        /// <summary>
        /// Saves the user mapping to a file.
        /// </summary>
        internal void SaveUsers()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(Users, options);
            File.WriteAllText(_usersPath, json);
        }

        /// <summary>
        /// Gets the user specified by the key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The specified user, else <see langword="null"/>.</returns>
        internal User? GetUser(string key)
        {
            if (Users.TryGetValue(key, out User? user))
                return user;
            return null;
        }

        /// <summary>
        /// Shows a user information.
        /// </summary>
        /// <param name="user">The user.</param>
        internal void ShowUser(User user)
        {
            var name = string.IsNullOrEmpty(user.Name) ? _notSetPlaceholder : user.Name;
            var email = string.IsNullOrEmpty(user.Email) ? _notSetPlaceholder : user.Email;

            if (!string.IsNullOrEmpty(user.Key))
                Console.WriteLine($"[{user.Key}]");
            Console.WriteLine($"Name:\t{name}");
            Console.WriteLine($"Email:\t{email}");
        }
    }
}
