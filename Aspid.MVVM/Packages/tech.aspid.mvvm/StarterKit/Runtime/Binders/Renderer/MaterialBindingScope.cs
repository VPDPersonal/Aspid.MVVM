#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    internal sealed class MaterialBindingScope : IDisposable
    {
        private readonly Dictionary<Renderer, MaterialInstanceConverter> _recipes = new();
        private readonly Dictionary<Renderer, List<MaterialInstanceConverter>> _targets = new();

        public void Set(Renderer target, IConverter<Material?, Material?>? converter, IReadOnlyCollection<Material>? values)
        {
            if (converter is not MaterialInstanceConverter recipe)
            {
                Release(target);
                target.SetMaterials(converter, values);
                return;
            }

            if (!_recipes.TryGetValue(target, out var previous) || !ReferenceEquals(previous, recipe))
            {
                Release(target);
                _recipes[target] = recipe;
            }

            if (!_targets.TryGetValue(target, out var slots))
                _targets.Add(target, slots = new List<MaterialInstanceConverter>());


            var count = values?.Count ?? 0;
            while (slots.Count > count)
            {
                slots[^1].Dispose();
                slots.RemoveAt(slots.Count - 1);
            }

            while (slots.Count < count)
                slots.Add(recipe.CreateScope());

            var materials = new Material[count];
            var index = 0;

            if (values is not null)
            {
                foreach (var value in values)
                {
                    materials[index] = slots[index].Convert(value)!;
                    index++;
                }
            }

            target.sharedMaterials = materials;
        }

        public void Dispose()
        {
            foreach (var slots in _targets.Values)
            {
                foreach (var slot in slots)
                    slot.Dispose();
            }

            _targets.Clear();
            _recipes.Clear();
        }

        private void Release(Renderer target)
        {
            if (!_targets.TryGetValue(target, out var slots)) return;

            foreach (var slot in slots)
                slot.Dispose();

            _targets.Remove(target);
            _recipes.Remove(target);
        }
    }
}
