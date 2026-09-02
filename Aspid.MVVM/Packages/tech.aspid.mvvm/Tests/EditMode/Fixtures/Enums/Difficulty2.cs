using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal enum Difficulty2
    {
        Easy,

        [InspectorName("Very hard")]
        Brutal,
    }
}
