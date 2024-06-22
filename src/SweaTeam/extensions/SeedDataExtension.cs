using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SweaTeam.Entities.Exercises;
using SweaTeam.Infra;

namespace SweaTeam.extensions
{
    public static class SeedDataExtension
    {
        public static void SeedData(this IApplicationBuilder app)
        {

            ArgumentNullException.ThrowIfNull(app);
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<SweaTeamContext>();


            SeedExercises(context);
            context.SaveChanges();

        }

        private static void SeedExercises(SweaTeamContext context)
        {

            context.AddRange(new List<Exercise>{
                new Exercise(
                    "https://youtu.be/SCVCLChPQFY",
                    "Bench Press",
                    "The bench press is an upper-body weight training exercise in which the trainee presses a weight upwards while lying on a weight training bench.",
                    MuscleGroupType.Chest,
                    EquipmentType.Barbell,
                    new List<MuscleGroupType>{MuscleGroupType.Triceps, MuscleGroupType.Shoulders}
                    ),
            });
        }
    }
}