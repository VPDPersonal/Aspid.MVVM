using UnityEngine.Events;

namespace UnityEngine.UI;

public class Toggle : Component
{
    public UnityEvent<bool> onValueChanged { get; private set; }
}