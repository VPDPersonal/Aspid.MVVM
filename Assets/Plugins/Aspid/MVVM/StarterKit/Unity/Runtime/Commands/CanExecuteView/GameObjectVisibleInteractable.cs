using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
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