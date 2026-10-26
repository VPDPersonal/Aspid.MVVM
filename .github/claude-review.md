# Rules for Claude in this repository

## Always

- Follow the root `AGENTS.md` and the skills in `.claude/skills/` that cover the files you touch.
- Write in English. Keep code identifiers as they are.

## When you review a pull request

The package is `Aspid.MVVM/Packages/tech.aspid.mvvm/`. Check, in this order:

1. Correctness: bugs and missed edge cases, with the input that breaks.
2. Data and code in user projects:
   - a new file or folder in the package has a `.meta`, and a renamed or moved one keeps its `.meta` GUID: users get
     the package as an immutable folder, where Unity cannot create a missing `.meta`;
   - a renamed serialized field gets `[FormerlySerializedAs]`, a renamed or moved type stored through
     `[SerializeReference]` (converters, serializable binders) gets `[MovedFrom]`: without them user projects lose
     saved data;
   - no Unity API that 6000.0 lacks, unless a `#if UNITY_6000_1_OR_NEWER` or later guard covers it;
   - no `UnityEditor` API reachable from runtime code (`Source/`, `Unity/Runtime/`, `StarterKit/Runtime/`) outside an
     `#if UNITY_EDITOR` block.
3. Generators and analyzers: their source lives in `Aspid.MVVM.Generators/`, `Aspid.MVVM.Analyzers/`
   and `Aspid.MVVM.Unity.Generators/`; this repo commits the built DLLs (`Aspid.MVVM.Generators.dll`,
   `Aspid.MVVM.Analyzers.dll`, `Unity/Aspid.MVVM.Unity.Generators.dll`). A DLL that changes without a change of its
   source folder is `[blocking]`: nothing in the repo then has its source.
4. Public API: XML docs on each new or changed member, by the conventions in `AGENTS.md`. Also:
   - a change that users see in shipped package code has the same entry under the top `Unreleased` version in
     `CHANGELOG.md` and `CHANGELOG.ru.md`, or the PR has the `needs-changelog` label (`gh pr view --json labels`);
     neither is a `[minor]`;
   - a removed or changed public member has a migration note in the guide of the next version, `Docs/migration/<version>.md`, and its
     translation in `Docs/i18n/ru/`;
   - a page in `Docs/docs/`, a sample tutorial in `Docs/tutorials/` or a skill in `.claude/skills/` that describes the changed API
     or behavior is updated;
   - a change to public API or XML docs without a regenerated `Docs/api/` (`cd Docs && npm run api`) is a `[minor]`,
     unless the PR says the reference still needs regenerating.
5. Workflows: actions pinned to a full commit SHA, only the permissions a job uses, and no `pull_request_target` job
   that checks out or runs the code of the PR.

Do not report:

- The PR title and labels, apart from the `needs-changelog` case above.
- Style and naming that `AGENTS.md` and the `aspid-code-style` and `starterkit-layout` skills do not cover.
- The wording of translations: `Docs/i18n/ru/`, `CHANGELOG.ru.md`.
- The content of generated or third-party files: `Docs/api/`, the committed DLLs, `packages-lock.json`,
  `package-lock.json`, `Aspid.MVVM/Assets/Plugins/`, `Aspid.MVVM/Assets/TextMesh Pro/`.
- The `asp-branch`, `asp-commit` and `asp-pr` skills in `.claude/skills/`: they are copied from Aspid.AgentKit as it
  ships them.

On a review of the commits after your last review, check only those commits and whether your earlier findings are
fixed. Do not repeat a finding that is fixed or that the author answered with a reason.

## Format

- At most 8 findings, most severe first, each in an inline comment on its line that starts with a severity tag:
  - `[blocking]`: the PR must not merge until it is fixed;
  - `[minor]`: an improvement that can wait.
- Post the main comment last, as a new comment with `gh pr comment`, for a requested review too: an agent waits for
  it, and an edit of an earlier comment does not notify it.
- Start the main comment with exactly one line `Verdict: <N> blocking, <M> minor` (`Verdict: 0 blocking, 0 minor`
  without findings): the agent reads it. Put at most 2 short sentences after it.
- Do not praise.
- Do not write the literal trigger phrase (at-sign + "claude"): it starts the workflow again.
