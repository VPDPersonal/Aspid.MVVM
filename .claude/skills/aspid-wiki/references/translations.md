# Translations (multilingual wiki)

`docs/wiki/` is **English and the source of truth**. Every other language is a **translation of the English wiki**, never generated from code directly.

## Layout

- `docs/wiki/` — English. Ingested from code (see `sync.md`).
- `docs/wiki-<lang>/` — one sibling vault per language: `docs/wiki-ru/`, later `docs/wiki-<xx>/`.
- Each translation **mirrors the English tree 1:1**: same folders, same filenames, same `[[wikilink]]` targets. Only the *content* (frontmatter `title`, the `#` H1, and the prose) is translated.

Mirroring filenames (not translating them) keeps `[[wikilinks]]` identical across every language, so the link graph and cross-references stay in sync automatically and a translated page is trivially matched to its English original (same relative path).

## What to translate vs keep

**Translate:** `title` frontmatter value, the H1, and all prose/body text, table cell prose, callouts.

**Keep verbatim (do NOT translate):**
- Code identifiers and type names (`IViewModel`, `BindMode`, `[Bind]`, `TMP_Text`).
- `[[wikilink]]` targets — they point at English filenames. For an aliased link `[[Target|alias]]`, keep `Target` and translate only the `alias`.
- Frontmatter keys, `type`, `status`, `source_paths`, `tags`, and all code blocks.

## Translation frontmatter

Add to every translated page (on top of the mirrored English frontmatter, with `title` translated):

```yaml
lang: ru
translated_from: <english-relative-path>   # e.g. concepts/BindMode.md
translated_at: YYYY-MM-DD
```

## Translation checkpoint & staleness

A translation is downstream of its English page. When the English page changes (Ingest updated it), re-translate the affected language pages. Track this by comparing the English page's `updated_at` (or the wiki commit) against the translation's `translated_at`; if the English page is newer, the translation is stale — re-translate it. The English vault's `last_commit`/`submodule_commits` checkpoint is authoritative; translations never advance the code checkpoint.

## Index & log per language

Each `docs/wiki-<lang>/` gets its own translated `index.md` and `log.md`. The translated `index.md` keeps the English checkpoint fields for reference but states that English is the source of truth and links resolve within the same-language vault.
