using System.Collections;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP2")]
    public sealed class SliderInspectorNormalizationTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator InspectorWholeNumbers_IsAppliedToTheNextBoundValue()
        {
            var slider = Spawn<Slider>("Slider");
            slider.maxValue = 10f;
            var binder = slider.gameObject.AddComponent<SliderValueMonoBinder>();
            binder.Mode = BindMode.TwoWay;
            var received = new List<float>();
            var member = new TwoWayBindableMember<float>(2.3f, received.Add);
            binder.Bind(new BindContext(member, binder));
#if UNITY_EDITOR
            var serialized = new UnityEditor.SerializedObject(slider);
            serialized.FindProperty("m_WholeNumbers").boolValue = true;
            serialized.ApplyModifiedPropertiesWithoutUndo();
#else
            slider.wholeNumbers = true;
#endif
            yield return null;
            received.Clear();
            member.Value = 3.4f;
            yield return null;
            Assert.AreEqual(3f, slider.value);
            CollectionAssert.AreEqual(new[] { 3f }, received);
        }
    }
}
