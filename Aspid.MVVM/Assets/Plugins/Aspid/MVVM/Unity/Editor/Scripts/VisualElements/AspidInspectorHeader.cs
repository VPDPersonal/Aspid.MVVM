#nullable enable
using UnityEngine;
using UnityEditor;
using Aspid.Internal;
using Aspid.FastTools;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A styled inspector header visual element used in the Aspid MVVM inspector UI.
    /// Displays an icon alongside a label derived from the inspected object's type name.
    /// </summary>
    public class AspidInspectorHeader : VisualElement
    {
        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>(path: "Styles/aspid-mvvm-inspector-header");
        
        private Color _color;
        public readonly Label Label;
        private readonly Image _icon;

        public AspidInspectorHeader(Object obj, MessageType messageType = MessageType.None)
            : this(obj.GetScriptName(), obj, messageType) { }
        
        public AspidInspectorHeader(string label, Object obj, MessageType messageType = MessageType.None)
        {
            var container = new AspidContainer(AspidContainer.StyleType.Dark)
                .SetMargin(top: 2)
                .SetOverflow(Overflow.Hidden);
            
            container.styleSheets.Add(_styleSheet);
            
            Label = new Label(label);
            _icon = new Image()
                .AddOpenScriptCommand(obj);

            Add(container
                .AddChild(_icon)
                .AddChild(Label));

            SetMessageType(messageType);
            AddIconHoverGradient(container);
        }

        public void SetMessageType(MessageType messageType)
        {
            _color = messageType switch
            {
                MessageType.Error => new Color(0.27f, 0.04f, 0.04f),
                MessageType.Warning => new Color(0.27f, 0.27f, 0.04f),
                _ => new Color(0.04f, 0.27f, 0.17f)
            };
            
            _icon.SetImageFromResource(GetIconPath());
            return;
            
            string GetIconPath() => messageType switch
            {
                MessageType.Error => EditorConstants.AspidIconRed,
                MessageType.Warning => EditorConstants.AspidIconYellow,
                _ => EditorConstants.AspidIconGreen,
            };
        }

        private void AddIconHoverGradient(VisualElement container)
        {
            var gradient = new VisualElement
            {
                style =
                {
                    position = Position.Absolute,
                    left = 0,
                    top = 0,
                    right = 0,
                    bottom = 0,
                },
                
                pickingMode = PickingMode.Ignore
            };

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

            _icon.RegisterCallback<MouseEnterEvent>(_ => targetProgress = 1f);
            _icon.RegisterCallback<MouseLeaveEvent>(_ => targetProgress = 0f);

            gradient.schedule.Execute(() =>
            {
                var previous = progress;
                progress = Mathf.Lerp(progress, targetProgress, 0.12f);
                
                if (Mathf.Abs(progress - previous) > 0.001f)
                    gradient.MarkDirtyRepaint();
            }).Every(16);

            container.Insert(index: 0, gradient);
        }
    }
}