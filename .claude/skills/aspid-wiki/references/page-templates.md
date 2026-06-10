# Page templates & writing style

## Frontmatter

### Hub — `docs/wiki/index.md`

```yaml
---
title: Aspid.MVVM Wiki
type: index
status: active
last_commit: ""              # superproject HEAD at last ingest; empty = never ingested (first run)
submodule_commits:
  generators: ""             # Aspid.MVVM.Generators HEAD
  unity_generators: ""       # Aspid.MVVM.Unity.Generators HEAD
  analyzers: ""              # Aspid.MVVM.Analyzers HEAD
updated_at: 2026-05-31
---
```

### Content page

```yaml
---
title: ViewModel Generation
type: concept            # overview | concept | entity | binder-category | converter | flow | generation | risk | reference | note
status: active           # draft | active | stale
source_paths:            # repo-relative paths this page is derived from; drives incremental sync
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM.Generators/.../ViewModelGenerator.cs
tags: [generation, viewmodel]
updated_at: 2026-05-31
---
```

`source_paths` may point into the main `Assets/` tree **or** into a submodule (`Aspid.MVVM.Generators/...`). It is the link back to code and the key the Ingest step uses to decide which pages a changed file affects.

## Page body template

```markdown
# <Title>

> One-sentence answer to "what is this and why do I care?"

## Why it exists
<The problem it solves / the decision behind it. Rationale ages slower than code — lead with it.>

## How it works
<The mechanism, at the altitude a smart newcomer needs. Name the generated members where source generation is involved.>

## Key relationships
- Implements / used by [[IViewModel]], drives [[Binders Catalog]] via [[BindMode]] …

## Gotchas
<Only if real. Link to the relevant [[risks]] page.>

## Source
<source_paths rendered as a short list, so a human can jump to code.>
```

Adapt sections to the page type — a `binder-category` page lists the family and variants; a `flow` page is an ordered walkthrough; a `risk` page is symptom → cause → fix.

## Writing style

- **Audience: a smart newcomer.** Explain *why a thing matters* before naming details.
- **Keep pages under ~500 words.** Prefer useful coverage over exhaustive documentation; link out rather than inline everything.
- **Separate facts from inferences.** State verified behavior plainly; mark anything inferred ("appears to…", "likely…") so it can be checked.
- **Document generated code.** For `[ViewModel]`/`[Bind]`/`[RelayCommand]`, describe the members the generator emits — they are invisible in the hand-written source but are the point of the framework.
- **Link liberally** with `[[wikilinks]]`. A dense link graph is what makes the vault navigable (and the Obsidian graph view useful).
- **Language:** match the codebase — content in English (Readme/XML docs/GitBook are English). Configurable per the language-variant convention if needed.
