#if UNITY_EDITOR || DEBUG
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal interface IRebindableBinder
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Rebind();
    }
}
#endif
