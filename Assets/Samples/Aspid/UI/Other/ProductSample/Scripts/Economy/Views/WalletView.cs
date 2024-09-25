using System.Collections.Generic;
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using UnityEngine;

namespace Aspid.UI.ProductSample.Economy.Views
{
    [View]
    public partial class WalletView : MonoView
    {
        [SerializeField] private MonoBinder[] _currencies;
        // public override IEnumerable<(string id, IReadOnlyList<IBinder> binders)> GetBindersLazy()
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}