namespace dotnet_dotenv_vault
{
    public static class DotEnvVault
    {
        public static void Load()
        {
            Helpers.ReadAndWrite(new LoadOptions());
        }
    }
}