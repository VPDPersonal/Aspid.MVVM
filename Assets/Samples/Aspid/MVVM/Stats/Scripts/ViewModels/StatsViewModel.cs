using System;

namespace Aspid.MVVM.Stats
{
    [ViewModel]
    public partial class StatsViewModel : IDisposable
    {
        [OneWayBind] private int _cool;
        [OneWayBind] private int _power;
        [OneWayBind] private int _reflexes;
        [OneWayBind] private int _intelligence; 
        [OneWayBind] private int _technicalAbility;
        
        [OneWayBind] private int _skillPointsAvailable;
        [OneWayBind] private bool _isDraft;
        
        private readonly Hero _hero;

        public StatsViewModel(Hero hero)
        {
            _hero = hero;

            ResetToDefault();
            Subscribe();
        }
        
        private void Subscribe() => 
            _hero.SkillChanged += OnSkillChanged;

        private void Unsubscribe() =>
            _hero.SkillChanged -= OnSkillChanged;

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
        
        private int GetNumberSkillPointFrom(Skill skill) => skill switch
        {
            Skill.Cool => Cool, 
            Skill.Power => Power,
            Skill.Reflexes => Reflexes, 
            Skill.Intelligence => Intelligence, 
            Skill.TechnicalAbility => TechnicalAbility, 
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
        };
        
        [RelayCommand(CanExecute = nameof(IsDraft))]
        private void Confirm()
        {
            _hero.SetSkillPointTo(Skill.Cool, Cool);
            _hero.SetSkillPointTo(Skill.Power, Power);
            _hero.SetSkillPointTo(Skill.Reflexes, Reflexes);
            _hero.SetSkillPointTo(Skill.Intelligence, Intelligence);
            _hero.SetSkillPointTo(Skill.TechnicalAbility, TechnicalAbility);
            
            IsDraft = false;
            if (_hero.SkillPointsAvailable != SkillPointsAvailable)
                throw new Exception();
        }
        
        [RelayCommand(CanExecute = nameof(IsDraft))]
        private void ResetToDefault()
        {
            Cool = _hero.GetNumberSkillPointFrom(Skill.Cool);
            Power = _hero.GetNumberSkillPointFrom(Skill.Power);
            Reflexes = _hero.GetNumberSkillPointFrom(Skill.Reflexes);
            Intelligence = _hero.GetNumberSkillPointFrom(Skill.Intelligence);
            TechnicalAbility = _hero.GetNumberSkillPointFrom(Skill.TechnicalAbility);
        
            SkillPointsAvailable = _hero.SkillPointsAvailable;
        }
        
        [RelayCommand(CanExecute = nameof(CanAddSkillPointTo))]
        private void AddSkillPointTo(Skill skill)
        {
            if (SkillPointsAvailable == 0) return;
        
            SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) + 1);
            SkillPointsAvailable--;
        }

        private bool CanAddSkillPointTo() =>
            SkillPointsAvailable > 0;
        
        [RelayCommand(CanExecute = nameof(CanRemoveSkillPointTo))]
        private void RemoveSkillPointTo(Skill skill)
        {
            var skillPoints = GetNumberSkillPointFrom(skill);
            if (skillPoints < 2) return;
        
            SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) - 1);
            SkillPointsAvailable++;
        }

        private bool CanRemoveSkillPointTo(Skill skill) =>
            GetNumberSkillPointFrom(skill) != _hero.GetNumberSkillPointFrom(skill);
        
        private void OnSkillChanged(Skill skill) 
        {
            SetSkillPointsTo(skill, _hero.GetNumberSkillPointFrom(skill));
            SkillPointsAvailable = _hero.SkillPointsAvailable;
        }
        
        partial void OnIsDraftChanged(bool newValue)
        {
            ConfirmCommand.NotifyCanExecuteChanged();
            ResetToDefaultCommand.NotifyCanExecuteChanged();
        }
        
        partial void OnSkillPointsAvailableChanged(int newValue)
        {
            IsDraft = newValue != _hero.SkillPointsAvailable;
            
            AddSkillPointToCommand.NotifyCanExecuteChanged();
            RemoveSkillPointToCommand.NotifyCanExecuteChanged();
        }
        
        public void Dispose() =>
            Unsubscribe();
    }
}