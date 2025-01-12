using CleanArchitecture.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.OptionsSetup
{
    public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        public JwtOptionsSetup(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public void Configure(JwtOptions options)
        {
            _configuration.GetSection("JwtSettings").Bind(options);
        }
    }
}
