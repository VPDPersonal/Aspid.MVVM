using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The inline type picker both References tabs dock under a clicked card header: one panel open at a time, dropped
    /// directly below its anchor inside the anchor's own card so the header, selector and the rows beneath it read as
    /// one active card.
    /// </summary>
    /// <remarks>
    /// The two tabs keep their own USS blocks, so the host wears the block's class names passed as a
    /// <see cref="PickerClasses"/> — the same arrangement <see cref="SerializeReferenceAuditUI.BuildLegendItem"/> uses
    /// for the legend. Anchors are expected to end in a chevron, which the host swaps in place (▼ ⇄ ▲) rather than
    /// rewriting the label, so every band verb keeps its own wording.
    /// </remarks>
    internal sealed class AuditPickerHost
    {
        /// <summary>The block-specific USS class names the shared picker host wears.</summary>
        internal readonly struct PickerClasses
        {
            /// <summary>The docked panel itself.</summary>
            public readonly string Picker;

            /// <summary>Welds the panel to the header above it; applied only when the anchor sits inside a card.</summary>
            public readonly string PickerAttached;

            /// <summary>Marks the hosting card as picking, so its divider / hover sweep stand down.</summary>
            public readonly string CardPicking;

            public PickerClasses(string picker, string pickerAttached, string cardPicking)
            {
                Picker = picker;
                PickerAttached = pickerAttached;
                CardPicking = cardPicking;
            }
        }

        private const char ChevronCollapsed = '▼';
        private const char ChevronExpanded = '▲';

        private readonly VisualElement _host;
        private readonly VisualElement _fallbackContainer;
        private readonly PickerClasses _classes;

        private VisualElement _picker;
        private AspidGradientButton _anchor;
        private VisualElement _card;

        /// <param name="host">The view itself — it reclaims keyboard focus when the picker closes.</param>
        /// <param name="fallbackContainer">Where the panel lands if an anchor is ever hosted outside a card.</param>
        /// <param name="classes">The hosting block's picker class names.</param>
        public AuditPickerHost(VisualElement host, VisualElement fallbackContainer, in PickerClasses classes)
        {
            _host = host;
            _fallbackContainer = fallbackContainer;
            _classes = classes;
        }

        /// <summary>Whether a picker is currently docked — the views suspend their keyboard ring while it is.</summary>
        public bool IsOpen => _picker is not null;

        /// <summary>
        /// The close half of a toggle: closes whatever is open and reports whether that was
        /// <paramref name="anchor"/>'s own picker, i.e. whether the click was a collapse and the caller should stop.
        /// </summary>
        public bool ToggleClosed(AspidGradientButton anchor)
        {
            var wasOpen = _anchor == anchor;
            Close();
            return wasOpen;
        }

        /// <summary>Docks <paramref name="content"/> directly below <paramref name="anchor"/> and focuses it.</summary>
        public void Open(AspidGradientButton anchor, TypeSelectorView content)
        {
            _picker = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(_classes.Picker)
                .AddChild(content);

            _anchor = anchor;
            if (anchor is not null) anchor.Text = anchor.Text.Replace(ChevronCollapsed, ChevronExpanded);

            // The anchor is a direct child of its card, so the panel drops right below it inside the card; the ??
            // fallback keeps a sane target if the anchor is ever hosted outside one.
            var card = anchor?.parent;
            var container = card ?? _fallbackContainer;
            container.InsertChild(container.IndexOf(anchor) + 1, _picker);

            if (card is not null)
            {
                _card = card;
                _card.AddClass(_classes.CardPicking);
                _picker.AddClass(_classes.PickerAttached);
            }

            content.FocusPicker();
        }

        /// <summary>Undocks the panel, restores its anchor's chevron and hands keyboard focus back to the host.</summary>
        public void Close()
        {
            _picker?.RemoveFromHierarchy();
            if (_anchor is not null) _anchor.Text = _anchor.Text.Replace(ChevronExpanded, ChevronCollapsed);
            _card?.RemoveClass(_classes.CardPicking);

            _picker = null;
            _anchor = null;
            _card = null;

            // The dismissed picker leaves keyboard focus dangling on its (removed) search field; reclaim it so the
            // arrow-key ring keeps working. Guarded — Close also runs from render paths before attach.
            if (_host.panel is not null) _host.Focus();
        }
    }
}
