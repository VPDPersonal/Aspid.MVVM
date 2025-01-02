using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.MVVM.Views;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using Aspid.MVVM.Mono.Views.Extensions;

namespace Aspid.MVVM.Mono.Views
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
        
        /// <summary>
        /// Validates the changed binders in the view. Removes old binders that are no longer in use
        /// and deletes duplicate binders with the same ID.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        /// <param name="oldBinders">The previous binders saved in the view.</param>
        /// <param name="newBinders">The new binders that have been added or modified.</param>
        public static void ValidateMonoBinderValidablesInView(
            IView view,
            BindersWithFieldName oldBinders,
            BindersWithFieldName newBinders)
        {
            var changedFields = GetChangedBinders(oldBinders, newBinders);
            if (changedFields.Length == 0) return;
            
            foreach (var changedBinder in changedFields)
            {
                SetBinderDefault(changedBinder);
                DeleteDuplicateMonoBinderValidable(changedBinder);
            }
            
            ValidateMonoBinderValidableByType(view);
            return;

            void SetBinderDefault(in ChangedBinder changedBinder)
            {
                foreach (var oldBinder in changedBinder.OldBinders)
                {
                    if (oldBinder == null) continue;
                    if (changedBinder.NewBinders.Contains(oldBinder)) continue;

                    if (oldBinder.IsMonoExist)
                        oldBinder.Reset();
                }

                foreach (var newBinder in changedBinder.NewBinders)
                {
                    if (!IsMonoBinderValidableChild(view, newBinder))
                    {
                        if (newBinder.IsMonoExist)
                            newBinder.Reset();
                    }
                }
            }
            
            void DeleteDuplicateMonoBinderValidable(in ChangedBinder changedBinder)
            {
                foreach (var newBinder in changedBinder.NewBinders)
                {
                    if (newBinder == null) continue;
                    if (string.IsNullOrEmpty(newBinder.Id)) return;
                    
                    if (!changedBinder.OldBinders.Contains(newBinder))
                        RemoveMonoBinderIfSet(view, newBinder, newBinder.Id);
                    
                    var field = GetMonoBinderValidableFields(view.GetType())
                        .FirstOrDefault(field => GetIdName(field.Name) == newBinder.Id);
                    if (field == null) continue;
            
                    var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(view);
                    if (viewBinders == null) continue;
            
                    field.SetValueFromCastValue(view, viewBinders.Distinct().ToArray());
                }
            }
        }
        
        /// <summary>
        /// Performs validation of the binders based on attributes and data types.
        /// Automatically associates binders with their view.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        public static void ValidateMonoBinderValidableByType(IView view)
        {
            var isChanged = false;
            var fields = GetMonoBinderValidableFields(view.GetType());
            
            foreach (var field in fields)
            {
                var isArray = field.FieldType.IsArray;
                var isRequire = Attribute.IsDefined(field, typeof(RequireBinderAttribute));
        
                var binders = isArray
                    ? (IMonoBinderValidable[])field.GetValue(view)
                    : new[] { (IMonoBinderValidable)field.GetValue(view) };
                if (binders == null || binders.Length == 0) continue;
        
                if (isRequire)
                {
                    var requiredTypes = field.GetCustomAttributes(typeof(RequireBinderAttribute), false)
                        .Select(attribute => ((RequireBinderAttribute)attribute).Type);
        
                    var binderCount = binders.Length;
                    binders = binders.Where(binder =>
                        {
                            if (binder == null) return true;
        
                            var interfaces = binder.GetType()
                                .GetInterfaces();

                            var result = IsMonoBinderValidableChild(view, binder);
                            
                            result = result && interfaces.Any(i =>
                                i.IsGenericType
                                && (i.GetGenericTypeDefinition() == typeof(IBinder<>) || i.GetGenericTypeDefinition() == typeof(IReverseBinder<>) )
                                && requiredTypes.Any(requiredType =>
                                    requiredType == i.GetGenericArguments()[0]));
                            
                            if (!result)
                            {
                                if (binder.IsMonoExist)
                                    binder.Reset();
                                
                                isChanged = true;
                            }

                            return result;
                        })
                        .ToArray();

                    if (binderCount != binders.Length) isChanged = true;
                }
        
                var name = GetIdName(field.Name);
                foreach (var binder in binders)
                {
                    if (binder == null) continue;
                    if (binder.Id == name && binder.View == view) continue;
                    
                    binder.Id = name;
                    binder.View = view;
                    isChanged = true;
                }
                
                field.SetValueFromCastValue(view, binders);
            }
            
            if (isChanged) 
                SaveView(view);
        }
        
        /// <summary>
        /// Removes a binder if it is already set in the specified view.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        /// <param name="binder">The binder to be removed.</param>
        /// <param name="id">The ID of the binder field to remove.</param>
        public static void RemoveMonoBinderIfSet(IView view, IMonoBinderValidable binder, string id)
        {
	        var field = GetFieldInfoById(view, id);
            
            if (field == null)
            {
                Debug.LogError($"Could not find IMonoBinderValidable by name {id} in view {view.GetType().Name}");
                return;
            }
            
            var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(view);
            if (viewBinders == null) return;
            
            if (!field.FieldType.IsArray)
            {
                if (viewBinders.Length == 0 || viewBinders[0] != binder) return;
                field.SetValue(view, null);
                SaveView(view);
                return;
            }
        
            viewBinders = viewBinders.Where(viewBinder => viewBinder != binder).ToArray();
            field.SetValueFromCastValue(view, viewBinders);
            SaveView(view);
        }
        
        /// <summary>
        /// Adds a binder to the view if it is not already set.
        /// </summary>
        /// <param name="view">The view containing the binders.</param>
        /// <param name="binder">The binder to add.</param>
        /// <param name="id">The ID of the binder field for adding the binder.</param>
        public static void SetMonoBinderIfNotSet(IView view, IMonoBinderValidable binder, string id)
        {
	        var field = GetFieldInfoById(view, id);
        
            if (field == null)
            {
                Debug.LogError($"Could not find IMonoBinderValidable by name {id} in view {view.GetType().Name}");
                return;
            }
            
            var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(view);
        
            if (viewBinders == null)
            {
                field.SetValueFromCastValue(view, binder);
                SaveView(view);
                return;
            }
        
            if (viewBinders.Any(viewBinder => viewBinder == binder)) return;
            
            if (!field.FieldType.IsArray)
            {
                if (viewBinders.Length <= 0) return;
                
                viewBinders[0].Id = null;
                field.SetValueFromCastValue(view, binder);
            }
            else
            {
                Array.Resize(ref viewBinders, viewBinders.Length + 1);
                viewBinders[^1] = binder;
                field.SetValueFromCastValue(view, viewBinders);
            }

            SaveView(view);
        }
        
        /// <summary>
        /// Finds all validable binders among the child objects of the view.
        /// </summary>
        /// <typeparam name="TView">The type of view that implements IView.</typeparam>
        /// <param name="view">The view in which to search.</param>
        public static void FindAllMonoBinderValidableInChildren<TView>(TView view)
            where TView : Component, IView
        {
            var fields = GetMonoBinderValidableFields(view.GetType());
            
            var bindersOnScene = view.GetComponentsInChildren<IMonoBinderValidable>(true)
                .Where(binder => binder.View is Component binderView && binderView == view).ToArray();
            
            foreach (var field in fields)
            {
                var id = GetIdName(field.Name);
                var binders = bindersOnScene.Where(binder => id == binder.Id).ToArray();
        
                field.SetValueFromCastValue(view, binders);
            }
        
            ValidateMonoBinderValidableByType(view);
        }
        
        /// <summary>
        /// Retrieves all `IMonoBinderValidable` binders from a view and associates them with the field names they are assigned to.
        /// </summary>
        /// <param name="view">The view object containing the binders.</param>
        /// <returns>
        /// A dictionary where the key is the field name and the value is an array of `IMonoBinderValidable` associated with that field.
        /// </returns>
        public static BindersWithFieldName GetMonoBinderValidableWithFieldName(IView view)
        {
            var fields = GetMonoBinderValidableFields(view.GetType());
            var bindersByFieldName = new BindersWithFieldName();

            foreach (var field in fields)
            {
                var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(view);
                bindersByFieldName.Add(field.Name, viewBinders);
            }
            
            return bindersByFieldName;
        }
        
        /// <summary>
        /// Retrieves all fields in the specified type that are of type `IMonoBinderValidable` or `IMonoBinderValidable[]`.
        /// Includes fields from base classes as well.
        /// </summary>
        /// <param name="type">The type to inspect for `IMonoBinderValidable` fields.</param>
        /// <returns>
        /// A collection of `FieldInfo` objects representing the fields that contain `IMonoBinderValidable` binders.
        /// </returns>
        public static IEnumerable<FieldInfo> GetMonoBinderValidableFields(Type type)
        {
            return type.GetFieldInfosIncludingBaseClasses(BindingFlags).Where(field => 
            {
                var fieldType = field.FieldType;
                return typeof(IMonoBinderValidable).IsAssignableFrom(fieldType) 
                    || typeof(IMonoBinderValidable[]).IsAssignableFrom(fieldType);
            });
        }

        public static bool IsMonoBinderValidableChild(IView view, IMonoBinderValidable binder)
        {
            if (view is not Component monoView || binder is not Component monoBinder) return true;
            return monoBinder.transform.IsChildOf(monoView.transform) || monoBinder.transform == monoView.transform;
        }
        
        /// <summary>
        /// Generates an ID for a binder based on its field name.
        /// Removes common prefixes like "_" or "m_" and ensures the first character is uppercase.
        /// </summary>
        /// <param name="fieldName">The original field name of the binder.</param>
        /// <returns>The processed ID string based on the field name.</returns>
        public static string GetIdName(string fieldName)
        {
            var prefixCount = GetPrefixCount();
            fieldName = fieldName.Remove(0, prefixCount);
        
            var firstSymbol = fieldName[0];
            if (char.IsLower(firstSymbol))
            {
                fieldName = fieldName.Remove(0, 1);
                fieldName = char.ToUpper(firstSymbol) + fieldName;
            }
        
            return fieldName;
        
            int GetPrefixCount() => fieldName.StartsWith("_") ? 1 : fieldName.StartsWith("m_") ? 2 : 0;
        }

        public static FieldInfo GetFieldInfoById(IView view, string id)
        {
	        return GetMonoBinderValidableFields(view.GetType())
		        .FirstOrDefault(field => GetIdName(field.Name) == id);
        }
        
        private static void SaveView(IView view)
        {
            if (Application.isPlaying) return;
            if (view is not Component component) return;
            
            EditorUtility.SetDirty(component);
            EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
        }
        
        /// <summary>
        /// Identifies fields whose binders have changed between two sets of `BindersWithFieldName`.
        /// </summary>
        /// <param name="oldBinders">The previous set of binders.</param>
        /// <param name="newBinders">The new set of binders to compare against the old ones.</param>
        /// <returns>
        /// A read-only list of `ChangedBinder` instances representing the fields where the binders have changed.
        /// </returns>
        private static ReadOnlySpan<ChangedBinder> GetChangedBinders(
            BindersWithFieldName oldBinders, 
            BindersWithFieldName newBinders)
        {
            if (oldBinders.Count != newBinders.Count) 
                throw new Exception();
            
            var changedFields = new List<ChangedBinder>(oldBinders.Count);

            foreach (var key in oldBinders.Keys)
            {
                var oldValue = oldBinders[key] ?? Array.Empty<IMonoBinderValidable>();
                var newValue = newBinders[key] ?? Array.Empty<IMonoBinderValidable>();
                if (oldValue == newValue) continue;
                
                changedFields.Add(new ChangedBinder(oldValue, newValue));
            }

            return changedFields.ToArray().AsSpan();
        }

        /// <summary>
        /// Represents the data structure for tracking changes to binders between two sets of values.
        /// </summary>
        private readonly struct ChangedBinder
        {
            public readonly IMonoBinderValidable[] OldBinders;
            public readonly IMonoBinderValidable[] NewBinders;

            public ChangedBinder(IMonoBinderValidable[] oldBinders, IMonoBinderValidable[] newBinders)
            {
                OldBinders = oldBinders;
                NewBinders = newBinders;
            }
        }
    }
}