using System;
using UnityEngine;
using UltimateUI.MVVM.ViewModels;
using System.Collections.Generic;
using UltimateUI.MVVM.Samples.Economic.Currency.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Economic
{
    // public partial class CoinsViewModel : IViewModel, IDisposable
    // {
    //     [Bind] private int _money;
    //     [Bind] private Sprite _icon;
    //     
    //     private bool _isInitialize;
    //     
    //     private readonly Wallet _wallet;
    //     private readonly CurrencyViewData _currencyViewData;
    //     
    //     public CoinsViewModel(
    //         Wallet wallet,
    //         CurrencyViewData currencyViewData)
    //     {
    //         _wallet = wallet;
    //         _currencyViewData = currencyViewData;
    //         
    //         _money = _wallet.Coins;
    //         _wallet.CoinChanged += OnCoinChanged;
    //     }
    //     
    //     private void UpdateIconState() =>
    //         Icon = _currencyViewData.GetIcon(Money);
    //     
    //     private void OnCoinChanged()
    //     {
    //         Money = _wallet.Coins;
    //         UpdateIconState();
    //     }
    //     
    //     public void Dispose()
    //     {
    //         _wallet.CoinChanged -= OnCoinChanged;
    //     }
    // }
    //
    // public partial class CoinsViewModel
    // {
    //     private event Action<int> MoneyChanged;
    //     private event Action<Sprite> IconChanged;
    //     
    //     private int Money
    //     {
    //         get => _money;
    //         set => ViewModelUtility.SetValue(ref _money, value, MoneyChanged);
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
    //         Dictionary<string, Action<IReadOnlyList<IBinder>>> binds = new()
    //         {
    //             { nameof(_money), binders => ViewModelUtility.Bind(Money, ref MoneyChanged, binders) },
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
    //         Dictionary<string, Action<IReadOnlyList<IBinder>>> unbinds = new()
    //         {
    //             { nameof(_money), binders => ViewModelUtility.Unbind(ref MoneyChanged, binders) },
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