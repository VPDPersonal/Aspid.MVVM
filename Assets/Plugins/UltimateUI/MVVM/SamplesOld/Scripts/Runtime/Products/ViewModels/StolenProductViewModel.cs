using System;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.Products.Models;
using UltimateUI.MVVM.Samples.Economic.Currency.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products
{
    // public partial class StolenProductViewModel : ProductViewModel
    // {
    //     [Bind] private int _theftProbability;
    //     
    //     public StolenProductViewModel(StolenProduct product, CurrencyViewData viewData)
    //         : base(product, viewData)
    //     {
    //         _theftProbability = product.TheftProbability;
    //     }
    // }
    //
    // public partial class StolenProductViewModel
    // {
    //     private event Action<int> TheftProbabilityChanged;
    //     
    //     private int TheftProbability
    //     {
    //         get => _theftProbability;
    //         set => ViewModelUtility.SetValue(ref _theftProbability, value, TheftProbabilityChanged);
    //     }
    //     
    //     protected override IReadOnlyDictionary<string, Action<IReadOnlyList<IBinder>>> GetBindFieldsForBind()
    //     {
    //         Dictionary<string, Action<IReadOnlyList<IBinder>>> binds = new ()
    //         {
    //             { nameof(TheftProbability), binders => ViewModelUtility.Bind(TheftProbability, ref TheftProbabilityChanged, binders) }
    //         };
    //         
    //         foreach (var pair in base.GetBindFieldsForBind())
    //             binds.Add(pair.Key, pair.Value);
    //         
    //         return binds;
    //     }
    //     
    //     protected override IReadOnlyDictionary<string, Action<IReadOnlyList<IBinder>>> GetBindFieldsForUnbind()
    //     {
    //         Dictionary<string, Action<IReadOnlyList<IBinder>>> unbinds = new ()
    //         {
    //             { nameof(TheftProbability), binders => ViewModelUtility.Unbind(ref TheftProbabilityChanged, binders) }
    //         };
    //         
    //         foreach (var pair in base.GetBindFieldsForUnbind())
    //             unbinds.Add(pair.Key, pair.Value);
    //         
    //         return unbinds;
    //     }
    // }
}