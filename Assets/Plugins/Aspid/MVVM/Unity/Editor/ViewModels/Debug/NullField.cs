using UnityEngine.UIElements;

namespace Aspid.MVVM.Unity
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