#nullable enable
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugDisableTextField : TextField
    {
        internal DebugDisableTextField()
        {
            SetEnabled(false);
        }

        internal DebugDisableTextField(string label) 
            : base(label)
        {
            SetEnabled(false);
        }
    }
}