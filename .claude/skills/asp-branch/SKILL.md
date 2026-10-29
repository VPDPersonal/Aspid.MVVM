---
name: asp-branch
description: Git branch naming convention. Use when creating a branch or choosing its name.
when_to_use: "«создай ветку», «new branch», branching off main before a commit."
effort: low
---

# Branch naming

Use `<type>/<short-description>`, where `<type>` is the Conventional Commits type the commit or PR title will use:

- `feat/` — new functionality.
- `fix/` — bug fixes.
- `perf/` — performance improvements.
- `refactor/` — restructuring without changing behavior.
- `style/` — formatting only.
- `docs/` — documentation.
- `test/` — tests.
- `build/` — build system and packaging.
- `ci/` — CI workflows.
- `chore/` — dependencies, tooling, and maintenance.
- `revert/` — reverting a previous change.

Description rules:

- English, lowercase kebab-case, 2–5 words.
- The whole name is 40 characters max.
- Exactly one slash.
- Describe the concrete change. Add the affected area only if the description alone does not show where the change is.
- No author, agent or tool prefixes. No dates. No vague words such as `final` or `working`.

Examples: `feat/generator-observable-properties`, `fix/handle-null-target`, `docs/update-installation`.

`git branch -m` or `git switch -c` says the name exists? Add one word to the description and run again.
