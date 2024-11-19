using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands.Adapters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(MonoCommandAdapter<int>))]
    public sealed class IntCommandMonoBinder : CommandMonoBinder<int> { }
}