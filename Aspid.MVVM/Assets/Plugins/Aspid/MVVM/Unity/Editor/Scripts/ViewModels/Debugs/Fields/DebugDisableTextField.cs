using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugDisableTextField : TextField
    {
        public DebugDisableTextField()
        {
            isReadOnly = true;
        }

        public DebugDisableTextField(string label) 
            : base(label)
        {
            isReadOnly = true;
        }
    }
}