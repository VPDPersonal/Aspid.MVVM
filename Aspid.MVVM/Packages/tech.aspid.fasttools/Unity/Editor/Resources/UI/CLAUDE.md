# USS Conventions

Naming rules for every `.uss` file in this package **and** for any C# code holding USS class names or `--aspid-*` variables (`Constants.cs`, `AspidStyles.cs`, component files). The `uss-bem-checker` agent reviews against these rules.

## Loading & layout

- Stylesheets are organized by domain subfolders (`UI/Components/`, `UI/Ids/`, …); shared base palette at `UI/Aspid-FastTools-Default-Dark.uss`. Files follow `Aspid-FastTools-{Feature}.uss`.
- Loaded via `.AddStyleSheetsFromResource("UI/{Domain}/Aspid-FastTools-{Feature}")`. Component code keeps the path in a `private const string StyleSheetPath`; ID-system code centralises paths in `Constants.cs`.
- Styling goes in USS; code only applies `.AddClass()`. On new internal editor components add the base palette `AspidStyles.DefaultStyleSheet` first — `AspidStyles` is the single source of truth for shared USS class/property names.
- `UI/../Icons/` holds editor icon assets referenced by USS via `--aspid-icons-*` variables.

## Class naming (BEM)

Follow Unity's recommended Block-Element-Modifier convention (see [UIE-USS-WritingStyleSheets](https://docs.unity3d.com/6000.4/Documentation/Manual/UIE-USS-WritingStyleSheets.html)).

Format: `aspid-fasttools-{block}[__{element}][--{modifier}]`

- **Prefix** `aspid-fasttools-` is mandatory and joined to the block with a single `-` (matches Unity's own `unity-foldout__toggle` style — the prefix is a namespace, not a BEM block).
- **Block** — feature/component name in kebab-case: `id-registry`, `id-drawer`, `enum-values`, `serializable-type`.
- **Element** — part of a block, joined with `__`: `aspid-fasttools-id-drawer__add-button`, `aspid-fasttools-id-registry__delete`.
- **Modifier** — state or variant, joined with `--`: `aspid-fasttools-id-registry__warning--visible`, `aspid-fasttools-status--error`.
- **kebab-case inside any segment** (`add-button`, never `addButton` or `add_button`).
- **Utility/state classes** (status, theme) are blocks of their own: `aspid-fasttools-status--error`, `aspid-fasttools-theme--dark`.

Pre-existing classes that use `-` instead of `__` between block and element (e.g. `aspid-fasttools-id-drawer-add-button`) are legacy. Migrate to BEM when touching the surrounding code; new classes must follow the rule from the start.

## Variable naming

USS custom properties are design tokens with a positional grammar — not BEM (variables have no block/element/modifier). Follow Unity's separator convention from built-in `--unity-*` variables: `-` between slots, `_` for compound words inside a single slot. See [UIE-USS-UnityVariables](https://docs.unity3d.com/6000.4/Documentation/Manual/UIE-USS-UnityVariables.html).

Format: `--{prefix}-{group}-{role}[-{state}][-{tone}]`

| Slot | Values | Required |
|---|---|---|
| `prefix` | `aspid` (palette shared between Aspid packages) / `aspid-fasttools` (product-specific) | yes |
| `group` | `colors` · `icons` · `metrics` · `prop` | yes |
| `role` | `bg`, `shade`, `text`, `border`, `icon`, `status`, `gradient`, `label_size`, `line_size`, `theme`, … | yes |
| `state` | `success`, `warning`, `error`, `info`, `hover`, `pressed`, … | optional |
| `tone` | `darkness`, `dark`, `light`, `lightness` | optional |

Rules:
- One word per slot, or one compound joined by `_` (`label_size`) — never two independent concepts in one slot.
- Order is `state` → `tone` (`success-darkness`, not `darkness-success`): "what is it" first, then "how bright".
- Color roles: `bg` — surface palette; `shade` — generic content palette (text/border/icon-tint when not specialised); `text`/`border`/`icon` — specialised component-local swatches; `status` — `success`/`warning`/`error`/`info` semantics.
- `prop` group is for inline component parameters (e.g. `--aspid-fasttools-prop-theme`), not palette tokens.
- Palette variables declared on `:root`; component-scoped variables on the component selector.

Examples:
```
--aspid-colors-bg-darkness                  /* surface, very dark */
--aspid-colors-shade-lightness              /* generic content, very light */
--aspid-colors-status-success-darkness      /* status, very dark variant */
--aspid-icons-status-error                  /* status icon resource */
--aspid-fasttools-metrics-label_size        /* compound role */
--aspid-fasttools-prop-status               /* inline component param */
```

All palette variables in `Aspid-FastTools-Default-Dark.uss` already follow this grammar; new variables in any other stylesheet must follow it from the start.
