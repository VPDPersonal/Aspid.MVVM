using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools.Samples.Types
{
    public class TypeSelectorTest : MonoBehaviour
    {
        [Header("TypeSelector")]
        [TypeSelector]
        [SerializeField] private string _typeSelector;
        
        [TypeSelector(typeof(Enum))]
        [SerializeField] private string _typeSelectorWithFilterEnum;
        
        [Header("SerializableType")]
        [SerializeField] private SerializableType _serializableType;
        [SerializeField] private SerializableType<Enum> _serializableTypeWithEnumFilter;
    }
}