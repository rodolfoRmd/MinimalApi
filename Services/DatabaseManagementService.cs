using  MinimalApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Services;

public static class DatabaseManagementService
{
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDb  = serviceScope.ServiceProvider
                             .GetService<AppDBContext>();
                             
            serviceDb.Database.Migrate();
        }
    }
}
