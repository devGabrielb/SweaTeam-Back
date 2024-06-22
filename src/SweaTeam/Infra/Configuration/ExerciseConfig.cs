using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SweaTeam.Entities.Exercises;

namespace SweaTeam.Infra.Configuration
{
    public class ExerciseConfig
    {
        private static readonly char[] separator = new[] { ',' };

        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            builder.HasKey(e => e.Id);
#pragma warning restore CA1062 // Validate arguments of public methods

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.ExrciseMedia)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.OtherMuscleGroup)
                .HasConversion(
                    v => string.Join(',', v!.Select(mg => mg.ToString())),
                    v => v.Split(separator).Select(s => (MuscleGroupType)Enum.Parse(typeof(MuscleGroupType), s)).ToList()
                );

            builder.Property(e => e.PrimaryMuscleGroup)
            .HasColumnType("int")
            .HasConversion(p => p.ToString(), p => (MuscleGroupType)Enum.Parse(typeof(MuscleGroupType), p))
                .IsRequired();

            builder.Property(e => e.Equipment)
                .HasColumnType("int")
                .HasConversion(e => e.ToString(), p => (EquipmentType)Enum.Parse(typeof(EquipmentType), p))
                .IsRequired();
        }
    }
}
