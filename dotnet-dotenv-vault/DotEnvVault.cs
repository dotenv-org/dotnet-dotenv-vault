using dotenv.net;
using System;

namespace dotenv_vault.net
{
    public static class DotEnvVault
    {
        public static void Load()
        {
            if (IsUsingVault())
                Helpers.ReadAndWrite(new LoadOptions());
            else
                DotEnv.Load();
        }

        private static bool IsUsingVault()
        {
            var dotenvKeyValue = Environment.GetEnvironmentVariable("DOTENV_KEY");

            if (!string.IsNullOrEmpty(dotenvKeyValue))
                return false;

            return true;
        }
    }
}