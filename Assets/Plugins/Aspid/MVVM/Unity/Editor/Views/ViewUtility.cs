#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Utility class for handling the management, validation, and assignment of `IMonoBinderValidable` objects within views.
    /// Provides helper methods for operations such as identifying changed binders, validating binder assignments,
    /// and automatically setting or removing binders based on specific criteria.
    /// </summary>
    /// <remarks>
    /// This class is primarily used in the context of MVVM architectures where `IView` implementations
    /// contain various binders that need to be dynamically updated, validated, or synchronized.
    /// It simplifies complex binder operations and ensures that the view's binders are in a consistent state.
    /// </remarks>
    public static class ViewUtility
    {
        #region Validate Methods
        /// <summary>
        /// Finds all validable binders among the child objects of the view.
        /// </summary>
        /// <typeparam name="TView">The type of view that implements IView.</typeparam>
        /// <param name="view">The view in which to search.</param>
        public static void ValidateView<TView>(TView view)
            where TView : Component, IView
        {
            var fields = view.GetMonoBinderValidableFields();
            
            var bindersOnScene = view.GetComponentsInChildren<IMonoBinderValidable>(true)
                .Where(binder => binder.View is Component binderView && binderView == view).ToArray();
            
            foreach (var field in fields)
            {
                var id = field.GetBinderId();
                
                var assignBinders = field.GetValueAsArray<IMonoBinderValidable>(view);
                var binders = bindersOnScene.Where(binder => id == binder.Id).ToArray();

                if (!EqualsBinders(assignBinders, binders))
                    field.SetValueFromCastValueAndSaveView(view, binders);
            }
            
            ValidateBindersInView(view);
            return;

            bool EqualsBinders(IMonoBinderValidable[]? array1, IMonoBinderValidable[]? array2)
            {
                if ((array1 is null || array1.Length is 0) && (array2 is null || array2.Length is 0)) return true;
                if (array1 is null || array2 is null) return false;
                if (array1.Length != array2.Length) return false;
                
                var hasSet = array2.ToHashSet();
                return array1.All(hasSet.Contains);
            }
        }
        
        /// <summary>
        /// Validates the changed binders in the view. Removes old binders that are no longer in use
        /// and deletes duplicate binders with the same ID.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        /// <param name="oldBinders">The previous binders saved in the view.</param>
        /// <param name="newBinders">The new binders that have been added or modified.</param>
        public static bool ValidateViewChanges(IView view, ValidableBindersById oldBinders, ValidableBindersById newBinders)
        {
            var changedFields = ChangedBinder.GetChangedBinders(oldBinders, newBinders);
            if (changedFields.Length is 0) return false;
            
            foreach (var changedBinder in changedFields)
            {
                ResetRemovedBinders(in changedBinder);
                DeleteDuplicates(in changedBinder);
            }
            
            ValidateBindersInView(view);
            return true;

            void ResetRemovedBinders(in ChangedBinder changedBinder)
            {
                foreach (var oldBinder in changedBinder.OldBinders)
                {
                    if (oldBinder is null) continue;
                    if (changedBinder.NewBinders.Contains(oldBinder)) continue;
                    
                    oldBinder.Id = null;
                }
            }
            
            void DeleteDuplicates(in ChangedBinder changedBinder)
            {
                var id = changedBinder.Id;
                if (!view.TryGetMonoBinderValidableFieldById(id, out var field)) return;
                
                var requiredTypes = field!.GetRequiredTypes();
                var validBinders = ValidNewBinders.Valid(view, changedBinder);

                foreach (var binder in validBinders.Binders)
                {
                    if (binder is null) continue;
                    if (string.IsNullOrWhiteSpace(binder.Id)) continue;
                        
                    if ((binder.View != view || binder.Id != id) && requiredTypes.IsBinderMatchRequiredType(binder))
                        RemoveBinderIfExist(binder);
                }
                    
                var oldCount = validBinders.Binders.Length;
                var distinctBinders = validBinders.Binders.Distinct().ToArray();
                var newCount = distinctBinders.Length;

                var delta = oldCount - newCount;
                if (delta > 0)
                {
                    Array.Resize(ref distinctBinders, distinctBinders.Length + delta);
                    for (var i = 1; i < delta + 1; i++)
                        distinctBinders[^i] = null;
                    
                    field!.SetValueFromCastValueAndSaveView(view, distinctBinders);
                }
                else if (validBinders.IsChanged)
                {
                    field!.SetValueFromCastValueAndSaveView(view, distinctBinders);
                }
            }
        }
        
        private static void ValidateBindersInView(IView view)
        {
            var fields = view.GetMonoBinderValidableFields();
            
            foreach (var field in fields)
            {
                var binders = field.FieldType.IsArray
                    ? (IMonoBinderValidable[])field.GetValue(view)
                    : new[] { (IMonoBinderValidable)field.GetValue(view) };
                
                var binderCount = binders?.Length ?? 0;
                if (binderCount is 0) continue;

                if (field.GetRequiredTypes().Any())
                {
                    binders = binders!.Where(binder => 
                        {
                            if (binder is null) return true;

                            var isChild = IsBinderInViewScope(view, binder);
                            var result = isChild && field.GetRequiredTypes().IsBinderMatchRequiredType(binder);

                            if (!result && isChild)
                                binder.Id = null;

                            return result;
                        })
                        .ToArray();
                }
                
                var id = field.GetBinderId();
 
                foreach (var binder in binders!)
                {
                    if (binder is null) continue;
                    if (binder.Id == id && binder.View == view) continue;
                    
                    binder.Id = id;
                    binder.View = view;
                }

                if (binderCount != binders.Length)
                    field.SetValueFromCastValueAndSaveView(view, binders);
            }
        }   
        #endregion

        #region SetBinderIfNotExist Methods
        /// <summary>
        /// Adds a binder to the view if it is not already set.
        /// </summary>
        /// <param name="binder">The binder to add.</param>
        public static void SetBinderIfNotExist(IMonoBinderValidable binder)
        {
            if (string.IsNullOrWhiteSpace(binder.Id))
                throw new NullReferenceException(nameof(binder.Id));
            
            if (binder.View is null || (binder is Component component && !component))
                throw new NullReferenceException(nameof(binder.View));
            
            SetBinderIfNotExist(binder.View, binder, binder.Id!);
        } 
        
        /// <summary>
        /// Adds a binder to the view if it is not already set.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        /// <param name="binder">The binder to add.</param>
        /// <param name="id">The ID of the binder field for adding the binder.</param>
        public static void SetBinderIfNotExist(IView view, IMonoBinderValidable binder, string id)
        {
            var field = view.GetMonoBinderValidableFieldById(id);
            field.ThrowExceptionIfMonoBinderValidableFieldIsNull(view, id);
            
            var viewBinders = field!.GetValueAsArray<IMonoBinderValidable>(view);
        
            if (viewBinders is null)
            {
                field!.SetValueFromCastValueAndSaveView(view, binder);
                return;
            }
            if (viewBinders.Any(viewBinder => viewBinder == binder)) return;
            
            if (!field!.FieldType.IsArray)
            {
                if (viewBinders.Length <= 0) return;
                
                viewBinders[0].Id = null;
                field.SetValueFromCastValueAndSaveView(view, binder);
            }
            else
            {
                Array.Resize(ref viewBinders, viewBinders.Length + 1);
                viewBinders[^1] = binder;
                field.SetValueFromCastValueAndSaveView(view, viewBinders);
            }
        }
        #endregion

        #region RemoveBinderIfExist Methods
        /// <summary>
        /// Removes a binder if it is already set in the specified view.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        public static void RemoveBinderIfExist(IMonoBinderValidable binder)
        {
            if (string.IsNullOrWhiteSpace(binder.Id))
                throw new NullReferenceException(nameof(binder.Id));
            
            if (binder.View is null || (binder is Component component && !component))
                throw new NullReferenceException(nameof(binder.View));
            
            RemoveBinderIfExist(binder.View, binder, binder.Id!);
        }

        /// <summary>
        /// Removes a binder if it is already set in the specified view.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        /// <param name="binder">The binder to be removed.</param>
        /// <param name="id">The ID of the binder field to remove.</param>
        public static void RemoveBinderIfExist(IView view, IBinder binder, string id)
        {
            var field = view.GetMonoBinderValidableFieldById(id);
            field.ThrowExceptionIfMonoBinderValidableFieldIsNull(view, id);

            var viewBinders = field!.GetValueAsArray<IMonoBinderValidable>(view);
            if (viewBinders is null) return;

            if (field!.FieldType.IsArray)
            {
                viewBinders = viewBinders.Where(viewBinder => viewBinder != binder).ToArray();
                field.SetValueFromCastValueAndSaveView(view, viewBinders);
            }
            else
            {
                if (viewBinders.Length is 0 || viewBinders[0] != binder) return;
                field.SetValueFromCastValueAndSaveView<IMonoBinderValidable>(view, null);
            }
        }
        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SaveView(this IView view)
        {
            if (Application.isPlaying) return;
            if (view is not Component component) return;
            
            EditorUtility.SetDirty(component);
            EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBinderInViewScope(this IView view, IBinder binder)
        {
            if (view is not Component monoView || binder is not Component monoBinder) return false;
            return monoBinder.transform.IsChildOf(monoView.transform) || monoBinder.transform == monoView.transform;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValueFromCastValueAndSaveView<T>(this FieldInfo field, IView view, params T?[]? binders)
            where T : IBinder
        {
            field.SetValueFromCastValue(view, binders);
            view.SaveView();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowExceptionIfMonoBinderValidableFieldIsNull(this FieldInfo? field, IView view, string id)
        {
            if (field is null)
                throw new Exception($"Could not find IMonoBinderValidable by name {id} in view {view.GetType().Name}");
        }

        private readonly struct ChangedBinder
        {
            public readonly string Id;
            public readonly IMonoBinderValidable?[] OldBinders;
            public readonly IMonoBinderValidable?[] NewBinders;

            private ChangedBinder(string id, IMonoBinderValidable?[] oldBinders, IMonoBinderValidable?[] newBinders)
            {
                Id = id;
                OldBinders = oldBinders;
                NewBinders = newBinders;
            }
            
            public static ReadOnlySpan<ChangedBinder> GetChangedBinders(ValidableBindersById oldBinders, ValidableBindersById newBinders)
            {
                // TODO Write message in Exception
                if (oldBinders.Count != newBinders.Count) throw new Exception();
                var changedFields = new List<ChangedBinder>(oldBinders.Count);

                foreach (var key in oldBinders.Keys)
                {
                    var oldValue = oldBinders[key] ?? Array.Empty<IMonoBinderValidable>();
                    var newValue = newBinders[key] ?? Array.Empty<IMonoBinderValidable>();
                    if (oldValue.SequenceEqual(newValue)) continue;
                
                    changedFields.Add(new ChangedBinder(BinderFieldInfoExtensions.GetBinderId(key), oldValue, newValue));
                }

                return changedFields.ToArray().AsSpan();
            }
        }

        private ref struct ValidNewBinders
        {
            public bool IsChanged { get; private set; }
            
            public IMonoBinderValidable?[] Binders { get; private set; }
            
            public static ValidNewBinders Valid(IView view, in ChangedBinder changedBinder)
            {
                var id = changedBinder.Id;
                var result = new ValidNewBinders();
                if (!view.TryGetMonoBinderValidableFieldById(id, out var field)) return result;
                
                var oldBinders = changedBinder.OldBinders.ToHashSet();
                var requiredTypes = field!.GetRequiredTypes();
                
                var validatingBinders = changedBinder.NewBinders.ToArray();
                var validatedBinders = new HashSet<IMonoBinderValidable>(validatingBinders.Length);

                for (var i = 0; i < validatingBinders.Length; i++)
                {
                    var validatingBinder = validatingBinders[i];
                    if (validatingBinder is null) continue;
                    
                    var isOldBinder = oldBinders.Contains(validatingBinder);
                    if (isOldBinder && validatedBinders.Add(validatingBinder)) continue;

                    if (!isOldBinder)
                    {
                        if (validatingBinder.View == view && validatingBinder.Id == id) continue;
                        if (requiredTypes.IsBinderMatchRequiredType(validatingBinder)) continue;

                        if (!view.IsBinderInViewScope(validatingBinder))
                        {
                            result.IsChanged = true;
                            validatingBinders[i] = null;
                            continue;
                        }
                    }
                    
                    result.IsChanged = true;
                    IMonoBinderValidable? candidateBinder = null;
                    
                    if (validatingBinder is Component component)
                    { 
                        candidateBinder = component.GetComponents<IMonoBinderValidable>()
                            .FirstOrDefault(candidate => (candidate.View != view || candidate.Id != id) && requiredTypes.IsBinderMatchRequiredType(candidate));
                    }
                    
                    validatingBinders[i] = candidateBinder;
                }

                result.Binders = validatingBinders;
                return result;
            }
        }
    }
}