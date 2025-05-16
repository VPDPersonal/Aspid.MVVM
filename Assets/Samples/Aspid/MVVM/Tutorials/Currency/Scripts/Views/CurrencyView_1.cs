using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.Currency.Views
{
    [View]
    public partial class CurrencyView_1 : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _soft;
    
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _hard;
    }
}
