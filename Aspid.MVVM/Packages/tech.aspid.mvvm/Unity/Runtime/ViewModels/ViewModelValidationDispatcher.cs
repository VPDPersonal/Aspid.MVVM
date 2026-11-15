#if UNITY_EDITOR
#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class ViewModelValidationDispatcher
    {
        private static bool _isNotifying;
        private static bool _isTransitioning;
        private static readonly HashSet<Object> _notified = new();
        private static ConcurrentQueue<(Object Target, Action<Object> Notify)> _pending = new();

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.update -= NotifyPending;
            EditorApplication.update += NotifyPending;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        public static void Enqueue(Object target, Action<Object> notify) =>
            Volatile.Read(ref _pending).Enqueue((target, notify));

        private static void NotifyPending()
        {
            if (_isNotifying || _isTransitioning) return;
            var pending = Volatile.Read(ref _pending);

            var count = pending.Count;
            if (count is 0) return;

            _isNotifying = true;
            try
            {
                for (var i = 0; i < count && pending.TryDequeue(out var request); i++)
                {
                    if (!ReferenceEquals(pending, Volatile.Read(ref _pending))) break;
                    if (!request.Target || !_notified.Add(request.Target)) continue;

                    try
                    {
                        request.Notify(request.Target);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogException(exception, request.Target);
                    }
                }
            }
            finally
            {
                _notified.Clear();
                _isNotifying = false;
            }
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode or PlayModeStateChange.ExitingPlayMode:
                    _isTransitioning = true;
                    Interlocked.Exchange(
                        location1: ref _pending,
                        value: new ConcurrentQueue<(Object Target, Action<Object> Notify)>());
                    break;

                case PlayModeStateChange.EnteredEditMode or PlayModeStateChange.EnteredPlayMode:
                    _isTransitioning = false;
                    break;
            }
        }
    }
}
#endif
