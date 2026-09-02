using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    // An InspectorName on the middle member, so the name and the inspector name sources can be told
    // apart, and Snow has neither attribute and falls back to its member name under both.
    internal enum Weather
    {
        Clear,

        [InspectorName("Light rain")]
        Rain,

        Snow,
    }
}
