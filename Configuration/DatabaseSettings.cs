namespace APINotificacionesV2.Configuration
{
    public class DatabaseSettings
    {
        public const string SectionName = "Database";
        public string ConnectionString { get; set; } = default!;
    }
}
