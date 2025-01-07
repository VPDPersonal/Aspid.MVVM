using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using Aspid.CustomEditors.Configs;
using Aspid.CustomEditors.Components;
using Aspid.CustomEditors.Extensions;
using Aspid.CustomEditors.Extensions.VisualElements;

namespace Aspid.MVVM.Mono
{
    public abstract class ViewModelEditor<T> : Editor
        where T : UnityEngine.Object, IViewModel
    {
        protected T ViewModel => target as T;
        
        protected VisualElement Root { get; private set; }
        
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
        
        public sealed override VisualElement CreateInspectorGUI()
        {
            Root = Build();
            return Root;
        }

        protected virtual VisualElement Build()
        {
            return new VisualElement()
                .AddChild(BuildHeader())
                .AddChild(BuildBaseInspector());
        }
        
        protected VisualElement BuildHeader() =>
            Elements.CreateHeader(IconPath, GetScriptName());
        
        protected VisualElement BuildBaseInspector()
        {
            return Elements.CreateContainer(EditorColor.LightContainer)
                .SetMargin(top: 10)
                .SetName("BaseInspector")
                .AddChild(new IMGUIContainer(DrawBaseInspector));
        }

        private void DrawBaseInspector()
        {
            var propertiesCount = 0;

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

                propertiesCount += OnDrewBaseInspector();
            }
            serializedObject.ApplyModifiedProperties();
            
            Root.Q<VisualElement>("BaseInspector").style.display = propertiesCount is 0 
                ? DisplayStyle.None 
                : DisplayStyle.Flex;
        }
        
        protected virtual int OnDrawingBaseInspector() => 0;

        protected virtual int OnDrewBaseInspector() => 0;

        protected virtual string GetScriptName() =>
            !ViewModel ? null : ViewModel.GetScriptName();
    }
}