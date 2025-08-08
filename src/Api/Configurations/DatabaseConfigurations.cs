using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations;

public static class DatabaseConfigurations
{
    public static void ConfigureDataBase(this WebApplicationBuilder builder)
    {
        var connectionStringMs = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionStringMs));
    }
}
