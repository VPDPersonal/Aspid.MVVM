using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Material Color Enum")]
    public sealed class RendererMaterialColorEnumMonoBinder : EnumMonoBinder<Renderer, Color>
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        protected override void SetValue(Color value) =>
            CachedComponent.material.SetColor(ColorPropertyId, value);
    }
}