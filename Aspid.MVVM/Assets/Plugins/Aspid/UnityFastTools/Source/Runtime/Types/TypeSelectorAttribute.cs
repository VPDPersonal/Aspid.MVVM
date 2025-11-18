#nullable enable
using System;
using UnityEngine;
using System.Diagnostics;

// TODO Aspid.UnityFastTools – Write summary
// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [Conditional("UNITY_EDITOR")]
    public sealed class TypeSelectorAttribute : PropertyAttribute
    {
        public readonly Type? Type;

        public TypeSelectorAttribute()
        {
            Type = typeof(object);
        }

        public TypeSelectorAttribute(Type type)
        {
            Type = type;
        }
    }
}