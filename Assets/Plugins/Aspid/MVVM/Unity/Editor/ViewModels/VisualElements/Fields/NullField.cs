using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class NullField : TextField
    {
        public NullField(string label = null)
            : base(label)
        {
            value = "null";
            SetEnabled(false);
        }
    }
}