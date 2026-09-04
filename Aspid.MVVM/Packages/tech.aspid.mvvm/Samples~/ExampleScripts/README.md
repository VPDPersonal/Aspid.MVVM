# API Reference Scripts

Minimal compilable snippets, one attribute or feature per file. There are no scenes here; read the code.

| Folder | Covers |
|---|---|
| `ViewModels/Bind` | `[Bind]`, mode-specific attributes, `readonly`/`const` as OneTime, `[BindAlso]`, `[Bind]` on properties |
| `ViewModels/Commands` | `RelayCommand` up to four parameters, `[RelayCommand]`, `CanExecute` forms |
| `ViewModels/Handlers` | `OnXxxChanging` / `OnXxxChanged` hooks |
| `ViewModels/Others` | `[Access]`, `[BindId]`, `MonoViewModel`, `ScriptableViewModel` |
| `Views/Bind` | binder fields, properties, arrays, `[AsBinder]` |
| `Views/Generic` | `IView<TViewModel>` |
| `Views/Handlers` | initialize/deinitialize and binder-instantiation hooks |
| `Views/Other` | `[IgnoreBind]`, `[BindId]` on the View side |
