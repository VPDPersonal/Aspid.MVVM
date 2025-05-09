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
        private readonly Dictionary<string, IDynamicProperty> _properties;
        
        public DynamicViewModel(Dictionary<string, IDynamicProperty> properties)
            : this(false, properties) { }
        
        public DynamicViewModel(bool throwErrorIfNotFindProperty, Dictionary<string, IDynamicProperty> properties)
        {
            _properties = properties;
            _throwErrorIfIdNotFind = throwErrorIfNotFindProperty;
        }

        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                if (_properties.TryGetValue(parameters.Id, out var value))
                    return new FindBindableMemberResult(value.GetAdder());

                return !_throwErrorIfIdNotFind ? default : throw new ArgumentException(nameof(parameters.Id));
            }
        }

        public FindBindableMemberResult<T> FindBindableMember<T>(in FindBindableMemberParameters parameters)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                if (_properties.TryGetValue(parameters.Id, out var value))
                    return new FindBindableMemberResult<T>(((BindableMember<T>)value.GetAdder())!);

                return !_throwErrorIfIdNotFind ? default : throw new ArgumentException(nameof(parameters.Id));
            }
        }
    }
}