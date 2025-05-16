using UnityEngine;
using Aspid.MVVM.Currency.Models;
using Aspid.MVVM.Currency.ViewModel;

namespace Aspid.MVVM.Currency.Infrastructure
{
    public sealed class Bootstrap_1 : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _soft;
        [SerializeField] [Min(0)] private int _hard;
        [SerializeField] private Views.CurrencyView_1 _view;

        private Wallet _wallet;
        
        private void Awake()
        {
            _wallet = new Wallet(_soft, _hard);

            var viewModel = new CurrencyViewModel_1(_wallet);
            _view.Initialize(viewModel);
        }
    }
}