using System.Collections.Generic;
using UltimateUI.MVVM.Views;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy
{
    [View]
    public partial class WalletView : MonoView
    {
        [SerializeField] private MonoBinder[] _currencies;
        public override IEnumerable<(string id, IReadOnlyList<IBinder> binders)> GetBindersLazy()
        {
            throw new System.NotImplementedException();
        }
    }
}