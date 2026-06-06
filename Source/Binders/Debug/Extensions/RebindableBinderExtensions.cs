using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IBinder"/> instances that implement <see cref="IRebindableBinder"/>.
    /// </summary>
    /// <remarks>
    /// Only active in <c>DEBUG</c> or <c>UNITY_EDITOR</c> builds.
    /// </remarks>
    public static class RebindableBinderExtensions
    {
        /// <summary>
        /// Rebinds the binder if it implements <see cref="IRebindableBinder"/>; otherwise, does nothing.
        /// </summary>
        /// <remarks>
        /// Call sites are stripped by the compiler in builds where neither <c>DEBUG</c> nor <c>UNITY_EDITOR</c> is defined.
        /// </remarks>
        /// <param name="binder">The binder instance to attempt to rebind.</param>
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
