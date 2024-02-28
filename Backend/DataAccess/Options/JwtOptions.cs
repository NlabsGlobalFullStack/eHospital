namespace DataAccess.Options;
// Jwt sınıfı, JWT (JSON Web Token) ile ilgili konfigürasyon ayarlarını içerir.
public sealed class JwtOptions
{
    // JWT'nin "issuer" (imzalayan) alanını temsil eder. Bu genellikle uygulamanın adını içerir.
    public string Issuer { get; set; } = String.Empty;

    // JWT'nin "audience" (hedef) alanını temsil eder. Bu genellikle uygulamayı kullanacak kişiyi içerir.
    public string Audience { get; set; } = String.Empty;

    // JWT'nin imzalanmasında kullanılacak gizli anahtarı temsil eder.
    public string SecretKey { get; set; } = String.Empty;
}