using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryToolbarVisualElement : VisualElement
    {
        private const string SortFieldTooltip = "Sort order";
        private const string GroupFieldTooltip = "Group entries by";
        
        public event Action<RegistrySortMode> SortChanged;
        public event Action<RegistryGroupMode> GroupChanged;

        public IdRegistryToolbarVisualElement(RegistrySortMode initialSort, RegistryGroupMode initialGroup)
        {
            var sortField = new EnumField(defaultValue: RegistrySortMode.RegistryOrder)
                .SetValue(initialSort)
                .SetTooltip(SortFieldTooltip)
                .AddValueChanged(e => SortChanged?.Invoke((RegistrySortMode)e.newValue));

            var groupField = new EnumField(defaultValue: RegistryGroupMode.None)
                .SetValue(initialGroup)
                .SetTooltip(GroupFieldTooltip)
                .AddValueChanged(e => GroupChanged?.Invoke((RegistryGroupMode)e.newValue));

            this.AddClass(Constants.Registry.Toolbar)
                .AddChild(BuildCell(
                    label: "Sort",
                    field: sortField)
                    .SetMargin(right: 2))
                .AddChild(BuildCell(
                    label: "Group",
                    field: groupField)
                    .SetMargin(left: 2));
        }
        
        private static VisualElement BuildCell(string label, VisualElement field) => new VisualElement()
            .AddChild(new Label(label))
            .AddChild(field);
    }
}
