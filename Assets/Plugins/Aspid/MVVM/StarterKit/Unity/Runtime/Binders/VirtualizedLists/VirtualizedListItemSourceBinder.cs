using UnityEngine;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Unity
{
    public sealed class VirtualizedListItemSourceBinder : TargetBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private IFilterFactory<IViewModel> _filterFactory;

        public VirtualizedListItemSourceBinder(VirtualizedList target, BindMode mode) 
            : this(target, null, mode) { }
        
        public VirtualizedListItemSourceBinder(VirtualizedList target, IFilterFactory<IViewModel> filterFactory = null, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _filterFactory = filterFactory;
        }

        protected override void OnUnbound() =>
            _filterFactory?.Release();

        public void SetValue(IReadOnlyList<IViewModel> value)
        {
            Target.ItemsSource = value is not null && _filterFactory is not null 
                ? _filterFactory.Create(value) 
                : value;
        }
    }
}