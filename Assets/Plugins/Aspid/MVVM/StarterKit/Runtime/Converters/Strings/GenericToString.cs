using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom, string>
    {
        [SerializeField] private string _format;
        
        public GenericToString() { }
        
        public GenericToString(string format)
        {
            _format = format;
        }

        public string Convert(TFrom value) =>
	        string.IsNullOrEmpty(_format) ? value?.ToString() : string.Format(_format, value);
    }
}