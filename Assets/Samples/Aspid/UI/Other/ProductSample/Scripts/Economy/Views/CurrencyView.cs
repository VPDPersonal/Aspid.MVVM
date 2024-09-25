using System.Collections.Generic;
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using UnityEngine;

namespace Aspid.UI.ProductSample.Economy.Views
{
    [View]
    public partial class CurrencyView : MonoView
    {
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private MonoBinder[] _icon;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _currency;
    }
}