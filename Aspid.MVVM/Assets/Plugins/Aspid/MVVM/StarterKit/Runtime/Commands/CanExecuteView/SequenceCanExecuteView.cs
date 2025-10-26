using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class SequenceCanExecuteView : ICanExecuteView
    {
        #if UNITY_2023_1_OR_NEWER
        [SerializeReferenceDropdown]
        [UnityEngine.SerializeReference] 
        #endif
        private ICanExecuteView[] _canExecuteViews;

        public SequenceCanExecuteView(params ICanExecuteView[] canExecuteViews)
        {
            _canExecuteViews = canExecuteViews;
        }

        public void SetCanExecute(bool canExecute)
        {
            foreach (var canExecuteView in _canExecuteViews)
                canExecuteView.SetCanExecute(canExecute);
        }
    }
}