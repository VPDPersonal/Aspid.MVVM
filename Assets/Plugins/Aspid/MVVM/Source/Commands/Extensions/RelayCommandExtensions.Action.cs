namespace Aspid.MVVM;

public static partial class RelayCommandExtensions
{
    /// <summary>
    /// Creates a <see cref="RelayCommand"/> from the specified execute and canExecute delegates.
    /// </summary>
    /// <param name="execute">The action to execute.</param>
    /// <param name="canExecute">The function to determine if the command can execute.</param>
    /// <returns>A new <see cref="RelayCommand"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand CreateCommand(
        this Action execute,
        Func<bool>? canExecute = null) =>
        new(execute, canExecute);

    /// <summary>
    /// Creates a <see cref="RelayCommand{T}"/> from the specified execute and canExecute delegates.
    /// </summary>
    /// <param name="execute">The action to execute.</param>
    /// <param name="canExecute">The function to determine if the command can execute.</param>
    /// <returns>A new <see cref="RelayCommand{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T?> CreateCommand<T>(
        this Action<T?> execute, 
        Func<T?, bool>? canExecute = null) =>
        new(execute, canExecute);

    /// <summary>
    /// Creates a <see cref="RelayCommand{T1, T2}"/> from the specified execute and canExecute delegates.
    /// </summary>
    /// <param name="execute">The action to execute.</param>
    /// <param name="canExecute">The function to determine if the command can execute.</param>
    /// <returns>A new <see cref="RelayCommand{T1, T2}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T1?, T2?> CreateCommand<T1, T2>(
        this Action<T1?, T2?> execute,
        Func<T1?, T2?, bool>? canExecute = null) =>
        new(execute, canExecute);

    /// <summary>
    /// Creates a <see cref="RelayCommand{T1, T2, T3}"/> from the specified execute and canExecute delegates.
    /// </summary>
    /// <param name="execute">The action to execute.</param>
    /// <param name="canExecute">The function to determine if the command can execute.</param>
    /// <returns>A new <see cref="RelayCommand{T1, T2, T3}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T1?, T2?, T3?> CreateCommand<T1, T2, T3>(
        this Action<T1?, T2?, T3?> execute,
        Func<T1?, T2?, T3?, bool>? canExecute = null) =>
        new(execute, canExecute);

    /// <summary>
    /// Creates a <see cref="RelayCommand{T1, T2, T3, T4}"/> from the specified execute and canExecute delegates.
    /// </summary>
    /// <param name="execute">The action to execute.</param>
    /// <param name="canExecute">The function to determine if the command can execute.</param>
    /// <returns>A new <see cref="RelayCommand{T1, T2, T3, T4}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommand<T1, T2, T3, T4>(
        this Action<T1?, T2?, T3?, T4?> execute,
        Func<T1?, T2?, T3?, T4?, bool>? canExecute = null) =>
        new(execute, canExecute);

    /// <summary>
    /// Creates a <see cref="RelayCommand"/> using the provided delegates.
    /// If <paramref name="execute"/> is <c>null</c>, returns a non-executable empty command.
    /// </summary>
    /// <param name="execute">The action to execute. If <c>null</c>, an empty command will be returned.</param>
    /// <param name="canExecute">The function that determines whether the command can execute. Optional.</param>
    /// <returns>
    /// A new <see cref="RelayCommand"/> instance, or <see cref="RelayCommand.Empty"/> if <paramref name="execute"/> is <c>null</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand CreateCommandOrEmpty(
        this Action? execute, 
        Func<bool>? canExecute = null) =>
        execute is not null ? new RelayCommand(execute, canExecute) : RelayCommand.Empty;

    /// <summary>
    /// Creates a <see cref="RelayCommand{T}"/> using the provided delegates.
    /// If <paramref name="execute"/> is <c>null</c>, returns a non-executable empty command.
    /// </summary>
    /// <param name="execute">The action to execute. If <c>null</c>, an empty command will be returned.</param>
    /// <param name="canExecute">Optional function to determine whether the command can execute.</param>
    /// <returns>
    /// A new <see cref="RelayCommand{T}"/> instance, or  <see cref="RelayCommand{T}.Empty"/> if <paramref name="execute"/> is <c>null</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T?> CreateCommandOrEmpty<T>(
        this Action<T?>? execute, 
        Func<T?, bool>? canExecute = null) =>
        execute is not null ? new RelayCommand<T?>(execute, canExecute) : RelayCommand<T?>.Empty;

    /// <summary>
    /// Creates a <see cref="RelayCommand{T1, T2}"/> using the provided delegates.
    /// If <paramref name="execute"/> is <c>null</c>, returns a non-executable empty command.
    /// </summary>
    /// <param name="execute">The action to execute. If <c>null</c>, an empty command will be returned.</param>
    /// <param name="canExecute">Optional function to determine whether the command can execute.</param>
    /// <returns>
    /// A new <see cref="RelayCommand{T1, T2}"/> instance, or <see cref="RelayCommand{T1, T2}.Empty"/> if <paramref name="execute"/> is <c>null</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T1?, T2?> CreateCommandOrEmpty<T1, T2>(
        this Action<T1?, T2?>? execute,
        Func<T1?, T2?, bool>? canExecute = null) =>
        execute is not null ? new RelayCommand<T1?, T2?>(execute, canExecute) : RelayCommand<T1?, T2?>.Empty;

    /// <summary>
    /// Creates a <see cref="RelayCommand{T1, T2, T3}"/> using the provided delegates.
    /// If <paramref name="execute"/> is <c>null</c>, returns a non-executable empty command.
    /// </summary>
    /// <param name="execute">The action to execute. If <c>null</c>, an empty command will be returned.</param>
    /// <param name="canExecute">Optional function to determine whether the command can execute.</param>
    /// <returns>
    /// A new <see cref="RelayCommand{T1, T2, T3}"/> instance, or <see cref="RelayCommand{T1, T2, T3}.Empty"/> if <paramref name="execute"/> is <c>null</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmpty<T1, T2, T3>(
        this Action<T1?, T2?, T3?>? execute,
        Func<T1?, T2?, T3?, bool>? canExecute = null) =>
        execute is not null ? new RelayCommand<T1?, T2?, T3?>(execute, canExecute) : RelayCommand<T1?, T2?, T3?>.Empty;

    /// <summary>
    /// Creates a <see cref="RelayCommand{T1, T2, T3, T4}"/> using the provided delegates.
    /// If <paramref name="execute"/> is <c>null</c>, returns a non-executable empty command.
    /// </summary>
    /// <param name="execute">The action to execute. If <c>null</c>, an empty command will be returned.</param>
    /// <param name="canExecute">Optional function to determine whether the command can execute.</param>
    /// <returns>
    /// A new <see cref="RelayCommand{T1, T2, T3, T4}"/> instance, or <see cref="RelayCommand{T1, T2, T3, T4}.Empty"/> if <paramref name="execute"/> is <c>null</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmpty<T1, T2, T3, T4>(
        this Action<T1?, T2?, T3?, T4?>? execute,
        Func<T1?, T2?, T3?, T4?, bool>? canExecute = null) =>
        execute is not null ? new RelayCommand<T1?, T2?, T3?, T4?>(execute, canExecute) : RelayCommand<T1?, T2?, T3?, T4?>.Empty;
}