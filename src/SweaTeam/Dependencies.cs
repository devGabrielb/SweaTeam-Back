using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SweaTeam.Infra;

namespace SweaTeam
{
    public static class Dependencies
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SweaTeamContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        }
    }
}
