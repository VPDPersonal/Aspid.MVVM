using UnityEngine.Events;

namespace UnityEngine.UI;

public class Slider : Component
{
    public UnityEvent<float> onValueChanged { get; private set; }
}