using System.Text;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Surfaces a breakage report non-intrusively: a fade-out <see cref="EditorWindow.ShowNotification"/> toast that
    /// steals no focus, plus a single console warning pointing at the Repair window's menu path. The same breakage
    /// set is shown at most once per session (a content hash in <see cref="SessionState"/>), so a recompile that
    /// re-detects the identical set does not nag.
    /// </summary>
    internal static class SerializeReferenceBreakageNotificationController
    {
        private const string ShownPrefix = "Aspid.FastTools.SerializeReferences.Breakage.Shown.";
        private const double FadeOutSeconds = 5.0;

        [InitializeOnLoadMethod]
        private static void Hook() => SerializeReferenceBreakageDetector.BreakageDetected += OnBreakageDetected;

        private static void OnBreakageDetected(BreakageReport report)
        {
            if (!report.HasAny || Application.isBatchMode) return;
            if (!SerializeReferenceSettings.BreakageDetectionEnabled) return;

            var shownKey = ShownPrefix + ProjectId() + "." + ContentHash(report);
            if (SessionState.GetBool(shownKey, false)) return;
            SessionState.SetBool(shownKey, true);

            var count = report.Entries.Count;
            var typeWord = report.TypeCount == 1 ? "type" : "types";

            // A [MovedFrom]-resolvable entry is not really broken — Unity migrates it in memory at load; only the
            // file text is stale. Word it as a calm "migrate" invitation, not a data-loss alarm.
            var migratable = report.Entries.Count(entry => entry.MigrationTarget is not null);

            // The cold-index path emits one TYPE-level entry per broken type (no per-site data without the index),
            // so counting entries as "references" would misreport — those reports speak in types.
            var typeLevel = report.Entries.Count > 0 && report.Entries[0].AssetPath is null;

            var plural = count == 1 ? "" : "s";
            var message = typeLevel
                ? migratable == count
                    ? $"{report.TypeCount} managed-reference {typeWord} carr{(report.TypeCount == 1 ? "ies" : "y")} " +
                      "an outdated name after a [MovedFrom] rename — open Repair to migrate"
                    : $"{report.TypeCount} managed-reference {typeWord} just became missing — open Repair"
                : migratable == count
                    ? $"{count} managed reference{plural} carr{(count == 1 ? "ies" : "y")} an outdated type name " +
                      $"after a [MovedFrom] rename ({report.TypeCount} {typeWord}) — open Repair to migrate"
                    : migratable > 0
                        ? $"{count} managed reference{plural} became missing ({report.TypeCount} {typeWord}; " +
                          $"{migratable} auto-migratable after a [MovedFrom] rename) — open Repair"
                        : $"{count} managed reference{plural} became missing ({report.TypeCount} {typeWord}) — open Repair";

            ShowToast(message);

            // A fully-migratable report logs at plain severity so the console matches the calm copy.
            var console = $"[Aspid FastTools] {message}. Open Tools/Aspid \U0001F40D/FastTools/Project References.";
            if (migratable == count) Debug.Log(console);
            else Debug.LogWarning(console);
        }

        private static void ShowToast(string message)
        {
            var content = new GUIContent(message);

            var sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                sceneView.ShowNotification(content, FadeOutSeconds);
                sceneView.Repaint();
                return;
            }

            foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (window == null) continue;
                window.ShowNotification(content, FadeOutSeconds);
                window.Repaint();
                return;
            }
            // No editor window open (rare) — the console warning above is the fallback signal.
        }

        // A stable identity for the breakage set: its sorted, distinct stored-type keys. Two events affecting the same
        // types are the "same" set and are not re-toasted.
        private static string ContentHash(BreakageReport report)
        {
            var keys = new SortedSet<string>(System.StringComparer.Ordinal);
            foreach (var entry in report.Entries)
                keys.Add(SerializeReferenceHelpers.StoredTypeKey(entry.StoredType));

            var builder = new StringBuilder();
            foreach (var key in keys) builder.Append(key).Append(';');
            return builder.ToString().GetHashCode().ToString("X8");
        }

        private static string ProjectId() =>
            PlayerSettings.productGUID.ToString();
    }
}
