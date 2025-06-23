using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class RelayCommandExtensions
    {
        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that executes the original command with the specified argument.
        /// </summary>
        /// <param name="command">The source command with a parameter.</param>
        /// <param name="param">The value to pass to the command.</param>
        /// <returns>A parameterless command that wraps the original command with the provided parameter.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T>(this IRelayCommand<T> command, T param) => 
            new RelayCommand(() => command.Execute(param), () => command.CanExecute(param));

        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that executes the original command with the specified arguments.
        /// </summary>
        /// <param name="command">The source command with two parameters.</param>
        /// <param name="param1">The first value to pass to the command.</param>
        /// <param name="param2">The second value to pass to the command.</param>
        /// <returns>A parameterless command that wraps the original command with the provided parameters.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2>(this IRelayCommand<T1, T2> command, T1 param1, T2 param2) => 
            new RelayCommand(() => command.Execute(param1, param2), () => command.CanExecute(param1, param2));
        
        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that executes the original command with the specified arguments.
        /// </summary>
        /// <param name="command">The source command with two parameters.</param>
        /// <param name="param1">The first value to pass to the command.</param>
        /// <param name="param2">The second value to pass to the command.</param>
        /// <param name="param3">The third value to pass to the command.</param>
        /// <returns>A parameterless command that wraps the original command with the provided parameters.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3>(this IRelayCommand<T1, T2, T3> command, T1 param1, T2 param2, T3 param3) => 
            new RelayCommand(() => command.Execute(param1, param2, param3), () => command.CanExecute(param1, param2, param3));
        
        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that executes the original command with the specified arguments.
        /// </summary>
        /// <param name="command">The source command with two parameters.</param>
        /// <param name="param1">The first value to pass to the command.</param>
        /// <param name="param2">The second value to pass to the command.</param>
        /// <param name="param3">The third value to pass to the command.</param>
        /// <param name="param4">The fourth value to pass to the command.</param>
        /// <returns>A parameterless command that wraps the original command with the provided parameters.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4> command, T1 param1, T2 param2, T3 param3, T4 param4) => 
            new RelayCommand(() => command.Execute(param1, param2, param3, param4), () => command.CanExecute(param1, param2, param3, param4));
        
        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that wraps the original command and uses the specified parameter.
        /// If the original command is <c>null</c>, returns an empty command.
        /// </summary>
        /// <param name="command">The source command with a parameter, or <c>null</c>.</param>
        /// <param name="param">The value to pass to the command.</param>
        /// <returns>
        /// A parameterless command that invokes the original command with the given parameter,
        /// or <see cref="RelayCommand.Empty"/> if the command is <c>null</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T>(this IRelayCommand<T>? command, T param) => command is not null
            ? new RelayCommand(() => command.Execute(param), () => command.CanExecute(param))
            : RelayCommand.Empty;

        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that wraps the original command and uses the specified parameter.
        /// If the original command is <c>null</c>, returns an empty command.
        /// </summary>
        /// <param name="command">The source command with a parameter, or <c>null</c>.</param>
        /// <param name="param1">The first value to pass to the command.</param>
        /// <param name="param2">The second value to pass to the command.</param>
        /// <returns>
        /// A parameterless command that invokes the original command with the given parameter,
        /// or <see cref="RelayCommand.Empty"/> if the command is <c>null</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2), () => command.CanExecute(param1, param2))
            : RelayCommand.Empty;
        
        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that wraps the original command and uses the specified parameter.
        /// If the original command is <c>null</c>, returns an empty command.
        /// </summary>
        /// <param name="command">The source command with a parameter, or <c>null</c>.</param>
        /// <param name="param1">The first value to pass to the command.</param>
        /// <param name="param2">The second value to pass to the command.</param>
        /// <param name="param3">The third value to pass to the command.</param>
        /// <returns>
        /// A parameterless command that invokes the original command with the given parameter,
        /// or <see cref="RelayCommand.Empty"/> if the command is <c>null</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2, param3), () => command.CanExecute(param1, param2, param3))
            : RelayCommand.Empty;
        
        /// <summary>
        /// Creates a parameterless <see cref="IRelayCommand"/> that wraps the original command and uses the specified parameter.
        /// If the original command is <c>null</c>, returns an empty command.
        /// </summary>
        /// <param name="command">The source command with a parameter, or <c>null</c>.</param>
        /// <param name="param1">The first value to pass to the command.</param>
        /// <param name="param2">The second value to pass to the command.</param>
        /// <param name="param3">The third value to pass to the command.</param>
        /// <param name="param4">The fourth value to pass to the command.</param>
        /// <returns>
        /// A parameterless command that invokes the original command with the given parameter,
        /// or <see cref="RelayCommand.Empty"/> if the command is <c>null</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2, param3, param4), () => command.CanExecute(param1, param2, param3, param4))
            : RelayCommand.Empty;
    }
}