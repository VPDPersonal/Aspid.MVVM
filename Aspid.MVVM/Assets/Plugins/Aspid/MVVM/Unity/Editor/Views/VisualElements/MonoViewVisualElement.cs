#nullable enable
using System.Linq;
using Aspid.Internal;
using UnityEngine.UIElements;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class MonoViewVisualElement : MonoViewVisualElement<MonoView, MonoViewEditor>
    {
        public MonoViewVisualElement(MonoViewEditor editor) :
            base(editor) { }
    }

    public abstract class MonoViewVisualElement<TView, TEditor> : ViewVisualElement<TView, TEditor>
        where TView : MonoView
        where TEditor : MonoViewEditor<TView, TEditor>
    {
        private UnassignedBindersVisualElement<TView, TEditor>? _unassignedBindersVisualElement;
        
        protected override string IconPath => Editor.UnassignedBinders.Any()
            ? EditorConstants.AspidIconYellow
            : base.IconPath;
        
        public MonoViewVisualElement(TEditor editor)
            : base(editor) { }
        
        protected override void OnUpdate() =>
            _unassignedBindersVisualElement!.Update();
        
        protected override VisualElement OnBuiltBaseInspector() =>
            BuildUnassignedBinders();

        private UnassignedBindersVisualElement<TView, TEditor> BuildUnassignedBinders()
        {
            _unassignedBindersVisualElement = new UnassignedBindersVisualElement<TView, TEditor>(Editor);
            return _unassignedBindersVisualElement;
        }
        
        protected override string GetScriptName()
        {
            var view = Editor.TargetAsSpecific;
            if (!view) return string.Empty;
	        
            var type = view.GetType();
            var views = view.GetComponents(type);
	        
            switch (views.Length)
            {
                case 0: return string.Empty;
                case 1: return views[0].GetScriptName();
                default:
                    {
                        var index = 0;
	        
                        foreach (var component in views)
                        {
                            if (component.GetType() != type) continue;
		        
                            index++;
                            if (component == view) return $"{view.GetScriptName()} ({index})";
                        }
				        
                        return string.Empty;
                    }
            }
        }
    }
}