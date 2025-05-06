using System;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit
{
    public sealed partial class DynamicViewModel : IViewModel
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addBinderMarker = new("DynamicViewModel.AddBinder"); 
#endif
        
        private readonly bool _throwErrorIfIdNotFind;
        private readonly Dictionary<Id, IDynamicProperty> _properties;
        
        public DynamicViewModel(Dictionary<Id, IDynamicProperty> properties)
            : this(false, properties) { }
        
        public DynamicViewModel(bool throwErrorIfNotFindProperty, Dictionary<Id, IDynamicProperty> properties)
        {
            _properties = properties;
            _throwErrorIfIdNotFind = throwErrorIfNotFindProperty;
        }

        public FindBindableMemberResult FindBindableMember(Id id)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                if (_properties.TryGetValue(id, out var value))
                    return new FindBindableMemberResult(true, value.GetAdder());

                return !_throwErrorIfIdNotFind ? default : throw new ArgumentException(nameof(id));
            }
        }

        public FindBindableMemberResult<T> FindBindableMember<T>(Id id)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                if (_properties.TryGetValue(id, out var value))
                    return new FindBindableMemberResult<T>(true, ((BindableMember<T>)value.GetAdder())!);

                return !_throwErrorIfIdNotFind ? default : throw new ArgumentException(nameof(id));
            }
        }
    }
}