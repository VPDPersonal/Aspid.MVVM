using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class UnityObjectHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsMissing<T>([NotNullWhen(returnValue: false)] this T? value) =>
            value is null || value is Object unityObject && unityObject == null;

        internal static void Destroy(Object target)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (target != null)
                    Object.DestroyImmediate(target);

                return;
            }
#endif
            Object.Destroy(target);
        }
    }
}
