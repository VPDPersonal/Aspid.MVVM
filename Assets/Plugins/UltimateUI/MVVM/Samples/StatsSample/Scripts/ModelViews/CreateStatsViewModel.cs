using System;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.StatsSample.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.ViewModels
{
    [ViewModel]
    public partial class CreateStatsViewModel : IDisposable
    {
        [Bind] private int _cool;
        [Bind] private int _power;
        [Bind] private int _reflexes;
        [Bind] private int _intelligence; 
        [Bind] private int _technicalAbility;
        
        [Bind] private int _skillPointsAvailable;
        [Bind] private bool _isDraft;

        [Bind] private IRelayCommand _confirmCommand;
        [Bind] private IRelayCommand _resetToDefaultCommand;
        [Bind] private IRelayCommand<Skill> _addSkillPointToCommand;
        [Bind] private IRelayCommand<Skill> _removeSkillPointToCommand;

        private readonly Hero _hero;

        public CreateStatsViewModel(Hero hero)
        {
            _hero = hero;
            
            _cool = _hero.GetNumberSkillPointFrom(Skill.Cool);
            _power = _hero.GetNumberSkillPointFrom(Skill.Cool);
            _reflexes = _hero.GetNumberSkillPointFrom(Skill.Cool);
            _intelligence = _hero.GetNumberSkillPointFrom(Skill.Cool);
            _technicalAbility = _hero.GetNumberSkillPointFrom(Skill.Cool);
            
            _skillPointsAvailable = _hero.SkillPointsAvailable;

            _confirmCommand = new RelayCommand(Confirm, () => IsDraft);
            _resetToDefaultCommand = new RelayCommand(ResetToDefault, () => IsDraft);
            _addSkillPointToCommand = new RelayCommand<Skill>(AddSkillPointTo, _ => SkillPointsAvailable > 0);
            _removeSkillPointToCommand = new RelayCommand<Skill>(RemoveSkillPointTo, 
                skill => GetNumberSkillPointFrom(skill) != _hero.GetNumberSkillPointFrom(skill));
            
            Subscribe();
        }

        private void Subscribe()
        {
            _hero.SkillChanged += OnSkillChanged;
        }
        
        private void Unsubscribe()
        {
            _hero.SkillChanged -= OnSkillChanged;
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

        private int GetNumberSkillPointFrom(Skill skill) => skill switch
        {
            Skill.Cool => Cool, 
            Skill.Power => Power,
            Skill.Reflexes => Reflexes, 
            Skill.Intelligence => Intelligence, 
            Skill.TechnicalAbility => TechnicalAbility, 
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
        };

        // [BindCommand]
        private void Confirm()
        {
            _hero.SettSkillPointTo(Skill.Cool, Cool);
            _hero.SettSkillPointTo(Skill.Power, Power);
            _hero.SettSkillPointTo(Skill.Reflexes, Reflexes);
            _hero.SettSkillPointTo(Skill.Intelligence, Intelligence);
            _hero.SettSkillPointTo(Skill.TechnicalAbility, TechnicalAbility);
            
            IsDraft = false;
            if (_hero.SkillPointsAvailable != SkillPointsAvailable)
                throw new Exception();
        }
        
        // [BindCommand]
        private void ResetToDefault()
        {
            Cool = _hero.GetNumberSkillPointFrom(Skill.Cool);
            Power = _hero.GetNumberSkillPointFrom(Skill.Power);
            Reflexes = _hero.GetNumberSkillPointFrom(Skill.Reflexes);
            Intelligence = _hero.GetNumberSkillPointFrom(Skill.Intelligence);
            TechnicalAbility = _hero.GetNumberSkillPointFrom(Skill.TechnicalAbility);

            SkillPointsAvailable = _hero.SkillPointsAvailable;
        }

        // [BindCommand]
        private void AddSkillPointTo(Skill skill)
        {
            if (SkillPointsAvailable == 0) return;

            SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) + 1);
            SkillPointsAvailable--;
        }

        // [BindCommand]
        private void RemoveSkillPointTo(Skill skill)
        {
            var skillPoints = GetNumberSkillPointFrom(skill);
            if (skillPoints < 2) return;

            SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) - 1);
            SkillPointsAvailable++;
        }

        private void OnSkillChanged(Skill skill) 
        {
            SetSkillPointsTo(skill, _hero.GetNumberSkillPointFrom(skill));
            SkillPointsAvailable = _hero.SkillPointsAvailable;
            RemoveSkillPointToCommand.NotifyCanExecuteChanged();
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

        public void Dispose() => Unsubscribe();
    }
}