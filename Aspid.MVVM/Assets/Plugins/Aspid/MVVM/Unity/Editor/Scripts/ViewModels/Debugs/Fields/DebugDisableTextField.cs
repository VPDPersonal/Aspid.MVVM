using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugDisableTextField : TextField
    {
        public DebugDisableTextField()
        {
            SetEnabled(false);
        }

        public DebugDisableTextField(string label) 
            : base(label)
        {
            SetEnabled(false);
        }
    }
}