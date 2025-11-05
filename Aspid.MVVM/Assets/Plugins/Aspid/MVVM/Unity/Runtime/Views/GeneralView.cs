using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class GeneralView : MonoView
    {
#if UNITY_EDITOR
        [SerializeField] private MonoScript _designViewModel;
#endif
        
        [RequireBinder(Id = "General")]
        [SerializeField] private Binders[] _bindersList;

        protected override void InitializeInternal(IViewModel viewModel)
        {
            foreach (var binders in _bindersList)
                binders.Bind(viewModel);
        }

        protected override void DeinitializeInternal()
        {
            foreach (var binders in _bindersList)
                binders.Unbind();
        }

        [Serializable]
        private sealed class Binders
        {
#if UNITY_EDITOR
            [SerializeField] private string _assemblyQualifiedName;      
#endif
            [SerializeField] private string _name;
            
            [RequireBinder(nameof(_assemblyQualifiedName), Id = nameof(_name))]
            [SerializeField] private MonoBinder[] _monoBinders;
            
            public void Bind(IViewModel viewModel)
            {
                var result = viewModel.FindBindableMember(new FindBindableMemberParameters(_name));
                if (!result.IsFound) return;
                
                _monoBinders.BindSafely(result);
            }

            public void Unbind()
            {
                foreach (var monoBinder in _monoBinders)
                {
                    monoBinder.Unbind();
                }
            }
        }
    }
}