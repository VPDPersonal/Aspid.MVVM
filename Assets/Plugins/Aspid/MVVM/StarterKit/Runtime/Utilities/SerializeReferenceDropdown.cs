#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
using UnityEngine;
using System.Diagnostics;

[Conditional("UNITY_EDITOR")]
public class SerializeReferenceDropdownAttribute : PropertyAttribute { }
#endif