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
    public static class ViewAndMonoBinderSyncValidator
    {
        #region Validate Methods
        public static void ValidateView<TView>(TView view)
            where TView : Component, IView
        {
            // Сначала необходимо провалидировать текущие биндеры.
            ValidateBinderFromView(view);
            
            // Получаем все поля, подходящие для валидируемых биндеров.
            var fields = view.GetRequireBinderFields();
            
            // Получаем все дочерние к данной view, валидируемые биндеры, которые связаны с данной view. 
            var bindersOnScene = view.GetComponentsInChildren<IMonoBinderValidable>(includeInactive: true)
                .Where(binder => binder.View is Component binderView && binderView == view).ToArray();
            
            foreach (var field in fields)
            {
                if (!field.IsValidation()) continue;
                
                var assignedBinders = field.GetValueAsArray<IMonoBinderValidable>(field.FieldContainerObj);
                var binders = bindersOnScene.Where(binder => field.Id == binder.Id).ToArray();

                // Если присоединенные биндеры и биндеры на сцене не совпадают, то установить новые присоединенные биндеры. 
                if (!EqualsBinders(assignedBinders, binders))
                    field.SetValueFromCastValueAndSaveView(view, binders);
            }
            
            // Для полной валидации так же необходимо провалидировать новые биндеры.
            ValidateBinderFromView(view);
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

        public static void ValidateViewChanges(IView view, ValidableBindersById oldBinders, ValidableBindersById newBinders)
        {
            var changedBinders = ChangedBinders.GetChangedBinders(oldBinders, newBinders);
            
            // Если Length is 0, то изменений нет.
            if (changedBinders.Length is 0) return;

            foreach (var changedBinder in changedBinders)
            {
                ResetRemovedBinders(in changedBinder); 
                RemoveDuplicateBinders(in changedBinder);
            }

            // Валидируем биндеры установленные во View.
            ValidateBinderFromView(view);
            return;
            
            // Сбрасываем Id у удаленных биндеров
            void ResetRemovedBinders(in ChangedBinders changedBinder)
            {
                foreach (var oldBinder in changedBinder.OldBinders)
                {
                    if (oldBinder is null) continue;
                    if (changedBinder.NewBinders.Contains(oldBinder)) continue;
                    
                    oldBinder.Id = null;
                    if (view.IsBinderInViewScope(oldBinder)) oldBinder.View = null;
                }
            }

            void RemoveDuplicateBinders(in ChangedBinders changedBinder)
            {
                var id = changedBinder.Id;
                if (!view.TryGetRequireBinderFieldsById(id, out var field)) return;
                if (field is null) throw new NullReferenceException(nameof(field));

                var validBinders = ValidNewBinders.Valid(view, changedBinder);

                foreach (var binder in validBinders.Binders)
                {
                    if (binder is null) continue;
                    if (string.IsNullOrWhiteSpace(binder.Id)) continue;

                    if ((binder.View != view || binder.Id != id) && field.IsBinderMatchRequiredType(binder))
                        RemoveBinderIfExistInView(binder);
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

        private static void ValidateBinderFromView(IView view)
        {
            foreach (var field in view.GetRequireBinderFields())
            {
                // Если это не валидируемое поле, то дальше.
                if (!field.IsValidation()) continue;
                
                var binders = field.GetValueAsArray<IMonoBinderValidable>(field.FieldContainerObj);

                var binderCount = binders.Length;
                if (binderCount is 0) continue;

                binders = binders.Where(binder =>
                {
                    // Если поле пустое, то оставляем его.
                    if (binder is null) return true;
                        
                    var isChild = IsBinderInViewScope(view, binder);
                    
                    // Проверка поддержки типа через IsBinderMatchRequiredType
                    var result = isChild && field.IsBinderMatchRequiredType(binder);

                    if (!result)
                    {
                        binder.Id = null;
                        if (!isChild) binder.View = null;
                    }

                    return result;
                }).ToArray();

                foreach (var binder in binders)
                {
                    if (binder is null) continue; 
                    
                    // Если биндер соответствует полю к которому присоединен, то дальше.
                    if (binder.Id == field.Id && binder.View == view) continue;
                    
                    // Устанавливаем корректные значения для биндера
                    binder.View = view;
                    binder.Id = field.Id;
                }
                
                // Если количество биндеров изменилось, то сохраняем новое значение.
                if (binderCount != binders.Length) 
                    field.SetValueFromCastValueAndSaveView(view, binders);
            }
        }
        #endregion
        
        #region SetBinderIfNotExistInView Methods
        public static void SetBinderIfNotExistInView(IMonoBinderValidable binder)
        {
            // Если Id не установлен, то мы не можем удалить этот биндер,
            // так как мы не знаем что искать
            if (binder.Id is null || string.IsNullOrWhiteSpace(binder.Id))
                throw new NullReferenceException(nameof(binder.Id));
            
            // Если View не установлен, то мы не можем удалить этот биндер,
            // так как мы не знаем где искать
            if (binder.View is null || (binder.View is Component viewComponent && !viewComponent))
                throw new NullReferenceException(nameof(binder.View));
            
            SetBinderIfNotExistInView(binder, binder.View, binder.Id);
        }
        
        public static void SetBinderIfNotExistInView(IMonoBinderValidable binder, IView view, string id)
        {
            var field = view.GetRequireBinderFieldById(id);
            if (field is null) throw new NullReferenceException(nameof(field));

            var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(field.FieldContainerObj);
           
            if (viewBinders is null)
            {
                field.SetValueFromCastValueAndSaveView(view, binder);
                return;
            }
            
            // Если биндер есть, то ничего не делаем.
            if (viewBinders.Any(viewBinder => viewBinder == binder)) return;

            if (viewBinders.Length is 0)
            {
                Array.Resize(ref viewBinders, newSize: 1);
                field.SetValueFromCastValueAndSaveView(view, viewBinders);
            }
            else if (field.FieldType.IsArray)
            {
                // Добавляем биндер в конце массива
                Array.Resize(ref viewBinders, newSize: viewBinders.Length + 1);
                viewBinders[^1] = binder;
                field.SetValueFromCastValueAndSaveView(view, viewBinders);
            }
            else
            {
                // Обнуляем Id у прошлого установленного биндера, который заменим
                viewBinders[0].Id = null;
                field.SetValueFromCastValueAndSaveView(view, binder);
            }
        }
        #endregion
        
        #region RemoveBinderIfExistInView Methods
        public static void RemoveBinderIfExistInView(IMonoBinderValidable binder)
        {
            // Если Id не установлен, то мы не можем удалить этот биндер,
            // так как мы не знаем что искать
            if (binder.Id is null || string.IsNullOrWhiteSpace(binder.Id))
                throw new NullReferenceException(nameof(binder.Id));
            
            // Если View не установлен, то мы не можем удалить этот биндер,
            // так как мы не знаем где искать
            if (binder.View is null || (binder.View is Component viewComponent && !viewComponent))
                throw new NullReferenceException(nameof(binder.View));
            
            RemoveBinderIfExistInView(binder, binder.View, binder.Id);
        }
        
        public static void RemoveBinderIfExistInView(IMonoBinderValidable binder, IView view, string id)
        {
            var field = view.GetRequireBinderFieldById(id);
            if (field is null) throw new NullReferenceException(nameof(field));
            
            var viewBinders = field.GetValueAsArray<IMonoBinderValidable>(field.FieldContainerObj);
            if (viewBinders is null) return;
            
            if (field.FieldType.IsArray)
            {
                viewBinders = viewBinders.Where(viewBinder => viewBinder != binder).ToArray();
                field.SetValueFromCastValueAndSaveView(view, viewBinders);
            }
            else
            {
                // Если биндер и так не установлен, то ничего не делаем.
                if (viewBinders.Length is 0 || viewBinders[0] != binder) return;
                field.SetValueFromCastValueAndSaveView<IMonoBinderValidable>(view, null);
            }
        }
        #endregion

        #region Helper Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValueFromCastValueAndSaveView<T>(this RequiredFieldForMonoBinder field, IView view, params T?[]? binders)
            where T : IBinder
        { 
            field.SetValueFromCastValue(field.FieldContainerObj, binders); 
            view.SaveView();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SaveView(this IView view)
        {
            if (Application.isPlaying) return;
            if (view is not Component viewComponent) throw new NullReferenceException(nameof(view));
            
            EditorUtility.SetDirty(viewComponent);
            EditorSceneManager.MarkSceneDirty(viewComponent.gameObject.scene);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBinderInViewScope(this IView view, IBinder binder)
        {
            if (view is not Component viewComponent || binder is not Component binderComponent) return false;
            return binderComponent.transform.IsChildOf(viewComponent.transform) || binderComponent.transform == viewComponent.transform;
        }
        #endregion
        
        private readonly struct ChangedBinders
        {
            public readonly string Id;
            public readonly IMonoBinderValidable?[] OldBinders;
            public readonly IMonoBinderValidable?[] NewBinders;

            private ChangedBinders(string id, IMonoBinderValidable?[] oldBinders, IMonoBinderValidable?[] newBinders)
            {
                Id = id;
                OldBinders = oldBinders;
                NewBinders = newBinders;
            }

            public static ReadOnlySpan<ChangedBinders> GetChangedBinders(ValidableBindersById oldBinders, ValidableBindersById newBinders)
            {
                if (oldBinders.Count != newBinders.Count) throw new Exception(/*TODO Aspid.MVVM – Write message in Exception*/);
                var changedFields = new List<ChangedBinders>(oldBinders.Count);

                foreach (var key in oldBinders.Keys)
                {
                    var oldValue = oldBinders[key] ?? Array.Empty<IMonoBinderValidable>();
                    var newValue = newBinders[key] ?? Array.Empty<IMonoBinderValidable>();
                    if (oldValue.SequenceEqual(newValue)) continue;

                    changedFields.Add(new ChangedBinders(BinderFieldInfoExtensions.GetBinderId(key), oldValue, newValue));
                }

                return changedFields.ToArray().AsSpan();
            }
        }

        private ref struct ValidNewBinders
        {
            public bool IsChanged { get; private set; }

            public IMonoBinderValidable?[] Binders { get; private set; }

            public static ValidNewBinders Valid(IView view, in ChangedBinders changedBinder)
            {
                var id = changedBinder.Id;
                var result = new ValidNewBinders();
                if (!view.TryGetRequireBinderFieldsById(id, out var field)) return result;

                var oldBinders = changedBinder.OldBinders.ToHashSet();

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
                        if (field!.IsBinderMatchRequiredType(validatingBinder)) continue;

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
                            .FirstOrDefault(candidate =>
                                (candidate.View != view || candidate.Id != id) &&
                                field!.IsBinderMatchRequiredType(candidate));
                    }

                    validatingBinders[i] = candidateBinder;
                }

                result.Binders = validatingBinders;
                return result;
            }
        }
    }
}