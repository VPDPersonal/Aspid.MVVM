using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class BindPropertyContext : IFieldContext
    {
        public object Target { get; }
        
        public Type MemberType { get; }

        public MemberInfo Member => _bindProperty;

        public bool IsAlternativeColor { get; }
        
        public bool IsReadonly => !_bindProperty.CanWrite;
        
        private readonly PropertyInfo _bindProperty;

        internal BindPropertyContext(object viewModel, PropertyInfo bindProperty, bool isAlternativeColor = false)
        {
            if (viewModel is not IViewModel) 
                throw new ArgumentException("Target is not ViewModel");
            
            if (!viewModel.GetType().IsDefined(typeof(ViewModelAttribute), inherit: true))
                throw new ArgumentException("Target has not ViewModelAttribute");

            Target = viewModel;
            _bindProperty = bindProperty;
            MemberType = bindProperty.PropertyType;
            IsAlternativeColor = isAlternativeColor;
        }
        
        public object GetValue() => 
            _bindProperty.GetValue(Target);

        public void SetValue(object value)
        {
            if (IsReadonly) return;
            _bindProperty.SetValue(Target, value);
        }

        public bool IsDefined(Type attributeType, bool inherit = false) =>
            Member.IsDefined(attributeType, inherit);
        
        public T GetCustomAttribute<T>() 
            where T : Attribute
        {
            return Member.GetCustomAttribute<T>();
        }
    }
}
