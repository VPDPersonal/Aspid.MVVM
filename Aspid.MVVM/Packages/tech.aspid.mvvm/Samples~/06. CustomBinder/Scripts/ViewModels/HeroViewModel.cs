using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.CustomBinder
{
    [ViewModel]
    [Serializable]
    public sealed partial class HeroViewModel
    {
        [OneWayBind]
        [SerializeField] [Range(0f, 1f)] private float _health = 1f;

        [RelayCommand]
        private void Hit() =>
            Health = Mathf.Max(0f, Health - 0.15f);

        [RelayCommand]
        private void Heal() =>
            Health = Mathf.Min(1f, Health + 0.25f);
    }
}
