using System.Collections.Generic;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine.UI;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP2")]
    public sealed class SliderNormalizationTests : SceneFixture
    {
        [TestCase(BindMode.TwoWay, 1)]
        [TestCase(BindMode.OneWay, 0)]
        [TestCase(BindMode.OneTime, 0)]
        public void FractionalBounds_UseAppliedValueAndConvertBack(BindMode mode, int notifications)
        {
            var slider = Spawn<Slider>("Slider");
            slider.minValue = 0.1f;
            slider.maxValue = 4.6f;
            slider.wholeNumbers = true;
            var binder = new SliderValueBinder(slider, NotifyMode.Never, new Scale(), mode);
            var values = new List<float>();
            var member = new TwoWayBindableMember<float>(100f, values.Add);
            binder.Bind(new BindContext(member, binder));
            Assert.AreEqual(5f, slider.value);
            Assert.AreEqual(notifications, values.Count);
            if (notifications > 0) Assert.AreEqual(2.5f, values[0]);
            binder.Unbind(new UnbindContext(binder));
        }

        private sealed class Scale : ITwoWayConverter<float, float>
        {
            public float Convert(float value) => value * 2f;
            public float ConvertBack(float value) => value / 2f;
        }

        [TestCase(false, NotifyMode.Never)]
        [TestCase(false, NotifyMode.Always)]
        [TestCase(false, NotifyMode.AfterBind)]
        [TestCase(true, NotifyMode.Never)]
        [TestCase(true, NotifyMode.Always)]
        [TestCase(true, NotifyMode.AfterBind)]
        public void RealBinding_NormalizesInitialAndLaterValues(bool serializable, NotifyMode notifyMode)
        {
            var slider = Spawn<Slider>("Slider");
            slider.maxValue = 10;
            slider.wholeNumbers = true;
            IBinder binder;
            if (serializable) binder = new SliderValueBinder(slider, notifyMode);
            else
            {
                var mono = slider.gameObject.AddComponent<SliderValueMonoBinder>();
                mono.Mode = BindMode.TwoWay;
                typeof(SliderValueMonoBinder).GetField("_notifyMode", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(mono, notifyMode);
                binder = mono;
            }
            var values = new List<float>();
            var member = new TwoWayBindableMember<float>(2.3f, values.Add);
            try
            {
                binder.Bind(new BindContext(member, binder));
                CollectionAssert.AreEqual(new[] { 2f }, values);
                member.Value = 50f;
                CollectionAssert.AreEqual(new[] { 2f, 10f }, values);
                member.Value = 4f;
                Assert.AreEqual(2, values.Count, "A normal write echoed back.");
                binder.Unbind(new UnbindContext(binder));
                binder.Bind(new BindContext(member, binder));
                Assert.AreEqual(2, values.Count, "Pending normalization leaked into a new binding.");
            }
            finally { binder.Unbind(new UnbindContext(binder)); }
        }
    }
}
