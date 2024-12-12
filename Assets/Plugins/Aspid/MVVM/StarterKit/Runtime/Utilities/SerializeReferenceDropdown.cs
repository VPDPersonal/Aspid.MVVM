#if !ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
using UnityEngine;
using System.Diagnostics;

[Conditional("UNITY_EDITOR")]
public class SerializeReferenceDropdown : PropertyAttribute { }
#endif