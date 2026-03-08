using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the gradient color on a <see cref="UnityEngine.LineRenderer"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient")]
    public class LineRendererColorMonoBinder : ComponentColorMonoBinder<LineRenderer>
    {
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        protected sealed override Color Property
        {
            get => CachedComponent.GetColor(_colorMode);
            set => CachedComponent.SetColor(value, _colorMode);
        }
    }
}