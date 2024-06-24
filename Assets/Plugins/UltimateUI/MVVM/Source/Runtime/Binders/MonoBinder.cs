using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class MonoBinder : MonoBehaviour, IBinder
    {
#if UNITY_EDITOR
        [field: SerializeField] 
        public string Id { get; set; }
#endif
    }
}