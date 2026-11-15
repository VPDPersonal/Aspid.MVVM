using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class BinderExceptionExtensions
    {
        internal static string AddExceptionMessage(this IBinder binder, string message)
        {
            if (binder is Component component && component)
            {
                var path = GetPath(component.transform);
                return $"{message}\nPath: {path.GetPathMessage()}";

                static string GetPath(Transform current) => current.parent is null
                    ? current.name
                    : GetPath(current.parent) + "/" + current.name;
            }

            return message;
        }
    }
}
