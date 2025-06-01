using System;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    public sealed class ViewModelFieldsUpdater
    {
        private readonly List<FieldUpdater> _updaters = new();

        public void AddField<T>(BaseField<T> field, ViewModelMemberInfo member)
        {
            _updaters.Add(new FieldUpdater<T>(member, field));
            
            field.RegisterValueChangedCallback(e =>
            {
                e.StopPropagation();
                member.Value = e.newValue;
                Update();
            });
        }

        public Button CreateButton(string text, Action action) => new(() =>
        {
            action?.Invoke();
            Update();
        })
        {
            text = text,
        };

        private void Update()
        {
            foreach (var updater in _updaters)
                updater.Update();
        }
        
        private abstract class FieldUpdater
        {
            protected readonly ViewModelMemberInfo Info;

            protected FieldUpdater(ViewModelMemberInfo info)
            {
                Info = info;
            }
            
            public abstract void Update();
        }
        
        private sealed class FieldUpdater<T> : FieldUpdater
        {
            private readonly BaseField<T> _field;

            public FieldUpdater(ViewModelMemberInfo info, BaseField<T> field) : base(info)
            {
                _field = field;
            }

            public override void Update() =>
                _field.SetValueWithoutNotify((T)Info.Value);
        }
    }
}