using System;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.ViewModels
{
    public sealed class DynamicViewModel : ViewModel
    {
        private readonly bool _throwErrorIfIdNotFind;
        private readonly Dictionary<string, IDynamicProperty> _properties;
        
        public DynamicViewModel(Dictionary<string, IDynamicProperty> properties)
            : this(false, properties) { }
        
        public DynamicViewModel(bool throwErrorIfNotFindProperty, Dictionary<string, IDynamicProperty> properties)
        {
            _properties = properties;
            _throwErrorIfIdNotFind = throwErrorIfNotFindProperty;
        }

        protected override BindResult AddBinderInternal(IBinder binder, string propertyName)
        {
            if (_properties.TryGetValue(propertyName, out var value))
                return value.AddBinder(binder);

            return !_throwErrorIfIdNotFind ? default : throw new ArgumentException(nameof(propertyName));
        }

        #region Create Methods
        public static DynamicViewModel Create<T>(DynamicPropertyData<T> p) => 
            new(new Dictionary<string, IDynamicProperty> 
            { 
                [p.Id] = new DynamicProperty<T>(p.Value), 
            });
        
        public static DynamicViewModel Create<T>(bool throwErrorIfNotFindProperty, DynamicPropertyData<T> p) => 
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty> 
            { 
                [p.Id] = new DynamicProperty<T>(p.Value), 
            });
        
        public static DynamicViewModel Create<T1, T2>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
            });
        
        public static DynamicViewModel Create<T1, T2>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5>(
            DynamicPropertyData<T1> p1, 
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1, 
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5, T6>(
            DynamicPropertyData<T1> p1, 
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4, 
            DynamicPropertyData<T5> p5, 
            DynamicPropertyData<T6> p6) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
                [p6.Id] = new DynamicProperty<T6>(p6.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5, T6>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1, 
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4, 
            DynamicPropertyData<T5> p5, 
            DynamicPropertyData<T6> p6) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
                [p6.Id] = new DynamicProperty<T6>(p6.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5, T6, T7>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3, 
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5,
            DynamicPropertyData<T6> p6, 
            DynamicPropertyData<T7> p7) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
                [p6.Id] = new DynamicProperty<T6>(p6.Value),
                [p7.Id] = new DynamicProperty<T7>(p7.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5, T6, T7>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3, 
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5,
            DynamicPropertyData<T6> p6, 
            DynamicPropertyData<T7> p7) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
                [p6.Id] = new DynamicProperty<T6>(p6.Value),
                [p7.Id] = new DynamicProperty<T7>(p7.Value),
            });

        public static DynamicViewModel Create<T1, T2, T3, T4, T5, T6, T7, T8>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3, 
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5,
            DynamicPropertyData<T6> p6, 
            DynamicPropertyData<T7> p7,
            DynamicPropertyData<T8> p8) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
                [p6.Id] = new DynamicProperty<T6>(p6.Value),
                [p7.Id] = new DynamicProperty<T7>(p7.Value),
                [p8.Id] = new DynamicProperty<T8>(p8.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5, T6, T7, T8>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3, 
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5,
            DynamicPropertyData<T6> p6, 
            DynamicPropertyData<T7> p7,
            DynamicPropertyData<T8> p8) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = new DynamicProperty<T1>(p1.Value),
                [p2.Id] = new DynamicProperty<T2>(p2.Value),
                [p3.Id] = new DynamicProperty<T3>(p3.Value),
                [p4.Id] = new DynamicProperty<T4>(p4.Value),
                [p5.Id] = new DynamicProperty<T5>(p5.Value),
                [p6.Id] = new DynamicProperty<T6>(p6.Value),
                [p7.Id] = new DynamicProperty<T7>(p7.Value),
                [p8.Id] = new DynamicProperty<T8>(p8.Value),
            });
        #endregion
    }
}