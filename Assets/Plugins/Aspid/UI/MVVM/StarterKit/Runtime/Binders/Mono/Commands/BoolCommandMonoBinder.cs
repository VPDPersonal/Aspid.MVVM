using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands.Adapters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(MonoCommandAdapter<bool>))]
    public sealed class BoolCommandMonoBinder : CommandMonoBinder<bool> { }
}