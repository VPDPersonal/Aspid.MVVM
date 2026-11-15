using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class LoggerHelper
    {
        internal static string GetPathMessage(this string message) =>
            message.GetMessage("D69D85");

        internal static string GetClassMessage(this Type type) =>
            GetTypeName(type).GetMessage("4EC9B0");

        internal static string GetClassMessage(this string message) =>
            message.GetMessage("4EC9B0");

        internal static string GetStructMessage(this Type type) =>
            GetTypeName(type).GetMessage("86C691");

        internal static string GetStructMessage(this string message) =>
            message.GetMessage("86C691");

        internal static string GetInterfaceMessage(this Type type) =>
            GetTypeName(type).GetMessage("B8D7A3");

        internal static string GetInterfaceMessage(this string message) =>
            message.GetMessage("B8D7A3");

        [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
        private static string GetTypeName(Type type)
        {
            if (type.IsGenericParameter)
                return type.Name;
            
            if (type.IsArray)
                return $"{GetTypeName(type.GetElementType()!)}[{new string(',', type.GetArrayRank() - 1)}]";
            
            if (type.IsByRef) 
                return $"{GetTypeName(type.GetElementType()!)}&";
            
            if (type.IsPointer) 
                return $"{GetTypeName(type.GetElementType()!)}*";
            
            if (Nullable.GetUnderlyingType(type) is { } underlyingType) 
                return $"{GetTypeName(underlyingType)}?";

            var alias = type.FullName switch
            {
                "System.Boolean" => "bool",
                "System.Byte" => "byte",
                "System.SByte" => "sbyte",
                "System.Char" => "char",
                "System.Int16" => "short",
                "System.UInt16" => "ushort",
                "System.Int32" => "int",
                "System.UInt32" => "uint",
                "System.Int64" => "long",
                "System.UInt64" => "ulong",
                "System.Single" => "float",
                "System.Double" => "double",
                "System.Decimal" => "decimal",
                "System.String" => "string",
                "System.Object" => "object",
                "System.IntPtr" => "nint",
                "System.UIntPtr" => "nuint",
                "System.Void" => "void",
                _ => null
            };

            if (alias is not null) return alias;

            var arguments = type.IsGenericType 
                ? type.GetGenericArguments()
                : Type.EmptyTypes;
            
            var argumentIndex = 0;
            
            return GetTypeName(type, arguments, ref argumentIndex);
        }

        private static string GetTypeName(Type type, Type[] arguments, ref int argumentIndex)
        {
            var declaringType = type.DeclaringType;
            var prefix = declaringType is null
                ? string.Empty
                : GetTypeName(declaringType, arguments, ref argumentIndex) + ".";

            var name = type.Name;
            var arityIndex = name.IndexOf('`');
            
            if (arityIndex >= 0)
                name = name[..arityIndex];

            var argumentCount = type.IsGenericType
                ? type.GetGenericArguments().Length - (declaringType?.GetGenericArguments().Length ?? 0)
                : 0;

            if (argumentCount is 0)
                return prefix + name;

            var names = new string[argumentCount];
            for (var i = 0; i < names.Length; i++)
                names[i] = GetTypeName(arguments[argumentIndex++]);

            return $"{prefix}{name}<{string.Join(", ", names)}>";
        }

        private static string GetMessage(this string message, string color)
        {
#if UNITY_EDITOR
            return $"<color=#{color}>{message}</color>";
#else
            return message;
#endif
        }
    }
}
