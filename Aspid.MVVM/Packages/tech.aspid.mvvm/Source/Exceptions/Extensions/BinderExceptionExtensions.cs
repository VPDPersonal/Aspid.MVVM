// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for enriching <see cref="IBinder"/> exception messages with Unity hierarchy path information.
    /// </summary>
    internal static class BinderExceptionExtensions
    {
        /// <summary>
        /// Appends the Unity GameObject hierarchy path to <paramref name="message"/> when the <paramref name="binder"/>
        /// is a <c>UnityEngine.Component</c>. Returns the original message unchanged outside Unity or for non-component binders.
        /// </summary>
        /// <param name="binder">The binder whose hierarchy path should be included in the message.</param>
        /// <param name="message">The base exception message to enrich.</param>
        /// <returns>The enriched message containing the GameObject path, or the original message if path information is unavailable.</returns>
        internal static string AddExceptionMessage(this IBinder binder, string message)
        {
#if UNITY_2022_1_OR_NEWER
            if (binder is UnityEngine.Component component)
            {
                var path = GetPath(component.transform) + $"/{component.GetType().Name}";

                return $"{message} GameObject Path: {path.GetPathMessage()}";
                
                static string GetPath(UnityEngine.Transform current) => current.parent is null
                    ? current.name
                    : GetPath(current.parent) + "/" + current.name;
            }
#endif

            return message;
        }
    }
}
