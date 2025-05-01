using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;

[View]
public partial class ColorView : MonoView
{
    [RequireBinder(typeof(Color))]
    [SerializeField] private MonoBinder[] _baseColor;
    
    [RequireBinder(typeof(Color))]
    [SerializeField] private MonoBinder[] _additionalColor;
}
