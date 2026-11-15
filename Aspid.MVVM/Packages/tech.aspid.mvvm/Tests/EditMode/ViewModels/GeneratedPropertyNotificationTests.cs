using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class GeneratedPropertyNotificationTests
    {
        [Test]
        public void PartialSettersAssignNotifyDependenciesAndSkipEqualValues()
        {
            var model = new NotificationTestViewModel();
            var changes = 0;
            var doubled = 0;
            model.ImplicitBindable.Changed += _ => changes++;
            model.DoubledBindable.Changed += value => doubled = value;
            model.Implicit = 7;
            model.Implicit = 7;
            Assert.That(model.Implicit, Is.EqualTo(7));
            Assert.That(changes, Is.EqualTo(1));
            Assert.That(doubled, Is.EqualTo(14));
        }

        [Test]
        public void DirectHelperInAnotherPartialPreservesHookAndNotificationOrder()
        {
            var model = new NotificationTestViewModel();
            model.DirectBindable.Changed += value => model.Trace.Add($"notify:{model.Direct}:{value}");
            model.Direct = 4;
            model.Direct = 4;
            Assert.That(model.Trace, Is.EqualTo(new[]
            {
                "changing:0:4", "changing-pair:0:4", "notify:4:4", "changed:4:4", "changed-pair:0:4"
            }));
        }

        [Test]
        public void SameLineExplicitNamesNotifyOnlyTheSelectedProperty()
        {
            var model = new NotificationTestViewModel();
            var a = 0;
            var b = 0;
            model.NamedABindable.Changed += _ => a++;
            model.NamedBBindable.Changed += _ => b++;
            model.NamedB = 9;
            Assert.That(model.NamedA, Is.Zero);
            Assert.That(model.NamedB, Is.EqualTo(9));
            Assert.That(a, Is.Zero);
            Assert.That(b, Is.EqualTo(1));
        }

        [Test]
        public void ImplicitAndExplicitNotificationsWorkAcrossPartials()
        {
            var model = new NotificationTestViewModel();
            var manual = 0;
            var direct = 0;
            model.ManualBindable.Changed += value => manual = value;
            model.DirectBindable.Changed += _ => direct++;
            model.Manual = 8;
            model.Notify(nameof(model.Direct));
            Assert.That(manual, Is.EqualTo(8));
            Assert.That(direct, Is.EqualTo(1));
            Assert.Throws<ArgumentException>(() => model.Notify("Missing"));
        }

        [Test]
        public void SourceOnlyHelperAssignsWithoutForwardNotification()
        {
            var model = new NotificationTestViewModel();
            model.SourceOnly = 3;
            Assert.That(model.SourceOnly, Is.EqualTo(3));
        }

        [Test]
        public void WarmImplicitSetterDoesNotAllocate()
        {
            var model = new NotificationTestViewModel();
            var changes = 0;
            model.ImplicitBindable.Changed += _ => changes++;
            for (var i = 0; i < 100; i++) model.Implicit = i;
            var before = GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < 1000; i++) model.Implicit = i;
            var allocated = GC.GetAllocatedBytesForCurrentThread() - before;
            Assert.That(allocated, Is.Zero);
            Assert.That(changes, Is.EqualTo(1099));
        }
    }
}
