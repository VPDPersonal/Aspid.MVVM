#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System.ComponentModel;
using Aspid.MVVM.Generation;

namespace Aspid.MVVM.Mono
{
    public abstract partial class MonoBinder : IMonoBinderValidable
    {
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoView __view;
        
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private string __id;
        
        /// <summary>
        /// Is there a component?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        bool IMonoBinderValidable.IsMonoExist => this;
        
        /// <summary>
        /// The View to which the Binder relates.
        /// (Editor only).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IView IMonoBinderValidable.View
        {
            get => __view;
            set
            {
                if (!((IMonoBinderValidable)this).IsMonoExist) return;

                if (value is null)
                {
                    __view = null;
                    return;
                }
                
                if (__view == value as MonoView) return;
                
                __view = value switch
                {
                    MonoView view => view,
                    _ => throw new ArgumentException("View is not a MonoView")
                };

                SaveBinderDataInEditor();
            }
        }
        
        /// <summary>
        /// The ID that must correspond to the name of any ViewModel property.
        /// (Editor only).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        string IMonoBinderValidable.Id
        {
            get => __id;
            set
            {
                if (!((IMonoBinderValidable)this).IsMonoExist) return;
                if (__id == value) return;
                
                __id = value;
                SaveBinderDataInEditor();
            }
        }

        partial void OnBindingDebug(in BindParameters parameters)
        {
            if (parameters.ViewModel != __view?.ViewModel) 
                throw new Exception($"ViewModel not match. {GetBindParametersInfo(parameters)} {GetBinderIdInfo()}");

            var id = parameters.Id;
            if (__id != id)
            {
                if (!string.IsNullOrWhiteSpace(__id))
                {
                    string[] fieldNames = null;
                    var viewType = __view.GetType();
                    const BindingFlags binding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    
                    for (var type = viewType; type is not null; type = type.BaseType)
                    {
                        if (type == typeof(object)) break;
                        if (type == typeof(MonoView) || type == typeof(MonoBehaviour)) break;
                        if (type.GetFields(binding).Any(CheckMember)) return;
                    }
                    
                    bool CheckMember(FieldInfo field)
                    {
                        var attribute = field.GetCustomAttribute<BindIdAttribute>();
                        if (attribute is null || attribute.Id != id) return false;

                        var fieldType = field.FieldType.IsArray ? field.FieldType.GetElementType() : field.FieldType;
                        
                        if (fieldType is null) return false;
                        if (fieldType.GetInterfaces().All(@interface => @interface != typeof(IBinder))) return false;
                        
                        return GetFieldNames().Any(fieldName => fieldName == field.Name);
                    }

                    string[] GetFieldNames()
                    {
                        if (fieldNames is not null) return fieldNames;
                        
                        var fieldName = __id;
                        fieldNames = new string[4];
                        fieldNames[0] = fieldName;

                        var firstChar = char.ToLower(fieldName[0]);
                        fieldName = firstChar + fieldName.Remove(0, 1);

                        fieldNames[1] = fieldName;
                        fieldNames[2] = "_" + fieldName;
                        fieldNames[3] = "m_" + fieldName;

                        return fieldNames;
                    }
                }
                
                throw new Exception($"Id not match. {GetBindParametersInfo(parameters)} {GetBinderIdInfo()}");
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SaveBinderDataInEditor()
        {
	        if (!((IMonoBinderValidable)this).IsMonoExist) return;
	        
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        private string GetBinderIdInfo() =>
            $"Binder {{ View: {__view}; ViewModel: {__view?.ViewModel}; Id: {__id} }}";
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static string GetBindParametersInfo(in BindParameters parameters) =>
            $"BindParameters: {{ ViewModel: {parameters.ViewModel}; Id: {parameters.Id} }}";
    }
}
#endif