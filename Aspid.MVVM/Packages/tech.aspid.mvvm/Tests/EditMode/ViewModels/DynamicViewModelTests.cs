using System;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for runtime-composed <see cref="DynamicViewModel"/> instances.
    /// </summary>
    [TestFixture]
    public sealed class DynamicViewModelTests
    {
        [Test]
        public void CollectionInitializer_AddsTypedProperties()
        {
            var viewModel = new DynamicViewModel
            {
                { "Title", "Hello" },
                { "Volume", 0.5f, BindMode.TwoWay }
            };

            Assert.AreEqual(2, viewModel.Count);
            Assert.AreEqual("Hello", viewModel.Get<string>("Title").Value);
            Assert.AreEqual(BindMode.TwoWay, viewModel.Get<float>("Volume").Mode);
        }

        [Test]
        public void Add_ReturnsHandleThatUpdatesOneWayBinder()
        {
            var viewModel = new DynamicViewModel();
            var property = viewModel.Add("Health", 100);
            var received = new List<int>();
            var binder = new DelegateOneWayBinder<int>(received.Add);
            binder.Bind(property.GetAdder());

            property.Value = 75;

            Assert.AreEqual(new[] { 100, 75 }, received);
            Assert.AreEqual(75, viewModel.Get<int>("Health").Value);
        }

        [Test]
        public void Value_SetThroughGet_UpdatesHandleAndRaisesValueChanged()
        {
            var viewModel = new DynamicViewModel();
            var property = viewModel.Add("Score", 1);
            var received = new List<int>();
            property.ValueChanged += received.Add;

            viewModel.Get<int>("Score").Value = 2;
            viewModel.Get<int>("Score").Value = 2;

            Assert.AreEqual(2, property.Value);
            Assert.AreEqual(new[] { 2 }, received);
        }

        [Test]
        public void TwoWayProperty_ReceivesViewValueAndUpdatesOtherBinders()
        {
            var property = new DynamicProperty<int>("Count", 1, BindMode.TwoWay);
            Action<int>? publishFromView = null;
            var receivedByView = new List<int>();
            var binder = new DelegateTwoWayBinder<int>(
                subscribe: callback => publishFromView = callback,
                setValue: receivedByView.Add);
            binder.Bind(property.GetAdder());

            publishFromView!.Invoke(4);

            Assert.AreEqual(4, property.Value);
            Assert.AreEqual(new[] { 1, 4 }, receivedByView);
        }

        [Test]
        public void OneTimeProperty_UsesCurrentValueForEachNewBinding()
        {
            var property = new DynamicProperty<int>("Version", 1, BindMode.OneTime);
            var firstValues = new List<int>();
            var firstBinder = new DelegateOneWayBinder<int>(firstValues.Add, BindMode.OneTime);
            firstBinder.Bind(property.GetAdder());

            property.Value = 2;

            var secondValues = new List<int>();
            var secondBinder = new DelegateOneWayBinder<int>(secondValues.Add, BindMode.OneTime);
            secondBinder.Bind(property.GetAdder());

            Assert.AreEqual(new[] { 1 }, firstValues);
            Assert.AreEqual(new[] { 2 }, secondValues);
        }

        [Test]
        public void UntypedValue_RejectsWrongRuntimeType()
        {
            IDynamicProperty property = new DynamicProperty<int>("Count", 1);

            var exception = Assert.Throws<ArgumentException>(() => property.UntypedValue = "wrong");

            StringAssert.Contains(typeof(int).FullName, exception!.Message);
            StringAssert.Contains(typeof(string).FullName, exception.Message);
        }

        [Test]
        public void Get_WithWrongType_ExplainsExpectedAndActualTypes()
        {
            var viewModel = new DynamicViewModel
            {
                { "Count", 1 }
            };

            var exception = Assert.Throws<ArgumentException>(() => viewModel.Get<string>("Count"));

            StringAssert.Contains(typeof(int).FullName, exception!.Message);
            StringAssert.Contains(typeof(string).FullName, exception.Message);
        }

        [Test]
        public void TryGet_DistinguishesMatchingMissingAndWrongType()
        {
            var viewModel = new DynamicViewModel
            {
                { "Count", 1 }
            };

            Assert.IsTrue(viewModel.TryGet<int>("Count", out var property));
            Assert.AreEqual(1, property!.Value);
            Assert.IsFalse(viewModel.TryGet<int>("Missing", out _));
            Assert.IsFalse(viewModel.TryGet<string>("Count", out _));
        }

        [Test]
        public void Add_DuplicateId_Throws()
        {
            var viewModel = new DynamicViewModel
            {
                { "Count", 1 }
            };

            Assert.Throws<ArgumentException>(() => viewModel.Add("Count", 2));
        }

        [Test]
        public void FindBindableMember_MissingId_UsesConfiguredPolicy()
        {
            var permissive = new DynamicViewModel();
            var strict = new DynamicViewModel(throwOnMissingMember: true);
            var parameters = new FindBindableMemberParameters("Missing");

            Assert.IsFalse(permissive.FindBindableMember(parameters).IsFound);
            Assert.Throws<KeyNotFoundException>(
                () => strict.FindBindableMember(new FindBindableMemberParameters("Missing")));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Add_InvalidId_Throws(string id)
        {
            var viewModel = new DynamicViewModel();

            Assert.Throws<ArgumentException>(() => viewModel.Add(id, 1));
        }

        [Test]
        public void IdentifierComparer_IsConfigurable()
        {
            var viewModel = new DynamicViewModel(idComparer: StringComparer.OrdinalIgnoreCase)
            {
                { "Title", "Hello" }
            };

            Assert.AreEqual("Hello", viewModel.Get<string>("title").Value);
        }
    }
}
