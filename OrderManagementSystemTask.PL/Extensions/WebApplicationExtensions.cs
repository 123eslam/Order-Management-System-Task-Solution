using OrderManagementSystemTask.DAL.Presistance.Data.DataSeeding;

namespace OrderManagementSystemTask.PL.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await dbIntializer.IntializIdentityAsync();
            return app;
        }
    }
}
