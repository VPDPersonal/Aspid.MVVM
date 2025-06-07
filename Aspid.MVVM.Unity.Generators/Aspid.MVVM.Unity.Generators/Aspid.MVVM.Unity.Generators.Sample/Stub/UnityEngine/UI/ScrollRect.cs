using System.Numerics;
using UnityEngine.Events;

namespace UnityEngine.UI;

public class ScrollRect : Component
{
    public UnityEvent<Vector2> onValueChanged { get; private set; }
}