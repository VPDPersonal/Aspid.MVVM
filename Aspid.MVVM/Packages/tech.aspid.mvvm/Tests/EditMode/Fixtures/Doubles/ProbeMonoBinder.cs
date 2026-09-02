using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Records a <see cref="MonoBinder"/>'s lifecycle hook calls in order, and lets a test force <see cref="CanBind"/>
    /// and the mode <c>Reset</c> applies.
    /// </summary>
    internal sealed class ProbeMonoBinder : MonoBinder, IBinder<bool>
    {
        public readonly List<string> Calls = new();

        public bool ForcedCanBind { get; set; } = true;

        public BindMode ForcedDefaultMode { get; set; } = BindMode.OneWay;

        public override bool CanBind => ForcedCanBind;

        protected override BindMode DefaultMode => ForcedDefaultMode;

        public void SetValue(bool value) { }

        public void InvokeReset() => Reset();

        protected override void OnBinding() => Calls.Add(nameof(OnBinding));

        protected override void OnBound() => Calls.Add(nameof(OnBound));

        protected override void OnUnbinding() => Calls.Add(nameof(OnUnbinding));

        protected override void OnUnbound() => Calls.Add(nameof(OnUnbound));
    }
}
