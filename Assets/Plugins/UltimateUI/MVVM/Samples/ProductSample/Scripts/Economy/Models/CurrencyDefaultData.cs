using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.Models
{
    [Serializable]
    public struct CurrencyDefaultData
    {
        [field: SerializeField]
        public int DefaultCount { get; private set; }
        
        [field: SerializeField] 
        public CurrencyType Type { get; private set; }

        public CurrencyDefaultData(CurrencyType type, int defaultCount)
        {
            Type = type;
            DefaultCount = defaultCount;
        }
    }
}