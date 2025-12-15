using System;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class BindFieldContext : IFieldContext
    {
        public object Target { get; }
        
        public Type MemberType { get; }

        public MemberInfo Member => _bindField;

        public bool IsAlternativeColor { get; }
        
        public bool IsReadonly => false;
        
        private readonly FieldInfo _bindField;
        private readonly PropertyInfo _generatedProperty;

        internal BindFieldContext(object viewModel, FieldInfo bindField, bool isAlternativeColor = false)
        {
            if (viewModel is not IViewModel) 
                throw new ArgumentException("Target is not ViewModel");
            
            if (!viewModel.GetType().IsDefined(typeof(ViewModelAttribute), inherit: true))
                throw new ArgumentException("Target has not ViewModelAttribute");

            Target = viewModel;
            _bindField = bindField;
            MemberType = bindField.FieldType;
            IsAlternativeColor = isAlternativeColor;

            // Find generated property.
            _generatedProperty = viewModel.GetType()
                .GetPropertyInfosIncludingBaseClasses(bindingFlags: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(property => property.Name == bindField.GetGeneratedPropertyName());
            
            if (_generatedProperty is null)
                throw new ArgumentException($"ViewModel has not generated property for field: {bindField.Name}");
        }
        
        public object GetValue() => 
            _bindField.GetValue(Target);

        public void SetValue(object value) =>
            _generatedProperty.SetValue(Target, value);
        
        public bool IsDefined(Type attributeType, bool inherit = false) =>
            Member.IsDefined(attributeType, inherit);
        
        public T GetCustomAttribute<T>() 
            where T : Attribute
        {
            return Member.GetCustomAttribute<T>();
        }
    }
}
