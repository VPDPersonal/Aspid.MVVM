using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class RendererMaterialsMonoBinder : ComponentMonoBinder<Renderer>, 
        IBinder<Material>, 
        IReverseBinder<Material>,
        IReverseBinder<Material[]>,
        IBinder<IReadOnlyCollection<Material>>
    {
        event Action<Material> IReverseBinder<Material>.ValueChanged
        {
            add => _reverseMaterial += value;
            remove => _reverseMaterial -= value;
        }
        
        event Action<Material[]> IReverseBinder<Material[]>.ValueChanged
        {
            add => _reverseMaterials += value;
            remove => _reverseMaterials -= value;
        }
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        private Action<Material> _reverseMaterial;
        private Action<Material[]> _reverseMaterials;
        
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = GetConvertedValue(value);
        
        [BinderLog]
        public void SetValue(IReadOnlyCollection<Material> values) =>
            CachedComponent.SetMaterials(_converter, values);

        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                _reverseMaterial?.Invoke(CachedComponent.material);

                if (_reverseMaterials is not null)
                {
                    var materials = CachedComponent.materials;
                    
                    if (_converter is not null)
                    {
                        materials = new Material[CachedComponent.materials.Length];

                        for (var i = 0; i < materials.Length; i++)
                            materials[i] = GetConvertedValue(CachedComponent.materials[i]);
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

        private Material GetConvertedValue(Material value) =>
            _converter?.Convert(value) ?? value;
    }
}