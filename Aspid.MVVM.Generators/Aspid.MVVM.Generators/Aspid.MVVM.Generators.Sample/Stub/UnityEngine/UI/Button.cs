using UnityEngine.Events;

namespace UnityEngine.UI;

public class Button : Component
{
    public UnityEvent onClick { get; private set; }
}