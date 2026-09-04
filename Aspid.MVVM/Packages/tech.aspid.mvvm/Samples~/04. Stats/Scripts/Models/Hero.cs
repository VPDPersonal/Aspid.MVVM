using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Stats
{
    // The model owns the rules; the ViewModel only prepares a draft and asks the model to apply it.
    public sealed class Hero
    {
        public const int MinSkillPoints = 1;

        private readonly Dictionary<Skill, int> _skills = new()
        {
            { Skill.Strength, MinSkillPoints },
            { Skill.Agility, MinSkillPoints },
            { Skill.Intelligence, MinSkillPoints },
        };

        public Hero(int pointsAvailable)
        {
            PointsAvailable = pointsAvailable;
        }

        public event Action Changed;

        public int PointsAvailable { get; private set; }

        public int this[Skill skill] => _skills[skill];

        public void Apply(IReadOnlyDictionary<Skill, int> skills, int pointsAvailable)
        {
            foreach (var (skill, points) in skills)
            {
                if (points < MinSkillPoints)
                    throw new ArgumentOutOfRangeException(nameof(skills), $"{skill} can't be below {MinSkillPoints}");

                _skills[skill] = points;
            }

            PointsAvailable = pointsAvailable;
            Changed?.Invoke();
        }
    }
}
