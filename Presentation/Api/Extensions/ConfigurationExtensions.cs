namespace Api.Extensions
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static IConfigurationSection GetJwtSecretSection(this IConfiguration configuration)
            => configuration.GetSection("JwtSettings");

    }
}