using System;
using UnityEngine;
using Aspid.UnityFastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class ViewModelInitializeComponent : InitializeComponent<IViewModel>
    {
        [TypeSelector(typeof(IViewModel))]
        [SerializeField] private string _typeName;
        
        public override void Validate()
        {
            base.Validate();

            _typeName = Resolve switch
            {
                ResolveType.Mono => null,
                ResolveType.References => null,
                ResolveType.ScriptableObject => null,
                _ => _typeName
            };
        }
        
        protected override Type GetTypeForDi() =>
            Type.GetType(_typeName);
    }
}