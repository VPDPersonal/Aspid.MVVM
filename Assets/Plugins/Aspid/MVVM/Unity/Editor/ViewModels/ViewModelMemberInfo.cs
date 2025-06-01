using System;
using System.Reflection;

namespace Aspid.MVVM.Unity
{
    public readonly struct ViewModelMemberInfo
    {
        public readonly string Tag;
        public readonly string Name;

        private readonly object _target;
        private readonly MemberInfo _info;

        public Type Type => _info switch
        {
            FieldInfo field => field.FieldType,
            PropertyInfo property => property.PropertyType,
            _ => _target.GetType(),
        };
        
        public bool IsReadonly => _info switch
        {
            FieldInfo field => field.IsInitOnly,
            PropertyInfo property => !property.CanWrite,
            _ => true,
        };

        public object Value
        {
            get => _info switch
            {
                FieldInfo field => field.GetValue(_target),
                PropertyInfo property => property.GetValue(_target),
                _ => _target
            };

            set
            {
                switch (_info)
                {
                    case FieldInfo field: field.SetValue(_target, value); break;
                    case PropertyInfo property: property.SetValue(_target, value); break;
                }
            }
        }

        public ViewModelMemberInfo(object value, string name)
        {
            Tag = null;
            Name = name;
            _info = null;
            _target = value;
        }
        
        public ViewModelMemberInfo(object target, System.Reflection.MemberInfo info, bool isBind = false)
        {
            _info = info;
            Name = info.Name;
            _target = target;
            Tag = isBind ? "B" : target is FieldInfo ? "F" : "P";
        }
    }
}