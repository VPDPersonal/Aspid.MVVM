// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal partial class NotificationTestViewModel
    {
        [Bind, BindAlso(nameof(Doubled))]
        public int Implicit
        {
            get => _implicit;
            set => SetField(ref _implicit, value);
        }

        [Bind]
        public int Direct
        {
            get => _direct;
            set => SetDirectField(ref _direct, value);
        }

        // Same-line setters deliberately exercise explicit-name routing.
        [Bind] public int NamedA { get => _namedA; set => SetField(ref _namedA, value, nameof(NamedA)); } [Bind] public int NamedB { get => _namedB; set => SetField(ref _namedB, value, nameof(NamedB)); }

        [Bind]
        public int Manual
        {
            get => _manual;
            set
            {
                _manual = value;
                OnPropertyChanged();
            }
        }

        [Bind(BindMode.OneWayToSource)]
        public int SourceOnly
        {
            get => _sourceOnly;
            set => SetField(ref _sourceOnly, value);
        }

        [Bind]
        public int Doubled => _implicit * 2;

        public void Notify(string name) =>
            OnPropertyChanged(name);

        partial void OnDirectChanging(int newValue) =>
            Trace.Add($"changing:{Direct}:{newValue}");

        partial void OnDirectChanging(int oldValue, int newValue) =>
            Trace.Add($"changing-pair:{oldValue}:{newValue}");

        partial void OnDirectChanged(int newValue) =>
            Trace.Add($"changed:{Direct}:{newValue}");

        partial void OnDirectChanged(int oldValue, int newValue) =>
            Trace.Add($"changed-pair:{oldValue}:{newValue}");
    }
}
