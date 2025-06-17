using System.Diagnostics;

namespace Aspid.MVVM
{
    public static class RebindableBinderExtensions
    {
#if UNITY_EDITOR
        [Conditional("UNITY_EDITOR")]
#else
        [Conditional("DEBUG")]
#endif
        public static void Rebind(this IBinder binder)
        {
#if UNITY_EDITOR || DEBUG
            if (binder is IRebindableBinder rebindableBinder)
                rebindableBinder.Rebind();
#endif
        }
    }
}