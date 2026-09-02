using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal enum Quest
    {
        [System.ComponentModel.Description("Not started yet")]
        Idle,

        [InspectorName("In progress")]
        [System.ComponentModel.Description("The quest is running")]
        Active,

        // An InspectorName and no Description: the Description source must not read the neighbour.
        [InspectorName("Wrapped up")]
        Done,

        // An attribute written with no text is a mistake, not a request for a blank label.
        [System.ComponentModel.Description("")]
        Failed,

        Abandoned,
    }
}
