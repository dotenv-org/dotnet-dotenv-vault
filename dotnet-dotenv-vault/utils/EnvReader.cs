using System;

namespace dotnet_dotenv_vault
{
    public static class EnvReader
    {
        /// <summary>
        /// Determine if an environment key has a set value or not
        /// </summary>
        /// <param name="key">The key to retrieve the value via</param>
        /// <returns>A value determining if a value is set or not</returns>
        public static bool HasValue(string key)
        {
            var retrievedValue = Environment.GetEnvironmentVariable(key);
            return !string.IsNullOrEmpty(retrievedValue);
        }
    }
}