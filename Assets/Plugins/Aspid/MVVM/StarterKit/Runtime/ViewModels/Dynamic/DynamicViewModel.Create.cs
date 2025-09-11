using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed partial class DynamicViewModel
    {
        public static DynamicViewModel Create<T>(DynamicPropertyData<T> p) => 
            new(new Dictionary<string, IDynamicProperty> 
            { 
                [p.Id] = DynamicPropertyFactory.Create(p.Value), 
            });
        
        public static DynamicViewModel Create<T>(bool throwErrorIfNotFindProperty, DynamicPropertyData<T> p) => 
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty> 
            { 
                [p.Id] = DynamicPropertyFactory.Create(p.Value), 
            });
        
        public static DynamicViewModel Create<T1, T2>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
            });
        
        public static DynamicViewModel Create<T1, T2>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4>(
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4>(
            bool throwErrorIfNotFindProperty,
            DynamicPropertyData<T1> p1,
            DynamicPropertyData<T2> p2,
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4) =>
            new(throwErrorIfNotFindProperty, new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
            });
        
        public static DynamicViewModel Create<T1, T2, T3, T4, T5>(
            DynamicPropertyData<T1> p1, 
            DynamicPropertyData<T2> p2, 
            DynamicPropertyData<T3> p3,
            DynamicPropertyData<T4> p4,
            DynamicPropertyData<T5> p5) =>
            new(new Dictionary<string, IDynamicProperty>
            {
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
                [p6.Id] = DynamicPropertyFactory.Create(p6.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
                [p6.Id] = DynamicPropertyFactory.Create(p6.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
                [p6.Id] = DynamicPropertyFactory.Create(p6.Value),
                [p7.Id] = DynamicPropertyFactory.Create(p7.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
                [p6.Id] = DynamicPropertyFactory.Create(p6.Value),
                [p7.Id] = DynamicPropertyFactory.Create(p7.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1.Value),
                [p2.Id] = DynamicPropertyFactory.Create(p2.Value),
                [p3.Id] = DynamicPropertyFactory.Create(p3.Value),
                [p4.Id] = DynamicPropertyFactory.Create(p4.Value),
                [p5.Id] = DynamicPropertyFactory.Create(p5.Value),
                [p6.Id] = DynamicPropertyFactory.Create(p6.Value),
                [p7.Id] = DynamicPropertyFactory.Create(p7.Value),
                [p8.Id] = DynamicPropertyFactory.Create(p8.Value),
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
                [p1.Id] = DynamicPropertyFactory.Create(p1),
                [p2.Id] = DynamicPropertyFactory.Create(p2),
                [p3.Id] = DynamicPropertyFactory.Create(p3),
                [p4.Id] = DynamicPropertyFactory.Create(p4),
                [p5.Id] = DynamicPropertyFactory.Create(p5),
                [p6.Id] = DynamicPropertyFactory.Create(p6),
                [p7.Id] = DynamicPropertyFactory.Create(p7),
                [p8.Id] = DynamicPropertyFactory.Create(p8),
            });
    }
}