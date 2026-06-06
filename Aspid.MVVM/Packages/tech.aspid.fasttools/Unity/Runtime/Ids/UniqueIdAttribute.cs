#nullable enable
using UnityEngine;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids
{
    /// <summary>
    /// Marks an integer field as a project-wide unique id. The editor drawer enforces uniqueness across all
    /// fields decorated with this attribute and offers a registry-aware id picker.
    /// The attribute is editor-only — its <see cref="ConditionalAttribute"/> ensures usages are stripped from player builds.
    /// </summary>
    [Conditional(conditionString: "UNITY_EDITOR")]
    public sealed class UniqueIdAttribute : PropertyAttribute { }
}
