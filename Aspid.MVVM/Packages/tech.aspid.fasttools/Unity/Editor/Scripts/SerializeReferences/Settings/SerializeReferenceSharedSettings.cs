using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Project-scoped, version-controlled home for the SerializeReference settings that must be the same for every
    /// teammate and for CI — the build/CI gate severity, the auto-de-alias behaviour and the excluded scan folders.
    /// Persisted as a YAML asset under <c>ProjectSettings/</c> (via <see cref="ScriptableSingleton{T}"/> +
    /// <see cref="FilePathAttribute"/>) so the chosen values are committed to VCS and travel to a clean CI runner —
    /// unlike the per-machine <see cref="EditorPrefs"/> that back the purely cosmetic rest of
    /// <see cref="SerializeReferenceSettings"/> (breakage detection). Commit
    /// <c>ProjectSettings/SerializeReferenceSharedSettings.asset</c> for these values to reach CI and the rest of the team.
    /// </summary>
    [FilePath("ProjectSettings/SerializeReferenceSharedSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    internal sealed class SerializeReferenceSharedSettings : ScriptableSingleton<SerializeReferenceSharedSettings>
    {
        [SerializeField] private GateSeverity _buildSeverity = GateSeverity.Warn;
        [SerializeField] private bool _autoDeAlias = true;
        [SerializeField] private string[] _excludedFolders = Array.Empty<string>();

        public GateSeverity BuildSeverity
        {
            get => _buildSeverity;
            set
            {
                if (_buildSeverity == value) return;
                _buildSeverity = value;
                Save(saveAsText: true);
            }
        }

        public bool AutoDeAlias
        {
            get => _autoDeAlias;
            set
            {
                if (_autoDeAlias == value) return;
                _autoDeAlias = value;
                Save(saveAsText: true);
            }
        }

        public string[] ExcludedFolders
        {
            // Defensive copy: an in-place mutation of the live array would change the asset without Save()/Changed
            // and make the facade's equality short-circuit swallow the follow-up assignment.
            get => _excludedFolders is { Length: > 0 } ? (string[])_excludedFolders.Clone() : Array.Empty<string>();
            set
            {
                _excludedFolders = value ?? Array.Empty<string>();
                Save(saveAsText: true);
            }
        }
    }
}
