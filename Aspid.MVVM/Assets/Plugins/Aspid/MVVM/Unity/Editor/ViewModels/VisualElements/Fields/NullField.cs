using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class NullField : TextField
    {
        // TODO Aspid.MVVM Unity – Refactor
        // TODO Aspid.MVVM Unity – Write summary
        public NullField(string label = null)
            : base(label)
        {
            value = "null";
            SetEnabled(false);
        }
    }
}