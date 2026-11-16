using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal partial class NestedGenerationTests
    {
        [Test]
        public void PrivateNestedModelAssignsAndNotifies()
        {
            var model = new PrivateModel();
            var changes = 0;
            model.CountBindable.Changed += _ => changes++;
            model.Write(7);
            model.Write(7);
            Assert.That(model.Read(), Is.EqualTo(7));
            Assert.That(changes, Is.EqualTo(1));
            Assert.That(((IViewModel)model).FindBindableMember(new("Count")).Adder,
                Is.SameAs(model.CountBindable));
        }

        [Test]
        public void GenericContainerPreservesTypedBindingAndCallerLineNumber()
        {
            var model = new Host<string>.Model();
            var changes = 0;
            model.ValueBindable.Changed += _ => changes++;
            model.Value = "updated";
            model.Value = "updated";
            Assert.That(model.Value, Is.EqualTo("updated"));
            Assert.That(changes, Is.EqualTo(1));
        }

        [Test]
        public void NestedTypedViewBindsUpdatesAndReleasesItsBinder()
        {
            var model = new Host<string>.Model { Value = "initial" };
            var view = new Host<string>.Panel();
            view.Initialize(model);
            Assert.That(view.Value.Last, Is.EqualTo("initial"));
            Assert.That(view.Value.IsBound, Is.True);
            model.Value = "updated";
            Assert.That(view.Value.Last, Is.EqualTo("updated"));
            view.Deinitialize();
            Assert.That(view.Value.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
            model.Value = "detached";
            Assert.That(view.Value.Last, Is.EqualTo("updated"));
        }

        [Test]
        public void EqualLeafNamesInDifferentContainersKeepIndependentMembers()
        {
            var first = new Host<string>.Model { Value = "first" };
            var second = new OtherHost.Model();
            second.Write(42);
            Assert.That(first.Value, Is.EqualTo("first"));
            Assert.That(second.Read(), Is.EqualTo(42));
        }

        [Test]
        public void NestedBinderLogForwardsTheTypedValue()
        {
            var binder = new Host<string>.ValueBinder();
            ((IBinder<string>)binder).SetValue("logged");
            Assert.That(binder.Last, Is.EqualTo("logged"));
        }

        [ViewModel]
        private partial class PrivateModel
        {
            [Bind] private int _count;

            public int Read() =>
                Count;

            public void Write(int value) =>
                Count = value;
        }

        internal partial class Host<T> where T : class
        {
            [ViewModel]
            internal partial class Model
            {
                private T _value;

                [Bind]
                public T Value
                {
                    get => _value;
                    set => SetField(ref _value, value);
                }
            }

            [View(AutoBinderFields = false)]
            internal partial class Panel : IView<Model>
            {
                public readonly ValueBinder Value = new ValueBinder();
            }

            internal partial class ValueBinder : Binder, IBinder<T>
            {
                public T Last { get; private set; }

                [BinderLog]
                public void SetValue(T value) =>
                    Last = value;
            }
        }

        internal partial class OtherHost
        {
            [ViewModel]
            internal partial class Model
            {
                [Bind] private int _value;

                public int Read() =>
                    Value;

                public void Write(int value) =>
                    Value = value;
            }
        }
    }
}
