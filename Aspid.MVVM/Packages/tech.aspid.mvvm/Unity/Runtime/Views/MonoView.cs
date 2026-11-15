using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for views with serialized binder groups.
    /// </summary>
    [View]
    [AddComponentMenu("Aspid/MVVM/Views/Mono View")]
    public partial class MonoView : MonoBehaviour, IDisposable
    {
#if UNITY_EDITOR
        [Tooltip("The type filters for the design ViewModel selector.")]
        [SerializeField] private string[] _designViewModelAssemblyQualifiedNames =
        {
            typeof(IViewModel).AssemblyQualifiedName,
        };

        [Tooltip("The ViewModel type used to configure bindings in the Inspector.")]
        [TypeSelector(nameof(_designViewModelAssemblyQualifiedNames))]
        [SerializeField] private string _designViewModel;
#endif

        [RequireBinder(Id = "DesignViewModel")]
        [Tooltip("The binder groups associated with ViewModel members.")]
        [SerializeField] private Binders[] _bindersList = Array.Empty<Binders>();

        /// <summary>
        /// Called when Unity destroys the component to dispose the view.
        /// </summary>
        /// <remarks>Overrides must call the base implementation.</remarks>
        protected virtual void OnDestroy() =>
            Dispose();

        partial void OnInitializingInternal(IViewModel viewModel)
        {
            foreach (var binders in _bindersList)
                binders.Bind(viewModel, owner: this);
        }

        partial void OnDeinitializingInternal()
        {
            var context = new UnbindContext(owner: this);

            foreach (var binders in _bindersList)
                binders.Unbind(in context);
        }

        /// <summary>
        /// Deinitializes the view without destroying its component, GameObject or ViewModel.
        /// </summary>
        /// <remarks>Overrides must call the base implementation to release bindings.</remarks>
        public virtual void Dispose() =>
            Deinitialize();

        [Serializable]
        private sealed class Binders
        {
#if UNITY_EDITOR
            [Tooltip("The assembly-qualified type name of the bound value.")]
            [SerializeField] private string _assemblyQualifiedName;
#endif
            [Tooltip("The identifier of the bound ViewModel member.")]
            [SerializeField] private string _name;

#if UNITY_EDITOR
            [RequireBinder(nameof(_assemblyQualifiedName), Id = nameof(_name))]
#endif
            [Tooltip("The components bound to the ViewModel member.")]
            [SerializeField] private MonoBinder[] _monoBinders;

            public void Unbind(in UnbindContext context) =>
                _monoBinders.UnbindSafely(in context, memberName: _name);

            public void Bind(IViewModel viewModel, MonoView owner)
            {
                var result = viewModel.FindBindableMember(new FindBindableMemberParameters(_name));
                _monoBinders.BindSafely(result, owner, _name);
            }
        }
    }
}
