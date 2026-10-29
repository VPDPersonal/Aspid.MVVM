using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for rebinding an <see cref="IBinder"/>.
    /// </summary>
    public static class RebindableBinderExtensions
    {
        /// <summary>
        /// Rebinds <paramref name="binder"/> when it implements <see cref="IRebindableBinder"/>; otherwise, does nothing.
        /// </summary>
        /// <remarks>
        /// Calls are removed from builds that define neither <c>DEBUG</c> nor <c>UNITY_EDITOR</c>.
        /// </remarks>
        /// <param name="binder">The binder to rebind.</param>
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
