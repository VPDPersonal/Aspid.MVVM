// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class BoundRelayCommand<T> : BoundRelayCommandBase<IRelayCommand<T>>
    {
        private readonly T? _param;

        public BoundRelayCommand(
            IRelayCommand<T> command,
            T? param)
            : base(command)
        {
            _param = param;
        }

        public override void Execute() =>
            Command.Execute(_param);

        public override bool CanExecute() =>
            Command.CanExecute(_param);

        protected override void SubscribeToSource() =>
            Command.CanExecuteChanged += SourceChangedHandler;

        protected override void UnsubscribeFromSource() =>
            Command.CanExecuteChanged -= SourceChangedHandler;
    }

    internal class BoundRelayCommand<T1, T2> : BoundRelayCommandBase<IRelayCommand<T1, T2>>
    {
        private readonly T1? _param1;
        private readonly T2? _param2;

        public BoundRelayCommand(
            IRelayCommand<T1, T2> command,
            T1? param1,
            T2? param2)
            : base(command)
        {
            _param1 = param1;
            _param2 = param2;
        }

        public override void Execute() =>
            Command.Execute(_param1, _param2);

        public override bool CanExecute() =>
            Command.CanExecute(_param1, _param2);

        protected override void SubscribeToSource() =>
            Command.CanExecuteChanged += SourceChangedHandler;

        protected override void UnsubscribeFromSource() =>
            Command.CanExecuteChanged -= SourceChangedHandler;
    }

    internal class BoundRelayCommand<T1, T2, T3> : BoundRelayCommandBase<IRelayCommand<T1, T2, T3>>
    {
        private readonly T1? _param1;
        private readonly T2? _param2;
        private readonly T3? _param3;

        public BoundRelayCommand(
            IRelayCommand<T1, T2, T3> command,
            T1? param1,
            T2? param2,
            T3? param3)
            : base(command)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
        }

        public override void Execute() =>
            Command.Execute(_param1, _param2, _param3);

        public override bool CanExecute() =>
            Command.CanExecute(_param1, _param2, _param3);

        protected override void SubscribeToSource() =>
            Command.CanExecuteChanged += SourceChangedHandler;

        protected override void UnsubscribeFromSource() =>
            Command.CanExecuteChanged -= SourceChangedHandler;
    }

    internal class BoundRelayCommand<T1, T2, T3, T4> : BoundRelayCommandBase<IRelayCommand<T1, T2, T3, T4>>
    {
        private readonly T1? _param1;
        private readonly T2? _param2;
        private readonly T3? _param3;
        private readonly T4? _param4;

        public BoundRelayCommand(
            IRelayCommand<T1, T2, T3, T4> command,
            T1? param1,
            T2? param2,
            T3? param3,
            T4? param4)
            : base(command)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;
        }

        public override void Execute() =>
            Command.Execute(_param1, _param2, _param3, _param4);

        public override bool CanExecute() =>
            Command.CanExecute(_param1, _param2, _param3, _param4);

        protected override void SubscribeToSource() =>
            Command.CanExecuteChanged += SourceChangedHandler;

        protected override void UnsubscribeFromSource() =>
            Command.CanExecuteChanged -= SourceChangedHandler;
    }
}
