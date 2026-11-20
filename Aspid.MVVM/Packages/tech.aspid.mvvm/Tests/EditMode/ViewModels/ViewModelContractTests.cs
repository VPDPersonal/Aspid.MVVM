using System;
using System.Linq;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    [TestFixture]
    internal sealed class ViewModelContractTests
    {
        [Test]
        public void QueriesKeepTheirBoxingRestriction() =>
            Assert.That(typeof(FindBindableMemberParameters).IsByRefLike, Is.True);

        [Test]
        public void NullQueryIdsAreRejected() =>
            Assert.Throws<ArgumentNullException>(() => new FindBindableMemberParameters(null));

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t\n")]
        public void EmptyQueryIdsAreRejected(string id) =>
            Assert.Throws<ArgumentException>(() => new FindBindableMemberParameters(id));

        [Test]
        public void EveryBuiltInLookupRejectsDefaultQueries()
        {
            IViewModel[] models = { new LookupContractViewModel(), new DynamicViewModel(), new EmptyViewModel() };
            foreach (var model in models)
                Assert.Throws<ArgumentNullException>(() => model.FindBindableMember(default));
        }

        [Test]
        public void GeneratedLookupFindsMembersAndReportsMissingIds()
        {
            var model = new LookupContractViewModel();
            Assert.That(model.FindBindableMember(new FindBindableMemberParameters("Aa")).IsFound, Is.True);
            Assert.That(model.FindBindableMember(new FindBindableMemberParameters("Bb")).IsFound, Is.True);
            Assert.That(model.FindBindableMember(new FindBindableMemberParameters("Missing")).IsFound, Is.False);
        }

        [Test]
        public void GeneratedCommandsKeepTheirMethodAndGeneratedPropertyPredicates()
        {
            var model = new LookupContractViewModel();
            var save = ((IReadOnlyValueBindableMember<RelayCommand<int>>)model.FindBindableMember(
                new FindBindableMemberParameters("SaveCommand")).Adder).Value;
            var confirm = ((IReadOnlyValueBindableMember<RelayCommand>)model.FindBindableMember(
                new FindBindableMemberParameters("ConfirmCommand")).Adder).Value;
            save.Execute(3);
            confirm.Execute();
            Assert.That(model.Calls, Is.Zero);
            model.Allowed = true;
            save.Execute(3);
            confirm.Execute();
            Assert.That(model.Calls, Is.EqualTo(4));
        }

        [Test]
        public void FoundStateAlwaysMatchesTheReturnedMember()
        {
            var member = new OneTimeStructBindableMember<int>(1);
            var result = new FindBindableMemberResult(member);
            Assert.That(result.IsFound, Is.True);
            Assert.That(result.Adder, Is.SameAs(member));
            Assert.That(new FindBindableMemberResult(null).IsFound, Is.False);
            Assert.That(default(FindBindableMemberResult).IsFound, Is.False);
        }

        [Test]
        public void BothDisposalOverloadsRejectNull()
        {
            DisposableModel typed = null;
            IViewModel untyped = typed;
            Assert.That(Assert.Throws<ArgumentNullException>(() => typed.DisposeViewModel()).ParamName, Is.EqualTo("viewModel"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => untyped.DisposeViewModel()).ParamName, Is.EqualTo("viewModel"));
        }

        [Test]
        public void BothDisposalOverloadsCallDisposeOnceAndPreserveExceptions()
        {
            var typed = new DisposableModel();
            var untyped = new DisposableModel();
            typed.DisposeViewModel();
            ((IViewModel)untyped).DisposeViewModel();
            Assert.That(typed.Calls, Is.EqualTo(1));
            Assert.That(untyped.Calls, Is.EqualTo(1));

            typed.Failure = new InvalidOperationException("dispose");
            Assert.That(Assert.Catch(() => typed.DisposeViewModel()), Is.SameAs(typed.Failure));
            Assert.That(Assert.Catch(() => ((IViewModel)typed).DisposeViewModel()), Is.SameAs(typed.Failure));
            Assert.DoesNotThrow(() => ((IViewModel)new EmptyViewModel()).DisposeViewModel());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SafeDisposalLogsExceptions(bool typed)
        {
            var model = new DisposableModel { Failure = new InvalidOperationException("safe disposal") };
            LogAssert.Expect(LogType.Exception, "InvalidOperationException: safe disposal");

            if (typed)
                model.DisposeViewModelSafely();
            else
                ((IViewModel)model).DisposeViewModelSafely();

            Assert.That(model.Calls, Is.EqualTo(1));
        }

        [TestCase(Access.Private)]
        [TestCase(Access.Protected)]
        [TestCase(Access.Public)]
        public void AccessAttributesRetainTheirConstructorValues(Access access)
        {
            var attribute = new AccessAttribute(access);
            Assert.That(attribute.Get, Is.EqualTo(access));
            Assert.That(attribute.Set, Is.EqualTo(access));
        }

        [Test]
        public void AccessAttributesValidateAndApplyOverrides()
        {
            var attribute = new AccessAttribute(Access.Public) { Set = Access.Private };
            Assert.That(attribute.Get, Is.EqualTo(Access.Public));
            Assert.That(attribute.Set, Is.EqualTo(Access.Private));
            Assert.Throws<ArgumentOutOfRangeException>(() => new AccessAttribute((Access)0));
            Assert.Throws<ArgumentOutOfRangeException>(() => attribute.Get = (Access)99);
            Assert.Throws<ArgumentOutOfRangeException>(() => attribute.Set = (Access)99);
        }

        [Test]
        public void BindAttributesRetainAndValidateExplicitModes()
        {
            Assert.That(new BindAttribute().Mode, Is.Null);
            Assert.That(new BindAttribute(BindMode.OneTime).Mode, Is.EqualTo(BindMode.OneTime));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BindAttribute(BindMode.None));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BindAttribute((BindMode)99));
            Assert.Throws<ArgumentNullException>(() => new BindAlsoAttribute(null));
            Assert.Throws<ArgumentException>(() => new BindAlsoAttribute(" "));
        }

        [TestCase(nameof(MetadataModel.Mutable), BindMode.TwoWay)]
        [TestCase(nameof(MetadataModel.Frozen), BindMode.OneTime)]
        [TestCase(nameof(MetadataModel.Explicit), BindMode.OneWay)]
        [TestCase(nameof(MetadataModel.ReadOnly), BindMode.OneWay)]
        [TestCase(nameof(MetadataModel.WriteOnly), BindMode.OneWayToSource)]
        [TestCase(nameof(MetadataModel.Both), BindMode.TwoWay)]
        [TestCase(nameof(MetadataModel.Snapshot), BindMode.OneTime)]
        [TestCase(nameof(MetadataModel.Input), BindMode.OneWayToSource)]
        public void EditorMetadataUsesTheDeclaredOrInferredMode(string name, BindMode expected)
        {
            var member = typeof(MetadataModel).GetMember(name).Single();
            Assert.That(new BindablePropertyMeta(typeof(MetadataModel), member).Mode, Is.EqualTo(expected));
        }

        [Test]
        public void EditorMetadataSkipsUnsupportedBindings()
        {
            var member = typeof(MetadataModel).GetField(nameof(MetadataModel.Unsupported));
            Assert.That(BindablePropertyMeta.IsBindableProperty(typeof(MetadataModel), member), Is.False);
        }

        private sealed class DisposableModel : IViewModel, IDisposable
        {
            public int Calls;
            public Exception Failure;
            public void Dispose() { Calls++; if (Failure is not null) throw Failure; }
            public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
            {
                parameters.EnsureValid();
                return default;
            }
        }

        private sealed class MetadataModel
        {
            [Bind] public int Mutable;
            [Bind] public readonly int Frozen;
            [Bind(BindMode.OneWay)] public int Explicit;
            [Bind] public int ReadOnly => 1;
            [Bind] public int WriteOnly { set { } }
            [TwoWayBind] public int Both { get; set; }
            [OneTimeBind] public int Snapshot { get; set; }
            [OneWayToSourceBind] public int Input { get; set; }
            [OneWayBind] public readonly int Unsupported;
        }
    }

    [ViewModel]
    internal partial class LookupContractViewModel
    {
        [Bind] private int _aa;
        [Bind] private int _bb;
        [Bind] private bool _isAllowed;

        public int Calls { get; private set; }

        public bool Allowed { set => IsAllowed = value; }

        private bool CanSave(int amount) =>
            IsAllowed && amount > 0;

        [RelayCommand(CanExecute = nameof(CanSave))]
        private void Save(int amount) =>
            Calls += amount;

        [RelayCommand(CanExecute = "IsAllowed")]
        private void Confirm() =>
            Calls++;
    }
}
