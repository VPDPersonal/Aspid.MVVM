using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="BoolLogicConverter"/> — every <see cref="LogicOperation"/> and its
    /// failure modes.
    /// </summary>
    [TestFixture]
    public sealed class BoolLogicConverterTests
    {
        [TestCase(LogicOperation.And, true, true, true)]
        [TestCase(LogicOperation.And, true, false, false)]
        [TestCase(LogicOperation.Or, false, true, true)]
        [TestCase(LogicOperation.Or, false, false, false)]
        [TestCase(LogicOperation.Xor, true, true, false)]
        [TestCase(LogicOperation.Xor, true, false, true)]
        [TestCase(LogicOperation.Nand, true, true, false)]
        [TestCase(LogicOperation.Nor, false, false, true)]
        [TestCase(LogicOperation.Xnor, true, true, true)]
        public void Convert_CombinesWithTheOperand(
            LogicOperation operation,
            bool value,
            bool operand,
            bool expected) =>
            Assert.AreEqual(expected, new BoolLogicConverter(operation, operand).Convert(value));

        // BoolLogicConverter joined the failure-mode family with ReturnInput as its authored
        // default, so an irreversible operation still passes the combined value back unchanged
        // out of the box.
        [Test]
        public void ConvertBack_IrreversibleByDefault_ReturnsTheInputUnchanged()
        {
            var converter = new BoolLogicConverter(LogicOperation.Or, operand: true);

            LogAssert.Expect(LogType.Error, new Regex("BoolLogicConverter.*Returning the input unchanged"));

            Assert.IsTrue(converter.ConvertBack(true));
        }

        // An undeclared operation — corrupted YAML or a stray cast — answers through the same
        // mode instead of throwing unconditionally.
        [Test]
        public void Convert_UndeclaredOperation_AnswersThroughTheMode()
        {
            var converter = new BoolLogicConverter((LogicOperation)999, operand: false);

            LogAssert.Expect(LogType.Error, new Regex("BoolLogicConverter.*not a declared LogicOperation"));

            Assert.IsTrue(converter.Convert(true), "ReturnInput passes the bound value through");
        }
    }
}
