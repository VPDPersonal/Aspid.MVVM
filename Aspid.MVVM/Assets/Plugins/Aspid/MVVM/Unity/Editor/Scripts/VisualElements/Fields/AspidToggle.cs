#nullable enable
using System;
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class AspidToggle : VisualElement
    {
        public event Action<bool>? OnValueChanged;
        
        private const float TrackWidth = 44f;
        private const float TrackHeight = 24f;
        private const float HandleSize = 20f;
        private const float HandleMargin = 2f;
        private const float AnimationDuration = 0.15f;
        
        private static readonly Color _trackOffColor = new(0.28f, 0.28f, 0.30f, 1f);
        private static readonly Color _trackOnColor = new(0.40f, 0.60f, 0.80f, 1f);
        
        private static readonly Color _handleColor = new(0.95f, 0.95f, 0.95f, 1f);
        private static readonly Color _handleShadowColor = new(0f, 0f, 0f, 0.15f);
        
        private readonly Label _label;
        private readonly VisualElement _track;
        private readonly VisualElement _handle;
        
        // 0 = off (left), 1 = on (right)
        private float _handlePosition; 
        private bool _value;
        
        private IVisualElementScheduledItem? _animationSchedule;
        
        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                
                _value = value;
                AnimateToValue(value);
                OnValueChanged?.Invoke(value);
            }
        }
        
        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }
        
        public new string tooltip
        {
            get => _track.tooltip;
            set
            {
                _track.tooltip = value;
                _label.tooltip = value;
            }
        }

        public AspidToggle()
            : this(label: string.Empty) { }
        
        public AspidToggle(string label, bool initialValue = false)
        {
            style.alignItems = Align.Center;
            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;
            
            _label = new Label(label)
                .SetFlexGrow(1)
                .SetFontSize(12)
                .SetMargin(right: 10)
                .SetUnityTextAlign(TextAnchor.MiddleLeft)
                .SetColor(new Color(0.75f, 0.75f, 0.75f));
            
            // Ellipse
            _track = new VisualElement()
                .SetFlexShrink(0)
                .SetBorderRadius(TrackHeight / 2)
                .SetBackgroundColor(_trackOffColor)
                .SetSize(TrackWidth, TrackHeight)
                .SetTransitionProperty(new List<StylePropertyName> { new("background-color") })
                .SetTransitionDuration(new List<TimeValue> { new(AnimationDuration, TimeUnit.Second) });

            // Circle
            _handle = new VisualElement()
                .SetBorderWidth(1)
                .SetSize(HandleSize)
                .SetMargin(HandleMargin)
                .SetPosition(Position.Absolute)
                .SetBorderRadius(HandleSize / 2)
                .SetBackgroundColor(_handleColor)
                .SetBorderColor(_handleShadowColor);

            this.AddChild(_label)
                .AddChild(_track
                    .AddChild(_handle));
            
            _value = initialValue;
            _handlePosition = initialValue ? 1f : 0f;
            
            UpdateVisuals();
            
            _track.RegisterCallback<ClickEvent>(OnTrackClicked);
            _label.RegisterCallback<ClickEvent>(OnTrackClicked);
        }
        
        public void SetValueWithoutNotify(bool newValue)
        {
            if (_value == newValue) return;
            _value = newValue;
            AnimateToValue(_value);
        }
        
        private void OnTrackClicked(ClickEvent evt)
        {
            Value = !_value;
            evt.StopPropagation();
        }

        #region Animation
        private void AnimateToValue(bool targetValue)
        {
            _animationSchedule?.Pause();
            
            var startPosition = _handlePosition;
            var targetPosition = targetValue ? 1f : 0f;
            
            var startTime = Time.realtimeSinceStartup;
            
            _animationSchedule = schedule.Execute(() =>
            {
                var elapsed = Time.realtimeSinceStartup - startTime;
                var time = Mathf.Clamp01(elapsed / AnimationDuration);
                
                // Ease out cubic for smooth deceleration
                time = 1f - Mathf.Pow(1f - time, 3f);
                
                _handlePosition = Mathf.Lerp(startPosition, targetPosition, time);
                UpdateVisuals();
                
                if (time >= 1f)
                    _animationSchedule?.Pause();
                
            }).Every(16); // ~60fps
        }
        
        private void UpdateVisuals()
        {
            // Calculate handle position
            const float maxLeft = TrackWidth - HandleSize - HandleMargin;
            
            var left = Mathf.Lerp(HandleMargin, maxLeft, _handlePosition);
            _handle.style.left = left;

            var trackColor = Color.Lerp(_trackOffColor, _trackOnColor, _handlePosition);
            _track.style.backgroundColor = trackColor;
        }
        #endregion
    }
}

