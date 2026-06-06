// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    internal class SwitcherMonoBinderVisualElement : MonoBinderVisualElement
    {
        protected override string ScriptSubtext => "Switcher";
        
        public SwitcherMonoBinderVisualElement(MonoBinderEditor editor) 
            : base(editor) { }

        protected override string GetScriptName() => HeaderNameHelper.StripTrailingSuffixPreservingIndex(
            name: base.GetScriptName(),
            suffix: $" {ScriptSubtext}");
    }
}
