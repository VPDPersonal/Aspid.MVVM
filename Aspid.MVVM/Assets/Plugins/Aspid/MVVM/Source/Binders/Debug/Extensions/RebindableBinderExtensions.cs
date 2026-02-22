using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extension methods for rebinding <see cref="IBinder"/> instances that implement <see cref="IRebindableBinder"/>.
    /// Only active in <c>DEBUG</c> or <c>UNITY_EDITOR</c> builds.
    /// </summary>
    public static class RebindableBinderExtensions
    {
        /// <summary>
        /// Rebinds the binder if it implements <see cref="IRebindableBinder"/>.  
        /// This method is only included in builds with <c>DEBUG</c> or <c>UNITY_EDITOR</c> defined.
        /// </summary>
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