using System;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.StatsSamples.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.ViewModels
{
    // public partial class StatsReadOnlyViewModel<T> : IViewModel, IDisposable
    //     where T : class, IStats
    // {
    //     [Bind] private int _hp;
    //     [Bind] private int _power;
    //     [Bind] private int _dexterity;
    //     
    //     protected readonly T Stats;
    //     
    //     public StatsReadOnlyViewModel(T stats)
    //     {
    //         Stats = stats;
    //         
    //         _hp = stats.Hp;
    //         _power = stats.Power;
    //         _dexterity = stats.Dexterity;
    //     }
    //     
    //     public void Initialize() => Subscribe();
    //     
    //     protected virtual void Subscribe()
    //     {
    //         Stats.HpChanged += OnHpChanged;
    //         Stats.PowerChanged += OnPowerChanged;
    //         Stats.DexterityChanged += OnDexterityChanged;
    //     }
    //     
    //     protected virtual void Unsubscribe()
    //     {
    //         Stats.HpChanged -= OnHpChanged;
    //         Stats.PowerChanged -= OnPowerChanged;
    //         Stats.DexterityChanged -= OnDexterityChanged;
    //     }
    //     
    //     private void OnHpChanged() => Hp = Stats.Hp;
    //     
    //     private void OnPowerChanged() => Power = Stats.Power;
    //     
    //     private void OnDexterityChanged() => Power = Stats.Dexterity;
    //     
    //     public void Dispose() => Unsubscribe();
    // }
    //
    // public partial class StatsReadOnlyViewModel<T>
    // {
    //     private event Action<int> HpChanged;
    //     private event Action<int> PowerChanged;
    //     private event Action<int> DexterityChanged;
    //     
    //     protected int Hp
    //     {
    //         get => _hp;
    //         set => ViewModelUtility.SetValue(ref _hp, value, HpChanged);
    //     }
    //     
    //     protected int Power
    //     {
    //         get => _power;
    //         set => ViewModelUtility.SetValue(ref _power, value, PowerChanged);
    //     }
    //     
    //     protected int Dexterity
    //     {
    //         get => _dexterity;
    //         set => ViewModelUtility.SetValue(ref _dexterity, value, DexterityChanged);
    //     }
    //     
    //     void IViewModel.Bind(IReadOnlyDictionary<string, IReadOnlyList<IBinder>> bindersById)
    //     {
    //         var binds = GetBindFieldsForBind();
    //         
    //         foreach (var pair in bindersById)
    //             binds[pair.Key].Invoke(pair.Value);
    //     }
    //     
    //     void IViewModel.Unbind(IReadOnlyDictionary<string, IReadOnlyList<IBinder>> bindersById)
    //     {
    //         var unbinds = GetBindFieldsForUnbind();
    //         
    //         foreach (var pair in bindersById)
    //             unbinds[pair.Key].Invoke(pair.Value);
    //     }
    //     
    //     protected virtual IReadOnlyDictionary<string, Action<IReadOnlyList<IBinder>>> GetBindFieldsForBind()
    //     {
    //         return new Dictionary<string, Action<IReadOnlyList<IBinder>>>
    //         {
    //             { nameof(Hp), binders => ViewModelUtility.Bind(Hp, ref HpChanged, binders) },
    //             { nameof(Power), binders => ViewModelUtility.Bind(Power, ref PowerChanged, binders) },
    //             { nameof(Dexterity), binders => ViewModelUtility.Bind(Dexterity, ref DexterityChanged, binders) }
    //         };
    //     }
    //     
    //     protected virtual IReadOnlyDictionary<string, Action<IReadOnlyList<IBinder>>> GetBindFieldsForUnbind()
    //     {
    //         return new Dictionary<string, Action<IReadOnlyList<IBinder>>>
    //         {
    //             { nameof(Hp), binders => ViewModelUtility.Unbind(ref HpChanged, binders) },
    //             { nameof(Power), binders => ViewModelUtility.Unbind(ref PowerChanged, binders) },
    //             { nameof(Dexterity), binders => ViewModelUtility.Unbind(ref DexterityChanged, binders) }
    //         };
    //     }
    // }
}