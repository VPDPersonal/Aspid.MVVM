using System;

namespace Aspid.UI.MVVM.Commands
{
    /// <summary>
    /// Интерфейс для команды, которая может быть выполнена без параметров.
    /// </summary>
    public interface IRelayCommand
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand> CanExecuteChanged;
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена.
        /// </summary>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute();

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        public void Execute();
        
        /// <summary>
        /// Уведомляет об изменении состояния выполнения команды, вызывая событие <see cref="CanExecuteChanged"/>.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Интерфейс для команды, которая может быть выполнена с параметром.
    /// </summary>
    /// <typeparam name="T">Тип параметра, передаваемого в команду.</typeparam>
    public interface IRelayCommand<in T>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T>> CanExecuteChanged;
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с переданным параметром.
        /// </summary>
        /// <param name="param">Параметр, используемый для выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T? param);

        /// <summary>
        /// Выполняет команду с переданным параметром.
        /// </summary>
        /// <param name="param">Параметр, используемый для выполнения команды.</param>
        public void Execute(T param);
        
        /// <summary>
        /// Уведомляет об изменении состояния выполнения команды, вызывая событие <see cref="CanExecuteChanged"/>.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Интерфейс для команды, которая может быть выполнена с двумя параметрами.
    /// </summary>
    /// <typeparam name="T1">Тип первого параметра, передаваемого в команду.</typeparam>
    /// <typeparam name="T2">Тип второго параметра, передаваемого в команду.</typeparam>
    public interface IRelayCommand<in T1, in T2>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T1, T2>> CanExecuteChanged;
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с переданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, используемый для выполнения команды.</param>
        /// <param name="param2">Второй параметр, используемый для выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2);

        /// <summary>
        /// Выполняет команду с переданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, используемый для выполнения команды.</param>
        /// <param name="param2">Второй параметр, используемый для выполнения команды.</param>
        public void Execute(T1 param1, T2 param2);
        
        /// <summary>
        /// Уведомляет об изменении состояния выполнения команды, вызывая событие <see cref="CanExecuteChanged"/>.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Интерфейс для команды, которая может быть выполнена с тремя параметрами.
    /// </summary>
    /// <typeparam name="T1">Тип первого параметра, передаваемого в команду.</typeparam>
    /// <typeparam name="T2">Тип второго параметра, передаваемого в команду.</typeparam>
    /// <typeparam name="T3">Тип третьего параметра, передаваемого в команду.</typeparam>
    public interface IRelayCommand<in T1, in T2, in T3>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3>> CanExecuteChanged;
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с переданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, используемый для выполнения команды.</param>
        /// <param name="param2">Второй параметр, используемый для выполнения команды.</param>
        /// <param name="param3">Третий параметр, используемый для выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3);

        /// <summary>
        /// Выполняет команду с переданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, используемый для выполнения команды.</param>
        /// <param name="param2">Второй параметр, используемый для выполнения команды.</param>
        /// <param name="param3">Третий параметр, используемый для выполнения команды.</param>
        public void Execute(T1 param1, T2 param2, T3 param3);
        
        /// <summary>
        /// Уведомляет об изменении состояния выполнения команды, вызывая событие <see cref="CanExecuteChanged"/>.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Интерфейс для команды, которая может быть выполнена с тремя параметрами.
    /// </summary>
    /// <typeparam name="T1">Тип первого параметра, передаваемого в команду.</typeparam>
    /// <typeparam name="T2">Тип второго параметра, передаваемого в команду.</typeparam>
    /// <typeparam name="T3">Тип третьего параметра, передаваемого в команду.</typeparam>
    /// <typeparam name="T4">Тип четвертого параметра, передаваемого в команду.</typeparam>
    public interface IRelayCommand<in T1, in T2, in T3, in T4>
    {
        /// <summary>
        /// Событие, которое вызывается при изменении состояния выполнения команды.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3, T4>> CanExecuteChanged;
        
        /// <summary>
        /// Определяет, может ли команда быть выполнена с переданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, используемый для выполнения команды.</param>
        /// <param name="param2">Второй параметр, используемый для выполнения команды.</param>
        /// <param name="param3">Третий параметр, используемый для выполнения команды.</param>
        /// <param name="param4">Четвертый параметр, используемый для выполнения команды.</param>
        /// <returns>Значение <c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4);

        /// <summary>
        /// Выполняет команду с переданными параметрами.
        /// </summary>
        /// <param name="param1">Первый параметр, используемый для выполнения команды.</param>
        /// <param name="param2">Второй параметр, используемый для выполнения команды.</param>
        /// <param name="param3">Третий параметр, используемый для выполнения команды.</param>
        /// <param name="param4">Четвертый параметр, используемый для выполнения команды.</param>
        public void Execute(T1 param1, T2 param2, T3 param3, T4 param4);
        
        /// <summary>
        /// Уведомляет об изменении состояния выполнения команды, вызывая событие <see cref="CanExecuteChanged"/>.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
}