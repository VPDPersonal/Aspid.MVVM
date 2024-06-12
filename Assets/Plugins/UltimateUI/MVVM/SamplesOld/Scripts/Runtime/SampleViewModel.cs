using System;
using UnityEngine;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples
{
    // public partial class SampleViewModel : IViewModel, IDisposable
    // {
    //     [Bind] private int _age;
    //     [Bind] private string _name;
    //     [Bind] private Sprite _icon;
    //     
    //     public void Dispose()
    //     {
    //         
    //     }
    // }
    //
    // public partial class SampleViewModel
    // {
    //     private event Action<int> AgeChanged;
    //     private event Action<string> NameChanged;
    //     private event Action<Sprite> IconChanged;
    //     
    //     private int Age
    //     {
    //         get => _age;
    //         set => ViewModelUtility.SetValue(ref _age, value, AgeChanged);
    //     }
    //     
    //     private string Name
    //     {
    //         get => _name;
    //         set => ViewModelUtility.SetValue(ref _name, value, NameChanged);
    //     }
    //     
    //     private Sprite Icon
    //     {
    //         get => _icon;
    //         set => ViewModelUtility.SetValue(ref _icon, value, IconChanged);
    //     }
    //     
    //     public virtual void Bind(IReadOnlyDictionary<string, IReadOnlyList<IBinder>> bindersById)
    //     {
    //         Dictionary<string, Action<IReadOnlyList<IBinder>>> binds = new ()
    //         {
    //             { nameof(_age), binders => ViewModelUtility.Bind(Age, ref AgeChanged, binders) },
    //             { nameof(_name), binders => ViewModelUtility.Bind(Name, ref NameChanged, binders) },
    //             { nameof(_icon), binders => ViewModelUtility.Bind(Icon, ref IconChanged, binders) },
    //         };
    //         
    //         foreach (var pair in bindersById)
    //             binds[pair.Key].Invoke(pair.Value);
    //         
    //         binds.Clear();
    //     }
    //     
    //     public virtual void Unbind(IReadOnlyDictionary<string, IReadOnlyList<IBinder>> bindersById)
    //     {
    //         Dictionary<string, Action<IReadOnlyList<IBinder>>> unbinds = new ()
    //         {
    //             { nameof(_age), binders => ViewModelUtility.Unbind(ref AgeChanged, binders) },
    //             { nameof(_name), binders => ViewModelUtility.Unbind(ref NameChanged, binders) },
    //             { nameof(_icon), binders => ViewModelUtility.Unbind(ref IconChanged, binders) },
    //         };
    //         
    //         foreach (var pair in bindersById)
    //             unbinds[pair.Key].Invoke(pair.Value);
    //         
    //         unbinds.Clear();
    //     }
    // }
}