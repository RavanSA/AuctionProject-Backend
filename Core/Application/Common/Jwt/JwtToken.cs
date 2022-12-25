namespace Application.AppSettingsModels
{
    using System;

    public class JwtToken
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}