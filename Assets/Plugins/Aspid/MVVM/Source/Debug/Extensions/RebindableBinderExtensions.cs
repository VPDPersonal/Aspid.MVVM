using System.Diagnostics;

namespace Aspid.MVVM
{
    public static class RebindableBinderExtensions
    {
        /// <summary>
        /// Rebinds the binder if it implements <see cref="IRebindableBinder"/>.  
        /// This method is only included in builds with <c>DEBUG</c> or <c>UNITY_EDITOR</c> defined.
        /// </summary>
        /// <param name="binder">The binder instance to attempt to rebind.</param>
        [Conditional("DEBUG")]
        [Conditional("UNITY_EDITOR")]
        public static void Rebind(this IBinder binder)
        {
#if UNITY_EDITOR || DEBUG
            if (binder is IRebindableBinder rebindableBinder)
                rebindableBinder.Rebind();
#endif
        }
    }
}