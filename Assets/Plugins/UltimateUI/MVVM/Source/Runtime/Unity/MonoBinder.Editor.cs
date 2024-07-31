#if UNITY_EDITOR && !ULTIMATE_UI_EDITOR_DISABLED
#nullable disable
using UnityEngine;
using UltimateUI.MVVM.Unity.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    public abstract partial class MonoBinder
    {
        [SerializeField] private MonoView _view;
        [SerializeField] private string _id;
        
        public MonoView View
        {
            get => _view;
            set => _view = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }
    }
}
#endif