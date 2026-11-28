using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for views with serialized binder groups.
    /// </summary>
    [View]
    [AddComponentMenu("Aspid/MVVM/Views/Mono View")]
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public partial class MonoView : MonoBehaviour, IDisposable
    {
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
            if (_bindersList is null) return;

            foreach (var binders in _bindersList)
                binders?.Bind(viewModel, owner: this);
        }

        partial void OnDeinitializingInternal()
        {
            if (_bindersList is null) return;

            var failures = new CleanupExceptions();
            var context = new UnbindContext(owner: this);

            foreach (var binders in _bindersList)
                binders?.Unbind(in context, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Deinitializes the view and releases its bindings without destroying any Unity objects.
        /// </summary>
        /// <remarks>Overrides must call the base implementation.</remarks>
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
            [Tooltip("The components bound to the ViewModel member. Empty slots are skipped.")]
            [SerializeField] private MonoBinder[] _monoBinders;

            public void Bind(IViewModel viewModel, MonoView owner)
            {
                try
                {
                    var result = viewModel.FindBindableMember(new FindBindableMemberParameters(_name));
                    if (result.Adder is null) return;

                    _monoBinders.BindSafely(new BindContext(result.Adder, owner));
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException($"Failed to bind field '{_name}'.", exception);
                }
            }

            public void Unbind(in UnbindContext context, ref CleanupExceptions failures)
            {
                try
                {
                    _monoBinders.UnbindSafely(in context);
                }
                catch (Exception exception)
                {
                    failures.Add(new InvalidOperationException($"Failed to unbind field '{_name}'.", exception));
                }
            }
        }
    }
}
