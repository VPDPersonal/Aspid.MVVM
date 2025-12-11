using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class DebugViewModelField : VisualElement, IUpdatableField
    {
        private readonly DebugField _debugField;
        
        private DebugViewModelField(DebugField debugField)
        {
            _debugField = debugField;
        }

        public static DebugViewModelField Create(object obj, MemberInfo memberInfo, bool isAlternativeColor = false)
        {
            var type = obj?.GetType();
            if (type is null) return null;
            
            if (memberInfo is FieldInfo)
            {
                var inputField = new DebugField(memberInfo.Name, FieldContextFactory.Create(obj, memberInfo, isAlternativeColor));
                
                return new DebugViewModelField(inputField)
                    .SetMargin(top: 4)
                    .AddChild(inputField);
            }

            return null;
        }
        
        public void UpdateValue()
        {
            _debugField?.UpdateValue();
        }
    }
}
