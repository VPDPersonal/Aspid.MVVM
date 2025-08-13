using System;
using System.Linq;
using UnityEditor;
using Aspid.CustomEditors;
using UnityEngine.UIElements;

namespace Aspid.MVVM.Unity
{
    public abstract class ViewEditor<T> : Editor
        where T : UnityEngine.Object, IView, IMonoBinderSource
    {
        protected T View => target as T;
        
        protected VisualElement Root { get; private set; }
        
        protected ValidableBindersById Binders { get; private set; }
        
        protected string IconPath => MessageType switch
        {
            ErrorType.None => "Aspid Icon",
            ErrorType.Error => "Aspid Icon Red",
            ErrorType.Warning => "Aspid Icon Yellow",
            _ => throw new ArgumentOutOfRangeException()
        };

        protected virtual ErrorType MessageType => ErrorType.None;
        
        protected virtual string[] PropertiesExcluding => new[]
        {
            "m_Script",
        };

        protected virtual void OnEnable()
        {
            UpdateBinders();
        }

        protected virtual void OnDisable()
        {
            if (View)
            {
                if (View is MonoView monoView)
                {
                    ViewUtility.ValidateView(monoView);
                }
                
                return;
            }
            
            if (Binders is null || Binders.Count is 0) return;
            
            foreach (var binders in Binders.Values)
            {
                if (binders is null) continue;

                foreach (var binder in binders)
                    binder.Reset();
            }
        }

        #region Build Methods
        public sealed override VisualElement CreateInspectorGUI()
        {
            Root = Build();
            return Root;
        }

        protected virtual VisualElement Build()
        {
            return new VisualElement()
                .AddChild(BuildHeader())
                .AddChild(BuildBaseInspector())
                .AddChild(BuildViewModel());
        }

        protected VisualElement BuildHeader() 
        {
            var header = Elements.CreateHeader(IconPath, GetScriptName());
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            return header;
        }

        protected VisualElement BuildBaseInspector()
        {
            return Elements.CreateContainer(EditorColor.LightContainer)
                .SetMargin(top: 10)
                .SetName("BaseInspector")
                .AddChild(new IMGUIContainer(DrawBaseInspector));
        }

        protected VisualElement BuildViewModel()
        {

            var title = Elements.CreateTitle(EditorColor.LightText, "View Model");
            
            var viewModel = Elements.CreateContainer(EditorColor.LightContainer)
                .AddChild(title)
                .AddChild(ViewModelDebugPanel.Build(View)
                    .SetName("ViewModelContainer"))
                .SetMargin(top: 10)
                .SetName("ViewModel");
            
            var refreshButton = new Button(Refresh)
            {
                text = "Refresh"
            };

            title.Q<VisualElement>("TextContainer").AddChild(refreshButton);

            return viewModel;

            void Refresh()
            {
                viewModel.Remove(viewModel.Q<VisualElement>("ViewModelContainer"));
                viewModel.AddChild(ViewModelDebugPanel.Build(View)
                    .SetName("ViewModelContainer"));
            }
        }   
        #endregion

        protected virtual string GetScriptName() =>
            !View ? null : View.GetScriptName();
        
        protected void UpdateBinders() =>
            Binders = View ? ValidableBindersById.GetValidableBindersById(View) : null;

        private void DrawBaseInspector()
        {
            var propertiesCount = 0;
            var oldBindersDictionary = ValidableBindersById.GetValidableBindersById(View);

            serializedObject.UpdateIfRequiredOrScript();
            {
                propertiesCount += OnDrawingBaseInspector();
                
                var enterChildren = true;
                var iterator = serializedObject.GetIterator();
                
                while (iterator.NextVisible(enterChildren))
                {
                    enterChildren = false;
                    if (PropertiesExcluding.Contains(iterator.name)) continue;
                    
                    propertiesCount++;
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }
            serializedObject.ApplyModifiedProperties();
            
            var newBindersDictionary = ValidableBindersById.GetValidableBindersById(View);
            ViewUtility.ValidateViewChanges(View, oldBindersDictionary, newBindersDictionary);
            UpdateBinders();
            
            serializedObject.UpdateIfRequiredOrScript();
            {
                propertiesCount += OnDrewBaseInspector();
            }
            serializedObject.ApplyModifiedProperties();
            
            Root.Q<VisualElement>("BaseInspector").style.display = propertiesCount is 0 
                ? DisplayStyle.None 
                : DisplayStyle.Flex;
        }
        
        protected virtual int OnDrawingBaseInspector() => 0;

        protected virtual int OnDrewBaseInspector() => 0;
    }
}