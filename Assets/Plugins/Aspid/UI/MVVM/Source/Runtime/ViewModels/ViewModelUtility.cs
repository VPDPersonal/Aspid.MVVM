using System;
using System.Collections.Generic;

namespace Aspid.UI.MVVM.ViewModels
{
    /// <summary>
    /// Служебный класс, предоставляющий общие операции для работы с ViewModels.
    /// </summary>
    public static class ViewModelUtility
    {
        /// <summary>
        /// Устанавливает значение свойства, если оно изменилось.
        /// </summary>
        /// <param name="field">Ссылка на поле, хранящее текущее значение.</param>
        /// <param name="newValue">Новое значение для установки.</param>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <returns>Возвращает true, если свойство было изменено, иначе false.</returns>
        public static bool SetProperty<T>(ref T field, T newValue)
        {
            if (EqualsDefault(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        /// <summary>
        /// Устанавливает значение свойства с помощью пользовательского <see cref="comparer"/>, если оно изменилось.
        /// </summary>
        /// <param name="field">Ссылка на поле, хранящее текущее значение.</param>
        /// <param name="newValue">Новое значение для установки.</param>
        /// <param name="comparer">Пользовательский <see cref="comparer"/> для проверки равенства.</param>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <returns>Возвращает true, если свойство было изменено, иначе false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool SetProperty<T>(ref T field, T newValue, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (comparer.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        /// <summary>
        /// Устанавливает значение свойства и вызывает <see cref="callback"/>, если оно изменилось.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">Новое значение для установки.</param>
        /// <param name="callback">Action для вызова, если значение было изменено.</param>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <returns>Возвращает true, если свойство было изменено, иначе false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool SetProperty<T>(T oldValue, T newValue, Action<T> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (EqualsDefault(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        /// <summary>
        /// Устанавливает значение свойства с помощью пользовательского <see cref="comparer"/> и вызывает <see cref="callback"/>, если оно изменилось.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">Новое значение для установки.</param>
        /// <param name="callback">Action для вызова, если значение было изменено.</param>
        /// /// <param name="comparer">Пользовательский <see cref="comparer"/> для проверки равенства.</param>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <returns>Возвращает true, если свойство было изменено, иначе false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool SetProperty<T>(T oldValue, T newValue, Action<T> callback, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (comparer.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        /// <summary>
        /// Проверяет равенство значений используя дефолтный Comparer.
        /// </summary>
        /// <param name="value">Текущие значение.</param>
        /// <param name="newValue">Новое значение поля.</param>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <returns>Возвращает true, если значения равны, иначе false.</returns>
        public static bool EqualsDefault<T>(T value, T newValue) =>
            EqualityComparer<T>.Default.Equals(value, newValue);

        /// <summary>
        /// Базовая реализация метода AddBinder из интерфейса IViewModel.
        /// </summary>
        /// <param name="binder">Binder для связывания.</param>
        /// <param name="value">Начальное значение.</param>
        /// <param name="changed">Action для связывания.</param>
        /// <param name="setValue">Необязательный Action для обратного связывания.</param>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <exception cref="Exception"></exception>
        public static void AddBinder<T>(IBinder binder, T value, ref Action<T> changed, Action<T>? setValue = null)
        {
            if (binder is not IBinder<T> specificBinder)
                throw new Exception($"binder ({binder.GetType()}) is not {typeof(IBinder<T>)}");
			        
            specificBinder.SetValue(value);
            changed += specificBinder.SetValue;

            if (setValue != null && binder.IsReverseEnabled)
            {
                if (binder is not IReverseBinder<T> specificReverseBinder)
                    throw new Exception($"binder ({binder.GetType()}) is not {typeof(IReverseBinder<T>)}");

                specificReverseBinder.ValueChanged += setValue;
            }
        }
        
        /// <summary>
        /// Базовая реализация метода RemoveBinder из интерфейса IViewModel.
        /// </summary>
        /// <param name="binder">Binder для разрыва привязки.</param>
        /// <param name="changed">Action для разрыва привязки.</param>
        /// <param name="setValue">Необязательный Action для разрыва обратного связывания.</param>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <exception cref="Exception"></exception>
        public static void RemoveBinder<T>(IBinder binder, ref Action<T> changed, Action<T>? setValue = null)
        {
            if (binder is not IBinder<T> specificBinder)
                throw new Exception($"binder ({binder.GetType()}) is not {typeof(IBinder<T>)}");
			        
            changed -= specificBinder.SetValue;

            if (setValue != null && binder.IsReverseEnabled)
            {
                if (binder is not IReverseBinder<T> specificReverseBinder)
                    throw new Exception($"binder ({binder.GetType()}) is not {typeof(IReverseBinder<T>)}");
			        
                specificReverseBinder.ValueChanged -= setValue;
            }
        }
    }
}