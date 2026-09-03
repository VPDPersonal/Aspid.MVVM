#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="InitializeComponent{T}"/> that resolves an <see cref="IViewModel"/>.
    /// </summary>
    [Serializable]
    public sealed class ViewModelInitializeComponent : InitializeComponent<IViewModel>
    {
        [Tooltip("ViewModel type requested from the DI container when Resolve Type is Di.")]
        [TypeSelector(typeof(IViewModel))]
        [SerializeField] private string? _typeName;

        /// <inheritdoc/>
        public override void Validate()
        {
            base.Validate();

            if (ResolveType is ResolveType.Component or ResolveType.Reference or ResolveType.ScriptableObject)
                _typeName = null;
        }

        /// <inheritdoc/>
        protected override Type? GetTypeForDi() =>
            _typeName is null ? null : Type.GetType(_typeName);
    }
}
