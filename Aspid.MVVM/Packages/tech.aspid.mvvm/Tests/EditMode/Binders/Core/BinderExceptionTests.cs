using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    [TestFixture]
    internal sealed class BinderExceptionTests : SceneFixture
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void FactoriesReturnExceptionsThatTheCallerCanThrow(int factory)
        {
            var exception = CreateException(factory, new PlainBinder());

            Assert.That(exception.StackTrace, Is.Null);
            Assert.That(Assert.Catch<InvalidCastException>(() => throw exception), Is.SameAs(exception));
            Assert.That(exception.StackTrace, Does.Contain(nameof(FactoriesReturnExceptionsThatTheCallerCanThrow)));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void MessagesSeparateTheFailureAndEverySupportedInterface(int factory)
        {
            var exception = CreateException(factory, new PlainBinder());
            var message = WithoutColors(exception.Message);
            var lines = message.Split('\n');

            Assert.That(lines, Has.Length.EqualTo(2));
            Assert.That(lines[0], Does.EndWith(ExpectedFailure(factory)));
            Assert.That(lines[1], Is.EqualTo("Required: " + string.Join(" or ", ExpectedInterfaces(factory)) + "."));
            Assert.That(message, Does.Contain(nameof(PlainBinder)));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ComponentMessagesIncludeTheHierarchyPath(int factory)
        {
            var root = Spawn("Root");
            var child = Spawn("Child");
            child.transform.SetParent(root.transform, false);
            var binder = child.AddComponent<TextMonoBinder>();

            var exception = CreateException(factory, binder);
            var lines = WithoutColors(exception.Message).Split('\n');

            Assert.That(lines, Has.Length.EqualTo(3));
            Assert.That(lines[0], Is.EqualTo(nameof(TextMonoBinder) + ExpectedFailure(factory)));
            Assert.That(lines[2], Is.EqualTo("Path: Root/Child"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DestroyedComponentsDoNotHideCompatibilityErrors(int factory)
        {
            var binder = Spawn<TextMonoBinder>();
            Destroy(binder);

            var exception = CreateException(factory, binder);

            Assert.That(exception, Is.TypeOf(ExpectedExceptionType(factory)));
            Assert.That(exception.Message, Does.Contain(nameof(TextMonoBinder)));
            Assert.That(exception.Message.Split('\n'), Has.Length.EqualTo(2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void IncompatibleBindingsStillThrowTheExpectedException(int factory)
        {
            IBinderAdder member = factory switch
            {
                0 => new OneTimeBindableMember<string>("value"),
                1 => new OneTimeStructBindableMember<int>(1),
                2 => new OneWayToSourceBindableMember<string>(_ => { }),
                3 => new OneWayToSourceStructBindableMember<int>(_ => { }),
                _ => throw new ArgumentOutOfRangeException(nameof(factory))
            };

            Assert.That(Assert.Catch(() => member.Add(new PlainBinder())), Is.TypeOf(ExpectedExceptionType(factory)));
        }

        [TestCase(typeof(bool), "bool")]
        [TestCase(typeof(byte), "byte")]
        [TestCase(typeof(sbyte), "sbyte")]
        [TestCase(typeof(char), "char")]
        [TestCase(typeof(short), "short")]
        [TestCase(typeof(ushort), "ushort")]
        [TestCase(typeof(int), "int")]
        [TestCase(typeof(uint), "uint")]
        [TestCase(typeof(long), "long")]
        [TestCase(typeof(ulong), "ulong")]
        [TestCase(typeof(float), "float")]
        [TestCase(typeof(double), "double")]
        [TestCase(typeof(decimal), "decimal")]
        [TestCase(typeof(string), "string")]
        [TestCase(typeof(object), "object")]
        [TestCase(typeof(IntPtr), "nint")]
        [TestCase(typeof(UIntPtr), "nuint")]
        [TestCase(typeof(DayOfWeek), "DayOfWeek")]
        [TestCase(typeof(int?), "int?")]
        [TestCase(typeof(int[]), "int[]")]
        [TestCase(typeof(int[,]), "int[,]")]
        [TestCase(typeof(string[][]), "string[][]")]
        [TestCase(typeof(List<string>), "List<string>")]
        [TestCase(typeof(Dictionary<string, List<int?[,]>>[]), "Dictionary<string, List<int?[,]>>[]")]
        [TestCase(typeof(Dictionary<string, int>.KeyCollection), "Dictionary<string, int>.KeyCollection")]
        public void ValueTypeNamesUseReadableCSharpNotation(Type valueType, string expected)
        {
            var factory = typeof(BinderInvalidCastException).GetMethod(nameof(BinderInvalidCastException.Class));
            var exception = (BinderInvalidCastException)factory.MakeGenericMethod(valueType)
                .Invoke(null, new object[] { new PlainBinder() });
            var message = WithoutColors(exception.Message);

            Assert.That(message.Split('\n')[0], Does.EndWith($" cannot receive {expected}."));
            Assert.That(message, Does.Contain($"IBinder<{expected}>"));
        }

        [Test]
        public void GenericBinderNamesIncludeTheirTypeArguments()
        {
            var exception = BinderInvalidCastException.Class<string>(new GenericBinder<int>());
            var message = WithoutColors(exception.Message);

            Assert.That(message.Split('\n')[0],
                Is.EqualTo("BinderExceptionTests.GenericBinder<int> cannot receive string."));
        }

        private static string WithoutColors(string message) =>
            Regex.Replace(message, @"</?color(?:=#[0-9a-fA-F]+)?>", string.Empty);

        private static InvalidCastException CreateException(int factory, IBinder binder) => factory switch
        {
            0 => BinderInvalidCastException.Class<string>(binder),
            1 => BinderInvalidCastException.Struct<int, ValueType>(binder),
            2 => ReverseBinderInvalidCastException<string>.Class(binder),
            3 => ReverseBinderInvalidCastException<int>.Struct<ValueType>(binder),
            _ => throw new ArgumentOutOfRangeException(nameof(factory))
        };

        private static Type ExpectedExceptionType(int factory) => factory switch
        {
            0 or 1 => typeof(BinderInvalidCastException),
            2 => typeof(ReverseBinderInvalidCastException<string>),
            3 => typeof(ReverseBinderInvalidCastException<int>),
            _ => throw new ArgumentOutOfRangeException(nameof(factory))
        };

        private static string[] ExpectedInterfaces(int factory) => factory switch
        {
            0 => new[] { "IBinder<string>", "IAnyBinder" },
            1 => new[] { "IBinder<int>", "IBinder<ValueType>", "IAnyBinder" },
            2 => new[] { "IReverseBinder<string>", "IAnyReverseBinder" },
            3 => new[] { "IReverseBinder<int>", "IReverseBinder<ValueType>", "IAnyReverseBinder" },
            _ => throw new ArgumentOutOfRangeException(nameof(factory))
        };

        private static string ExpectedFailure(int factory) => factory switch
        {
            0 => " cannot receive string.",
            1 => " cannot receive int.",
            2 => " cannot report string to the ViewModel.",
            3 => " cannot report int to the ViewModel.",
            _ => throw new ArgumentOutOfRangeException(nameof(factory))
        };

        private sealed class PlainBinder : Binder
        {
            public PlainBinder()
                : base(BindMode.TwoWay) { }
        }

        private class GenericBinder<T> : Binder { }
    }
}
