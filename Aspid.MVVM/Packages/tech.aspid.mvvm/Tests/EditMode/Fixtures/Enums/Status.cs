using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    [Flags]
    internal enum Status
    {
        None = 0,

        [InspectorName("On fire")]
        Burning = 1,

        // A Description and no InspectorName, so the two sources have to part ways on this member.
        [System.ComponentModel.Description("Losing health slowly")]
        Poisoned = 2,

        Frozen = 4,
    }
}
