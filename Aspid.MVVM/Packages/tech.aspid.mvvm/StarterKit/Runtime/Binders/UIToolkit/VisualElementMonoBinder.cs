#if ASPID_MVVM_HAS_UIELEMENTS
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that resolves one <typeparamref name="TElement"/> in a
    /// <see cref="UIDocument"/> by name or USS class.
    /// </summary>
    /// <remarks>
    /// The element is resolved lazily, since the document builds its tree in <c>OnEnable</c>; a failed lookup is
    /// reported and retried on the next access.
    /// </remarks>
    /// <typeparam name="TElement">The type of element this binder drives.</typeparam>
    public abstract class VisualElementMonoBinder<TElement> : MonoBinder
        where TElement : VisualElement
    {
        [Tooltip("Document holding the element; empty searches this object and its parents.")]
        [SerializeField] private UIDocument _document;

        [Tooltip("Element name from UXML; empty falls back to the class.")]
        [SerializeField] private string _elementName;

        [Tooltip("USS class to search by when no name is set; the first match is used.")]
        [SerializeField] private string _elementClass;

        private TElement _element;
        private bool _isRefreshing;
        private bool _isAttached;
        private VisualElement _root;

        /// <summary>
        /// Gets the resolved element, or <see langword="null"/> when it cannot be found.
        /// </summary>
        protected TElement Element
        {
            get
            {
                RefreshElement();
                return _element;
            }
        }

        /// <summary>
        /// Resolves the current element before a new value is applied, without replaying the previous value.
        /// </summary>
        /// <returns>The current element, or <see langword="null"/> before its tree exists.</returns>
        protected TElement ResolveForWrite()
        {
            RefreshElement(restore: false);
            return _element;
        }

        /// <inheritdoc/>
        protected override void OnAttach()
        {
            _isAttached = true;
            RefreshElement();
            if (_element is not null) OnElementAttached(_element);
        }

        /// <inheritdoc/>
        protected override void OnDetach()
        {
            _isAttached = false;
            ReleaseElement();
            ClearValue();
        }

        /// <inheritdoc/>
        protected override void OnBinding() =>
            ClearValue();

        /// <summary>
        /// Called when a binding ends or starts to release its retained value.
        /// </summary>
        protected virtual void ClearValue() { }

        /// <summary>
        /// Called when a replacement element needs the last bound value.
        /// </summary>
        protected virtual void RestoreElement() { }

        /// <summary>
        /// Called when an element joins the active binding. Override to subscribe to input.
        /// </summary>
        /// <param name="element">The resolved element.</param>
        protected virtual void OnElementAttached(TElement element) { }

        /// <summary>
        /// Called before an element leaves the binding. Override to release its callbacks.
        /// </summary>
        /// <param name="element">The previous element.</param>
        protected virtual void OnElementDetached(TElement element) { }

        private void LateUpdate()
        {
            if (IsBound) RefreshElement(reportMissing: false);
        }

        private void RefreshElement(bool reportMissing = true, bool restore = true)
        {
            if (_isRefreshing) return;
            var document = _document ? _document : GetComponentInParent<UIDocument>();
            var root = document && document.isActiveAndEnabled ? document.rootVisualElement : null;
            if (ReferenceEquals(root, _root) && _element is not null
                && (ReferenceEquals(root, _element) || root.Contains(_element))) return;

            _isRefreshing = true;
            try
            {
                ReleaseElement();
                _root = root;
                if (root is null)
                {
                    if (reportMissing && !document) Resolve();
                    return;
                }
                _element = Resolve(reportMissing);
                if (_element is null) return;
                _element.RegisterCallback<DetachFromPanelEvent>(OnElementLeftPanel);
                if (restore) RestoreElement();
                if (_isAttached) OnElementAttached(_element);
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void OnElementLeftPanel(DetachFromPanelEvent evt)
        {
            if (ReferenceEquals(evt.target, _element)) ReleaseElement();
        }

        private void ReleaseElement()
        {
            var element = _element;
            _element = null;
            _root = null;
            if (element is null) return;
            element.UnregisterCallback<DetachFromPanelEvent>(OnElementLeftPanel);
            OnElementDetached(element);
        }

        private TElement Resolve(bool reportMissing = true)
        {
            var document = _document ? _document : GetComponentInParent<UIDocument>();

            if (!document)
                return Refuse("no UIDocument is assigned or found on this object and its parents");

            if (document.rootVisualElement is not { } root)
                return Refuse("the document has no visual tree yet");

            var hasName = !string.IsNullOrWhiteSpace(_elementName);
            var hasClass = !string.IsNullOrWhiteSpace(_elementClass);

            if (!hasName && !hasClass)
                return Refuse("neither an element name nor a USS class is set");

            var element = hasName
                ? root.Q<TElement>(_elementName)
                : root.Q<TElement>(className: _elementClass);

            if (element is not null) return element;

            var query = hasName ? $"named '{_elementName}'" : $"with class '{_elementClass}'";
            return reportMissing ? Refuse($"the document holds no {typeof(TElement).Name} {query}") : null;
        }

        private TElement Refuse(string problem)
        {
            this.LogError(
                problem: problem,
                consequence: "The binder does nothing.");

            return null;
        }
    }
}
#endif
