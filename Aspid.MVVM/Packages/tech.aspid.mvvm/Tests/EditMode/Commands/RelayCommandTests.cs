#nullable enable
using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class RelayCommandTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void ExecuteChecksTheCurrentPredicateOnce(int arity)
        {
            var enabled = false;
            var executions = 0;
            var checks = 0;
            var command = Create(arity, () => executions++, () => { checks++; return enabled; });
            command.Execute();
            Assert.That(executions, Is.Zero);
            Assert.That(checks, Is.EqualTo(1));
            enabled = true;
            command.Execute();
            Assert.That(executions, Is.EqualTo(1));
            Assert.That(checks, Is.EqualTo(2));
            enabled = false;
            command.Execute();
            Assert.That(executions, Is.EqualTo(1));
            Assert.That(checks, Is.EqualTo(3));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void MissingPredicateAllowsExecution(int arity)
        {
            var executions = 0;
            var command = Create(arity, () => executions++, null);
            Assert.That(command.CanExecute(), Is.True);
            command.Execute();
            Assert.That(executions, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void NullActionIsRejectedAtConstruction(int arity)
        {
            Assert.That(Assert.Throws<ArgumentNullException>(() => Create(arity, null, null))!.ParamName,
                Is.EqualTo("execute"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void ActionAndPredicateExceptionsPropagateUnchanged(int arity)
        {
            var failure = new InvalidOperationException("callback");
            var action = Create(arity, () => throw failure, null);
            Assert.That(Assert.Throws<InvalidOperationException>(() => action.Execute()), Is.SameAs(failure));
            var predicate = Create(arity, () => { }, () => throw failure);
            Assert.That(Assert.Throws<InvalidOperationException>(() => predicate.CanExecute()), Is.SameAs(failure));
            Assert.That(Assert.Throws<InvalidOperationException>(() => predicate.Execute()), Is.SameAs(failure));
        }

        [TestCase(0, false)]
        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(1, true)]
        [TestCase(2, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(4, true)]
        public void EmptyCommandsHaveStableIdentityAndModes(int arity, bool enabled)
        {
            var first = Empty(arity, enabled);
            var second = Empty(arity, enabled);
            Assert.That(first.Command, Is.SameAs(second.Command));
            Assert.That(first.CanExecute(), Is.EqualTo(enabled));
            Assert.DoesNotThrow(() => first.Execute());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void NotificationUsesTheCommandAsSenderAndPreservesHandlerFailures(int arity)
        {
            var command = Create(arity, () => { }, null);
            object? sender = null;
            command.Subscribe(value => sender = value);
            command.Notify();
            Assert.That(sender, Is.SameAs(command.Command));
            var failure = new InvalidOperationException("subscriber");
            var laterCalls = 0;
            command.Subscribe(_ => throw failure);
            command.Subscribe(_ => laterCalls++);
            Assert.That(Assert.Throws<InvalidOperationException>(() => command.Notify()), Is.SameAs(failure));
            Assert.That(laterCalls, Is.Zero);
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 0)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 0)]
        [TestCase(4, 1)]
        [TestCase(4, 2)]
        public void ActionFactoriesValidateNullOrReturnTheRequestedFallback(int arity, int fallback)
        {
            if (fallback is 0)
            {
                Assert.That(Assert.Throws<ArgumentNullException>(() => FromAction(arity, null, fallback))!.ParamName,
                    Is.EqualTo("execute"));
            }
            else
            {
                Assert.That(FromAction(arity, null, fallback).Command,
                    Is.SameAs(Empty(arity, fallback is 2).Command));
            }

            var executions = 0;
            var command = FromAction(arity, () => executions++, fallback);
            command.Execute();
            Assert.That(executions, Is.EqualTo(1));
        }

        [TestCase(0, false)]
        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(1, true)]
        [TestCase(2, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(4, true)]
        public void GetSelfOrEmptyPreservesCommandsAndSelectsTheRequestedFallback(int arity, bool enabled)
        {
            CheckFallback(arity, enabled);
        }

        private static CommandProbe Create(int arity, Action? execute, Func<bool>? predicate)
        {
            switch (arity)
            {
                case 0: return Probe(new RelayCommand(execute!, predicate));
                case 1: return Probe(new RelayCommand<int>(execute is null ? null! : (a) => execute(), predicate is null ? null : (a) => predicate()));
                case 2: return Probe(new RelayCommand<int, int>(execute is null ? null! : (a, b) => execute(), predicate is null ? null : (a, b) => predicate()));
                case 3: return Probe(new RelayCommand<int, int, int>(execute is null ? null! : (a, b, c) => execute(), predicate is null ? null : (a, b, c) => predicate()));
                case 4: return Probe(new RelayCommand<int, int, int, int>(execute is null ? null! : (a, b, c, d) => execute(), predicate is null ? null : (a, b, c, d) => predicate()));
                default: throw new ArgumentOutOfRangeException(nameof(arity));
            }
        }

        private static CommandProbe Empty(int arity, bool enabled) => arity switch
        {
            0 => Probe(enabled ? RelayCommand.EmptyExecution : RelayCommand.Empty),
            1 => Probe(enabled ? RelayCommand<int>.EmptyExecution : RelayCommand<int>.Empty),
            2 => Probe(enabled ? RelayCommand<int, int>.EmptyExecution : RelayCommand<int, int>.Empty),
            3 => Probe(enabled ? RelayCommand<int, int, int>.EmptyExecution : RelayCommand<int, int, int>.Empty),
            4 => Probe(enabled ? RelayCommand<int, int, int, int>.EmptyExecution : RelayCommand<int, int, int, int>.Empty),
            _ => throw new ArgumentOutOfRangeException(nameof(arity)),
        };

        private static CommandProbe FromAction(int arity, Action? execute, int fallback)
        {
            switch (arity)
            {
                case 0:
                {
                    Action? action = execute;
                    return Probe(fallback switch
                    {
                        0 => action!.CreateCommand(),
                        1 => action.CreateCommandOrEmpty(),
                        _ => action.CreateCommandOrEmptyExecution(),
                    });
                }
                case 1:
                {
                    Action<int>? action = execute is null ? null : (a) => execute();
                    return Probe(fallback switch
                    {
                        0 => action!.CreateCommand(),
                        1 => action.CreateCommandOrEmpty(),
                        _ => action.CreateCommandOrEmptyExecution(),
                    });
                }
                case 2:
                {
                    Action<int, int>? action = execute is null ? null : (a, b) => execute();
                    return Probe(fallback switch
                    {
                        0 => action!.CreateCommand(),
                        1 => action.CreateCommandOrEmpty(),
                        _ => action.CreateCommandOrEmptyExecution(),
                    });
                }
                case 3:
                {
                    Action<int, int, int>? action = execute is null ? null : (a, b, c) => execute();
                    return Probe(fallback switch
                    {
                        0 => action!.CreateCommand(),
                        1 => action.CreateCommandOrEmpty(),
                        _ => action.CreateCommandOrEmptyExecution(),
                    });
                }
                case 4:
                {
                    Action<int, int, int, int>? action = execute is null ? null : (a, b, c, d) => execute();
                    return Probe(fallback switch
                    {
                        0 => action!.CreateCommand(),
                        1 => action.CreateCommandOrEmpty(),
                        _ => action.CreateCommandOrEmptyExecution(),
                    });
                }
                default: throw new ArgumentOutOfRangeException(nameof(arity));
            }
        }

        private static void CheckFallback(int arity, bool enabled)
        {
            switch (arity)
            {
                case 0:
                {
                    IRelayCommand? missing = null;
                    IRelayCommand original = new RelayCommand(() => { });
                    Assert.That(enabled ? original.GetSelfOrEmptyExecution() : original.GetSelfOrEmpty(),
                        Is.SameAs(original));
                    Assert.That(enabled ? missing.GetSelfOrEmptyExecution() : missing.GetSelfOrEmpty(),
                        Is.SameAs(Empty(arity, enabled).Command));
                    break;
                }
                case 1:
                {
                    IRelayCommand<int>? missing = null;
                    IRelayCommand<int> original = new RelayCommand<int>((a) => { });
                    Assert.That(enabled ? original.GetSelfOrEmptyExecution() : original.GetSelfOrEmpty(),
                        Is.SameAs(original));
                    Assert.That(enabled ? missing.GetSelfOrEmptyExecution() : missing.GetSelfOrEmpty(),
                        Is.SameAs(Empty(arity, enabled).Command));
                    break;
                }
                case 2:
                {
                    IRelayCommand<int, int>? missing = null;
                    IRelayCommand<int, int> original = new RelayCommand<int, int>((a, b) => { });
                    Assert.That(enabled ? original.GetSelfOrEmptyExecution() : original.GetSelfOrEmpty(),
                        Is.SameAs(original));
                    Assert.That(enabled ? missing.GetSelfOrEmptyExecution() : missing.GetSelfOrEmpty(),
                        Is.SameAs(Empty(arity, enabled).Command));
                    break;
                }
                case 3:
                {
                    IRelayCommand<int, int, int>? missing = null;
                    IRelayCommand<int, int, int> original = new RelayCommand<int, int, int>((a, b, c) => { });
                    Assert.That(enabled ? original.GetSelfOrEmptyExecution() : original.GetSelfOrEmpty(),
                        Is.SameAs(original));
                    Assert.That(enabled ? missing.GetSelfOrEmptyExecution() : missing.GetSelfOrEmpty(),
                        Is.SameAs(Empty(arity, enabled).Command));
                    break;
                }
                case 4:
                {
                    IRelayCommand<int, int, int, int>? missing = null;
                    IRelayCommand<int, int, int, int> original = new RelayCommand<int, int, int, int>((a, b, c, d) => { });
                    Assert.That(enabled ? original.GetSelfOrEmptyExecution() : original.GetSelfOrEmpty(),
                        Is.SameAs(original));
                    Assert.That(enabled ? missing.GetSelfOrEmptyExecution() : missing.GetSelfOrEmpty(),
                        Is.SameAs(Empty(arity, enabled).Command));
                    break;
                }
            }
        }

        private static CommandProbe Probe(IRelayCommand command) =>
            new()
            {
                Command = command,
                CanExecute = () => command.CanExecute(),
                Execute = () => command.Execute(),
                Notify = command.NotifyCanExecuteChanged,
                Subscribe = callback => command.CanExecuteChanged += source => callback(source),
            };

        private static CommandProbe Probe(IRelayCommand<int> command) =>
            new()
            {
                Command = command,
                CanExecute = () => command.CanExecute(1),
                Execute = () => command.Execute(1),
                Notify = command.NotifyCanExecuteChanged,
                Subscribe = callback => command.CanExecuteChanged += source => callback(source),
            };

        private static CommandProbe Probe(IRelayCommand<int, int> command) =>
            new()
            {
                Command = command,
                CanExecute = () => command.CanExecute(1, 2),
                Execute = () => command.Execute(1, 2),
                Notify = command.NotifyCanExecuteChanged,
                Subscribe = callback => command.CanExecuteChanged += source => callback(source),
            };

        private static CommandProbe Probe(IRelayCommand<int, int, int> command) =>
            new()
            {
                Command = command,
                CanExecute = () => command.CanExecute(1, 2, 3),
                Execute = () => command.Execute(1, 2, 3),
                Notify = command.NotifyCanExecuteChanged,
                Subscribe = callback => command.CanExecuteChanged += source => callback(source),
            };

        private static CommandProbe Probe(IRelayCommand<int, int, int, int> command) =>
            new()
            {
                Command = command,
                CanExecute = () => command.CanExecute(1, 2, 3, 4),
                Execute = () => command.Execute(1, 2, 3, 4),
                Notify = command.NotifyCanExecuteChanged,
                Subscribe = callback => command.CanExecuteChanged += source => callback(source),
            };

        private sealed class CommandProbe
        {
            public object Command = null!;
            public Func<bool> CanExecute = null!;
            public Action Execute = null!;
            public Action Notify = null!;
            public Action<Action<object>> Subscribe = null!;
        }
    }
}
