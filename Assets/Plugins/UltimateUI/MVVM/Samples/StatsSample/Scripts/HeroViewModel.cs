using System;
using UnityEngine.Profiling;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.ViewModels;
using Unity.Collections.LowLevel.Unsafe;
using UltimateUI.MVVM.Samples.StatsSample.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample
{
    public partial class HeroViewModel : IDisposable
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

        public HeroViewModel(Hero hero)
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

    public partial class HeroViewModel
    {
        public event Action<int> CoolChanged;
        public event Action<int> PowerChanged;
        public event Action<int> ReflexesChanged;
        public event Action<int> IntelligenceChanged;
        public event Action<int> TechnicalAbilityChanged;
        public event Action<int> SkillPointsAvailableChanged;
        public event Action<bool> IsDraftChanged;
        public event Action<IRelayCommand> ConfirmCommandChanged;
        public event Action<IRelayCommand> ResetToDefaultCommandChanged;
        public event Action<IRelayCommand<Skill>> AddSkillPointToCommandChanged;
        public event Action<IRelayCommand<Skill>> RemoveSkillPointToCommandChanged;
        
		
		private int Cool
		{
		    get => _cool;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_cool, value)) return;
		        
		        OnCoolChanging(_cool, value);
		        _cool = value;
		        OnCoolChanged(value);;
		        CoolChanged?.Invoke(_cool);
		    }
		}
        
		
		private int Power
		{
		    get => _power;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_power, value)) return;
		        
		        OnPowerChanging(_power, value);
		        _power = value;
		        OnPowerChanged(value);
		        PowerChanged?.Invoke(_power);
		    }
		}
        
		
		private int Reflexes
		{
		    get => _reflexes;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_reflexes, value)) return;
		        
		        OnReflexesChanging(_reflexes, value);
		        _reflexes = value;
		        OnReflexesChanged(value);;
		        ReflexesChanged?.Invoke(_reflexes);
		    }
		}
        
		
		private int Intelligence
		{
		    get => _intelligence;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_intelligence, value)) return;
		        
		        OnIntelligenceChanging(_intelligence, value);
		        _intelligence = value;
		        OnIntelligenceChanged(value);;
		        IntelligenceChanged?.Invoke(_intelligence);
		    }
		}
        
		
		private int TechnicalAbility
		{
		    get => _technicalAbility;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_technicalAbility, value)) return;
		        
		        OnTechnicalAbilityChanging(_technicalAbility, value);
		        _technicalAbility = value;
		        OnTechnicalAbilityChanged(value);;
		        TechnicalAbilityChanged?.Invoke(_technicalAbility);
		    }
		}
        
		
		private int SkillPointsAvailable
		{
		    get => _skillPointsAvailable;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_skillPointsAvailable, value)) return;
		        
		        OnSkillPointsAvailableChanging(_skillPointsAvailable, value);
		        _skillPointsAvailable = value;
		        OnSkillPointsAvailableChanged(value);;
		        SkillPointsAvailableChanged?.Invoke(_skillPointsAvailable);
		    }
		}
        
		
		private bool IsDraft
		{
		    get => _isDraft;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_isDraft, value)) return;
		        
		        OnIsDraftChanging(_isDraft, value);
		        _isDraft = value;
		        OnIsDraftChanged(value);;
		        IsDraftChanged?.Invoke(_isDraft);
		    }
		}
        
		
		private UltimateUI.MVVM.Commands.IRelayCommand ConfirmCommand
		{
		    get => _confirmCommand;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_confirmCommand, value)) return;
		        
		        OnConfirmCommandChanging(_confirmCommand, value);
		        _confirmCommand = value;
		        OnConfirmCommandChanged(value);;
		        ConfirmCommandChanged?.Invoke(_confirmCommand);
		    }
		}
        
		
		private UltimateUI.MVVM.Commands.IRelayCommand ResetToDefaultCommand
		{
		    get => _resetToDefaultCommand;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_resetToDefaultCommand, value)) return;
		        
		        OnResetToDefaultCommandChanging(_resetToDefaultCommand, value);
		        _resetToDefaultCommand = value;
		        OnResetToDefaultCommandChanged(value);;
		        ResetToDefaultCommandChanged?.Invoke(_resetToDefaultCommand);
		    }
		}
        
		
		private IRelayCommand<Skill> AddSkillPointToCommand
		{
		    get => _addSkillPointToCommand;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_addSkillPointToCommand, value)) return;
		        
		        OnAddSkillPointToCommandChanging(_addSkillPointToCommand, value);
		        _addSkillPointToCommand = value;
		        OnAddSkillPointToCommandChanged(value);;
		        AddSkillPointToCommandChanged?.Invoke(_addSkillPointToCommand);
		    }
		}
        
		
		private IRelayCommand<Skill> RemoveSkillPointToCommand
		{
		    get => _removeSkillPointToCommand;
		    set 
		    {
		        if (ViewModelUtility.EqualsDefault(_removeSkillPointToCommand, value)) return;
		        
		        OnRemoveSkillPointToCommandChanging(_removeSkillPointToCommand, value);
		        _removeSkillPointToCommand = value;
		        OnRemoveSkillPointToCommandChanged(value);;
		        RemoveSkillPointToCommandChanged?.Invoke(_removeSkillPointToCommand);
		    }
		}
        
		
		partial void OnCoolChanging(int oldValue, int newValue);
		
		
		partial void OnCoolChanged(int newValue);
        
		
		partial void OnPowerChanging(int oldValue, int newValue);
		
		
		partial void OnPowerChanged(int newValue);
        
		
		partial void OnReflexesChanging(int oldValue, int newValue);
		
		
		partial void OnReflexesChanged(int newValue);
        
		
		partial void OnIntelligenceChanging(int oldValue, int newValue);
		
		
		partial void OnIntelligenceChanged(int newValue);
        
		
		partial void OnTechnicalAbilityChanging(int oldValue, int newValue);
		
		
		partial void OnTechnicalAbilityChanged(int newValue);
        
		
		partial void OnSkillPointsAvailableChanging(int oldValue, int newValue);
		
		
		partial void OnSkillPointsAvailableChanged(int newValue);
        
		
		partial void OnIsDraftChanging(bool oldValue, bool newValue);
		
		
		partial void OnIsDraftChanged(bool newValue);
        
		
		partial void OnConfirmCommandChanging(IRelayCommand oldValue, IRelayCommand newValue);
		
		
		partial void OnConfirmCommandChanged(IRelayCommand newValue);
        
		
		partial void OnResetToDefaultCommandChanging(IRelayCommand oldValue, IRelayCommand newValue);
		
		
		partial void OnResetToDefaultCommandChanged(IRelayCommand newValue);
        
		
		partial void OnAddSkillPointToCommandChanging(IRelayCommand<Skill> oldValue, IRelayCommand<Skill> newValue);
		
		
		partial void OnAddSkillPointToCommandChanged(IRelayCommand<Skill> newValue);
        
		
		partial void OnRemoveSkillPointToCommandChanging(IRelayCommand<Skill> oldValue, IRelayCommand<Skill> newValue);
		
		
		partial void OnRemoveSkillPointToCommandChanged(IRelayCommand<Skill> newValue);
    }

    public partial class HeroViewModel
    {
        private const string CoolId = "Cool";
        private const string PowerId = "Power";
        private const string ReflexesIdId = "Reflexes";
        private const string IntelligenceId = "Intelligence";
        private const string TechnicalAbilityId = "TechnicalAbility";
        private const string SkillPointsAvailableId = "SkillPointsAvailable";
        private const string IsDraftId = "IsDraft";
        private const string ConfirmCommandId = "ConfirmCommand";
        private const string ResetToDefaultCommandId = "ResetToDefaultCommand";
        private const string AddSkillPointToCommandId = "AddSkillPointToCommand";
        private const string RemoveSkillPointToCommandId = "RemoveSkillPointToCommand";
    }
    
    public partial class HeroViewModel : IViewModel
    {
        public virtual void AddBinder(IBinder binder, string propertyName)
        {
            // switch (propertyName)
            // {
            //     case CoolId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<int>>(ref binder);
            //             
            //             specificBinder.SetValue(Cool);
            //             CoolChanged += specificBinder.SetValue;
            //         }
            //         break;
            //
            //     case PowerId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<int>>(ref binder);
            //
            //             specificBinder.SetValue(Power);
            //             PowerChanged += specificBinder.SetValue;
            //         }
            //         break;
            //
            //     case ReflexesIdId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<int>>(ref binder);
            //
            //             specificBinder.SetValue(Reflexes);
            //             ReflexesChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     
            //     case IntelligenceId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<int>>(ref binder);
            //
            //             specificBinder.SetValue(Intelligence);
            //             IntelligenceChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     
            //     case TechnicalAbilityId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<int>>(ref binder);
            //
            //             specificBinder.SetValue(TechnicalAbility);
            //             TechnicalAbilityChanged += specificBinder.SetValue;
            //             break;
            //         }
            //     case SkillPointsAvailableId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<int>>(ref binder);
            //
            //             specificBinder.SetValue(SkillPointsAvailable);
            //             SkillPointsAvailableChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case IsDraftId:
            //         {
            //             var specificBinder = UnsafeUtility.As<IBinder, IBinder<bool>>(ref binder);
            //
            //             specificBinder.SetValue(IsDraft);
            //             IsDraftChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case ConfirmCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(ConfirmCommand);
            //             ConfirmCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case ResetToDefaultCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(ResetToDefaultCommand);
            //             ResetToDefaultCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case AddSkillPointToCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand<Skill>> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(AddSkillPointToCommand);
            //             AddSkillPointToCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case RemoveSkillPointToCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand<Skill>> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(RemoveSkillPointToCommand);
            //             RemoveSkillPointToCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            // }
            
            // switch (propertyName)
            // {
            //     case CoolId:
            //         {
            //             if (binder is not IBinder<int> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(Cool);
            //             CoolChanged += specificBinder.SetValue;
            //         }
            //         break;
            //
            //     case PowerId:
            //         {
            //             if (binder is not IBinder<int> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(Power);
            //             PowerChanged += specificBinder.SetValue;
            //         }
            //         break;
            //
            //     case ReflexesIdId:
            //         {
            //             if (binder is not IBinder<int> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(Reflexes);
            //             ReflexesChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     
            //     case IntelligenceId:
            //         {
            //             if (binder is not IBinder<int> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(Intelligence);
            //             IntelligenceChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     
            //     case TechnicalAbilityId:
            //         {
            //             if (binder is not IBinder<int> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(TechnicalAbility);
            //             TechnicalAbilityChanged += specificBinder.SetValue;
            //             break;
            //         }
            //     case SkillPointsAvailableId:
            //         {
            //             if (binder is not IBinder<int> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(SkillPointsAvailable);
            //             SkillPointsAvailableChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case IsDraftId:
            //         {
            //             if (binder is not IBinder<bool> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(IsDraft);
            //             IsDraftChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case ConfirmCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(ConfirmCommand);
            //             ConfirmCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case ResetToDefaultCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(ResetToDefaultCommand);
            //             ResetToDefaultCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case AddSkillPointToCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand<Skill>> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(AddSkillPointToCommand);
            //             AddSkillPointToCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            //     case RemoveSkillPointToCommandId:
            //         {
            //             if (binder is not IBinder<IRelayCommand<Skill>> specificBinder)
            //                 throw new Exception();
            //
            //             specificBinder.SetValue(RemoveSkillPointToCommand);
            //             RemoveSkillPointToCommandChanged += specificBinder.SetValue;
            //         }
            //         break;
            // }
            
            switch (propertyName)
            {
                case CoolId: AddBinderLocal(Cool, ref CoolChanged); break;
                case PowerId: AddBinderLocal(Power, ref PowerChanged); break;
                case ReflexesIdId: AddBinderLocal(Reflexes, ref ReflexesChanged); break;
                case IntelligenceId: AddBinderLocal(Intelligence, ref IntelligenceChanged); break;
                case TechnicalAbilityId: AddBinderLocal(TechnicalAbility, ref TechnicalAbilityChanged); break;
                case SkillPointsAvailableId: AddBinderLocal(SkillPointsAvailable, ref SkillPointsAvailableChanged); break;
                case IsDraftId: AddBinderLocal(IsDraft, ref IsDraftChanged); break;
                case ConfirmCommandId: AddBinderLocal(ConfirmCommand, ref ConfirmCommandChanged); break;
                case ResetToDefaultCommandId: AddBinderLocal(ResetToDefaultCommand, ref ResetToDefaultCommandChanged); break;
                case AddSkillPointToCommandId: AddBinderLocal(AddSkillPointToCommand, ref AddSkillPointToCommandChanged); break;
                case RemoveSkillPointToCommandId: AddBinderLocal(RemoveSkillPointToCommand, ref RemoveSkillPointToCommandChanged); break;
            }
            return;
            
            void AddBinderLocal<T>(T value, ref Action<T> changed)
            {
                if (binder is not IBinder<T> specificBinder)
                    throw new Exception();
                
                specificBinder.SetValue(value);
                changed += specificBinder.SetValue;
            }
        }
        
        public void RemoveBinder(IBinder binder, string propertyName)
        {
            switch (propertyName)
            {
                case CoolId: RemoveBinderLocal(ref CoolChanged); break;
                case PowerId: RemoveBinderLocal(ref PowerChanged); break;
                case ReflexesIdId: RemoveBinderLocal(ref ReflexesChanged); break;
                case IntelligenceId: RemoveBinderLocal(ref IntelligenceChanged); break;
                case TechnicalAbilityId: RemoveBinderLocal(ref TechnicalAbilityChanged); break;
                case SkillPointsAvailableId: RemoveBinderLocal(ref SkillPointsAvailableChanged); break;
                case IsDraftId: RemoveBinderLocal(ref IsDraftChanged); break;
                case ConfirmCommandId: RemoveBinderLocal(ref ConfirmCommandChanged); break;
                case ResetToDefaultCommandId: RemoveBinderLocal(ref ResetToDefaultCommandChanged); break;
                case AddSkillPointToCommandId: RemoveBinderLocal(ref AddSkillPointToCommandChanged); break;
                case RemoveSkillPointToCommandId: RemoveBinderLocal(ref RemoveSkillPointToCommandChanged); break;
            }
            return;

            void RemoveBinderLocal<T>(ref Action<T> changed)
            {
                if (binder is IBinder<T> specificBinder)
                    changed -= specificBinder.SetValue;
            }
        }
    }
}