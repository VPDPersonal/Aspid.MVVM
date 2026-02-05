using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static partial class SerializePropertyExtensions
    {
        public static T Update<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.Update();
            return property;
        }

        public static T UpdateIfRequiredOrScript<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.UpdateIfRequiredOrScript();
            return property;
        }
        
        public static T ApplyModifiedProperties<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.ApplyModifiedProperties();
            return property;
        }
    }
}