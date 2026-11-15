using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class UnityObjectDestruction
    {
        public static void Destroy(Object target)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (target)
                    Object.DestroyImmediate(target);

                return;
            }
#endif
            Object.Destroy(target);
        }
    }
}
