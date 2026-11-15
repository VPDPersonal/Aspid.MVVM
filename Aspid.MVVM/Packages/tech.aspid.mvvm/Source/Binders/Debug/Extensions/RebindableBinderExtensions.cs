using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class RebindableBinderExtensions
    {
        [Conditional(conditionString: "DEBUG")]
        [Conditional(conditionString: "UNITY_EDITOR")]
        public static void Rebind(this IBinder binder)
        {
#if UNITY_EDITOR || DEBUG
            if (binder is IRebindableBinder rebindableBinder)
                rebindableBinder.Rebind();
#endif
        }
    }
}
