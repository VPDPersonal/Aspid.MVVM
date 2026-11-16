#nullable enable
using System;
using System.Reflection;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed partial class RequiredFieldForMonoBinder
    {
        /// <inheritdoc/>
        public override string Name => _field.Name;

        /// <inheritdoc/>
        public override Type FieldType => _field.FieldType;
        
        /// <inheritdoc/>
        public override Type? ReflectedType => _field.ReflectedType;
        
        /// <inheritdoc/>
        public override Type? DeclaringType => _field.DeclaringType;
        
        /// <inheritdoc/>
        public override FieldAttributes Attributes => _field.Attributes;
        
        /// <inheritdoc/>
        public override RuntimeFieldHandle FieldHandle => _field.FieldHandle;
        
        /// <inheritdoc/>
        public override object[] GetCustomAttributes(bool inherit) =>
            _field.GetCustomAttributes(inherit);

        /// <inheritdoc/>
        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            _field.GetCustomAttributes(attributeType, inherit);

        /// <inheritdoc/>
        public override object GetValue(object obj) => 
            _field.GetValue(obj);

        /// <inheritdoc/>
        public override bool IsDefined(Type attributeType, bool inherit) =>
            _field.IsDefined(attributeType, inherit);

        /// <inheritdoc/>
        public override void SetValue(object obj, object value, BindingFlags invokeAttr, System.Reflection.Binder binder, CultureInfo culture) =>
            _field.SetValue(obj, value);
    }
}