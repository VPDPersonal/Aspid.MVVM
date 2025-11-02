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
        
        [SerializeField] private Binders[] _binderList;

        protected override void InitializeInternal(IViewModel viewModel)
        {
            foreach (var binders in _binderList)
                binders.Bind(viewModel);
        }

        protected override void DeinitializeInternal()
        {
            foreach (var binders in _binderList)
                binders.Unbind();
        }

        [Serializable]
        private sealed class Binders
        {
#if UNITY_EDITOR
            [SerializeField] private string _assemblyQualifiedName;      
#endif
            [SerializeField] private string _id;
            
            [RequireBinder(nameof(_assemblyQualifiedName), Name = nameof(_id))]
            [SerializeField] private MonoBinder[] _monoBinders;
            
            public void Bind(IViewModel viewModel)
            {
                var result = viewModel.FindBindableMember(new FindBindableMemberParameters(_id));
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