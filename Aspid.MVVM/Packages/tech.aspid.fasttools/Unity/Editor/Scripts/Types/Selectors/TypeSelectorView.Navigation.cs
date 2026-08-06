using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    // Activating a row: toggling a section, drilling into a namespace, navigating back, emitting a final selection,
    // and toggling favorites. The generic-argument drill-down branches out from SelectNode into the .Generics file.
    internal sealed partial class TypeSelectorView
    {
        private void ActivateNode(TreeNode node)
        {
            if (node.IsSectionTitle) ToggleSectionKeepSelection(node);
            else if (node.HasChildren && !Nav.IsSearching) NavigateInto(node);
            else if (node.IsSelectable) SelectNode(node);
        }

        private void NavigateInto(TreeNode node)
        {
            HideError();
            Nav.NavigateInto(node);
            RefreshView();

            _listView.selectedIndex = 0;
            _listView.ScrollToItem(0);
        }

        private void NavigateBack()
        {
            HideError();

            if (Nav.CanNavigateBack)
            {
                var previousNode = Nav.NavigateBack();
                RefreshView();

                var index = Nav.CurrentItems.IndexOf(previousNode);
                _listView.selectedIndex = index >= 0 ? index : 0;
                _listView.ScrollToItem(_listView.selectedIndex);
                return;
            }

            if (_pages.Count > 1)
                PopPage();
        }

        private void SelectNode(TreeNode node)
        {
            HideError();

            var page = _pages[^1];
            var aqn = node.AssemblyQualifiedName;
            var type = string.IsNullOrEmpty(aqn) ? null : Type.GetType(aqn, throwOnError: false);

            // An open generic definition is not a final selection — drill into its argument flow instead.
            if (type is { IsGenericTypeDefinition: true })
            {
                var validationFieldTypes = page.IsBase ? _fieldTypes : new[] { page.ConstraintType };
                BeginResolveGeneric(type, page.ConstraintType, validationFieldTypes, page.OnPicked);
                return;
            }

            if (page.IsBase)
            {
                Emit(aqn);
                return;
            }

            if (type is not null)
                page.OnPicked(type);
        }

        private void Emit(string assemblyQualifiedName)
        {
            // Recents surface only AQNs the hierarchy actually contains, and the hierarchy holds the OPEN generic
            // definition, never a closed form — recording "Modifier`1[[Single…]]" would burn an invisible Recent
            // slot forever (the raw-entry trim then evicts a real, displayable recent per generic pick). Record the
            // definition instead: "Modifier<T>" under Recent is also the row the user actually clicked.
            var recorded = assemblyQualifiedName;
            if (!string.IsNullOrEmpty(assemblyQualifiedName) &&
                Type.GetType(assemblyQualifiedName, throwOnError: false) is { IsConstructedGenericType: true } constructed)
                recorded = constructed.GetGenericTypeDefinition().AssemblyQualifiedName;

            TypeSelectorPreferences.RecordRecent(recorded);

            _onSelected?.Invoke(assemblyQualifiedName);
            _onDismiss?.Invoke();
        }

        private void ToggleFavorite(TreeNode node)
        {
            if (!node.IsType) return;

            TypeSelectorPreferences.ToggleFavorite(node.AssemblyQualifiedName);

            Nav.RefreshFavoritesSection();

            // On the root page the recomposed section must be re-rendered; on a search/namespace page only the row's
            // own star glyph changed, so a lighter item refresh suffices.
            if (Nav.IsAtRoot) RefreshView();
            else _listView.RefreshItems();
        }
    }
}
