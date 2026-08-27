using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class GameObjectVisibleInteractable : ICanExecuteView
    {
        [SerializeField] private GameObject _gameObject;
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        public GameObjectVisibleInteractable(GameObject gameObject, IConverter<bool, bool> converter = null)
        {
            _converter = converter;
            _gameObject = gameObject;
        }

        public void SetCanExecute(bool canExecute) =>
            _gameObject.SetActive(_converter?.Convert(canExecute) ?? canExecute);
    }
}
