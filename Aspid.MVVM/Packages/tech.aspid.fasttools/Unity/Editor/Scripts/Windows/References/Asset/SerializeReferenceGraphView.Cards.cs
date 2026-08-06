using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    // Document-level layout: one collapsible card per serialized-object document, the walk that flattens its reference
    // tree into a stack of sibling cards, and the trailing "Orphaned" group. Nesting is carried by each card's
    // threaded field path, never by indentation — the individual cards are built by the .Nodes partial.
    internal sealed partial class SerializeReferenceGraphView
    {
        private const string DocumentClass = RootClass + "__document";
        private const string DocumentHeaderClass = RootClass + "__document-header";
        private const string DocumentHeaderIssuesClass = DocumentHeaderClass + "--issues";
        private const string DocumentHeaderRowClass = RootClass + "__document-header-row";
        private const string DocumentTitleClass = RootClass + "__document-title";
        private const string DocumentCountClass = RootClass + "__document-count";
        private const string DocumentBodyClass = RootClass + "__document-body";

        private const string OrphanGroupClass = RootClass + "__orphan-group";
        private const string OrphanGroupHeaderClass = RootClass + "__orphan-group-header";

        private const string DocumentChevronExpanded = "▼";
        private const string DocumentChevronCollapsed = "▶";

        // One serialized object document: a collapsible header band over a flat stack of node cards plus a trailing
        // "Orphaned" group. The header is dropped for a single-document asset — there it would only restate the
        // ObjectField above it.
        private VisualElement BuildDocument(string assetPath, ReferenceGraphDocument document, bool showHeader)
        {
            // Pending migrations are not issues — a document whose only findings are migrations keeps the calm
            // header, matching the info-toned overview; orphans and genuinely broken nodes still glow amber.
            var (broken, migrations) = SerializeReferenceGraphAnalysis.CountUnresolved(assetPath, document, _constraints);
            var hasIssues = document.Orphans.Count > 0 || broken > 0;

            var body = new VisualElement().AddClass(DocumentBodyClass);

            // The header is built (and registered on the nav ring) BEFORE the body cards, so the keyboard order
            // matches the visual order — the band sits above the cards it collapses.
            var header = showHeader ? BuildDocumentHeader(document, body, hasIssues, broken, migrations) : null;

            // Missing roots render first. Two passes over the asset's field order keep the partition stable between
            // rescans; empty (unassigned) roots are not missing, so they fall to the second pass.
            foreach (var root in document.Roots)
            {
                if (root.IsEmpty || !SerializeReferenceGraphAnalysis.RootIsMissing(document, root.Rid)) continue;
                AppendNode(body, assetPath, document, root.Rid, root.Label, new HashSet<long>());
            }

            foreach (var root in document.Roots)
            {
                if (root.IsEmpty)
                {
                    body.AddChild(BuildEmptySlotCard(assetPath, document.FileId, root.Label));
                    continue;
                }

                if (SerializeReferenceGraphAnalysis.RootIsMissing(document, root.Rid)) continue;
                AppendNode(body, assetPath, document, root.Rid, root.Label, new HashSet<long>());
            }

            var orphans = BuildOrphanGroup(assetPath, document);
            if (orphans is not null) body.AddChild(orphans);

            // Single-document asset: no header band — the ObjectField above already names it. Always expanded.
            if (header is null)
                return new VisualElement().AddClass(DocumentClass).AddChild(body);

            return new VisualElement()
                .AddClass(DocumentClass)
                .AddChild(header)
                .AddChild(body);
        }

        // The collapse band over a document's body. The self-reference lets the click handler flip its own chevron
        // alongside toggling the body.
        private AspidGradientButton BuildDocumentHeader(ReferenceGraphDocument document, VisualElement body,
            bool hasIssues, int broken, int migrations)
        {
            var collapsed = false;
            AspidGradientButton header = null;

            var toggle = new Action(() =>
            {
                collapsed = !collapsed;
                body.style.display = collapsed ? DisplayStyle.None : DisplayStyle.Flex;
                header.Text = collapsed ? DocumentChevronCollapsed : DocumentChevronExpanded;
            });

            header = new AspidGradientButton(DocumentChevronExpanded, _ => toggle())
                .AddClass(DocumentHeaderClass);
            if (hasIssues) header.AddClass(DocumentHeaderIssuesClass);
            header.tooltip = $"fileId {document.FileId}";
            RegisterNavTarget(header, toggle);

            // Ignored for picking so clicks fall through to the band's own handler.
            header.AddLeadingContent(new VisualElement()
                .AddClass(DocumentHeaderRowClass)
                .SetPickingMode(PickingMode.Ignore)
                .AddChild(new Label(document.TypeName)
                    .AddClass(DocumentTitleClass)
                    .SetPickingMode(PickingMode.Ignore))
                .AddChild(new Label(SerializeReferenceGraphSummary.BuildDocumentCountText(document, broken, migrations))
                    .AddClass(DocumentCountClass)
                    .SetPickingMode(PickingMode.Ignore)));

            return header;
        }

        // Appends a node's card and, recursively, its children's as flat siblings — nesting is carried by the threaded
        // field path, not the layout. The visited set makes the walk cycle-safe: a rid already on the current path
        // renders as a back-edge leaf instead of recursing forever.
        private void AppendNode(VisualElement container, string assetPath, ReferenceGraphDocument document, long rid, string pathLabel, HashSet<long> visited)
        {
            if (!visited.Add(rid))
            {
                container.AddChild(BuildBackEdgeCard(rid));
                return;
            }

            var node = document.FindNode(rid);
            container.AddChild(BuildNodeCard(assetPath, document, node, rid, pathLabel, isOrphan: false));

            foreach (var edge in document.ChildrenOf(rid))
            {
                var childPath = SerializeReferenceGraphAnalysis.CombinePath(pathLabel, edge.Label);
                if (edge.IsEmpty)
                    container.AddChild(BuildEmptySlotCard(assetPath, document.FileId, childPath));
                else
                    AppendNode(container, assetPath, document, edge.Rid, childPath, visited);
            }

            // Leaving the recursion: drop the rid so a sibling subtree may legitimately reference it again (shared),
            // while a back-edge on the current path is still caught above.
            visited.Remove(rid);
        }

        // Warning-tinted group for rids no root reaches. Each orphan is a full node card (so a missing orphan is still
        // fixable inline) with a footer Clear, without recursion into children.
        private VisualElement BuildOrphanGroup(string assetPath, ReferenceGraphDocument document)
        {
            if (document.Orphans.Count == 0) return null;

            var group = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(OrphanGroupClass);

            group.AddChild(new AspidLabel("Orphaned", AspidLabelPreset.Default
                    .SetLabelStatus(StatusStyle.Type.Warning)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                    .SetLineSize(AspidDividingLineSizeStyle.Type.None))
                .AddClass(OrphanGroupHeaderClass));

            foreach (var node in document.Nodes)
            {
                if (!document.Orphans.Contains(node.Rid)) continue;
                group.AddChild(BuildNodeCard(assetPath, document, node, node.Rid, pathLabel: null, isOrphan: true));
            }

            return group;
        }
    }
}
