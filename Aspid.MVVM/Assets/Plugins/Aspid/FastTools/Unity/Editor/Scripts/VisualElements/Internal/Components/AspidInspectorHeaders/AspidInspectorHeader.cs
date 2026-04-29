using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that renders an Inspector section header consisting of a
    /// script icon (with an optional open-script command), a primary label, an optional subtext label,
    /// and an animated color gradient that appears on icon hover when a status is set.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    public sealed partial class AspidInspectorHeader : VisualElement
    {
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidInspectorHeader";

        private const string ContainerClass = "aspid-fasttools-inspector-header__container";
        private const string IconClass = "aspid-fasttools-inspector-header__icon";
        private const string TextContainerClass = "aspid-fasttools-inspector-header__text-container";
        private const string TextClass = "aspid-fasttools-inspector-header__text";
        private const string SubtextClass = "aspid-fasttools-inspector-header__subtext";

        private const float DoubleClickTime = 0.3f;

        private readonly AspidBox _container;
        private readonly AspidLabel _textElement;
        private readonly AspidLabel _subtextElement;
        private readonly AspidHoverGradientOverlay _overlay;
        private readonly StatusStyle _status;

        private Object _obj;
        private MonoScript _script;
        private float _lastClickTime;

        /// <summary>
        /// Gets or sets the primary header text.
        /// </summary>
        [UxmlAttribute]
        public string Text
        {
            get => _textElement.Text;
            set => _textElement.Text = value;
        }

        /// <summary>
        /// Gets or sets the secondary (subtext) label beneath the primary text.
        /// </summary>
        [UxmlAttribute]
        public string Subtext
        {
            get => _subtextElement.Text;
            set => _subtextElement.Text = value;
        }

        /// <summary>
        /// Gets or sets the status color accent used by the hover gradient effect.
        /// </summary>
        [UxmlAttribute]
        public StatusStyle.Type Status
        {
            get => _status.Value;
            set => _status.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the Unity Object whose script is opened on icon double-click.
        /// Setting <see langword="null"/> or an object without a resolvable script disables the open-script command.
        /// </summary>
        public Object Obj
        {
            get => _obj;
            set
            {
                _obj = value;
                _script = value switch
                {
                    MonoBehaviour mono => MonoScript.FromMonoBehaviour(mono),
                    ScriptableObject scriptable => MonoScript.FromScriptableObject(scriptable),
                    _ => null,
                };
            }
        }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> using <see cref="AspidInspectorHeaderPreset.Default"/>
        /// without an associated script object.
        /// </summary>
        public AspidInspectorHeader()
            : this(AspidInspectorHeaderPreset.Default, obj: null) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> with the given preset and no script object.
        /// </summary>
        /// <param name="preset">The configuration preset.</param>
        public AspidInspectorHeader(AspidInspectorHeaderPreset preset)
            : this(preset, obj: null) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> using the script name of <paramref name="obj"/> as the label.
        /// </summary>
        /// <param name="obj">The Unity Object whose script name is displayed.</param>
        public AspidInspectorHeader(Object obj)
            : this(AspidInspectorHeaderPreset.Default.SetText(obj.GetScriptName()), obj) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> using the script name with component index of
        /// <paramref name="component"/> as the label.
        /// </summary>
        /// <param name="component">The Component whose script name (with index) is displayed.</param>
        public AspidInspectorHeader(Component component)
            : this(AspidInspectorHeaderPreset.Default.SetText(component.GetScriptNameWithIndex()), component) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> with an explicit label and the given object for the icon command.
        /// </summary>
        /// <param name="label">The text to display as the primary header label.</param>
        /// <param name="obj">The Unity Object used for the open-script icon command.</param>
        public AspidInspectorHeader(string label, Object obj)
            : this(AspidInspectorHeaderPreset.Default.SetText(label), obj) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> with the given preset and Unity Object for the open-script icon command.
        /// </summary>
        /// <param name="preset">The configuration preset.</param>
        /// <param name="obj">The Unity Object used for the open-script icon command, or <see langword="null"/> if none.</param>
        public AspidInspectorHeader(AspidInspectorHeaderPreset preset, Object obj)
        {
            this.AddStyleSheetsFromResource(StyleSheetPath);

            _textElement = new AspidLabel(preset.Text).AddClass(TextClass);
            _subtextElement = new AspidLabel(preset.Subtext).AddClass(SubtextClass);

            Obj = obj;

            var iconElement = new Image().AddClass(IconClass);
            iconElement.RegisterCallback<MouseUpEvent>(OnIconMouseUp);
            iconElement.RegisterCallback<MouseEnterEvent>(OnIconMouseEnter);
            iconElement.RegisterCallback<MouseLeaveEvent>(OnIconMouseLeave);

            _overlay = new AspidHoverGradientOverlay();
            _ = new AspidInspectorHeaderGradientStyle(this, _overlay);

            var textContainer = new VisualElement()
                .AddClass(TextContainerClass)
                .AddChild(_textElement)
                .AddChild(_subtextElement);

            _container = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Dark));
            _container.AddClass(ContainerClass)
                .AddChild(_overlay)
                .AddChild(iconElement)
                .AddChild(textContainer);

            _status = new StatusStyle(this, preset.Status);

            this.AddChild(_container);
        }

        private void OnIconMouseUp(MouseUpEvent _)
        {
            if (_script == null) return;

            var currentTime = (float)EditorApplication.timeSinceStartup;
            if (currentTime - _lastClickTime < DoubleClickTime)
                AssetDatabase.OpenAsset(_script);

            _lastClickTime = currentTime;
        }

        private void OnIconMouseEnter(MouseEnterEvent _)
        {
            if (Status is StatusStyle.Type.None) return;

            _overlay.SetTarget(1f);
            _container.Status = Status;
            _textElement.LabelStatus = Status;
            _subtextElement.LabelStatus = Status;
            _container.Theme = ThemeStyle.Type.Darkness;
        }

        private void OnIconMouseLeave(MouseLeaveEvent _)
        {
            if (Status is StatusStyle.Type.None) return;

            _overlay.SetTarget(0f);
            _container.Theme = ThemeStyle.Type.Dark;
            _container.Status = StatusStyle.Type.None;
            _textElement.LabelStatus = StatusStyle.Type.None;
            _subtextElement.LabelStatus = StatusStyle.Type.None;
        }
    }
}
