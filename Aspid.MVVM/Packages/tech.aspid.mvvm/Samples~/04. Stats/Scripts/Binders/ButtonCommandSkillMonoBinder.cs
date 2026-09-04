using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Stats
{
    // The StarterKit ships the generic ButtonCommandMonoBinder<T>; closing it over a project enum is one line.
    // The Skill to pass is picked in the Inspector on each button.
    [AddComponentMenu("Aspid/MVVM/Binders/Samples/Button Binder – Skill Command")]
    public sealed class ButtonCommandSkillMonoBinder : ButtonCommandMonoBinder<Skill> { }
}
