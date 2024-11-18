using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands.Adapters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(MonoCommandAdapter<Vector2>))]
    public sealed class Vector2CommandMonoBinder : CommandMonoBinder<Vector2> { }
}