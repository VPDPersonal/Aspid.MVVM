using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Headless CI entry point. Invoke with
    /// <c>Unity -batchmode -quit -projectPath . -executeMethod Aspid.FastTools.SerializeReferences.Editors.SerializeReferenceCiGate.RunCheck</c>.
    /// Scans the project, writes a report, logs each violation, and exits non-zero when violations exist so a pipeline
    /// can fail the job. The effective behaviour is driven by the committed Project Settings gate severity
    /// (<see cref="SerializeReferenceSharedSettings"/>) — <c>Off</c> skips the check, <c>Warn</c> logs but exits 0,
    /// <c>Fail</c> exits 1 on violations — so a clean CI runner enforces the developer's checked-in choice rather than
    /// the default. Flags: <c>-srGateReport &lt;path&gt;</c> (report file), <c>-srGateRequired</c> (also scan
    /// unset-required fields), <c>-srGateWarnOnly</c> (force exit 0, overrides severity), <c>-srGateFail</c> (force exit
    /// 1 on violations, overrides severity; <c>-srGateWarnOnly</c> wins if both are passed).
    /// </summary>
    internal static class SerializeReferenceCiGate
    {
        private const string DefaultReportPath = "SerializeReferenceGateReport.txt";

        // ReSharper disable once UnusedMember.Global — invoked via -executeMethod.
        public static void RunCheck()
        {
            if (!Application.isBatchMode)
            {
                Debug.LogWarning("[Aspid FastTools] SerializeReferenceCiGate.RunCheck is intended for -batchmode; ignoring.");
                return;
            }

            int exitCode;
            try
            {
                var args = Environment.GetCommandLineArgs();
                var reportPath = GetArgValue(args, "-srGateReport") ?? DefaultReportPath;
                var scanRequired = HasFlag(args, "-srGateRequired");
                var warnOnly = HasFlag(args, "-srGateWarnOnly");
                var failOverride = HasFlag(args, "-srGateFail");

                var severity = ResolveSeverity(SerializeReferenceSettings.BuildSeverity, warnOnly, failOverride);
                if (severity == GateSeverity.Off)
                {
                    Debug.Log("[Aspid FastTools] SerializeReference gate severity is Off; CI check skipped.");
                    exitCode = 0;
                }
                else
                {
                    var options = scanRequired ? GateOptions.Full : GateOptions.MissingOnly;
                    var violations = SerializeReferenceGateScanner.Scan(options);

                    File.WriteAllText(reportPath, BuildReport(violations));
                    foreach (var violation in violations)
                        Debug.LogError($"[Aspid FastTools] {violation}");

                    exitCode = ComputeExitCode(violations.Count, severity);
                    Debug.Log($"[Aspid FastTools] Gate check complete: {violations.Count} violation(s), severity {severity}, exit code {exitCode}. Report: {reportPath}");
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Gate check failed: {exception}");
                exitCode = 2; // distinguish an internal failure from a clean violation result
            }

            EditorApplication.Exit(exitCode);
        }

        /// <summary>
        /// Resolves the effective gate severity: the committed Project Settings value, overridable per run by CLI flags.
        /// <c>-srGateWarnOnly</c> forces <see cref="GateSeverity.Warn"/> (log but never fail) and wins over
        /// <c>-srGateFail</c>, which forces <see cref="GateSeverity.Fail"/>.
        /// </summary>
        public static GateSeverity ResolveSeverity(GateSeverity committed, bool warnOnly, bool failOverride)
        {
            if (warnOnly) return GateSeverity.Warn;
            return failOverride ? GateSeverity.Fail : committed;
        }

        /// <summary>
        /// 0 when clean or when severity is anything but <see cref="GateSeverity.Fail"/>; 1 only when violations exist
        /// and severity is <see cref="GateSeverity.Fail"/>.
        /// </summary>
        public static int ComputeExitCode(int violationCount, GateSeverity severity) =>
            violationCount > 0 && severity == GateSeverity.Fail ? 1 : 0;

        private static string BuildReport(IReadOnlyList<GateViolation> violations)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"# SerializeReference Gate Report");
            builder.AppendLine($"# Violations: {violations.Count}");
            builder.AppendLine();

            foreach (var violation in violations)
            {
                // Machine-readable line: KIND<TAB>assetPath<TAB>fileId<TAB>rid<TAB>StoredType<TAB>fieldPath
                builder.Append(violation.Kind).Append('\t')
                    .Append(violation.AssetPath).Append('\t')
                    .Append(violation.FileId).Append('\t')
                    .Append(violation.Rid).Append('\t')
                    .Append(violation.StoredType.Class ?? string.Empty).Append('\t')
                    .Append(violation.FieldPath ?? string.Empty)
                    .AppendLine();
            }

            return builder.ToString();
        }

        private static string GetArgValue(string[] args, string flag)
        {
            for (var i = 0; i < args.Length - 1; i++)
            {
                if (string.Equals(args[i], flag, StringComparison.OrdinalIgnoreCase))
                    return args[i + 1];
            }

            return null;
        }

        private static bool HasFlag(string[] args, string flag) =>
            args.Any(arg => string.Equals(arg, flag, StringComparison.OrdinalIgnoreCase));
    }
}
