#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Mono
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
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;

        #region Validate Methods
        /// <summary>
        /// Finds all validable binders among the child objects of the view.
        /// </summary>
        /// <typeparam name="TView">The type of view that implements IView.</typeparam>
        /// <param name="view">The view in which to search.</param>
        public static void ValidateView<TView>(TView view)
            where TView : Component, IView
        {
            var fields = GetValidableBinderFields(view);
            
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
                foreach (var newBinder in changedBinder.NewBinders)
                {
                    if (newBinder is null) continue;
                    if (string.IsNullOrWhiteSpace(newBinder.Id)) return;

                    var isOldBinder = changedBinder.OldBinders.Contains(newBinder);
                    if (!isOldBinder && view.IsBinderInViewScope(newBinder))
                        RemoveBinderIfExist(newBinder);

                    var field = GetValidableBinderFieldById(view, newBinder.Id!);
                    var viewBinders = field?.GetValueAsArray<IMonoBinderValidable?>(view);
                    if (viewBinders is null) continue;

                    var oldCount = viewBinders.Length;
                    var distinctViewBinders = viewBinders.Distinct().ToList();
                    var newCount = distinctViewBinders.Count;

                    var delta = oldCount - newCount;
                    if (delta <= 0) continue;
                    
                    for (var i = 0; i < delta; i++)
                        distinctViewBinders.Add(null);
                        
                    field!.SetValueFromCastValueAndSaveView(view, distinctViewBinders.ToArray());
                }
            }
        }
        
        private static void ValidateBindersInView(IView view)
        {
            var fields = GetValidableBinderFields(view);
            
            foreach (var field in fields)
            {
                var binders = field.FieldType.IsArray
                    ? (IMonoBinderValidable[])field.GetValue(view)
                    : new[] { (IMonoBinderValidable)field.GetValue(view) };
                
                var binderCount = binders?.Length ?? 0;
                if (binderCount is 0) continue;
                
                if (field.GetCustomAttributes<RequireBinderAttribute>(false) is { } requireAttributes)
                {
                    var requiredTypes = requireAttributes.Select(attribute => attribute.Type);
        
                    binders = binders!.Where(binder => 
                        {
                            if (binder is null) return true;

                            var isChild = IsBinderInViewScope(view, binder);
                            var result = isChild && BinderMatchRequiredType(requiredTypes, binder);

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
            var field = GetValidableBinderFieldById(view, id);
            field.ThrowExceptionIfFieldNull(view, id);
            
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
            var field = GetValidableBinderFieldById(view, id);
            field.ThrowExceptionIfFieldNull(view, id);

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
                field.SetValueAndSave(view, null);
            }
        }
        #endregion
        
        /// <summary>
        /// Cleans a field in the view based on the field's identifier.
        /// If the field is an array, it filters out invalid or null Unity objects
        /// and updates the field with the filtered array. Otherwise, the field is set to `null`.
        /// </summary>
        /// <param name="view">The view instance containing the field to be cleaned.</param>
        /// <param name="id">The identifier of the field to clean.</param>
        public static void CleanViewField(IView view, string id)
        {
            var field = GetValidableBinderFieldById(view, id);
            field.ThrowExceptionIfFieldNull(view, id);
            
            if (field!.FieldType.IsArray)
            {
                var binders = new List<IMonoBinderValidable>();
                binders.AddRange(((IMonoBinderValidable[])field.GetValue(view)).Where(binder =>
                {
                    var result = binder is not null;

                    if (result && binder is UnityEngine.Object mono && !mono)
                        result = false;

                    return result;
                }));
                
                field.SetValueFromCastValueAndSaveView(view, binders.ToArray());
            }
            else
            {
                field.SetValueAndSave(view, null);
            }
        }
        
        /// <summary>
        /// Retrieves all `IMonoBinderValidable` binders from a view and associates them with the field names they are assigned to.
        /// </summary>
        /// <param name="view">The view object containing the binders.</param>
        /// <returns>
        /// A dictionary where the key is the field name and the value is an array of `IMonoBinderValidable` associated with that field.
        /// </returns>
        public static ValidableBindersById GetValidableBindersById(IView view)
        {
            var fields = GetValidableBinderFields(view);
            var bindersByFieldName = new ValidableBindersById();

            foreach (var field in fields)
            {
                var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(view);
                
                if (viewBinders is { Length: > 0 })
                {
                    var copyViewBinders = new IMonoBinderValidable[viewBinders.Length];
                    viewBinders.CopyTo(copyViewBinders, 0);
                    
                    bindersByFieldName.Add(field.Name, copyViewBinders);
                }
                else
                {
                    bindersByFieldName.Add(field.Name, viewBinders);
                }
            }
            
            return bindersByFieldName;
        }
        
        #region Get Fields Method
        /// <summary>
        /// Retrieves field in the specified type that is of type `IMonoBinderValidable` or `IMonoBinderValidable[]` by id.
        /// </summary>
        /// <param name="view">The type to inspect for `IMonoBinderValidable` fields.</param>
        /// <param name="id">The name of fields.</param>
        /// <returns>
        /// FieldInfo` objects representing the field that contain `IMonoBinderValidable` binders.
        /// </returns>
        public static FieldInfo? GetValidableBinderFieldById(IView view, string id)
        {
            return GetValidableBinderFields(view)
                .FirstOrDefault(field => field.GetBinderId() == id);
        }
        
        /// <summary>
        /// Retrieves all fields in the specified type that are of type `IMonoBinderValidable` or `IMonoBinderValidable[]`.
        /// Includes fields from base classes as well.
        /// </summary>
        /// <param name="view">The type to inspect for `IMonoBinderValidable` fields.</param>
        /// <returns>
        /// A collection of `FieldInfo` objects representing the fields that contain `IMonoBinderValidable` binders.
        /// </returns>
        public static IEnumerable<FieldInfo> GetValidableBinderFields(IView view)
        {
            return view.GetType().GetFieldInfosIncludingBaseClasses(BindingFlags).Where(field => 
            {
                var fieldType = field.FieldType;
                return typeof(IMonoBinderValidable).IsAssignableFrom(fieldType) 
                    || typeof(IMonoBinderValidable[]).IsAssignableFrom(fieldType);
            });
        }   
        #endregion
        
        #region BinderMatchRequiredType Methods
        /// <summary>
        /// Checks if the given binder matches the required types specified by a collection of RequireBinderAttribute.
        /// This method extracts the types from the attributes and delegates the type-checking logic to the overloaded method.
        /// </summary>
        /// <param name="attributes">A collection of RequireBinderAttribute, which contain the required types.</param>
        /// <param name="binder">The binder object to check.</param>
        /// <returns>True if the binder matches any of the required types; otherwise, false.</returns>
        public static bool BinderMatchRequiredType(IEnumerable<RequireBinderAttribute> attributes, object binder) =>
            BinderMatchRequiredType(attributes.Select(attribute => attribute.Type), binder);
        
        /// <summary>
        /// Checks if the given binder implements an interface (either IBinder or IReverseBinder) 
        /// whose generic type argument is compatible with any of the required types.
        /// </summary>
        /// <param name="requiredTypes">A collection of required types to match against the binder's interfaces.</param>
        /// <param name="binder">The binder object to check.</param>
        /// <returns>True if the binder matches any of the required types; otherwise, false.</returns>
        public static bool BinderMatchRequiredType(IEnumerable<Type> requiredTypes, object binder)
        {
            return binder.GetType().GetInterfaces().Any(@interface =>
            {
                if (!@interface.IsGenericType) return false;
                if (@interface.GetGenericTypeDefinition() != typeof(IBinder<>) 
                    && @interface.GetGenericTypeDefinition() != typeof(IReverseBinder<>)) return false;

                return requiredTypes.Any(requiredType =>
                    @interface.GetGenericArguments()[0].IsAssignableFrom(requiredType));
            });
        }
        #endregion

        #region Field SetValue Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValueAndSave(this FieldInfo field, IView view, object? value)
        {
            field.SetValue(field, value);
            view.SaveView();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValueFromCastValueAndSaveView<T>(this FieldInfo field, IView view, params T?[] binders)
            where T : IBinder
        {
            field.SetValueFromCastValue(view, binders);
            view.SaveView();
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
        private static void ThrowExceptionIfFieldNull(this FieldInfo? field, IView view, string id)
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
                // TODO Write message from Exception
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
    }
}