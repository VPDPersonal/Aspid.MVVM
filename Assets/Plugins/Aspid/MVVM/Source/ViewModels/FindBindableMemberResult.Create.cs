using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public readonly partial struct FindBindableMemberResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult OneWay<T>(IViewModelEventAdder adder, in T value) =>
            new(BindableMember<T?>.OneWay(adder, value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult TwoWay<T>(IViewModelEventAdder adder, in T value) =>
            new(BindableMember<T?>.TwoWay(adder, value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult OneTime<T>(in T value) =>
            new(BindableMember<T?>.OneTime(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult OneWayToSource<T>(IOneWayToSourceViewModelEvent<T> adder) =>
            new(BindableMember<T?>.OneWayToSource(adder));
    }

    public readonly partial struct FindBindableMemberResult<T>
    {
        #region Create By ValueType
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> OneWayByValueType<TFrom>(IViewModelEventAdder adder, in TFrom value)
            where TFrom : struct
        {
            if (typeof(TFrom) == typeof(T))
            {
                var result = new FindBindableMemberResult<TFrom>(BindableMember<TFrom>.OneWay(adder, value));
                return Cast(ref result);
            }

            if (typeof(T) == typeof(object))
            {
                var result = new FindBindableMemberResult<object>(BindableMember<object?>.OneWay(adder, value));
                return Cast(ref result);
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> TwoWayByValueType<TFrom>(IViewModelEventAdder adder, in TFrom value)
            where TFrom : struct
        {
            if (typeof(TFrom) == typeof(T))
            {
                var result = new FindBindableMemberResult<TFrom>(BindableMember<TFrom>.TwoWay(adder, value));
                return Cast(ref result);
            }

            if (typeof(T) == typeof(object))
            {
                var result = new FindBindableMemberResult<object>(BindableMember<object?>.TwoWay(adder, value));
                return Cast(ref result);
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> OneTimeByValueType<TFrom>(in TFrom value)
            where TFrom : struct
        {
            if (typeof(TFrom) == typeof(T))
            {
                var result = new FindBindableMemberResult<TFrom>(BindableMember<TFrom>.OneTime(value));
                return Cast(ref result);
            }

            if (typeof(T) == typeof(object))
            {
                var result = new FindBindableMemberResult<object>(BindableMember<object?>.OneTime(value));
                return Cast(ref result);
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> OneWayToSourceByValueType<TFrom>(
            IOneWayToSourceViewModelEvent<TFrom> adder)
            where TFrom : struct
        {
            if (adder is IOneWayToSourceViewModelEvent<T> specificAdder)
                return new FindBindableMemberResult<T>(BindableMember<T?>.OneWayToSource(specificAdder));

            return default;
        }
        #endregion

        #region Create By ReferenceType
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> OneWay<TFrom>(IViewModelEventAdder adder, in TFrom value)
            where TFrom : class
        {
            if (value is T toValue)
                return new FindBindableMemberResult<T>(BindableMember<T?>.OneWay(adder, toValue));

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> TwoWay<TFrom>(IViewModelEventAdder adder, in TFrom value)
            where TFrom : class
        {
            if (value is T toValue)
                return new FindBindableMemberResult<T>(BindableMember<T?>.TwoWay(adder, toValue));

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> OneTime<TFrom>(in TFrom value)
            where TFrom : class
        {
            if (value is T toValue)
                return new FindBindableMemberResult<T>(BindableMember<T?>.OneTime(toValue));

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FindBindableMemberResult<T> OneWayToSource<TFrom>(IOneWayToSourceViewModelEvent<TFrom> adder)
            where TFrom : class
        {
            if (adder is IOneWayToSourceViewModelEvent<T> specificAdder)
                return new FindBindableMemberResult<T>(BindableMember<T?>.OneWayToSource(specificAdder));

            return default;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FindBindableMemberResult<T> Cast<TFrom>(ref FindBindableMemberResult<TFrom> from) =>
            Unsafe.As<FindBindableMemberResult<TFrom>, FindBindableMemberResult<T>>(ref from);
    }
}