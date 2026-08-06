using System.Text;
using UnityEngine;
using UnityEditor.Build;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Fails or warns a player build when the project contains missing managed-reference types, governed by the
    /// Project Settings gate severity. Runs the fast pure-YAML missing-type scan (required-field scanning is reserved
    /// for the CI entry point, which opts in explicitly, to keep build start fast).
    /// </summary>
    internal sealed class SerializeReferenceBuildGate : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var severity = SerializeReferenceSettings.BuildSeverity;
            if (severity == GateSeverity.Off) return;

            var violations = SerializeReferenceGateScanner.Scan(GateOptions.MissingOnly);
            if (violations.Count is 0) return;

            var summary = BuildSummary(violations);

            if (severity == GateSeverity.Fail)
                throw new BuildFailedException(summary);

            Debug.LogWarning(summary);
        }

        private static string BuildSummary(IReadOnlyList<GateViolation> violations)
        {
            var files = new HashSet<string>();
            var types = new HashSet<string>();

            foreach (var violation in violations)
            {
                files.Add(violation.AssetPath);
                types.Add(SerializeReferenceHelpers.StoredTypeKey(violation.StoredType));
            }

            var builder = new StringBuilder();
            builder.AppendLine($"[Aspid FastTools] {violations.Count} missing managed reference(s) across {files.Count} file(s), {types.Count} broken type(s):");

            foreach (var violation in violations)
                builder.AppendLine($"  {violation}");

            return builder.ToString();
        }
    }
}
