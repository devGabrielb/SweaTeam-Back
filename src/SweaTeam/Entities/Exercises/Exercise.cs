using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SweaTeam.Common;

namespace SweaTeam.Entities.Exercises
{
    public class Exercise : Entity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Exercise()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }
        public Exercise(string? exrciseMedia, string title, string description, MuscleGroupType primaryMuscleGroup, EquipmentType equipment, IList<MuscleGroupType>? otherMuscleGroup, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            ExrciseMedia = exrciseMedia;
            Title = title;
            Description = description;
            PrimaryMuscleGroup = primaryMuscleGroup;
            Equipment = equipment;
            OtherMuscleGroup = otherMuscleGroup ?? new List<MuscleGroupType>();
        }

        public string? ExrciseMedia { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public MuscleGroupType PrimaryMuscleGroup { get; private set; }
        public IList<MuscleGroupType>? OtherMuscleGroup { get; private set; }
        public EquipmentType Equipment { get; private set; }
    }
}
