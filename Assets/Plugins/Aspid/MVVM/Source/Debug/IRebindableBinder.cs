#if UNITY_EDITOR || DEBUG
using System.ComponentModel;

namespace Aspid.MVVM
{
    public interface IRebindableBinder
    {
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Rebind();
    }
}
#endif