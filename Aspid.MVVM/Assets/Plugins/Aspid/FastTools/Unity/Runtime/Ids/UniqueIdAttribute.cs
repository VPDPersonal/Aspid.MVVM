#nullable enable
using UnityEngine;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    [Conditional(conditionString: "UNITY_EDITOR")]
    public sealed class UniqueIdAttribute : PropertyAttribute { }
}
