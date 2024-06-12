using UltimateUI.MVVM.Samples.ProductSample.Economy;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;
using UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Views;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample
{
    public class ProductSampleBootstrap : MonoBehaviour
    {
        [SerializeField] private CurrencyView _coinsView;
        [SerializeField] private CurrencyView _emeraldView;
        [SerializeField] private CurrencyViewDataCollection _currencyViewDataCollection;
        
        [Space]
        [SerializeField] [Min(0)] private int _coinsCount = 100;
        [SerializeField] [Min(0)] private int _emeraldCount = 100;
        
        private void Awake()
        {
            var wallet = new Wallet((CurrencyType.Coins, _coinsCount), (CurrencyType.Emeralds, _emeraldCount));
            
            var coinsViewModel = new CurrencyViewModel(wallet, _currencyViewDataCollection, CurrencyType.Coins);
            var emeraldsViewModel = new CurrencyViewModel(wallet, _currencyViewDataCollection, CurrencyType.Emeralds);
            
            BindViewAndViewModel(_coinsView, coinsViewModel);
            BindViewAndViewModel(_emeraldView, emeraldsViewModel);
        }
        
        private void BindViewAndViewModel(IView view, IViewModel viewModel)
        {
            var binds = viewModel.GetBinds();
            var binders = view.GetBinders();
            
            foreach (var binder in binders)
            {
                if (binds.ContainsKey(binder.Key))
                    binds[binder.Key]?.Invoke(binder.Value);
            }
        }
    }
}
