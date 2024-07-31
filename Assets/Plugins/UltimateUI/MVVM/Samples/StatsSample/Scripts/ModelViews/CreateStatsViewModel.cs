using System;
using System.Collections.Generic;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.StatsSample.Models;
using Unity.Profiling;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.ViewModels
{
    // [ViewModel]
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
    
    public partial class CreateStatsViewModel
    {
        private const string CoolId = nameof(Cool);
        private const string PowerId = nameof(Power);
        private const string ReflexesId = nameof(Reflexes);
        private const string IntelligenceId = nameof(Intelligence);
        private const string TechnicalAbilityId = nameof(TechnicalAbility);
        private const string SkillPointsAvailableId = nameof(SkillPointsAvailable);
        private const string IsDraftId = nameof(IsDraft);
        private const string ConfirmCommandId = nameof(ConfirmCommand);
        private const string ResetToDefaultCommandId = nameof(ResetToDefaultCommand);
        private const string AddSkillPointToCommandId = nameof(AddSkillPointToCommand);
        private const string RemoveSkillPointToCommandId = nameof(RemoveSkillPointToCommand);
    }
    
    public partial class CreateStatsViewModel
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
		    set => SetCool(value);
		}
        
		private int Power
		{
		    get => _power;
		    set => SetPower(value);
		}
		
		private int Reflexes
		{
		    get => _reflexes;
		    set => SetReflexes(value);
		}
		
		private int Intelligence
		{
		    get => _intelligence;
		    set => SetIntelligence(value);
		}
		
		private int TechnicalAbility
		{
		    get => _technicalAbility;
		    set => SetTechnicalAbility(value);
		}
		
		private int SkillPointsAvailable
		{
		    get => _skillPointsAvailable;
		    set => SetSkillPointsAvailable(value);
		}
        
		private bool IsDraft
		{
		    get => _isDraft;
		    set => SetIsDraft(value);
		}
        
		private IRelayCommand ConfirmCommand
		{
		    get => _confirmCommand;
		    set => SetConfirmCommand(value);
		}
		
		private IRelayCommand ResetToDefaultCommand
		{
		    get => _resetToDefaultCommand;
		    set => SetResetToDefaultCommand(value);
		}
        
		private IRelayCommand<Skill> AddSkillPointToCommand
		{
		    get => _addSkillPointToCommand;
		    set => SetAddSkillPointToCommand(value);
		}
		
		private IRelayCommand<Skill> RemoveSkillPointToCommand
		{
		    get => _removeSkillPointToCommand;
		    set => SetRemoveSkillPointToCommand(value);
		}
		
		private void SetCool(int value)
		{
		    if (EqualityComparer<int>.Default.Equals(_cool, value)) return;
		    
		    OnCoolChanging(_cool, value);
		    _cool = value;
		    OnCoolChanged(value);
		    CoolChanged?.Invoke(_cool);
		}
		
		
		partial void OnCoolChanging(int oldValue, int newValue);
		
		
		partial void OnCoolChanged(int newValue);
		
		
		private void SetPower(int value)
		{
		    if (EqualityComparer<int>.Default.Equals(_power, value)) return;
		    
		    OnPowerChanging(_power, value);
		    _power = value;
		    OnPowerChanged(value);
		    PowerChanged?.Invoke(_power);
		}
		
		
		partial void OnPowerChanging(int oldValue, int newValue);
		
		
		partial void OnPowerChanged(int newValue);
		
		
		private void SetReflexes(int value)
		{
		    if (EqualityComparer<int>.Default.Equals(_reflexes, value)) return;
		    
		    OnReflexesChanging(_reflexes, value);
		    _reflexes = value;
		    OnReflexesChanged(value);
		    ReflexesChanged?.Invoke(_reflexes);
		}
		
		
		partial void OnReflexesChanging(int oldValue, int newValue);
		
		
		partial void OnReflexesChanged(int newValue);
		
		
		private void SetIntelligence(int value)
		{
		    if (EqualityComparer<int>.Default.Equals(_intelligence, value)) return;
		    
		    OnIntelligenceChanging(_intelligence, value);
		    _intelligence = value;
		    OnIntelligenceChanged(value);
		    IntelligenceChanged?.Invoke(_intelligence);
		}
		
		
		partial void OnIntelligenceChanging(int oldValue, int newValue);
		
		
		partial void OnIntelligenceChanged(int newValue);
		
		
		private void SetTechnicalAbility(int value)
		{
		    if (EqualityComparer<int>.Default.Equals(_technicalAbility, value)) return;
		    
		    OnTechnicalAbilityChanging(_technicalAbility, value);
		    _technicalAbility = value;
		    OnTechnicalAbilityChanged(value);
		    TechnicalAbilityChanged?.Invoke(_technicalAbility);
		}
		
		
		partial void OnTechnicalAbilityChanging(int oldValue, int newValue);
		
		
		partial void OnTechnicalAbilityChanged(int newValue);
		
		
		private void SetSkillPointsAvailable(int value)
		{
		    if (EqualityComparer<int>.Default.Equals(_skillPointsAvailable, value)) return;
		    
		    OnSkillPointsAvailableChanging(_skillPointsAvailable, value);
		    _skillPointsAvailable = value;
		    OnSkillPointsAvailableChanged(value);
		    SkillPointsAvailableChanged?.Invoke(_skillPointsAvailable);
		}
		
		
		partial void OnSkillPointsAvailableChanging(int oldValue, int newValue);
		
		
		partial void OnSkillPointsAvailableChanged(int newValue);
		
		
		private void SetIsDraft(bool value)
		{
		    if (EqualityComparer<bool>.Default.Equals(_isDraft, value)) return;
		    
		    OnIsDraftChanging(_isDraft, value);
		    _isDraft = value;
		    OnIsDraftChanged(value);
		    IsDraftChanged?.Invoke(_isDraft);
		}
		
		
		partial void OnIsDraftChanging(bool oldValue, bool newValue);
		
		
		partial void OnIsDraftChanged(bool newValue);
		
		
		private void SetConfirmCommand(IRelayCommand value)
		{
		    if (EqualityComparer<IRelayCommand>.Default.Equals(_confirmCommand, value)) return;
		    
		    OnConfirmCommandChanging(_confirmCommand, value);
		    _confirmCommand = value;
		    OnConfirmCommandChanged(value);
		    ConfirmCommandChanged?.Invoke(_confirmCommand);
		}
		
		
		partial void OnConfirmCommandChanging(IRelayCommand oldValue, IRelayCommand newValue);
		
		
		partial void OnConfirmCommandChanged(IRelayCommand newValue);
		
		
		private void SetResetToDefaultCommand(IRelayCommand value)
		{
		    if (EqualityComparer<IRelayCommand>.Default.Equals(_resetToDefaultCommand, value)) return;
		    
		    OnResetToDefaultCommandChanging(_resetToDefaultCommand, value);
		    _resetToDefaultCommand = value;
		    OnResetToDefaultCommandChanged(value);
		    ResetToDefaultCommandChanged?.Invoke(_resetToDefaultCommand);
		}
		
		
		partial void OnResetToDefaultCommandChanging(IRelayCommand oldValue, IRelayCommand newValue);
		
		
		partial void OnResetToDefaultCommandChanged(IRelayCommand newValue);
		
		
		private void SetAddSkillPointToCommand(IRelayCommand<Skill> value)
		{
		    if (EqualityComparer<IRelayCommand<Skill>>.Default.Equals(_addSkillPointToCommand, value)) return;
		    
		    OnAddSkillPointToCommandChanging(_addSkillPointToCommand, value);
		    _addSkillPointToCommand = value;
		    OnAddSkillPointToCommandChanged(value);
		    AddSkillPointToCommandChanged?.Invoke(_addSkillPointToCommand);
		}
		
		
		partial void OnAddSkillPointToCommandChanging(IRelayCommand<Skill> oldValue, IRelayCommand<Skill> newValue);
		
		
		partial void OnAddSkillPointToCommandChanged(IRelayCommand<Skill> newValue);
		
		
		private void SetRemoveSkillPointToCommand(IRelayCommand<Skill> value)
		{
		    if (EqualityComparer<IRelayCommand<Skill>>.Default.Equals(_removeSkillPointToCommand, value)) return;
		    
		    OnRemoveSkillPointToCommandChanging(_removeSkillPointToCommand, value);
		    _removeSkillPointToCommand = value;
		    OnRemoveSkillPointToCommandChanged(value);
		    RemoveSkillPointToCommandChanged?.Invoke(_removeSkillPointToCommand);
		}
		
		
		partial void OnRemoveSkillPointToCommandChanging(IRelayCommand<Skill> oldValue, IRelayCommand<Skill> newValue);
		
		
		partial void OnRemoveSkillPointToCommandChanged(IRelayCommand<Skill> newValue);
		
    }
    
    public partial class CreateStatsViewModel : IViewModel
    {
		#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
		private static readonly ProfilerMarker _addBinderMarker = new("CreateStatsViewModel.AddBinder");
		
		private static readonly ProfilerMarker _removeBinderMarker = new("CreateStatsViewModel.RemoveBinder");
		#endif
		
		
		public void AddBinder(IBinder binder, string propertyName)
		{
		    #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
		    using (_addBinderMarker.Auto())
		    #endif
		    {
		        AddBinderIternal(binder, propertyName);
		    }
		}

        public void AddBinder(IReadOnlyList<IBinder> binders, string propertyName)
        {
            using (_addBinderMarker.Auto())
            {
                if (binders.Count == 0) return;
                
                switch (propertyName)
                {
                    case CoolId:
                        Action<int> setCool = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<int>;
                            AddBinderLocal(specificBinder, Cool, ref CoolChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setCool ??= SetCool;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<int>, setCool);
                            }
                        }
                        return;
                    case PowerId:
                        Action<int> setPower = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<int>;
                            AddBinderLocal(specificBinder, Power, ref PowerChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setPower ??= SetPower;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<int>, setPower);
                            }
                        }
                        return;
                    case ReflexesId:
                        Action<int> setReflexes = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<int>;
                            AddBinderLocal(specificBinder, Reflexes, ref ReflexesChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setReflexes ??= SetReflexes;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<int>, setReflexes);
                            }
                        }
                        return;
                    case IntelligenceId:
                        Action<int> setIntelligence = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<int>;
                            AddBinderLocal(specificBinder, Intelligence, ref IntelligenceChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setIntelligence ??= SetIntelligence;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<int>, setIntelligence);
                            }
                        }
                        return;
                    case TechnicalAbilityId:
                        Action<int> setTechnicalAbility = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<int>;
                            AddBinderLocal(specificBinder, TechnicalAbility, ref TechnicalAbilityChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setTechnicalAbility ??= SetTechnicalAbility;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<int>, setTechnicalAbility);
                            }
                        }
                        return;
                    case SkillPointsAvailableId:
                        Action<int> setSkillPointsAvailable = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<int>;
                            AddBinderLocal(specificBinder, SkillPointsAvailable, ref SkillPointsAvailableChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setSkillPointsAvailable ??= SetSkillPointsAvailable;
                                AddReverseBinderLocal<int>(specificBinder as IReverseBinder<int>, setSkillPointsAvailable);
                            }
                        }
                        return;
                    case IsDraftId:
                        Action<bool> setIsDraft = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<bool>;
                            AddBinderLocal(specificBinder, IsDraft, ref IsDraftChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setIsDraft ??= SetIsDraft;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<bool>, setIsDraft);
                            }
                        }
                        return;
                    case ConfirmCommandId:
                        Action<IRelayCommand> setConfirmCommand = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<IRelayCommand>;
                            AddBinderLocal(specificBinder, ConfirmCommand, ref ConfirmCommandChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setConfirmCommand ??= SetConfirmCommand;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<IRelayCommand>, setConfirmCommand);
                            }
                        }
                        return;
                    case ResetToDefaultCommandId:
                        Action<IRelayCommand> setResetToDefaultCommand = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<IRelayCommand>;
                            AddBinderLocal(specificBinder, ResetToDefaultCommand, ref ResetToDefaultCommandChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setResetToDefaultCommand ??= SetResetToDefaultCommand;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<IRelayCommand>, setResetToDefaultCommand);
                            }
                        }
                        return;
                    case AddSkillPointToCommandId:
                        Action<IRelayCommand<Skill>> setAddSkillPointToCommand = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<IRelayCommand<Skill>>;
                            AddBinderLocal(specificBinder, AddSkillPointToCommand, ref AddSkillPointToCommandChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setAddSkillPointToCommand ??= SetAddSkillPointToCommand;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<IRelayCommand<Skill>>, setAddSkillPointToCommand);
                            }
                        }
                        return;
                    case RemoveSkillPointToCommandId:
                        
                        Action<IRelayCommand<Skill>> setRemoveSkillPointToCommand = null;
                        for (var i = 0; i < binders.Count; i++)
                        {
                            var specificBinder = binders[i] as IBinder<IRelayCommand<Skill>>;
                            AddBinderLocal(specificBinder, RemoveSkillPointToCommand, ref RemoveSkillPointToCommandChanged);
                            
                            if (binders[i].IsReverseEnabled)
                            {
                                setRemoveSkillPointToCommand ??= SetRemoveSkillPointToCommand;
                                AddReverseBinderLocal(specificBinder as IReverseBinder<IRelayCommand<Skill>>, setRemoveSkillPointToCommand);
                            }
                        }
                        
                        return;
                    default:
                        var flag = false;
                        // AddBinderManual(binder, propertyName, ref flag);
                        if (flag) return;
                        break;
                }
                return;

                void AddBinderLocal<T>(IBinder<T> binder, T value, ref Action<T> changed)
                {
                    binder.SetValue(value);
                    changed += binder.SetValue;
                    binder.OnBound();
                }

                void AddReverseBinderLocal<T>(IReverseBinder<T> binder, Action<T> setValue)
                {
                    binder.ValueChanged += setValue;   
                }
            }
        }


        protected virtual void AddBinderIternal(IBinder binder, string propertyName)
		{
		    switch (propertyName)
		    {
				case CoolId: 
				    AddBinderLocal(Cool, ref CoolChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<int>(SetCool);
				    return;
				case PowerId: 
				    AddBinderLocal(Power, ref PowerChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<int>(SetPower);
				    return;
				case ReflexesId: 
				    AddBinderLocal(Reflexes, ref ReflexesChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<int>(SetReflexes);
				    return;
				case IntelligenceId: 
				    AddBinderLocal(Intelligence, ref IntelligenceChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<int>(SetIntelligence);
				    return;
				case TechnicalAbilityId: 
				    AddBinderLocal(TechnicalAbility, ref TechnicalAbilityChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<int>(SetTechnicalAbility);
				    return;
				case SkillPointsAvailableId: 
				    AddBinderLocal(SkillPointsAvailable, ref SkillPointsAvailableChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<int>(SetSkillPointsAvailable);
				    return;
				case IsDraftId: 
				    AddBinderLocal(IsDraft, ref IsDraftChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<bool>(SetIsDraft);
				    return;
				case ConfirmCommandId: 
				    AddBinderLocal(ConfirmCommand, ref ConfirmCommandChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<IRelayCommand>(SetConfirmCommand);
				    return;
				case ResetToDefaultCommandId: 
				    AddBinderLocal(ResetToDefaultCommand, ref ResetToDefaultCommandChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<IRelayCommand>(SetResetToDefaultCommand);
				    return;
				case AddSkillPointToCommandId: 
				    AddBinderLocal(AddSkillPointToCommand, ref AddSkillPointToCommandChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<IRelayCommand<Skill>>(SetAddSkillPointToCommand);
				    return;
				case RemoveSkillPointToCommandId: 
				    AddBinderLocal(RemoveSkillPointToCommand, ref RemoveSkillPointToCommandChanged);
				    if (binder.IsReverseEnabled) AddReverseBinderLocal<IRelayCommand<Skill>>(SetRemoveSkillPointToCommand);
				    return;
				default:
				   var flag = false;
				   AddBinderManual(binder, propertyName, ref flag);
				   if (flag) return;
				   break;
            }
			return;
			
			void AddBinderLocal<T>(T value, ref Action<T> changed)
			{   
			    if (binder is not IBinder<T> specificBinder)
			        throw new Exception();
			        
			    specificBinder.SetValue(value);
			    changed += specificBinder.SetValue;
			}
			
			void AddReverseBinderLocal<T>(Action<T> setValue)
			{
			    if (binder is not IReverseBinder<T> specificReverseBinder)
			        throw new Exception();
			        
			    specificReverseBinder.ValueChanged += setValue;
			}
        }
        
		
		public void RemoveBinder(IBinder binder, string propertyName) 
		{
		    #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
		    using (_removeBinderMarker.Auto())
		    #endif
		    {
		        RemoveBinderIternal(binder, propertyName);
		    }
		}
		
		
		protected virtual void RemoveBinderIternal(IBinder binder, string propertyName)
		{
		    switch (propertyName)
		    {    
				case CoolId: 
				    RemoveBinderLocal(ref CoolChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<int>(SetCool);
				    return;
				case PowerId: 
				    RemoveBinderLocal(ref PowerChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<int>(SetPower);
				    return;
				case ReflexesId: 
				    RemoveBinderLocal(ref ReflexesChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<int>(SetReflexes);
				    return;
				case IntelligenceId: 
				    RemoveBinderLocal(ref IntelligenceChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<int>(SetIntelligence);
				    return;
				case TechnicalAbilityId: 
				    RemoveBinderLocal(ref TechnicalAbilityChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<int>(SetTechnicalAbility);
				    return;
				case SkillPointsAvailableId: 
				    RemoveBinderLocal(ref SkillPointsAvailableChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<int>(SetSkillPointsAvailable);
				    return;
				case IsDraftId: 
				    RemoveBinderLocal(ref IsDraftChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<bool>(SetIsDraft);
				    return;
				case ConfirmCommandId: 
				    RemoveBinderLocal(ref ConfirmCommandChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<IRelayCommand>(SetConfirmCommand);
				    return;
				case ResetToDefaultCommandId: 
				    RemoveBinderLocal(ref ResetToDefaultCommandChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<IRelayCommand>(SetResetToDefaultCommand);
				    return;
				case AddSkillPointToCommandId: 
				    RemoveBinderLocal(ref AddSkillPointToCommandChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<IRelayCommand<Skill>>(SetAddSkillPointToCommand);
				    return;
				case RemoveSkillPointToCommandId: 
				    RemoveBinderLocal(ref RemoveSkillPointToCommandChanged);
				    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<IRelayCommand<Skill>>(SetRemoveSkillPointToCommand);
				    return;
				default:
				   var flag = false;
				   RemoveBinderManual(binder, propertyName, ref flag);
				   if (flag) return;
				   break;
            }
			return;
			
			void RemoveBinderLocal<T>(ref Action<T> changed)
			{
			    if (binder is not IBinder<T> specificBinder)
			        throw new Exception();
			        
			    changed -= specificBinder.SetValue;
			}      
			
			void RemoveReverseBinderLocal<T>(Action<T> setValue)
			{
			    if (binder is not IReverseBinder<T> specificReverseBinder)
			        throw new Exception();
			        
			    specificReverseBinder.ValueChanged -= setValue;
			}
        }
        
		partial void AddBinderManual(IBinder binder, string propertyName, ref bool flag);
		
		partial void RemoveBinderManual(IBinder binder, string propertyName, ref bool flag);
    }
}