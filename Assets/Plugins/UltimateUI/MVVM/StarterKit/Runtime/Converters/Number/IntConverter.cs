using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Converters.Number
{
    [Serializable]
    public sealed class IntConverter
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private int _coefficient;

        public int Convert(int value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}