namespace GestionInventario.src.Modules.Auths.Config
{
    public class JwtConfig
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}