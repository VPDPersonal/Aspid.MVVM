using System.Collections.Generic;
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using UnityEngine;

namespace Aspid.UI.ProductSample
{
    [View]
    public partial class ProductView : MonoView
    {
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private MonoBinder[] _icon;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _name;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _description;
        
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private Sprite _currencyIcon;
    }
}