// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class BinderExceptionExtensions
    {
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