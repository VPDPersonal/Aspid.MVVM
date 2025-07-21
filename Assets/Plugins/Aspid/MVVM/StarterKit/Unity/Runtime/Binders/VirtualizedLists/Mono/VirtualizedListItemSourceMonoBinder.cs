using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using FilterFactory = Aspid.MVVM.StarterKit.IFilterFactory<Aspid.MVVM.IViewModel>;
#else
using FilterFactory = Aspid.MVVM.StarterKit.IViewModelFilterFactory;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList Binder - ItemSource")]
    [AddComponentContextMenu(typeof(VirtualizedList),"Add VirtualizedList Binder/VirtualizedList Binder - ItemSource")]
    public sealed partial class VirtualizedListItemSourceMonoBinder : ComponentMonoBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private FilterFactory _filterFactory;

        protected override void OnUnbound() =>
            _filterFactory?.Release();

        [BinderLog]
        public void SetValue(IReadOnlyList<IViewModel> value)
        {
            CachedComponent.ItemsSource = value is not null && _filterFactory is not null 
                ? _filterFactory.Create(value) 
                : value;
        }
    }
}