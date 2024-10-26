using System;
using Aspid.UI.Stats.Models;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.Stats.ViewModels
{
    [ViewModel]
    public partial class ReadOnlyStatsViewModel : IDisposable
    {
        [Bind] private int _cool;
        [Bind] private int _power;
        [Bind] private int _reflexes;
        [Bind] private int _intelligence;
        [Bind] private int _technicalAbility;
        
        [Bind] private int _skillPointsAvailable;

        protected readonly Hero Hero;

        public ReadOnlyStatsViewModel(Hero hero)
        {
            Hero = hero;

            _cool = hero.GetNumberSkillPointFrom(Skill.Cool);
            _power = hero.GetNumberSkillPointFrom(Skill.Cool);
            _reflexes = hero.GetNumberSkillPointFrom(Skill.Cool);
            _intelligence = hero.GetNumberSkillPointFrom(Skill.Cool);
            _technicalAbility = hero.GetNumberSkillPointFrom(Skill.Cool);

            _skillPointsAvailable = hero.SkillPointsAvailable;
            Hero.SkillChanged += OnSkillChanged;
        }
        
        private void SetSkillPointsTo(Skill skill, int points)
        {
            switch (skill)
            {
                case Skill.Cool: Cool = points; break;
                case Skill.Power: Power = points; break;
                case Skill.Reflexes: Reflexes = points; break;
                case Skill.Intelligence: Intelligence = points; break;
                case Skill.TechnicalAbility: TechnicalAbility = points; break;
                default: throw new ArgumentOutOfRangeException(nameof(skill), skill, null);
            }
        }
        
        private void OnSkillChanged(Skill skill) 
        {
            SkillPointsAvailable = Hero.SkillPointsAvailable;
            SetSkillPointsTo(skill, Hero.GetNumberSkillPointFrom(skill));
        }
        
        public virtual void Dispose() =>
            Hero.SkillChanged -= OnSkillChanged;
    }
}