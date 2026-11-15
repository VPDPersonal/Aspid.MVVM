using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    internal static class Rollback
    {
        public static void Run(Exception primary, Action rollback, string context)
        {
            try
            {
                rollback();
            }
            catch (Exception recovery)
            { 
                Throw(primary, recovery, context);
            }
        }

        public static void Run<T>(Exception primary, T state, Action<T> rollback, string context)
        {
            try
            {
                rollback(state);
            }
            catch (Exception recovery)
            {
                Throw(primary, recovery, context);
            }
        }

        private static void Throw(Exception primary, Exception recovery, string context) => throw new AggregateException(
            message: $"{context}: the operation failed and rollback also failed. The first inner exception is the primary failure.",
            innerExceptions: new[] { primary, recovery });
    }
}
