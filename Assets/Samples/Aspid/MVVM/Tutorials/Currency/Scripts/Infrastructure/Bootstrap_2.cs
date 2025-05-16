using UnityEngine;
using Aspid.MVVM.Currency.Views;
using Aspid.MVVM.Currency.Models;
using Aspid.MVVM.Currency.ViewModel;

namespace Aspid.MVVM.Currency.Infrastructure
{
    public sealed class Bootstrap_2 : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _soft;
        [SerializeField] [Min(0)] private int _hard;
        [SerializeField] private CurrencyView_2 _view;

        private IShop _shop;
        private Wallet _wallet;
        
        private void Awake()
        {
            _wallet = new Wallet(_soft, _hard);
            _shop = new DebugShop(_wallet);

            var viewModel = new CurrencyViewModel_2(_shop, _wallet);
            _view.Initialize(viewModel);
        }
    }
}