#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Renderer}"/> that sets the <see cref="Renderer.material"/> or <see cref="Renderer.materials"/> array.
    /// Supports binding a single <see cref="Material"/> or an <see cref="IReadOnlyCollection{T}"/> of materials.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current material(s)
    /// are sent back to the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-Renderer-Materials-1.1.0.xml" path="doc//member[@name='RendererMaterialsBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class RendererMaterialsBinder : TargetBinder<Renderer>, 
        IBinder<Material>,
        IReverseBinder<Material>,
        IReverseBinder<Material[]>,
        IBinder<IReadOnlyCollection<Material>>
    {
        event Action<Material?>? IReverseBinder<Material>.ValueChanged
        {
            add => _reverseMaterial += value;
            remove => _reverseMaterial -= value;
        }
        
        event Action<Material[]?>? IReverseBinder<Material[]>.ValueChanged
        {
            add => _reverseMaterials += value;
            remove => _reverseMaterials -= value;
        }
        
        [Tooltip("The optional converter applied to each material before it is assigned to the Renderer.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        private Action<Material?>? _reverseMaterial;
        private Action<Material[]?>? _reverseMaterials;
        
        /// <summary>
        /// Initializes a new instance of <see cref="RendererMaterialsBinder"/> without a converter.
        /// </summary>
        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public RendererMaterialsBinder(Renderer target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="RendererMaterialsBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="converter">The converter applied to each material before assignment, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public RendererMaterialsBinder(Renderer target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        public void SetValue(Material? value) =>
            Target.material = GetConvertedValue(value);
        
        public void SetValue(IReadOnlyCollection<Material>? values) =>
            Target.SetMaterials(_converter, values);
        
        /// <summary>
        /// Called after binding is established.
        /// Sends the current material(s) back to the ViewModel when in <see cref="BindMode.OneWayToSource"/> mode.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                _reverseMaterial?.Invoke(Target.material);

                if (_reverseMaterials is not null)
                {
                    var materials = Target.materials;
                    
                    if (_converter is not null)
                    {
                        materials = new Material[Target.materials.Length];

                        for (var i = 0; i < materials.Length; i++)
                            materials[i] = GetConvertedValue(Target.materials[i]);
                    }
                    
                    _reverseMaterials?.Invoke(materials);
                }
            }
        }

        /// <summary>
        /// Called when the binding is removed.
        /// Sends <see langword="null"/> to all reverse subscribers.
        /// </summary>
        protected override void OnUnbound()
        {
            _reverseMaterial?.Invoke(null);
            _reverseMaterials?.Invoke(null);
        }

        private Material? GetConvertedValue(Material? value) =>
            _converter?.Convert(value) ?? value;
    }
}