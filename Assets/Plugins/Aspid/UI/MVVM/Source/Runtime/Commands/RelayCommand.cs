using System;

namespace Aspid.UI.MVVM.Commands
{
    /// <summary>
    /// Реализация интерфейса <see cref="IRelayCommand"/> для команд без параметров.
    /// Позволяет определять, может ли команда выполняться, и исполнять команду.
    /// </summary>
    public sealed class RelayCommand : IRelayCommand
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand>? CanExecuteChanged;
        
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand"/>, принимая действие для выполнения команды.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand"/>, принимая действие для выполнения команды и функцию для проверки возможности её выполнения.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <param name="canExecute">Функция, возвращающая <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> или <paramref name="canExecute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action execute, Func<bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена.
        /// </summary>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute() =>
            _canExecute?.Invoke() ?? true;

        /// <summary>
        /// Выполняет команду, если она может быть выполнена.
        /// </summary>
        public void Execute()
        {
            if (CanExecute()) 
                _execute.Invoke();
        }
        
        /// <summary>
        /// Уведомляет об изменении состояния возможности выполнения команды.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }

    /// <summary>
    /// Реализация интерфейса <see cref="IRelayCommand{T}"/> для команд с одним параметром.
    /// Позволяет определять, может ли команда выполняться, и исполнять команду с заданным параметром.
    /// </summary>
    /// <typeparam name="T">Тип параметра команды.</typeparam>
    public sealed class RelayCommand<T> : IRelayCommand<T>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния возможности выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T>>? CanExecuteChanged;
        
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool>? _canExecute;
        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T}"/>, принимая действие для выполнения команды.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T}"/>, принимая действие для выполнения команды и функцию для проверки возможности её выполнения.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <param name="canExecute">Функция, возвращающая <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с заданным параметром.
        /// </summary>
        /// <param name="param">Параметр, который передается для проверки возможности выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T? param) =>
            _canExecute?.Invoke(param) ?? true;
        
        /// <summary>
        /// Выполняет команду с заданным параметром, если она может быть выполнена.
        /// </summary>
        /// <param name="param">Параметр, который передается команде для выполнения.</param>
        public void Execute(T? param)
        {
            if (CanExecute(param)) 
                _execute.Invoke(param);
        }
        
        /// <summary>
        /// Уведомляет об изменении состояния возможности выполнения команды.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Реализация интерфейса <see cref="IRelayCommand{T1, T2}"/> для команд с двумя параметрами.
    /// Позволяет определять, может ли команда выполняться, и исполнять команду с заданными параметрами.
    /// </summary>
    /// <typeparam name="T1">Тип первого параметра команды.</typeparam>
    /// <typeparam name="T2">Тип второго параметра команды.</typeparam>
    public sealed class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?> _execute;
        private readonly Func<T1?, T2?, bool>? _canExecute;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T1, T2}"/>, принимая действие для выполнения команды.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T1, T2}"/>, принимая действие для выполнения команды и функцию для проверки возможности её выполнения.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <param name="canExecute">Функция, возвращающая <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?> execute, Func<T1?, T2?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с заданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, который передается для проверки возможности выполнения команды.</param>
        /// <param name="param2">Второй параметр, который передается для проверки возможности выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2) =>
            _canExecute?.Invoke(param1, param2) ?? true;
        
        /// <summary>
        /// Выполняет команду с заданными параметрами, если она может быть выполнена.
        /// </summary>
        /// <param name="param1">Первый параметр, который передается команде для выполнения.</param>
        /// <param name="param2">Второй параметр, который передается команде для выполнения.</param>
        public void Execute(T1? param1, T2? param2)
        {
            if (CanExecute(param1, param2)) 
                _execute.Invoke(param1, param2);
        }
        
        /// <summary>
        /// Уведомляет об изменении состояния возможности выполнения команды.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Реализация интерфейса <see cref="IRelayCommand{T1, T2, T3}"/> для команд с тремя параметрами.
    /// Позволяет определять, может ли команда выполняться, и исполнять команду с заданными параметрами.
    /// </summary>
    /// <typeparam name="T1">Тип первого параметра команды.</typeparam>
    /// <typeparam name="T2">Тип второго параметра команды.</typeparam>
    /// <typeparam name="T3">Тип третьего параметра команды.</typeparam>
    public sealed class RelayCommand<T1, T2, T3> : IRelayCommand<T1, T2, T3>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?, T3?> _execute;
        private readonly Func<T1?, T2?, T3?, bool>? _canExecute;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T1, T2, T3}"/>, принимая действие для выполнения команды.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T1, T2, T3}"/>, принимая действие для выполнения команды и функцию для проверки возможности её выполнения.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <param name="canExecute">Функция, возвращающая <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?> execute, Func<T1?, T2?, T3?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с заданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, который передается для проверки возможности выполнения команды.</param>
        /// <param name="param2">Второй параметр, который передается для проверки возможности выполнения команды.</param>
        /// <param name="param3">Третий параметр, который передается для проверки возможности выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3) =>
            _canExecute?.Invoke(param1, param2, param3) ?? true;
        
        /// <summary>
        /// Выполняет команду с заданными параметрами, если она может быть выполнена.
        /// </summary>
        /// <param name="param1">Первый параметр, который передается команде для выполнения.</param>
        /// <param name="param2">Второй параметр, который передается команде для выполнения.</param>
        /// <param name="param3">Третий параметр, который передается команде для выполнения.</param>
        public void Execute(T1? param1, T2? param2, T3? param3)
        {
            if (CanExecute(param1, param2, param3)) 
                _execute.Invoke(param1, param2, param3);
        }
        
        /// <summary>
        /// Уведомляет об изменении состояния возможности выполнения команды.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Реализация интерфейса <see cref="IRelayCommand{T1, T2, T3, T4}"/> для команд с четырьмя параметрами.
    /// Позволяет определять, может ли команда выполняться, и исполнять команду с заданными параметрами.
    /// </summary>
    /// <typeparam name="T1">Тип первого параметра команды.</typeparam>
    /// <typeparam name="T2">Тип второго параметра команды.</typeparam>
    /// <typeparam name="T3">Тип третьего параметра команды.</typeparam>
    /// <typeparam name="T4">Тип четвертого параметра команды.</typeparam>
    public sealed class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?, T3?, T4?> _execute;
        private readonly Func<T1?, T2?, T3?, T4?, bool>? _canExecute;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T1, T2, T3, T4}"/>, принимая действие для выполнения команды.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand{T1, T2, T3, T4}"/>, принимая действие для выполнения команды и функцию для проверки возможности её выполнения.
        /// </summary>
        /// <param name="execute">Действие, которое будет выполнено командой.</param>
        /// <param name="canExecute">Функция, возвращающая <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="execute"/> равно <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с заданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, который передается для проверки возможности выполнения команды.</param>
        /// <param name="param2">Второй параметр, который передается для проверки возможности выполнения команды.</param>
        /// <param name="param3">Третий параметр, который передается для проверки возможности выполнения команды.</param>
        /// <param name="param4">Четвертый параметр, который передается для проверки возможности выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4) =>
            _canExecute?.Invoke(param1, param2, param3, param4) ?? true;
        
        /// <summary>
        /// Выполняет команду с заданными параметрами, если она может быть выполнена.
        /// </summary>
        /// <param name="param1">Первый параметр, который передается команде для выполнения.</param>
        /// <param name="param2">Второй параметр, который передается команде для выполнения.</param>
        /// <param name="param3">Третий параметр, который передается команде для выполнения.</param>
        /// <param name="param4">Четвертый параметр, который передается команде для выполнения.</param>
        public void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
        {
            if (CanExecute(param1, param2, param3, param4)) 
                _execute.Invoke(param1, param2, param3, param4);
        }
        
        /// <summary>
        /// Уведомляет об изменении состояния возможности выполнения команды.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
}