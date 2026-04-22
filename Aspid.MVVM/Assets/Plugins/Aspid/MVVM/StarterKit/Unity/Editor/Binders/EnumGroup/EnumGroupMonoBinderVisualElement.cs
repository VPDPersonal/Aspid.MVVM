// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class EnumGroupMonoBinderVisualElement : MonoBinderVisualElement
    {
        protected override string ScriptSubtext => "EnumGroup";
        
        public EnumGroupMonoBinderVisualElement(MonoBinderEditor editor) 
            : base(editor) { }
        
        protected override string GetScriptName() => HeaderNameHelper.StripTrailingSuffixPreservingIndex(
            name: base.GetScriptName(),
            suffix: $" {ScriptSubtext}");
    }
}
