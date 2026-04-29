// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class Constants
    {
        public const string NoneOption = "<None>";

        public const string StringIdFieldName = "__stringId";
        public const string IntIdFieldName = "_id";

        public static class Drawer
        {
            public const string StyleSheetPath = "Styles/Aspid-FastTools-Id-Drawer";

            public const string Root = "aspid-fasttools-id-drawer";
            public const string MainRow = "aspid-fasttools-id-drawer-main-row";
            public const string Label = "aspid-fasttools-id-drawer-label";
            public const string Dropdown = "aspid-fasttools-id-drawer-dropdown";
            public const string CreateButton = "aspid-fasttools-id-drawer-create-button";
            public const string CreateRow = "aspid-fasttools-id-drawer-create-row";
            public const string Input = "aspid-fasttools-id-drawer-input";
            public const string AddButton = "aspid-fasttools-id-drawer-add-button";
            public const string CancelButton = "aspid-fasttools-id-drawer-cancel-button";
            public const string Error = "aspid-fasttools-id-drawer-error";
            public const string OpenButton = "aspid-fasttools-id-drawer-open-button";
            public const string IntOnlyHint = "aspid-fasttools-id-drawer-int-only-hint";
        }

        public static class Registry
        {
            public const string StyleSheetPath = "Styles/Aspid-FastTools-Id-Registry";
            
            public const string Delete = "aspid-fasttools-id-registry-delete";
            public const string Confirm = "aspid-fasttools-id-registry-confirm";
            public const string Add = "aspid-fasttools-id-registry-add";
            public const string Warning = "aspid-fasttools-id-registry-warning";
            public const string WarningVisible = "aspid-fasttools-id-registry-warning--visible";
            public const string WarningLabel = "aspid-fasttools-id-registry-warning-label";
            public const string WarningButton = "aspid-fasttools-id-registry-warning-button";
            public const string NextId = "aspid-fasttools-id-registry-next-id";
            public const string Toolbar = "aspid-fasttools-id-registry-toolbar";
            public const string GroupFoldout = "aspid-fasttools-id-registry-group-foldout";
            
            public const int ScrollThreshold = 10;
            public const int MaxVisibleRows = 10;
            public const float RowHeight = 32.5f;
        }

        public static class Selector
        {
            public const string StyleSheetPath = "Styles/Aspid-FastTools-Id-Selector";

            public const string Container = "aspid-fasttools-id-selector-container";
            public const string Item = "aspid-fasttools-id-selector-item";
            public const string ItemName = "aspid-fasttools-id-selector-item-name";
            public const string ItemId = "aspid-fasttools-id-selector-item-id";
        }
    }
}
