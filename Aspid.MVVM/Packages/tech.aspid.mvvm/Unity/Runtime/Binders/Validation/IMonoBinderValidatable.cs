#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// Defines editor-side validation of a <see cref="MonoBinder"/>: the View and field ID it is wired to, with their last known values.
    /// </summary>
    public interface IMonoBinderValidatable : IBinder
    {
        /// <summary>
        /// Gets a value indicating whether the underlying component exists and has not begun destruction.
        /// </summary>
        public bool IsAlive { get; }

        /// <summary>
        /// Gets the View this binder belongs to, or <see langword="null"/>.
        /// </summary>
        public IView? View { get; }

        /// <summary>
        /// Gets the last non-empty View.
        /// </summary>
        public MonoBinderPreviousView PreviousView { get; }

        /// <summary>
        /// Gets the ID of the View field this binder is bound through; an empty string when unassigned.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the last non-empty ID.
        /// </summary>
        public MonoBinderPreviousId PreviousId { get; }

        /// <summary>
        /// Sets <see cref="View"/>; <see langword="null"/> resets it.
        /// </summary>
        /// <param name="view">The View, or <see langword="null"/>.</param>
        public void SetView(IView? view);

        /// <summary>
        /// Sets <see cref="Id"/>; a blank value resets it.
        /// </summary>
        /// <param name="id">The ID, or <see langword="null"/>.</param>
        public void SetId(string? id);

        /// <summary>
        /// Clears <see cref="View"/>.
        /// </summary>
        /// <param name="mode">Whether <see cref="PreviousView"/> is cleared as well.</param>
        public void ResetView(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        /// <summary>
        /// Clears <see cref="Id"/>.
        /// </summary>
        /// <param name="mode">Whether <see cref="PreviousId"/> is cleared as well.</param>
        public void ResetId(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        /// <summary>
        /// Clears both <see cref="Id"/> and <see cref="View"/>.
        /// </summary>
        /// <param name="mode">Whether the previous values are cleared as well.</param>
        public void Reset(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
        {
            if (!IsAlive) return;

            ResetId(mode);
            ResetView(mode);
        }
    }
}
