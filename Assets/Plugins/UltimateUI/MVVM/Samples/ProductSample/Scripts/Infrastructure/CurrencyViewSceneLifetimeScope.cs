using VContainer;
using UnityEngine;
using VContainer.Unity;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;
using UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels;
using UltimateUI.MVVM.ViewBinders;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Infrastructure
{
    public sealed class CurrencyViewSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private CurrencyViewDataCollection _currencyViewDataCollection;
        [SerializeField] private CurrencyDefaultData[] _currencies;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Wallet>(Lifetime.Singleton)
                .WithParameter(_currencies);

            builder.RegisterInstance(_currencyViewDataCollection);

            builder.Register<WalletViewModel>(Lifetime.Singleton);
            
            builder.Register<CoinsViewModel>(Lifetime.Singleton);
            builder.Register<EmeraldsViewModel>(Lifetime.Singleton);
        }
    }
}
