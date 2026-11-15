#nullable enable
using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class BoundRelayCommandTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void AdapterForwardsArgumentsWithoutAddingAnExecutionCheck(int arity)
        {
            var (source, adapter) = Create(arity);
            source.Enabled = false;
            adapter.Execute();
            Assert.That(source.ExecuteCalls, Is.EqualTo(1));
            Assert.That(source.CanExecuteCalls, Is.Zero);
            Assert.That(source.LastArguments, Is.EqualTo(ExpectedArguments(arity)));
            Assert.That(adapter.CanExecute(), Is.False);
            source.Enabled = true;
            Assert.That(adapter.CanExecute(), Is.True);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void NativeRelayCommandPredicateIsEvaluatedOnce(int arity)
        {
            var checks = 0;
            var executions = 0;
            var adapter = Native(arity, () => executions++, () => { checks++; return true; });
            adapter.Execute();
            Assert.That(checks, Is.EqualTo(1));
            Assert.That(executions, Is.EqualTo(1));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void NotificationsUseTheAdapterAsSenderAndSubscriptionsFollowListeners(int arity)
        {
            var (source, adapter) = Create(arity);
            Assert.That(source.ListenerCount, Is.Zero);
            IRelayCommand? sender = null;
            var calls = 0;
            Action<IRelayCommand> first = value => { sender = value; calls++; };
            Action<IRelayCommand> second = _ => calls++;
            adapter.CanExecuteChanged += first;
            adapter.CanExecuteChanged += second;
            Assert.That(source.AddCalls, Is.EqualTo(1));
            Assert.That(source.ListenerCount, Is.EqualTo(1));
            source.NotifyCanExecuteChanged();
            Assert.That(sender, Is.SameAs(adapter));
            Assert.That(calls, Is.EqualTo(2));
            adapter.CanExecuteChanged -= first;
            Assert.That(source.ListenerCount, Is.EqualTo(1));
            adapter.CanExecuteChanged -= second;
            Assert.That(source.ListenerCount, Is.Zero);
            source.NotifyCanExecuteChanged();
            Assert.That(calls, Is.EqualTo(2));
            adapter.CanExecuteChanged += first;
            adapter.NotifyCanExecuteChanged();
            Assert.That(calls, Is.EqualTo(3));
            adapter.CanExecuteChanged -= first;
            Assert.That(source.ListenerCount, Is.Zero);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void DuplicateAndNullHandlersFollowNormalEventSemantics(int arity)
        {
            var (source, adapter) = Create(arity);
            adapter.CanExecuteChanged += null;
            Assert.That(source.AddCalls, Is.Zero);
            var calls = 0;
            Action<IRelayCommand> handler = _ => calls++;
            adapter.CanExecuteChanged += handler;
            adapter.CanExecuteChanged += handler;
            source.NotifyCanExecuteChanged();
            Assert.That(calls, Is.EqualTo(2));
            adapter.CanExecuteChanged -= handler;
            source.NotifyCanExecuteChanged();
            Assert.That(calls, Is.EqualTo(3));
            adapter.CanExecuteChanged -= handler;
            adapter.CanExecuteChanged -= handler;
            Assert.That(source.ListenerCount, Is.Zero);
            Assert.That(source.RemoveCalls, Is.EqualTo(1));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void FailedSubscriptionRollsBackAndCanRetry(int arity)
        {
            var (source, adapter) = Create(arity);
            source.FailAdd = true;
            source.FailRemove = true;
            Action<IRelayCommand> handler = _ => { };
            Assert.That(Assert.Throws<InvalidOperationException>(() => adapter.CanExecuteChanged += handler)!.Message,
                Is.EqualTo("subscribe"));
            Assert.That(source.ListenerCount, Is.Zero);
            source.FailAdd = source.FailRemove = false;
            adapter.CanExecuteChanged += handler;
            Assert.That(source.ListenerCount, Is.EqualTo(1));
            adapter.CanExecuteChanged -= handler;
            Assert.That(source.ListenerCount, Is.Zero);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void FailedUnsubscriptionCanBeRetried(int arity)
        {
            var (source, adapter) = Create(arity);
            Action<IRelayCommand> handler = _ => { };
            adapter.CanExecuteChanged += handler;
            source.FailRemove = true;
            Assert.Throws<InvalidOperationException>(() => adapter.CanExecuteChanged -= handler);
            source.FailRemove = false;
            adapter.CanExecuteChanged -= handler;
            Assert.That(source.ListenerCount, Is.Zero);
            adapter.CanExecuteChanged += handler;
            Assert.That(source.ListenerCount, Is.EqualTo(1));
            adapter.CanExecuteChanged -= handler;
        }

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
        public void FactoriesRejectNullOrChooseTheRequestedFallback(int arity, int fallback)
        {
            if (fallback is 0)
                Assert.That(Assert.Throws<ArgumentNullException>(() => Missing(arity, fallback))!.ParamName,
                    Is.EqualTo("command"));
            else
                Assert.That(Missing(arity, fallback),
                    Is.SameAs(fallback is 1 ? RelayCommand.Empty : RelayCommand.EmptyExecution));

            var (source, adapter) = Create(arity, fallback);
            var calls = 0;
            Action<IRelayCommand> handler = _ => calls++;
            adapter.CanExecuteChanged += handler;
            source.NotifyCanExecuteChanged();
            Assert.That(calls, Is.EqualTo(1));
            adapter.CanExecuteChanged -= handler;
            Assert.That(source.ListenerCount, Is.Zero);
        }

        [Test]
        public void BoundReferenceArgumentsCanBeNull()
        {
            string? received = "initial";
            var source = new RelayCommand<string>(value => received = value);
            source.CreateCommandWithoutParameters(null).Execute();
            Assert.That(received, Is.Null);
        }

        [Test]
        public void HandlerExceptionsPropagateAndHandlersCanUnsubscribeDuringNotification()
        {
            var (source, adapter) = Create(1);
            Action<IRelayCommand> handler = null!;
            handler = _ => adapter.CanExecuteChanged -= handler;
            adapter.CanExecuteChanged += handler;
            source.NotifyCanExecuteChanged();
            Assert.That(source.ListenerCount, Is.Zero);
            var failure = new InvalidOperationException("handler");
            Action<IRelayCommand> throwing = _ => throw failure;
            adapter.CanExecuteChanged += throwing;
            Assert.That(Assert.Throws<InvalidOperationException>(source.NotifyCanExecuteChanged), Is.SameAs(failure));
            adapter.CanExecuteChanged -= throwing;
        }

        private static object[] ExpectedArguments(int arity)
        {
            var arguments = new object[arity];
            for (var i = 0; i < arity; i++)
                arguments[i] = i + 1;
            return arguments;
        }

        private static (SourceProbe Source, IRelayCommand Adapter) Create(int arity, int fallback = 0)
        {
            switch (arity)
            {
                case 1:
                {
                    var source = new Source1();
                    return (source, fallback switch
                    {
                        0 => source.CreateCommandWithoutParameters(1),
                        1 => source.CreateCommandWithoutParametersOrEmpty(1),
                        _ => source.CreateCommandWithoutParametersOrEmptyExecution(1),
                    });
                }
                case 2:
                {
                    var source = new Source2();
                    return (source, fallback switch
                    {
                        0 => source.CreateCommandWithoutParameters(1, 2),
                        1 => source.CreateCommandWithoutParametersOrEmpty(1, 2),
                        _ => source.CreateCommandWithoutParametersOrEmptyExecution(1, 2),
                    });
                }
                case 3:
                {
                    var source = new Source3();
                    return (source, fallback switch
                    {
                        0 => source.CreateCommandWithoutParameters(1, 2, 3),
                        1 => source.CreateCommandWithoutParametersOrEmpty(1, 2, 3),
                        _ => source.CreateCommandWithoutParametersOrEmptyExecution(1, 2, 3),
                    });
                }
                case 4:
                {
                    var source = new Source4();
                    return (source, fallback switch
                    {
                        0 => source.CreateCommandWithoutParameters(1, 2, 3, 4),
                        1 => source.CreateCommandWithoutParametersOrEmpty(1, 2, 3, 4),
                        _ => source.CreateCommandWithoutParametersOrEmptyExecution(1, 2, 3, 4),
                    });
                }
                default: throw new ArgumentOutOfRangeException(nameof(arity));
            }
        }

        private static IRelayCommand Missing(int arity, int fallback)
        {
            switch (arity)
            {
                case 1:
                {
                    IRelayCommand<int>? missing = null;
                    return fallback switch
                    {
                        0 => missing!.CreateCommandWithoutParameters(1),
                        1 => missing.CreateCommandWithoutParametersOrEmpty(1),
                        _ => missing.CreateCommandWithoutParametersOrEmptyExecution(1),
                    };
                }
                case 2:
                {
                    IRelayCommand<int, int>? missing = null;
                    return fallback switch
                    {
                        0 => missing!.CreateCommandWithoutParameters(1, 2),
                        1 => missing.CreateCommandWithoutParametersOrEmpty(1, 2),
                        _ => missing.CreateCommandWithoutParametersOrEmptyExecution(1, 2),
                    };
                }
                case 3:
                {
                    IRelayCommand<int, int, int>? missing = null;
                    return fallback switch
                    {
                        0 => missing!.CreateCommandWithoutParameters(1, 2, 3),
                        1 => missing.CreateCommandWithoutParametersOrEmpty(1, 2, 3),
                        _ => missing.CreateCommandWithoutParametersOrEmptyExecution(1, 2, 3),
                    };
                }
                case 4:
                {
                    IRelayCommand<int, int, int, int>? missing = null;
                    return fallback switch
                    {
                        0 => missing!.CreateCommandWithoutParameters(1, 2, 3, 4),
                        1 => missing.CreateCommandWithoutParametersOrEmpty(1, 2, 3, 4),
                        _ => missing.CreateCommandWithoutParametersOrEmptyExecution(1, 2, 3, 4),
                    };
                }
                default: throw new ArgumentOutOfRangeException(nameof(arity));
            }
        }

        private static IRelayCommand Native(int arity, Action execute, Func<bool> canExecute) => arity switch
        {
            1 => new RelayCommand<int>((a) => execute(), (a) => canExecute()).CreateCommandWithoutParameters(1),
            2 => new RelayCommand<int, int>((a, b) => execute(), (a, b) => canExecute()).CreateCommandWithoutParameters(1, 2),
            3 => new RelayCommand<int, int, int>((a, b, c) => execute(), (a, b, c) => canExecute()).CreateCommandWithoutParameters(1, 2, 3),
            4 => new RelayCommand<int, int, int, int>((a, b, c, d) => execute(), (a, b, c, d) => canExecute()).CreateCommandWithoutParameters(1, 2, 3, 4),
            _ => throw new ArgumentOutOfRangeException(nameof(arity)),
        };

        private abstract class SourceProbe
        {
            public bool Enabled;
            public bool FailAdd;
            public bool FailRemove;
            public int AddCalls;
            public int RemoveCalls;
            public int CanExecuteCalls;
            public int ExecuteCalls;
            public object[] LastArguments = Array.Empty<object>();
            public abstract int ListenerCount { get; }
            public abstract void NotifyCanExecuteChanged();

            protected bool Check(params object[] arguments)
            {
                CanExecuteCalls++;
                LastArguments = arguments;
                return Enabled;
            }

            protected void Run(params object[] arguments)
            {
                ExecuteCalls++;
                LastArguments = arguments;
            }
        }

        private abstract class SourceProbe<TCommand> : SourceProbe
        {
            private Action<TCommand>? _changed;

            public event Action<TCommand>? CanExecuteChanged
            {
                add
                {
                    AddCalls++;
                    _changed += value;
                    if (FailAdd) throw new InvalidOperationException("subscribe");
                }
                remove
                {
                    RemoveCalls++;
                    _changed -= value;
                    if (FailRemove) throw new InvalidOperationException("unsubscribe");
                }
            }

            public override int ListenerCount =>
                _changed?.GetInvocationList().Length ?? 0;

            public override void NotifyCanExecuteChanged() =>
                _changed?.Invoke((TCommand)(object)this);
        }

        private sealed class Source1 : SourceProbe<IRelayCommand<int>>, IRelayCommand<int>
        {
            public bool CanExecute(int p1) =>
                Check(p1);

            public void Execute(int p1) =>
                Run(p1);
        }

        private sealed class Source2 : SourceProbe<IRelayCommand<int, int>>, IRelayCommand<int, int>
        {
            public bool CanExecute(int p1, int p2) =>
                Check(p1, p2);

            public void Execute(int p1, int p2) =>
                Run(p1, p2);
        }

        private sealed class Source3 : SourceProbe<IRelayCommand<int, int, int>>, IRelayCommand<int, int, int>
        {
            public bool CanExecute(int p1, int p2, int p3) =>
                Check(p1, p2, p3);

            public void Execute(int p1, int p2, int p3) =>
                Run(p1, p2, p3);
        }

        private sealed class Source4 : SourceProbe<IRelayCommand<int, int, int, int>>, IRelayCommand<int, int, int, int>
        {
            public bool CanExecute(int p1, int p2, int p3, int p4) =>
                Check(p1, p2, p3, p4);

            public void Execute(int p1, int p2, int p3, int p4) =>
                Run(p1, p2, p3, p4);
        }

    }
}
