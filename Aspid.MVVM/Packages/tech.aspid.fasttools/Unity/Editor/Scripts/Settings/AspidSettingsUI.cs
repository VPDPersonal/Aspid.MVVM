using System;
using UnityEditor;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.SerializeReferences.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// The shared vocabulary and composition of the package's settings surfaces — the SerializeReference window's
    /// Settings tab, the <c>Preferences → Aspid.FastTools</c> page and the
    /// <c>Project Settings → Aspid.FastTools → SerializeReference</c> page. Owns the surface's stylesheet path and USS
    /// class names (sections, legend, row/action primitives, and the storage-scope markers), the one definition of
    /// the surface's content (<see cref="BuildSurfaceContent"/>: the legend and every package area's section, filtered
    /// by <see cref="AspidSettingsScope"/>), its per-scope reset footer (<see cref="BuildResetFooter"/>), the branded
    /// page host the two Unity settings pages share (<see cref="BuildProviderPage"/>), and the live-sync helper every
    /// per-area settings builder wires its controls with. Keeping these here — rather than on one view — is what keeps
    /// the surfaces identical and lets each package area define its controls once.
    /// </summary>
    internal static class AspidSettingsUI
    {
        /// <summary>
        /// The settings surface stylesheet — dark-branded cards, scoped under <see cref="RootClass"/>.
        /// </summary>
        internal const string StyleSheetPath = "UI/Windows/Aspid-FastTools-Settings";

        internal const string RootClass = "aspid-fasttools-settings";

        // Canvas pair for pages hosting the surface outside the SerializeReference window (which owns its own
        // dotted canvas): the host fills the page, the background gives the dots their black base.
        internal const string CanvasClass = "aspid-fasttools-settings-canvas";
        internal const string CanvasBackgroundClass = "aspid-fasttools-settings-canvas__background";
        internal const string HeaderClass = "aspid-fasttools-settings__header";
        internal const string HeaderTitleClass = "aspid-fasttools-settings__header-title";
        internal const string HeaderDescriptionClass = "aspid-fasttools-settings__header-description";
        internal const string SectionClass = "aspid-fasttools-settings__section";
        internal const string SectionTitleClass = "aspid-fasttools-settings__section-title";
        internal const string SectionDividerClass = "aspid-fasttools-settings__section-divider";
        internal const string SectionContentClass = "aspid-fasttools-settings__section-content";
        internal const string LegendClass = "aspid-fasttools-settings__legend";
        internal const string LegendItemClass = "aspid-fasttools-settings__legend-item";
        internal const string LegendSwatchClass = "aspid-fasttools-settings__legend-swatch";
        internal const string LegendTextClass = "aspid-fasttools-settings__legend-text";
        internal const string FooterClass = "aspid-fasttools-settings__footer";

        // Storage-scope markers, applied by the per-area builders to every settings row.
        internal const string SharedScopeClass = "aspid-fasttools-settings-scope--shared";
        internal const string UserScopeClass = "aspid-fasttools-settings-scope--user";

        // The stripe element a scoped row mounts on its left edge, and the backplate that carries the row's
        // hover/keyboard-focus fill clear of the stripe (see WithScopeStripe).
        internal const string ScopeStripeClass = "aspid-fasttools-settings__scope-stripe";
        internal const string RowBackplateClass = "aspid-fasttools-settings__row-backplate";

        // Keyboard ring markers (the other tabs' nav idiom): the window's Settings tab walks a flat focus ring over
        // the actionable elements; the focused class mirrors each element's hover treatment in USS.
        internal const string NavTargetClass = "aspid-fasttools-settings__nav-target";
        internal const string NavTargetFocusedClass = "aspid-fasttools-settings__nav-target--focused";

        // Row primitives for non-BaseField settings rows (e.g. action rows of buttons), styled to match the field
        // rows. Danger = red hover family for destructive actions; info = blue for per-user-scope actions.
        internal const string RowClass = "aspid-fasttools-settings__row";
        internal const string RowCaptionClass = "aspid-fasttools-settings__row-caption";
        internal const string RowNoteClass = "aspid-fasttools-settings__row-note";
        internal const string ActionClass = "aspid-fasttools-settings__action";
        internal const string ActionDangerClass = "aspid-fasttools-settings__action--danger";
        internal const string ActionInfoClass = "aspid-fasttools-settings__action--info";

        /// <summary>
        /// Dresses <paramref name="element"/> as a settings surface: loads the shared stylesheet and applies
        /// <see cref="RootClass"/>, under which every rule in the sheet is scoped.
        /// </summary>
        internal static T AsSurface<T>(this T element) where T : VisualElement =>
            element.AddStyleSheetsFromResource(StyleSheetPath).AddClass(RootClass);

        /// <summary>
        /// Marks a settings row with its storage scope: applies <paramref name="scopeClass"/> and mounts two absolute
        /// children — the stripe on the row's left edge (a separate element, not a border-left, so it stays a straight
        /// line whatever the row's corner radius) and, behind the content, the backplate that carries the row's
        /// hover / keyboard-focus fill. The fill lives on the backplate — inset past the stripe — rather than on the
        /// row itself, so a lit row never touches its stripe. (The stripe also cannot hang outside the row's box in a
        /// margin gutter: some field types, e.g. AspidSwitch and EnumField, clip children to their bounds.)
        /// </summary>
        internal static T WithScopeStripe<T>(this T element, string scopeClass) where T : VisualElement
        {
            element.AddClass(scopeClass);

            // hierarchy, not Add: a BaseField routes Add through its (closed) contentContainer, but both mounts
            // belong on the row element itself — they position absolutely against the row's box. The backplate goes
            // in at index 0 so it paints behind the row's content; the stripe appends last, over everything.
            element.hierarchy.Insert(0, new VisualElement()
                .AddClass(RowBackplateClass)
                .SetPickingMode(PickingMode.Ignore));

            element.hierarchy.Add(new VisualElement()
                .AddClass(ScopeStripeClass)
                .AddClass(scopeClass)
                .SetPickingMode(PickingMode.Ignore));
            return element;
        }

        /// <summary>
        /// Composes a whole Unity settings page (Preferences or Project Settings) hosting the branded surface for
        /// <paramref name="scope"/>: the window's dotted canvas as the page backdrop (the branded cards read wrong
        /// over Unity's native grey panel), the scope-filtered surface content in a scroll, and the matching reset
        /// footer pinned under it. One definition, so the two Unity-native pages stay pixel-identical to each other
        /// and to the window's Settings tab.
        /// </summary>
        internal static void BuildProviderPage(VisualElement root, AspidSettingsScope scope)
        {
            BuildProviderHost(root, surface =>
            {
                var scroll = new ScrollView(ScrollViewMode.Vertical) { style = { flexGrow = 1 } };
                BuildSurfaceContent(scroll.contentContainer, scope);

                surface.Add(scroll);
                surface.Add(BuildResetFooter(scope));
            });
        }

        /// <summary>
        /// Composes a Unity settings page for one package area — the child pages under
        /// <c>Preferences → Aspid.FastTools</c>: the branded host, the area's name as the underlined page header
        /// (with the scope legend for the page's per-user rows) and one untitled section card that
        /// <paramref name="buildControls"/> fills — the card skips its own title so the page header isn't repeated
        /// right under itself. No reset footer: the per-scope reset lives on the parent aggregate page.
        /// </summary>
        internal static void BuildAreaProviderPage(VisualElement root, string title, Action<VisualElement> buildControls)
        {
            BuildProviderHost(root, surface =>
            {
                var scroll = new ScrollView(ScrollViewMode.Vertical) { style = { flexGrow = 1 } };
                var content = scroll.contentContainer;

                content.Add(BuildSurfaceHeader(AspidSettingsScope.User, title, description: null));
                AddSection(content, title: null, buildControls);
                surface.Add(scroll);
            });
        }

        // The branded page host every Unity-native settings page shares: the window's dotted canvas as the backdrop
        // (the branded cards read wrong over Unity's native grey panel) with the surface that fill composes over it.
        private static void BuildProviderHost(VisualElement root, Action<VisualElement> fill)
        {
            // The theme sheets make a user override apply to this page too.
            root.AddAspidThemeStyleSheets();
            root.style.flexGrow = 1;

            // The host loads the surface sheet so canvas and card rules reach the subtree.
            var host = new VisualElement()
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddClass(CanvasClass);

            // A settings page carries no status of its own; the calm idle wash keeps the canvas neutral here.
            var canvas = new AspidAnimatedDotsBackground()
                .SetStatus(StatusStyle.Type.Info)
                .AddClass(CanvasBackgroundClass)
                .SetPickingMode(PickingMode.Ignore);

            var surface = new VisualElement().AddClass(RootClass);
            fill(surface);

            // The window's version footer closes the page too — sans the keyboard-ring key: the Unity-native pages
            // don't run the ring. A sibling of the surface, not a child, so its hairline spans the page edge to edge
            // clear of the surface's side padding (the window mounts it at root level the same way).
            host.AddChild(canvas).AddChild(surface).AddChild(new AspidWindowFooter(showKeysHint: false));
            root.Add(host);
        }

        /// <summary>
        /// Fills <paramref name="container"/> with the settings surface's content for <paramref name="scope"/> — the
        /// scope legend followed by every package area's section that has controls in that scope, one definition per
        /// area. Every surface calls this, so the window tab (<see cref="AspidSettingsScope.All"/>), the Preferences
        /// page (<see cref="AspidSettingsScope.User"/>) and the Project Settings page
        /// (<see cref="AspidSettingsScope.Shared"/>) render the same control definitions and mirror each other live.
        /// </summary>
        internal static void BuildSurfaceContent(VisualElement container, AspidSettingsScope scope = AspidSettingsScope.All)
        {
            container.Add(BuildSurfaceHeader(
                scope,
                title: "Settings",
                description: "Every FastTools setting in one place. The stripe on each row shows where the value is stored."));

            AddSection(container, "SerializeReference", content => SerializeReferenceSettingsUI.BuildControls(content, scope));

            // The remaining areas are per-user through and through, so a shared-only surface simply has no sections
            // for them rather than empty headers.
            if ((scope & AspidSettingsScope.User) == 0) return;

            AddSection(container, "Type Selector", TypeSelectorSettingsView.BuildControls);
            // Temporarily hidden pending a decision on the theme-override feature. The store, live-apply
            // (AddAspidThemeStyleSheets) and reset stay in place, so any override set earlier still applies
            // and is still cleared by "Reset to defaults" — only the picker UI is out. Re-enable to restore.
            // AddSection(container, "Appearance", AspidThemeSettingsUI.BuildControls);
            AddSection(container, "Welcome", WelcomeSettingsUI.BuildControls);
        }

        /// <summary>
        /// The surface's header — the audit tabs' results-header idiom: an underlined heading over the bare canvas
        /// (no card), a one-line dim description and, right where the reader starts, the scope legend decoding the
        /// row stripes below. Only the scopes the surface renders get a legend item, so a single-scope page never
        /// explains a stripe it doesn't show.
        /// </summary>
        private static VisualElement BuildSurfaceHeader(AspidSettingsScope scope, string title, string description)
        {
            var heading = new AspidLabel(title, AspidLabelPreset.Default
                    .SetLabelTheme(ThemeStyle.Type.Lightness)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H4)
                    .SetLineTheme(ThemeStyle.Type.Dark))
                .AddClass(HeaderTitleClass);

            var header = new VisualElement().AddClass(HeaderClass)
                .AddChild(heading);

            if (description != null)
                header.AddChild(new Label(description).AddClass(HeaderDescriptionClass));

            return header.AddChild(BuildScopeLegend(scope));
        }

        /// <summary>
        /// Appends a titled settings section: a glass group card — the window's card idiom (the Project References
        /// group cards, the Welcome sample cards) — with the section title as its static header row, the shared dim
        /// header divider under it, and a content container that <paramref name="buildContent"/> fills with flat
        /// settings rows. Each package area (SerializeReference, Type Selector, Appearance, Welcome) gets its own
        /// card so a surface reads as one grouped composition from a single definition per area. A <c>null</c>
        /// <paramref name="title"/> skips the header row — the per-area provider pages already carry the area's name
        /// as the page header.
        /// </summary>
        internal static void AddSection(VisualElement container, string title, Action<VisualElement> buildContent)
        {
            var card = new VisualElement().AddClass(SectionClass);

            if (title != null)
                card.AddChild(new Label(title).AddClass(SectionTitleClass))
                    .AddChild(new AspidDividingLine(AspidDividingLinePreset.Default
                            .SetTheme(ThemeStyle.Type.Light)
                            .SetSize(AspidDividingLineSizeStyle.Type.Thin))
                        .AddClass(SectionDividerClass));

            var content = new VisualElement().AddClass(SectionContentClass);
            buildContent(content);
            container.Add(card.AddChild(content));
        }

        /// <summary>
        /// One dim line under a settings row surfacing what its tooltip would otherwise hide — the meaning of a
        /// row's values or a team-wide toggle's real effect. For the few rows whose caption alone doesn't carry the
        /// stakes; self-explanatory rows stay bare so the notes keep their weight.
        /// </summary>
        internal static Label CreateRowNote(string text) => new Label(text).AddClass(RowNoteClass);

        /// <summary>
        /// The one-line key to the rows' scope stripes: each item pairs a dot painted by the same scope class the
        /// rows wear with a caption naming what that colour means for persistence. Lives in the surface header, so
        /// the reader meets the key before the first striped row. Only the scopes the surface renders get an item,
        /// so a single-scope page never explains a stripe it doesn't show.
        /// </summary>
        internal static VisualElement BuildScopeLegend(AspidSettingsScope scope = AspidSettingsScope.All)
        {
            var legend = new VisualElement().AddClass(LegendClass);

            if ((scope & AspidSettingsScope.Shared) != 0)
                legend.Add(BuildLegendItem(SharedScopeClass, "Shared — committed, same for the whole team"));

            if ((scope & AspidSettingsScope.User) != 0)
                legend.Add(BuildLegendItem(UserScopeClass, "Per-user — stored locally, just for you"));

            return legend;
        }

        private static VisualElement BuildLegendItem(string scopeClass, string caption)
        {
            return new VisualElement().AddClass(LegendItemClass)
                .AddChild(new VisualElement().AddClass(LegendSwatchClass).AddClass(scopeClass))
                .AddChild(new Label(caption).AddClass(LegendTextClass));
        }

        /// <summary>
        /// The surface's footer: one row with a reset button per storage scope the surface renders, each wearing its
        /// scope's stripe (the same classes the settings rows wear) and confirming with the exact default values
        /// before touching anything. Two separate buttons — not one — because the scopes have different blast radii:
        /// the shared reset changes the committed asset for the whole team, the per-user one only this machine's
        /// EditorPrefs. Pinned by the caller under its scroll, so the affordance stays reachable however long the
        /// surface grows.
        /// </summary>
        internal static VisualElement BuildResetFooter(AspidSettingsScope scope = AspidSettingsScope.All)
        {
            var row = new VisualElement().AddClass(RowClass)
                .AddChild(new Label("Reset to defaults").AddClass(RowCaptionClass));

            if ((scope & AspidSettingsScope.Shared) != 0)
            {
                var shared = new Button(ResetSharedToDefaults)
                {
                    text = "Shared",
                    tooltip = "Reset the team-wide settings to defaults: Auto de-alias on, Build / CI gate Warn, no excluded scan folders.\n"
                        + "Changes the committed ProjectSettings asset — affects every teammate once committed.",
                };
                row.AddChild(shared.AddClass(ActionClass).AddClass(SharedScopeClass));
            }

            if ((scope & AspidSettingsScope.User) != 0)
            {
                var user = new Button(ResetUserToDefaults)
                {
                    text = "Per-user",
                    tooltip = "Reset your per-user settings to defaults: Breakage detection on, dropdown without [TypeSelector] off, "
                        + $"Favorites section on, Recent items {TypeSelectorSettings.DefaultRecentsCapacity}, no theme override, auto-show Welcome on.\n"
                        + "Only this machine; the saved Favorites / Recent lists are kept.",
                };
                row.AddChild(user.AddClass(ActionClass).AddClass(ActionInfoClass).AddClass(UserScopeClass));
            }

            // The scope legend lives in the surface header (BuildSurfaceHeader); the footer is just the reset row.
            return new VisualElement().AddClass(FooterClass)
                .AddChild(row);
        }

        private static void ResetSharedToDefaults()
        {
            var confirmed = EditorUtility.DisplayDialog(
                "Reset shared settings",
                "Reset the team-wide settings to defaults?\n\n"
                + "• Auto de-alias duplicated list elements: On\n"
                + "• Build / CI gate: Warn\n"
                + "• Excluded scan folders: none\n\n"
                + "This edits the committed ProjectSettings asset, so it affects the whole team once committed.",
                "Reset",
                "Cancel");

            if (confirmed) SerializeReferenceSettings.ResetSharedToDefaults();
        }

        private static void ResetUserToDefaults()
        {
            var confirmed = EditorUtility.DisplayDialog(
                "Reset per-user settings",
                "Reset your per-user settings to defaults?\n\n"
                + "• Breakage detection: On\n"
                + "• Dropdown without [TypeSelector]: Off\n"
                + "• Favorites section: On\n"
                + $"• Recent items: {TypeSelectorSettings.DefaultRecentsCapacity}\n"
                + "• Theme override: none\n"
                + "• Auto-show Welcome: On\n\n"
                + "Only this machine is affected; the saved Favorites / Recent lists are kept.",
                "Reset",
                "Cancel");

            if (!confirmed) return;

            SerializeReferenceSettings.ResetUserToDefaults();
            TypeSelectorSettings.ResetToDefaults();
            AspidThemeSettings.OverrideStyleSheet = null;
            WelcomeSettings.ResetToDefaults();
        }

        /// <summary>
        /// Keeps <paramref name="control"/> in lock-step with a settings store so every surface rendering the same
        /// control mirrors the others live: on each store change signal the control re-reads its backing value through
        /// <paramref name="read"/> <i>without notifying</i>, so it never writes back or loops. The control the user is
        /// actively editing is skipped, so an in-progress edit is never normalized out from under the cursor. The
        /// store's change event is passed as a <paramref name="subscribe"/> / <paramref name="unsubscribe"/> pair
        /// (events can't travel as values); the subscription follows the panel lifecycle so a closed surface leaks
        /// nothing and a re-parented one keeps mirroring.
        /// </summary>
        internal static void SyncFromSettings<TControl, TValue>(
            TControl control,
            Func<TValue> read,
            Action<Action> subscribe,
            Action<Action> unsubscribe)
            where TControl : VisualElement, INotifyValueChanged<TValue>
        {

            // Docking/undocking re-parents the tree to another panel (a detach then an attach WITHOUT a rebuild), so
            // the sync is armed from build time and then follows the attach/detach pair, kept single by the guard;
            // re-arming also re-reads, so a change made while detached is not missed.
            var subscribed = false;

            control.RegisterCallback<AttachToPanelEvent>(_ => Arm());
            control.RegisterCallback<DetachFromPanelEvent>(_ => Disarm());
            Arm();
            return;

            void Arm()
            {
                if (subscribed) return;
                subscribed = true;
                subscribe(Handler);
                control.SetValueWithoutNotify(read());
            }

            void Disarm()
            {
                if (!subscribed) return;
                subscribed = false;
                unsubscribe(Handler);
            }

            void Handler()
            {
                // Only an in-progress TEXT edit must survive a store change: composite fields focus an inner element
                // (hence the containment check), and a clicked switch/enum keeps panel focus indefinitely, so
                // skipping any focused control would silently freeze that surface's mirror.
                if (control.focusController?.focusedElement is VisualElement focused &&
                    (focused == control || control.Contains(focused)) &&
                    focused is ITextEdition or TextElement)
                    return;

                control.SetValueWithoutNotify(read());
            }
        }
    }
}
