#nullable enable
using System;
using UnityEngine;
using System.Reflection;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Routes inspector elements into named foldout groups based on
    /// <see cref="HeaderGroupAttribute"/>, <see cref="HeaderGroupStartAttribute"/>
    /// and <see cref="HeaderGroupEndAttribute"/>.
    /// </summary>
    public sealed class HeaderGroupRouter
    {
        private const string HeaderFoldoutViewDataKeyPrefix = "AspidMonoView.Header";

        private readonly string _viewTypeName;
        private readonly VisualElement _rootContainer;

        private int _rootChildCount;
        private string? _currentRange;
        private readonly Dictionary<string, GroupEntry> _groups = new();

        public HeaderGroupRouter(VisualElement rootContainer, Type? targetType = null)
        {
            _rootContainer = rootContainer;
            _viewTypeName = targetType?.FullName ?? "Unknown";
        }

        public void Add(VisualElement element, FieldInfo? fieldInfo)
        {
            var (range, group) = ResolveGroups(fieldInfo, _currentRange);
            _currentRange = range;
            Add(element, group);
        }

        public void Add(VisualElement element, BindablePropertyMeta? meta)
        {
            var (range, group) = ResolveGroups(meta, _currentRange);
            _currentRange = range;
            Add(element, group, unityHeader: meta?.Header);
        }

        public void Add(VisualElement element, string? groupName, string? unityHeader = null)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                if (!string.IsNullOrWhiteSpace(unityHeader))
                {
                    _rootContainer.Add(BuildUnityHeaderLabel(unityHeader));
                    _rootChildCount++;
                }

                element.SetMargin(top: _rootChildCount > 0 ? 3: 0);
                _rootContainer.Add(element);
                _rootChildCount++;
            }
            else
            {
                if (!_groups.TryGetValue(groupName, out var entry))
                {
                    var (foldout, counter) = BuildHeaderFoldout(groupName!, _viewTypeName);
                    _rootContainer.Add(foldout);
                    entry = new GroupEntry(foldout.contentContainer, counter);
                    _groups[groupName] = entry;
                    _rootChildCount++;
                }

                if (!string.IsNullOrWhiteSpace(unityHeader))
                    entry.Content.Add(BuildUnityHeaderLabel(unityHeader!));

                element.SetMargin(top: 4);
                entry.Content.Add(element);
                entry.Count++;
                entry.Counter.SetValueWithoutNotify(entry.Count);
            }
        }

        private static Label BuildUnityHeaderLabel(string text) => new Label(text)
            .SetUnityFontStyleAndWeight(FontStyle.Bold)
            .SetMargin(top: 8, bottom: 2, left: 0);

        private static (string? Range, string? Group) ResolveGroups(FieldInfo? fieldInfo, string? currentRange)
        {
            if (fieldInfo is null) return (currentRange, currentRange);
            if (fieldInfo.IsDefined(typeof(HeaderGroupEndAttribute))) return (null, null);

            var startAttribute = fieldInfo.GetCustomAttribute<HeaderGroupStartAttribute>();
            if (startAttribute is not null) return (startAttribute.Title, startAttribute.Title);

            var attribute = fieldInfo.GetCustomAttribute<HeaderGroupAttribute>();
            if (attribute is not null) return (currentRange, attribute.Title);

            return (currentRange, currentRange);
        }

        private static (string? Range, string? Group) ResolveGroups(BindablePropertyMeta? meta, string? currentRange)
        {
            if (meta is null) return (currentRange, currentRange);
            if (meta.HeaderGroupEnd) return (null, null);
            if (!string.IsNullOrWhiteSpace(meta.HeaderGroupStart)) return (meta.HeaderGroupStart, meta.HeaderGroupStart);
            if (!string.IsNullOrWhiteSpace(meta.HeaderGroup)) return (currentRange, meta.HeaderGroup);
            return (currentRange, currentRange);
        }
        
        private static (Foldout foldout, IntegerField counter) BuildHeaderFoldout(string header, string viewTypeName)
        {
            var foldout = new Foldout()
                .SetValue(true)
                .SetText(header)
                .SetPadding(left: 0)
                .SetMargin(top: 3, right: 0)
                .SetViewDataKey($"{HeaderFoldoutViewDataKeyPrefix}.{viewTypeName}.{header}");

            foldout.contentContainer
                .SetMargin(left: 0)
                .SetPadding(left: 10)
                .AddChild(new AspidDividingLine()
                    .SetPosition(Position.Absolute)
                    .SetDistance(top: 3, bottom: 0, left: 3)
                    .SetDirection(AspidDividingLineDirectionStyle.Type.Vertical));

            var counter = new IntegerField()
                .SetMargin(0)
                .SetWidth(50)
                .SetMinWidth(50)
                .SetMaxWidth(50)
                .SetEnabledSelf(false)
                .SetPickingMode(PickingMode.Ignore);

            counter.Q<TextElement>()
                .SetMarginLeft(0)
                .SetPaddingLeft(2);

            foldout.Q<Toggle>()
                .SetMargin(0)
                .SetBorderRadius(5)
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row)
                .SetPadding(top: 2, right: 5, bottom: 2, left: 3)
                .SetBackgroundColor(new Color(0x38 / 255f, 0x38 / 255f, 0x38 / 255f))
                .AddChild(counter);

            return (foldout, counter);
        }

        private sealed class GroupEntry
        {
            public int Count { get; set; }
            public VisualElement Content { get; }
            public IntegerField Counter { get; }

            public GroupEntry(VisualElement content, IntegerField counter)
            {
                Content = content;
                Counter = counter;
            }
        }
    }
}
