using VContainer;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.Infrastructure
{
    public sealed class StatsUIFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _statsPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Button _createButton;

        private IObjectResolver _diContainer;

        [VContainer.Inject]
        private void Constructor(IObjectResolver diContainer)
        {
            _diContainer = diContainer;
        }
        
        private void OnEnable() =>
            _createButton.onClick.AddListener(Create);

        private void OnDisable() =>
            _createButton.onClick.RemoveListener(Create);

        private void Create() =>
            _diContainer.Instantiate(_statsPrefab, _container);
    }
}
