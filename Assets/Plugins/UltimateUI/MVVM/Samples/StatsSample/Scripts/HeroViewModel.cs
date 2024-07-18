using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugins.UltimateUI.MVVM.Samples.StatsSample.Scripts;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.Samples.StatsSample.Models;
using UltimateUI.MVVM.ViewModels;

namespace UltimateUI.MVVM.Samples.StatsSample
{
    public partial class HeroViewModel : INotifyPropertyChanged
    {
        [Bind] private int _cool;
        [Bind] private int _power;
        [Bind] private int _reflexes;
        [Bind] private int _intelligence;
        [Bind] private int _technicalAbility;
        
        [Bind] private int _skillPointsAvailable;
        [Bind] private bool _isDraft;
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public partial class HeroViewModel
    {
        public int Cool
        {
            get => GetCool();
            set
            {
                if (ViewModelUtility.EqualsDefault(_cool, value)) return;
		        
                OnCoolChanging(_cool, value);
                _cool = value;
                OnCoolChanged(value);
                OnPropertyChanged();
            }
        }
        
        public int Power
        {
            get => _power;
            set
            {
                if (ViewModelUtility.EqualsDefault(_power, value)) return;
		        
                OnPowerChanging(_power, value);
                _power = value;
                OnPowerChanged(value);
                OnPropertyChanged();
            }
        }
        
        public int Reflexes
        {
            get => _reflexes;
            set
            {
                if (ViewModelUtility.EqualsDefault(_reflexes, value)) return;
		        
                OnReflexesChanging(_reflexes, value);
                _reflexes = value;
                OnReflexesChanged(value);
                OnPropertyChanged();
            }
        }
        
        public int Intelligence
        {
            get => _intelligence;
            set
            {
                if (ViewModelUtility.EqualsDefault(_intelligence, value)) return;
		        
                OnIntelligenceChanging(_intelligence, value);
                _intelligence = value;
                OnIntelligenceChanged(value);
                OnPropertyChanged();
            }
        }
        
        public int TechnicalAbility
        {
            get => _technicalAbility;
            set
            {
                if (ViewModelUtility.EqualsDefault(_technicalAbility, value)) return;
		        
                OnTechnicalAbilityChanging(_technicalAbility, value);
                _technicalAbility = value;
                OnTechnicalAbilityChanged(value);
                OnPropertyChanged();
            }
        }
        
        public int SkillPointsAvailable
        {
            get => _skillPointsAvailable;
            set
            {
                if (ViewModelUtility.EqualsDefault(_skillPointsAvailable, value)) return;
		        
                OnSkillPointsAvailableChanging(_skillPointsAvailable, value);
                _skillPointsAvailable = value;
                OnSkillPointsAvailableChanged(value);
                OnPropertyChanged();
            }
        }
        
        public bool IsDraft
        {
            get => _isDraft;
            set
            {
                if (ViewModelUtility.EqualsDefault(_isDraft, value)) return;
		        
                OnIsDraftChanging(_isDraft, value);
                _isDraft = value;
                OnIsDraftChanged(value);
                OnPropertyChanged();
            }
        }

        public int GetCool() => _cool;

        partial void OnCoolChanging(int oldValue, int newValue);
        
        partial void OnCoolChanged(int oldValue);
        
        partial void OnPowerChanging(int oldValue, int newValue);
        
        partial void OnPowerChanged(int oldValue);
        
        partial void OnReflexesChanging(int oldValue, int newValue);
        
        partial void OnReflexesChanged(int oldValue);
        
        partial void OnIntelligenceChanging(int oldValue, int newValue);
        
        partial void OnIntelligenceChanged(int oldValue);
        
        partial void OnTechnicalAbilityChanging(int oldValue, int newValue);
        
        partial void OnTechnicalAbilityChanged(int oldValue);
        
        partial void OnSkillPointsAvailableChanging(int oldValue, int newValue);
        
        partial void OnSkillPointsAvailableChanged(int oldValue);
        
        partial void OnIsDraftChanging(bool oldValue, bool newValue);
        
        partial void OnIsDraftChanged(bool oldValue);
    }
    
    // [ViewModel]
    // public partial class HeroViewModel : IDisposable
    // {
    //     [Bind] private int _cool;
    //     [Bind] private int _power;
    //     [Bind] private int _reflexes;
    //     [Bind] private int _intelligence;
    //     [Bind] private int _technicalAbility;
    //     
    //     [Bind] private int _skillPointsAvailable;
    //     [Bind] private bool _isDraft;
    //
    //     [Bind] private IRelayCommand _confirmCommand;
    //     [Bind] private IRelayCommand _resetToDefaultCommand;
    //     [Bind] private IRelayCommand<Skill> _addSkillPointToCommand;
    //     [Bind] private IRelayCommand<Skill> _removeSkillPointToCommand;
    //
    //     private readonly Hero _hero;
    //
    //     public HeroViewModel(Hero hero)
    //     {
    //         _hero = hero;
    //         
    //         _cool = _hero.GetNumberSkillPointFrom(Skill.Cool);
    //         _power = _hero.GetNumberSkillPointFrom(Skill.Cool);
    //         _reflexes = _hero.GetNumberSkillPointFrom(Skill.Cool);
    //         _intelligence = _hero.GetNumberSkillPointFrom(Skill.Cool);
    //         _technicalAbility = _hero.GetNumberSkillPointFrom(Skill.Cool);
    //         
    //         _skillPointsAvailable = _hero.SkillPointsAvailable;
    //
    //         _confirmCommand = new RelayCommand(Confirm, () => IsDraft);
    //         _resetToDefaultCommand = new RelayCommand(ResetToDefault, () => IsDraft);
    //         _addSkillPointToCommand = new RelayCommand<Skill>(AddSkillPointTo, _ => SkillPointsAvailable > 0);
    //         _removeSkillPointToCommand = new RelayCommand<Skill>(RemoveSkillPointTo, 
    //             skill => GetNumberSkillPointFrom(skill) != _hero.GetNumberSkillPointFrom(skill));
    //         
    //         Subscribe();
    //     }
    //
    //     private void Subscribe()
    //     {
    //         _hero.SkillChanged += OnSkillChanged;
    //     }
    //     
    //     private void Unsubscribe()
    //     {
    //         _hero.SkillChanged -= OnSkillChanged;
    //     }
    //
    //     private void SetSkillPointsTo(Skill skill, int points)
    //     {
    //         switch (skill)
    //         {
    //             case Skill.Cool: Cool = points; break;
    //             case Skill.Power: Power = points; break;
    //             case Skill.Reflexes: Reflexes = points; break;
    //             case Skill.Intelligence: Intelligence = points; break;
    //             case Skill.TechnicalAbility: TechnicalAbility = points; break;
    //             default: throw new ArgumentOutOfRangeException(nameof(skill), skill, null);
    //         }
    //     }
    //
    //     private int GetNumberSkillPointFrom(Skill skill) => skill switch
    //     {
    //         Skill.Cool => Cool, 
    //         Skill.Power => Power,
    //         Skill.Reflexes => Reflexes, 
    //         Skill.Intelligence => Intelligence, 
    //         Skill.TechnicalAbility => TechnicalAbility, 
    //         _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
    //     };
    //
    //     // [BindCommand]
    //     private void Confirm()
    //     {
    //         _hero.SettSkillPointTo(Skill.Cool, Cool);
    //         _hero.SettSkillPointTo(Skill.Power, Power);
    //         _hero.SettSkillPointTo(Skill.Reflexes, Reflexes);
    //         _hero.SettSkillPointTo(Skill.Intelligence, Intelligence);
    //         _hero.SettSkillPointTo(Skill.TechnicalAbility, TechnicalAbility);
    //         
    //         IsDraft = false;
    //         if (_hero.SkillPointsAvailable != SkillPointsAvailable)
    //             throw new Exception();
    //     }
    //     
    //     // [BindCommand]
    //     private void ResetToDefault()
    //     {
    //         Cool = _hero.GetNumberSkillPointFrom(Skill.Cool);
    //         Power = _hero.GetNumberSkillPointFrom(Skill.Power);
    //         Reflexes = _hero.GetNumberSkillPointFrom(Skill.Reflexes);
    //         Intelligence = _hero.GetNumberSkillPointFrom(Skill.Intelligence);
    //         TechnicalAbility = _hero.GetNumberSkillPointFrom(Skill.TechnicalAbility);
    //
    //         SkillPointsAvailable = _hero.SkillPointsAvailable;
    //     }
    //
    //     // [BindCommand]
    //     private void AddSkillPointTo(Skill skill)
    //     {
    //         if (SkillPointsAvailable == 0) return;
    //
    //         SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) + 1);
    //         SkillPointsAvailable--;
    //     }
    //
    //     // [BindCommand]
    //     private void RemoveSkillPointTo(Skill skill)
    //     {
    //         var skillPoints = GetNumberSkillPointFrom(skill);
    //         if (skillPoints < 2) return;
    //
    //         SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) - 1);
    //         SkillPointsAvailable++;
    //     }
    //
    //     private void OnSkillChanged(Skill skill) 
    //     {
    //         SetSkillPointsTo(skill, _hero.GetNumberSkillPointFrom(skill));
    //         RemoveSkillPointToCommand.NotifyCanExecuteChanged();
    //     }
    //
    //     partial void OnIsDraftChanged(bool newValue)
    //     {
    //         ConfirmCommand.NotifyCanExecuteChanged();
    //         ResetToDefaultCommand.NotifyCanExecuteChanged();
    //     }
    //
    //     partial void OnSkillPointsAvailableChanged(int newValue)
    //     {
    //         IsDraft = newValue != _hero.SkillPointsAvailable;
    //         
    //         AddSkillPointToCommand.NotifyCanExecuteChanged();
    //         RemoveSkillPointToCommand.NotifyCanExecuteChanged();
    //     }
    //
    //     public void Dispose() => Unsubscribe();
    // }
}