#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// <see cref="IBinder"/> with Editor validation of its View, field identifier and previous assignments.
    /// </summary>
    public interface IMonoBinderValidatable : IBinder
    {
        /// <summary>
        /// Gets a value indicating whether the underlying component exists and has not begun destruction.
        /// </summary>
        public bool IsAlive { get; }

        /// <summary>
        /// Gets the assigned component view, or <see langword="null"/> when missing or destroyed.
        /// </summary>
        public IView? View { get; }

        /// <summary>
        /// Gets the last assigned view.
        /// </summary>
        public MonoBinderPreviousView PreviousView { get; }

        /// <summary>
        /// Gets the binder field ID. Returns an empty string when unassigned.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the last non-empty ID.
        /// </summary>
        public MonoBinderPreviousId PreviousId { get; }

        /// <summary>
        /// Sets the view used for validation.
        /// </summary>
        /// <param name="view">The component view to assign. A <see langword="null"/> or destroyed view clears the assignment and its history.</param>
        public void SetView(IView? view);

        /// <summary>
        /// Sets the binder field ID used for validation.
        /// </summary>
        /// <param name="id">The ID to assign, or a blank value to reset it.</param>
        public void SetId(string? id);

        /// <summary>
        /// Clears <see cref="View"/>.
        /// </summary>
        /// <param name="mode">Use <see cref="MonoBinderResetMode.Hard"/> to also clear <see cref="PreviousView"/>.</param>
        public void ResetView(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        /// <summary>
        /// Clears <see cref="Id"/>.
        /// </summary>
        /// <param name="mode">Use <see cref="MonoBinderResetMode.Hard"/> to also clear <see cref="PreviousId"/>.</param>
        public void ResetId(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        /// <summary>
        /// Clears both <see cref="Id"/> and <see cref="View"/>.
        /// </summary>
        /// <param name="mode">Use <see cref="MonoBinderResetMode.Hard"/> to also clear previous values.</param>
        public void Reset(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
        {
            if (!IsAlive) return;

            ResetId(mode);
            ResetView(mode);
        }
    }
}
