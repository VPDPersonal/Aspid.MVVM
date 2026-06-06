using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that displays an animated logo with three layered images.
    /// On hover, the layers cross-fade in a cycle and the logo gently pulses; pulse parameters
    /// and per-layer textures can all be inherited from USS custom properties via the
    /// <see cref="AspidAnimatedLogoPulseSpeedStyle"/>, <see cref="AspidAnimatedLogoPulseHoverAmplitudeStyle"/>
    /// and <see cref="AspidAnimatedLogoLayerImageStyle"/> bindings.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidAnimatedLogo : VisualElement
    {
        private const int LayerCount = 3;
        private const long AnimationIntervalMs = 33;
        private const float PulseAmplitudeSmoothing = 0.07f;

        private const string LayerClass = "aspid-fasttools-animated-logo__layer";
        private const string LayerVisibleClass = "aspid-fasttools-animated-logo__layer--visible";

        private readonly VisualElement[] _layers = new VisualElement[LayerCount];

        private readonly AspidAnimatedLogoPulseSpeedStyle _pulseSpeed;
        private readonly AspidAnimatedLogoPulseHoverAmplitudeStyle _pulseHoverAmplitude;
        private readonly AspidAnimatedLogoLayerImageStyle _layer1Image;
        private readonly AspidAnimatedLogoLayerImageStyle _layer2Image;
        private readonly AspidAnimatedLogoLayerImageStyle _layer3Image;

        private int _currentLayer;
        private bool _hovered;
        private float _pulseAmplitude;
        private IVisualElementScheduledItem _colorCycle;
        private IVisualElementScheduledItem _pulse;

        /// <summary>
        /// Gets or sets the interval (in milliseconds) between color-layer transitions while hovered.
        /// </summary>
        [UxmlAttribute]
        public long ColorCycleIntervalMs { get; set; }

        /// <summary>
        /// Gets or sets the angular speed of the hover pulse animation.
        /// </summary>
        [UxmlAttribute]
        public float PulseSpeed
        {
            get => _pulseSpeed.Value;
            set => _pulseSpeed.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the maximum scale amplitude of the hover pulse, expressed as a fraction (e.g. 0.04 = ±4%).
        /// </summary>
        [UxmlAttribute]
        public float PulseHoverAmplitude
        {
            get => _pulseHoverAmplitude.Value;
            set => _pulseHoverAmplitude.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the texture for the first logo layer (always visible by default).
        /// </summary>
        [UxmlAttribute]
        public Texture2D Image1
        {
            get => _layer1Image.Value;
            set => _layer1Image.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the texture for the second logo layer.
        /// </summary>
        [UxmlAttribute]
        public Texture2D Image2
        {
            get => _layer2Image.Value;
            set => _layer2Image.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the texture for the third logo layer.
        /// </summary>
        [UxmlAttribute]
        public Texture2D Image3
        {
            get => _layer3Image.Value;
            set => _layer3Image.SetValue(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidAnimatedLogo"/> using <see cref="AspidAnimatedLogoPreset.Default"/>.
        /// </summary>
        public AspidAnimatedLogo()
            : this(AspidAnimatedLogoPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidAnimatedLogo"/> with the given preset and starts the pulse loop.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidAnimatedLogo(AspidAnimatedLogoPreset preset)
        {
            this.AddStyleSheetsFromResource("UI/Components/Aspid-FastTools-AspidAnimatedLogo");

            ColorCycleIntervalMs = preset.ColorCycleIntervalMs;

            for (var i = 0; i < LayerCount; i++)
            {
                var layer = new VisualElement()
                    .AddClass(LayerClass)
                    .AddClass($"{LayerClass}--{i}")
                    .SetPickingMode(PickingMode.Ignore);

                this.AddChild(layer);
                _layers[i] = layer;
            }

            _layers[0].AddClass(LayerVisibleClass);

            _pulseSpeed = new AspidAnimatedLogoPulseSpeedStyle(this, preset.PulseSpeed);
            _pulseHoverAmplitude = new AspidAnimatedLogoPulseHoverAmplitudeStyle(this, preset.PulseHoverAmplitude);

            _layer1Image = new AspidAnimatedLogoLayerImageStyle(
                target: _layers[0],
                eventSource: this,
                AspidAnimatedLogoLayerImageStyle.Layer1StyleProperty,
                preset.Image1);

            _layer2Image = new AspidAnimatedLogoLayerImageStyle(
                target: _layers[1],
                eventSource: this,
                AspidAnimatedLogoLayerImageStyle.Layer2StyleProperty,
                preset.Image2);

            _layer3Image = new AspidAnimatedLogoLayerImageStyle(
                target: _layers[2],
                eventSource: this,
                AspidAnimatedLogoLayerImageStyle.Layer3StyleProperty,
                preset.Image3);

            RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            RegisterCallback<PointerLeaveEvent>(OnPointerLeave);

            _pulse = schedule.Execute(UpdatePulse).Every(AnimationIntervalMs);

            RegisterCallback<AttachToPanelEvent>(_ => _pulse.Resume());
            RegisterCallback<DetachFromPanelEvent>(_ =>
            {
                _pulse.Pause();
                _colorCycle?.Pause();
            });
        }

        private void OnPointerEnter(PointerEnterEvent evt)
        {
            _hovered = true;
            StartColorCycle();
        }

        private void OnPointerLeave(PointerLeaveEvent evt)
        {
            _hovered = false;
            StopColorCycle();
        }

        private void StartColorCycle()
        {
            if (_colorCycle != null) return;
            _colorCycle = schedule.Execute(AdvanceLayer).Every(ColorCycleIntervalMs);
        }

        private void StopColorCycle()
        {
            _colorCycle?.Pause();
            _colorCycle = null;

            // Snap back to layer 0 so the idle state is consistent across hovers; the CSS
            // opacity transition smooths the cross-fade from whatever layer was active.
            if (_currentLayer == 0) return;
            _layers[_currentLayer].RemoveClass(LayerVisibleClass);
            _layers[0].AddClass(LayerVisibleClass);
            _currentLayer = 0;
        }

        private void AdvanceLayer()
        {
            _layers[_currentLayer].RemoveClass(LayerVisibleClass);
            _currentLayer = (_currentLayer + 1) % LayerCount;
            _layers[_currentLayer].AddClass(LayerVisibleClass);
        }

        private void UpdatePulse()
        {
            // Amplitude lerps to 0 when idle, so the pulse fades in/out smoothly on hover —
            // toggling it on/off discretely would snap the scale mid-cycle.
            var targetAmplitude = _hovered ? PulseHoverAmplitude : 0f;
            _pulseAmplitude = Mathf.Lerp(_pulseAmplitude, targetAmplitude, PulseAmplitudeSmoothing);

            var time = (float)EditorApplication.timeSinceStartup;
            var pulse = 1f + Mathf.Sin(time * PulseSpeed) * _pulseAmplitude;

            style.scale = new Scale(new Vector3(pulse, pulse, 1f));
        }
    }
}
