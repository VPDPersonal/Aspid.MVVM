using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterObjectToString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [View]
    public partial class DebugLobMonoView : MonoView
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();
        
        [NonSerialized]
        private List<DebugLogBinder> _binders;
        
        partial void OnInitializingInternal(IViewModel viewModel)
        {
            _binders ??= new List<DebugLogBinder>();
            
            if (viewModel is IBinderAdderEnumerable enumerable)
            {
                var index = 0;
                
                foreach (var binderAdder in enumerable.GetBinderAdders())
                {
                    if (index >= _binders.Count)
                        _binders.Add(new DebugLogBinder(_converter));
                    
                    _binders[index++].Bind(binderAdder);
                }
            }
        }

        partial void OnDeinitializedInternal()
        {
            foreach (var binder in _binders)
            {
                binder.Unbind();
            }
        }
    }
}