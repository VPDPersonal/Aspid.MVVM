// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class Constants
    {
        public const string NoneOption = "<None>";
        public const string IntIdFieldName = "_id";
        public const string StringIdFieldName = "__stringId";
        
        public static class Registry
        {
            public const string StyleSheetPath = "UI/Ids/Aspid-FastTools-Id-Registry";
            
            public const string Add = "aspid-fasttools-id-registry__add";
            public const string WarningVisible = "aspid-fasttools-id-registry__warning--visible";
            public const string Toolbar = "aspid-fasttools-id-registry__toolbar";
            public const string GroupFoldout = "aspid-fasttools-id-registry__group-foldout";
            public const string List = "aspid-fasttools-id-registry__list";

            public const int ScrollThreshold = 10;
            public const int MaxVisibleRows = 10;
            public const float RowHeight = 32.5f;
        }
    }
}
