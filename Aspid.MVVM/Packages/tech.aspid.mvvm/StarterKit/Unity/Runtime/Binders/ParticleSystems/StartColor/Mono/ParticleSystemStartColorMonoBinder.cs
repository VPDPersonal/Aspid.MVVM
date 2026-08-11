using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{ParticleSystem}"/> that binds
    /// <see cref="ParticleSystem.MainModule.startColor"/>.
    /// </summary>
    /// <remarks>
    /// The colour an effect is emitted in — team colour on a trail, rarity on a pickup, damage type on a hit. The
    /// alternative is a material per colour, and a material per colour is a draw call per colour.
    /// <para/>
    /// Only particles emitted after the write take the new colour: the ones already alive keep the colour they
    /// started with. Writing a <see cref="Color"/> replaces whatever the module held, so a start colour authored as
    /// a gradient or a random range is collapsed to a single colour by the first bound value.
    /// </remarks>
    [AddBinderContextMenu(typeof(ParticleSystem), serializePropertyNames: "startColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Start Color")]
    public class ParticleSystemStartColorMonoBinder : ComponentColorMonoBinder<ParticleSystem>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.main.startColor.color;
            set
            {
                // Модуль — структура-обёртка над самой системой: запись через локальную копию доходит до системы,
                // а обратиться к свойству модуля напрямую язык не даёт (main — свойство, а не поле).
                var main = CachedComponent.main;
                main.startColor = value;
            }
        }
    }
}
