namespace AioiTest.Helper
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; }

        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static string GetEncryptionKey()
        {
            return Configuration["EncryptionSettings:AESKey"];
        }
    }
}
