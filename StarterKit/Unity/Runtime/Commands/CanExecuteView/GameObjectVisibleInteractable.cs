using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class GameObjectVisibleInteractable : ICanExecuteView
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _isInvert;

        public GameObjectVisibleInteractable(GameObject gameObject, bool isInvert = false)
        {
            _isInvert = isInvert;
            _gameObject = gameObject;
        }

        public void SetCanExecute(bool canExecute) =>
            _gameObject.SetActive(_isInvert ? !canExecute : canExecute);
    }
}