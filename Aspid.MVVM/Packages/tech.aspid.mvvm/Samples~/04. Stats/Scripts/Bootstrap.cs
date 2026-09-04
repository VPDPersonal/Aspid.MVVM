using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Stats
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _pointsAvailable = 5;

        [Header("Views")]
        [SerializeField] private StatsView _editView;
        [SerializeField] private StatsView _committedView;

        private void Awake()
        {
            // One model, two ViewModels: the second one has no buttons, so it always shows the committed values.
            var hero = new Hero(_pointsAvailable);

            _editView.Initialize(new StatsViewModel(hero));
            _committedView.Initialize(new StatsViewModel(hero));
        }

        private void OnDestroy()
        {
            _editView.DeinitializeView()?.DisposeViewModel();
            _committedView.DeinitializeView()?.DisposeViewModel();
        }
    }
}
