using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.Currency.Models;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.Currency.Views
{
    [View]
    public partial class CurrencyView_2 : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _soft;
    
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _hard;

        [SerializeField] private ButtonCommandBinder<ShopCategory>[] _openShopCommand;
    }
}