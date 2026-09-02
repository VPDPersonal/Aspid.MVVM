#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// Editor-side view of a <see cref="MonoBinder"/>: the View and field ID it is wired to, with their last known values.
    /// </summary>
    public interface IMonoBinderValidatable : IBinder
    {
        /// <summary>
        /// Indicates whether the underlying component still exists.
        /// </summary>
        bool IsMonoAlive { get; }

        #region View Properties
        /// <summary>
        /// Gets the View this binder belongs to, or <see langword="null"/>.
        /// </summary>
        IView? View { get; }

        /// <summary>
        /// Gets the last non-empty View.
        /// </summary>
        MonoBinderPreviousView PreviousView { get; }
        #endregion

        #region Id Properties
        /// <summary>
        /// Gets the ID of the View field this binder is bound through.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the last non-empty ID.
        /// </summary>
        MonoBinderPreviousId PreviousId { get; }
        #endregion

        #region Set Methods
        /// <summary>
        /// Sets <see cref="View"/>; <see langword="null"/> resets it.
        /// </summary>
        /// <param name="view">The View, or <see langword="null"/>.</param>
        void SetView(IView? view);

        /// <summary>
        /// Sets <see cref="Id"/>; a blank value resets it.
        /// </summary>
        /// <param name="id">The ID, or <see langword="null"/>.</param>
        void SetId(string? id);
        #endregion

        #region Reset Methods
        /// <summary>
        /// Clears <see cref="View"/>.
        /// </summary>
        /// <param name="mode">Whether <see cref="PreviousView"/> is cleared as well.</param>
        void ResetView(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        /// <summary>
        /// Clears <see cref="Id"/>.
        /// </summary>
        /// <param name="mode">Whether <see cref="PreviousId"/> is cleared as well.</param>
        void ResetId(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        /// <summary>
        /// Clears both <see cref="Id"/> and <see cref="View"/>.
        /// </summary>
        /// <param name="mode">Whether the previous values are cleared as well.</param>
        void Reset(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
        {
            if (!IsMonoAlive) return;

            ResetId(mode);
            ResetView(mode);
        }
        #endregion
    }
}
