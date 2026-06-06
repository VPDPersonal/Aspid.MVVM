using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    [Serializable]
    public struct MonoBinderPreviousView
    {
        [SerializeField] private string _name;
        [SerializeField] private Component _view;
        
        public string Name => _name;
        
        public Component View => _view;

        public MonoBinderPreviousView(Component view)
        {
            _view = view;
            _name = view ? view.name : string.Empty;
        }
    }
}