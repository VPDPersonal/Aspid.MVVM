using System;
using Aspid.MVVM.Stats.Models;

namespace Aspid.MVVM.Stats.ViewModels
{
    [ViewModel]
    public partial class ReadOnlyStatsViewModel : IDisposable
    {
        [OneWayBind] private int _cool;
        [OneWayBind] private int _power;
        [OneWayBind] private int _reflexes;
        [OneWayBind] private int _intelligence;
        [OneWayBind] private int _technicalAbility;
        
        [OneWayBind] private int _skillPointsAvailable;

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
        
        public partial interface IBindableMembers : IReadOnlyStatsViewModel { }
    }
}