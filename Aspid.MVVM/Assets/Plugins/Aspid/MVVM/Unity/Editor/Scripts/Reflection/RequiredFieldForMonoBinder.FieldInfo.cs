#nullable enable
using System;
using System.Reflection;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed partial class RequiredFieldForMonoBinder
    {
        public override string Name => _field.Name;

        public override Type FieldType => _field.FieldType;
        
        public override Type? ReflectedType => _field.ReflectedType;
        
        public override Type? DeclaringType => _field.DeclaringType;
        
        public override FieldAttributes Attributes => _field.Attributes;
        
        public override RuntimeFieldHandle FieldHandle => _field.FieldHandle;
        
        public override object[] GetCustomAttributes(bool inherit) =>
            _field.GetCustomAttributes(inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            _field.GetCustomAttributes(attributeType, inherit);

        public override object GetValue(object obj) => 
            _field.GetValue(obj);

        public override bool IsDefined(Type attributeType, bool inherit) =>
            _field.IsDefined(attributeType, inherit);

        public override void SetValue(object obj, object value, BindingFlags invokeAttr, System.Reflection.Binder binder, CultureInfo culture) =>
            _field.SetValue(obj, value);
    }
}