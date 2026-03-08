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
    /// Binder that sets the material or materials on a <see cref="Renderer"/> when the bound ViewModel value changes.
    /// Supports binding a single <see cref="Material"/> or a collection of materials. Supports <see cref="BindMode.OneWayToSource"/>
    /// to read the current material(s) back to the ViewModel on bind.
    /// </summary>
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
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        private Action<Material?>? _reverseMaterial;
        private Action<Material[]?>? _reverseMaterials;
        
        public RendererMaterialsBinder(Renderer target, BindMode mode)
            : this(target, converter: null, mode) { }
        
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

        protected override void OnUnbound()
        {
            _reverseMaterial?.Invoke(null);
            _reverseMaterials?.Invoke(null);
        }

        private Material? GetConvertedValue(Material? value) =>
            _converter?.Convert(value) ?? value;
    }
}