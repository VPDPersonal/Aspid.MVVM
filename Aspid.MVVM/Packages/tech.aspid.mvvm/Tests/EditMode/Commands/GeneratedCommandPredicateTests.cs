using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class GeneratedCommandPredicateTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void GeneratedPredicatesGateExecutionAndNotifyExplicitly(int variant)
        {
            var model = new PredicateTestViewModel();
            var command = model.GetCommand(variant);
            var notifications = 0;
            command.CanExecuteChanged += _ => notifications++;
            foreach (var enabled in new[] { false, true, false })
            {
                model.SetEnabled(enabled);
                Assert.That(command.CanExecute(), Is.EqualTo(enabled));
                command.Execute();
            }

            Assert.That(model.Executions, Is.EqualTo(1));
            Assert.That(notifications, Is.Zero);
            command.NotifyCanExecuteChanged();
            Assert.That(notifications, Is.EqualTo(1));
        }
    }

    [ViewModel]
    internal partial class PredicateTestViewModel
    {
        private bool _canRun;
        [Bind] private bool _generatedCanRun;

        public int Executions { get; private set; }

        private bool CanRunProperty => _canRun;

        public void SetEnabled(bool value)
        {
            _canRun = value;
            GeneratedCanRun = value;
        }

        public IRelayCommand GetCommand(int variant) => variant switch
        {
            0 => FromFieldCommand,
            1 => FromPropertyCommand,
            2 => FromMethodCommand,
            3 => FromGeneratedPropertyCommand,
            _ => FromBackingFieldCommand,
        };

        private bool CanRunMethod() =>
            _canRun;

        [RelayCommand(CanExecute = nameof(_canRun))]
        private void FromField() =>
            Executions++;

        [RelayCommand(CanExecute = nameof(CanRunProperty))]
        private void FromProperty() =>
            Executions++;

        [RelayCommand(CanExecute = nameof(CanRunMethod))]
        private void FromMethod() =>
            Executions++;

        [RelayCommand(CanExecute = nameof(GeneratedCanRun))]
        private void FromGeneratedProperty() =>
            Executions++;

        [RelayCommand(CanExecute = nameof(_generatedCanRun))]
        private void FromBackingField() =>
            Executions++;
    }
}
