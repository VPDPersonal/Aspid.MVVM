using Aspid.UI.ProductSample.Economy.Data;
using Aspid.UI.ProductSample.Economy.Models;
using Aspid.UI.ProductSample.Economy.ViewModels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Aspid.UI.ProductSample.Infrastructure
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
