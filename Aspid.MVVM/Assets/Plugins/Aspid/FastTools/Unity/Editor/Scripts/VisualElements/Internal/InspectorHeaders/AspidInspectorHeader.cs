using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that renders an Inspector section header consisting of a
    /// script icon (with an open-script command), a primary label, an optional subtext label,
    /// and an animated color gradient that appears on icon hover when a status is set.
    /// </summary>
    public sealed class AspidInspectorHeader : VisualElement
    {
        private readonly AspidLabel _textElement;
        private readonly AspidLabel _subtextElement;

        private Color _color;
        private StyleOverride<StatusStyle> _status;

        /// <summary>
        /// Gets or sets the primary header text.
        /// </summary>
        public string Text
        {
            get => _textElement.Text;
            set => _textElement.Text = value;
        }

        /// <summary>
        /// Gets or sets the secondary (subtext) label beneath the primary text.
        /// </summary>
        public string Subtext
        {
            get => _subtextElement.Text;
            set => _subtextElement.Text = value;
        }

        /// <summary>
        /// Gets or sets the status color accent used by the hover gradient effect.
        /// </summary>
        public StatusStyle Status
        {
            get => _status;
            set => _status.Set(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> using the script name of <paramref name="obj"/> as the label.
        /// </summary>
        /// <param name="obj">The Unity Object whose script name is displayed.</param>
        public AspidInspectorHeader(Object obj)
            : this(label: obj.GetScriptName(), obj: obj) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> using the script name with component index of
        /// <paramref name="component"/> as the label.
        /// </summary>
        /// <param name="component">The Component whose script name (with index) is displayed.</param>
        public AspidInspectorHeader(Component component)
            : this(label: component.GetScriptNameWithIndex(), obj: component) { }

        /// <summary>
        /// Creates an <see cref="AspidInspectorHeader"/> with an explicit label and the given object for the icon command.
        /// </summary>
        /// <param name="label">The text to display as the primary header label.</param>
        /// <param name="obj">The Unity Object used for the open-script icon command.</param>
        public AspidInspectorHeader(string label, Object obj)
        {
            _textElement = new AspidLabel(label);
            _subtextElement = new AspidLabel().SetName("subtext");
            
            var iconElement = new Image().AddOpenScriptCommand(obj);
            var hoverGradientElement = CreateHoverGradient(iconElement, _textElement, _subtextElement);

            var container = new AspidBox(ThemeStyle.Dark)
                .AddChild(hoverGradientElement)
                .AddChild(iconElement)
                .AddChild(new VisualElement().SetName("text-container")
                    .AddChild(_textElement)
                    .AddChild(_subtextElement));
            
            _status = new StyleOverride<StatusStyle>(StatusStyle.Success, (oldValue, newValue) =>
            {
                this.RemoveClass(oldValue.ToUss())
                    .AddClass(newValue.ToUss());
            });

            this.AddChild(container);
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StyleClasses.InspectorHeader.GradientColorProperty, out var color))
                _color = color;
            
            if (evt.customStyle.TryGetByEnum<StatusStyle>(StyleClasses.Status.Property, out var statusValue))
                _status.SetDefault(statusValue);
        }

        private VisualElement CreateHoverGradient(
            Image iconElement,
            AspidLabel textElement,
            AspidLabel subtextElement)
        {
            var gradient = new VisualElement()
                .SetDistance(0)
                .SetPosition(Position.Absolute)
                .SetPickingMode(PickingMode.Ignore);

            var progress = 0f;
            var targetProgress = 0f;

            gradient.generateVisualContent += ctx =>
            {
                if (progress <= 0.01f) return;

                const int steps = 75;
                var painter = ctx.painter2D;
                var rect = gradient.contentRect;
                var stripWidth = rect.width / steps;

                for (var i = 0; i < steps; i++)
                {
                    var t = (float)i / (steps - 1);
                    var alpha = (1f - t) * (1f - t) * progress * 0.35f;

                    painter.fillColor = new Color(_color.r, _color.g, _color.b, alpha);
                    painter.BeginPath();
                    {
                        painter.MoveTo(new Vector2(i * stripWidth, 0));
                        painter.LineTo(new Vector2((i + 1) * stripWidth, 0));
                        painter.LineTo(new Vector2((i + 1) * stripWidth, rect.height));
                        painter.LineTo(new Vector2(i * stripWidth, rect.height));
                    }
                    painter.ClosePath();
                    painter.Fill();
                }
            };

            iconElement.RegisterCallback<MouseEnterEvent>(_ =>
            {
                if (Status is StatusStyle.None) return;

                targetProgress = 1f;
                textElement.LabelStatus = Status;
                subtextElement.LabelStatus = Status;
            });

            iconElement.RegisterCallback<MouseLeaveEvent>(_ =>
            {
                if (Status is StatusStyle.None) return;

                targetProgress = 0f;
                textElement.LabelStatus = StatusStyle.None;
                subtextElement.LabelStatus = StatusStyle.None;
            });

            gradient.schedule.Execute(updateEvent: () =>
            {
                var previous = progress;
                progress = Mathf.Lerp(progress, targetProgress, 0.12f);

                if (Mathf.Abs(progress - previous) > 0.001f)
                    gradient.MarkDirtyRepaint();
            }).Every(16);

            return gradient;
        }
    }
}
