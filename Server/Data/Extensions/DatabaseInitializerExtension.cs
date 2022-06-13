namespace Server.Data.Extensions
{
    public static class DatabaseInitializerExtension
    {
        public static async Task UseDatabaseInitializer(this WebApplication app)
        {
            using var scoped = app.Services.CreateScope();

            var services = scoped.ServiceProvider;

            var dbInitializer = services.GetRequiredService<DatabaseInitializer>();
            ;
            await dbInitializer.Run();
        }
    }
}