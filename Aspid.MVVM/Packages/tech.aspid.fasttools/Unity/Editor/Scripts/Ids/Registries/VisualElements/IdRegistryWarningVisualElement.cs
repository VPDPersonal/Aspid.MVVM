using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryWarningVisualElement : VisualElement
    {
        private readonly Label _label;

        public event Action ReviewRequested;

        public IdRegistryWarningVisualElement()
        {
            _label = new Label();
            var reviewButton = new Button()
                .SetText("Review")
                .AddClicked(() => ReviewRequested?.Invoke());
            
            this.AddChild(_label)
                .AddChild(reviewButton);
        }

        public void Bind(CleanUpSummary summary)
        {
            var isVisible = summary.Total > 0;
            EnableInClassList(Constants.Registry.WarningVisible, isVisible);
            
            if (isVisible) 
                _label.text = summary.ToShortLabel();
        }
    }
}
