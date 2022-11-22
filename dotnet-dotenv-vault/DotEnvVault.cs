namespace dotenv_vault.net
{
    public static class DotEnvVault
    {
        public static void Load()
        {
            Helpers.ReadAndWrite(new LoadOptions());
        }
    }
}