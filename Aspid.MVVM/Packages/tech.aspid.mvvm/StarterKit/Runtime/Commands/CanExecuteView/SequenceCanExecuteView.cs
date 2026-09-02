using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class SequenceCanExecuteView : ICanExecuteView
    {
        [TypeSelector]
        [SerializeReference] private ICanExecuteView[] _canExecuteViews;

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