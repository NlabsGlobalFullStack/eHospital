using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DataAccess.Options;
// JwtSetupOptions sınıfı, JwtBearerOptions sınıfını yapılandırmak için kullanılır.
public sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        configuration.GetSection("Jwt").Bind(options);
    }
}