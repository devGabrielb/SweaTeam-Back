using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SweaTeam.Infra;

namespace SweaTeam.extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SweaTeamContext>();

            context.Database.Migrate();
        }
    }
}