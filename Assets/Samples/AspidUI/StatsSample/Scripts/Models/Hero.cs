using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AspidUI.StatsSample.Models
{
    public class Hero
    {
        public event Action<Skill> SkillChanged;

        private int _skillPointsAvailable;
        private readonly Dictionary<Skill, int> _skills;

        public int SkillPointsAvailable
        {
            get => _skillPointsAvailable;
            private set
            {
                ThrowExceptionIfValueLessThanZero(value);
                _skillPointsAvailable = value;
            }
        }

        public Hero(int skillPointsAvailable)
        {
            ThrowExceptionIfValueLessThanZero(skillPointsAvailable);

            _skills = new Dictionary<Skill, int>()
            {
                { Skill.Cool, 1 },
                { Skill.Power, 1 },
                { Skill.Reflexes, 1 },
                { Skill.Intelligence, 1 },
                { Skill.TechnicalAbility, 1 },
            };
            
            SkillPointsAvailable = skillPointsAvailable;
        }

        public void AddSkillPointTo(Skill skill)
        {
            if (SkillPointsAvailable == 0) return;
            
            _skills[skill]++;
            SkillPointsAvailable--;
            
            SkillChanged?.Invoke(skill);
        }

        public void SetSkillPointTo(Skill skill, int value)
        {
            ThrowExceptionIfValueLessThanZero(value);
            
            var number = GetNumberSkillPointFrom(skill);

            var delta = value - number;
            if (delta == 0) return;
            if (number + delta < 1) return;
            if (SkillPointsAvailable - delta < 0) return;

            _skills[skill] = value;
            SkillPointsAvailable -= delta;
            
            SkillChanged?.Invoke(skill);
        }

        public int GetNumberSkillPointFrom(Skill skill) => _skills[skill];

        private static void ThrowExceptionIfValueLessThanZero(int value, [CallerMemberName] string name = "")
        {
            if (value < 0)
                throw new ArgumentException($"{name} can't be less than 0. {name} = {value}");
        }
    }
}