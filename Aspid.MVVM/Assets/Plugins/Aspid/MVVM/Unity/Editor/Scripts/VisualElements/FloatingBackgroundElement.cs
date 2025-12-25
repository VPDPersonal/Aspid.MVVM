#nullable enable
using UnityEngine;
using Aspid.Internal;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A visual element that displays animated floating AspidInspectorHeader-style elements in the background.
    /// Elements smoothly move left-to-right and back, creating a dynamic visual effect.
    /// </summary>
    internal sealed class FloatingBackgroundElement : VisualElement
    {
        private const int GridSize = 40;
        private const int FloatingItemCount = 25;
        private const float AnimationInterval = 16f; // ~60fps
        
        private readonly FloatingItem[] _items;
        
        public FloatingBackgroundElement()
        {
            pickingMode = PickingMode.Ignore;

            this.SetDistance(0)
                .SetOverflow(Overflow.Hidden)
                .SetPosition(Position.Absolute);
            
            // Draw grid directly on this element's background
            generateVisualContent += OnGenerateVisualContent;
            
            _items = new FloatingItem[FloatingItemCount];
            
            for (var i = 0; i < FloatingItemCount; i++)
            {
                _items[i] = new FloatingItem(i);
                Add(_items[i].Element);
            }
            
            var scheduledAnimation = schedule
                .Execute(UpdateAnimation)
                .Every((long)AnimationInterval);
            
            RegisterCallback<GeometryChangedEvent>(_ => MarkDirtyRepaint());
            RegisterCallback<AttachToPanelEvent>(_ => scheduledAnimation?.Resume());
            RegisterCallback<DetachFromPanelEvent>(_ => scheduledAnimation?.Pause());
        }
        
        private void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var bounds = contentRect;
            if (bounds.width <= 0 || bounds.height <= 0) return;
            
            var painter = ctx.painter2D;
            painter.strokeColor = new Color(1f, 1f, 1f, 0.04f);
            painter.lineWidth = 1f;
            
            // Draw vertical lines
            for (var x = GridSize; x < bounds.width; x += GridSize)
            {
                painter.BeginPath();
                painter.MoveTo(new Vector2(x, 0));
                painter.LineTo(new Vector2(x, bounds.height));
                painter.Stroke();
            }
            
            // Draw horizontal lines
            for (var y = GridSize; y < bounds.height; y += GridSize)
            {
                painter.BeginPath();
                painter.MoveTo(new Vector2(0, y));
                painter.LineTo(new Vector2(bounds.width, y));
                painter.Stroke();
            }
        }
        
        private void UpdateAnimation()
        {
            var bounds = contentRect;
            if (bounds.width <= 0 || bounds.height <= 0) return;
            
            foreach (var item in _items)
                item.Update(bounds);
        }
        
        private sealed class FloatingItem
        {
            private static readonly string[] _labels = 
            {
                "MonoView",
                "ViewModel",
                "MonoBinder", 
                "RelayCommand",
                "ObservableList",
                "ViewInitializer",
            };
            
            private static readonly string[] _iconPaths =
            {
                EditorConstants.AspidIconRed,
                EditorConstants.AspidIconGreen,
                EditorConstants.AspidIconYellow,
            };
            
            private const int RowCount = 4; 
            private const float BaseSpeed = 0.4f;
            
            private float _x;
            private float _verticalOffset;
            
            private readonly float _baseY;
            private readonly float _speed;
            private readonly float _direction;
            private readonly float _verticalSpeed;
            private readonly float _verticalAmplitude;
            
            public VisualElement Element { get; }

            public FloatingItem(int index)
            {
                var random = new System.Random(Seed: index * 137 + 42);
                
                // Constant speed per item (no slowdown)
                _speed = BaseSpeed + (float)random.NextDouble() * 0.3f;
                _direction = random.NextDouble() > 0.5 ? 1f : -1f;
                
                // Vertical wave parameters
                _verticalSpeed = 0.3f + (float)random.NextDouble() * 0.4f;
                _verticalAmplitude = 0.02f + (float)random.NextDouble() * 0.03f;
                _verticalOffset = (float)random.NextDouble() * Mathf.PI * 2;
                
                // More chaotic horizontal distribution
                // Use golden ratio for better spacing
                const float goldenRatio = 1.618033988749895f;
                var baseX = index * goldenRatio % 1.0f;
                var xOffset = ((float)random.NextDouble() - 0.5f) * 0.15f;
                _x = baseX + xOffset;
                
                // Assign to row with offset for visual variety
                var row = index % RowCount;
                const float rowHeight = 1.0f / RowCount;
                var rowCenter = (row + 0.5f) * rowHeight;
                
                // Add larger random offset within row for more natural look
                var yOffset = ((float)random.NextDouble() - 0.5f) * rowHeight * 0.6f;
                _baseY = Mathf.Clamp(rowCenter + yOffset, 0.05f, 0.95f);
                
                var label = _labels[index % _labels.Length];
                var iconPath = _iconPaths[index % _iconPaths.Length];
                var opacity = 0.08f + (float)random.NextDouble() * 0.07f;
                var scale = 0.6f + (float)random.NextDouble() * 0.3f;
                
                // Create a header-style element
                Element = CreateHeaderElement(label, iconPath, opacity, scale);
                Element.name = $"floating-header-{index}";
                Element.pickingMode = PickingMode.Ignore;
            }
            
            private static VisualElement CreateHeaderElement(string label, string iconPath, float opacity, float scale)
            {
                var container = new VisualElement()
                    .SetPadding(8)
                    .SetBorderWidth(1)
                    .SetBorderRadius(8)
                    .SetOpacity(opacity * 10f) // Scale up since the container already has opacity
                    .SetAlignItems(Align.Center)
                    .SetPosition(Position.Absolute)
                    .SetFlexDirection(FlexDirection.Row)
                    .SetScale(new Scale(new Vector2(scale, scale)))
                    .SetBackgroundColor(new Color(0.15f, 0.15f, 0.17f, opacity))
                    .SetBorderColor(new Color(0.75f, 0.75f, 0.75f, opacity * 0.5f));
                
                var icon = new Image()
                    .SetSize(28)
                    .SetMargin(right: 8)
                    .SetImageFromResource(iconPath);
                
                icon.pickingMode = PickingMode.Ignore;

                var labelElement = new Label(label)
                    .SetFontSize(12)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold)
                    .SetColor(new Color(0.75f, 0.75f, 0.75f, 1f));
                
                labelElement.pickingMode = PickingMode.Ignore;
                
                return container
                    .AddChild(icon)
                    .AddChild(labelElement);
            }

            public void Update(Rect bounds)
            {
                // Constant horizontal movement
                _x += _speed * _direction * 0.0005f;
                
                // Wrap around edges
                if (_x < -0.3f)
                    _x = 1.3f;
                else if (_x > 1.3f)
                    _x = -0.3f;
                
                // Smooth vertical wave movement
                _verticalOffset += _verticalSpeed * 0.016f;
                var verticalWave = Mathf.Sin(_verticalOffset) * _verticalAmplitude;
                var currentY = Mathf.Clamp(_baseY + verticalWave, 0.02f, 0.98f);
                
                // Calculate screen position
                var posX = _x * bounds.width;
                var posY = currentY * (bounds.height - 50f);

                Element.style.left = posX;
                Element.style.top = posY;
            }
        }
    }
}

