using System;
using UnityEngine;
using Aspid.UnityFastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a base class for views in a Unity context that are derived from <see cref="MonoBehaviour"/>.
    /// Implements <see cref="IDisposable"/> to allow cleanup of resources, including the destruction of the associated GameObject.
    /// </summary>
    [View]
    [AddComponentMenu("Aspid/MVVM/Views/Mono View")]
    public partial class MonoView : MonoBehaviour, IDisposable
    {
#if UNITY_EDITOR
        [TypeSelector(typeof(IViewModel))]
        [SerializeField] private string _designViewModel;
#endif
        
        [RequireBinder(Id = "DesignViewModel")]
        [SerializeField] private Binders[] _bindersList;

        partial void OnInitializingInternal(IViewModel viewModel)
        {
            foreach (var binders in _bindersList)
                binders.Bind(viewModel);
        }

        partial void OnDeinitializingInternal()
        {
            foreach (var binders in _bindersList)
                binders.Unbind();
        }
        
        /// <summary>
        /// Destroys the GameObject of the View.
        /// May be overridden by a derived class.
        /// </summary>
        public virtual void Dispose()
        {
            Deinitialize();
            if (!this) return;

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                DestroyImmediate(gameObject);
                return;
            }
#endif
            Destroy(gameObject);
        }

        protected virtual void OnDestroy() =>
            Deinitialize();
        
        [Serializable]
        private sealed class Binders
        {
#if UNITY_EDITOR
            [SerializeField] private string _assemblyQualifiedName;      
#endif
            [SerializeField] private string _name;
            
#if UNITY_EDITOR
            [RequireBinder(nameof(_assemblyQualifiedName), Id = nameof(_name))]
#endif
            [SerializeField] private MonoBinder[] _monoBinders;
            
            public void Bind(IViewModel viewModel)
            {
                var result = viewModel.FindBindableMember(new FindBindableMemberParameters(_name));
                if (!result.IsFound) return;
                
                _monoBinders.BindSafely(result);
            }

            public void Unbind() =>
                _monoBinders.UnbindSafely();
        }
    }
}