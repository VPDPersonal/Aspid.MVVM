// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class EnumMonoBinderVisualElement : MonoBinderVisualElement
    {
        protected override string ScriptSubtext => "Enum";
        
        public EnumMonoBinderVisualElement(MonoBinderEditor editor) : base(editor) { }

        protected override string GetScriptName() =>
            HeaderNameHelper.StripTrailingSuffixPreservingIndex(base.GetScriptName(), " " + ScriptSubtext);
    }
}
