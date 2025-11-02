using System;
using System.Reflection;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class MonoBinderValidableFieldInfo : FieldInfo
    {
        private readonly object _target;
        private RequiredTypes _requiredTypes;
        private readonly FieldInfo _fieldInfo;

        public override FieldAttributes Attributes => _fieldInfo.Attributes;
        
        public override RuntimeFieldHandle FieldHandle => _fieldInfo.FieldHandle;

        public override Type FieldType => _fieldInfo.FieldType;
        
        public override Type DeclaringType =>_fieldInfo.DeclaringType;

        public override string Name => _fieldInfo.Name;
        
        public override Type ReflectedType => _fieldInfo.ReflectedType;

        public MonoBinderValidableFieldInfo(object target, FieldInfo field)
        {
            _target = target;
            _fieldInfo = field;
        }

        public RequiredTypes GetRequiredTypes()
        {
            _requiredTypes ??= new RequiredTypes(_target, this);
            return _requiredTypes;
        }

        public override object[] GetCustomAttributes(bool inherit) =>
            _fieldInfo.GetCustomAttributes(inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            _fieldInfo.GetCustomAttributes(attributeType, inherit);

        public override object GetValue(object obj) =>
            _fieldInfo.GetValue(obj);

        public override bool IsDefined(Type attributeType, bool inherit) =>
            _fieldInfo.IsDefined(attributeType, inherit);

        public override void SetValue(object obj, object value, BindingFlags invokeAttr, System.Reflection.Binder binder, CultureInfo culture) =>
            _fieldInfo.SetValue(obj, value, invokeAttr, binder, culture);
    }
}