using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Converters.Number
{
    [Serializable]
    public sealed class FloatConverter
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private float _coefficient;

        public float Convert(float value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}