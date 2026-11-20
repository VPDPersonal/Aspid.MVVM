using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class UnityObjectHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsMissing<T>([NotNullWhen(returnValue: false)] this T? value) =>
            value is null || value is Object unityObject && unityObject == null;
    }
}
