# Анализаторы

Roslyn-анализаторы проверяют корректность MVVM-кода на этапе компиляции и предлагают автоматические исправления.

## Содержание

- [Обзор](#обзор)
- [Список диагностик](#список-диагностик)
- [Установка](#установка)
- [Настройка](#настройка)

---

## Обзор

Aspid.MVVM включает два набора анализаторов:

| Пакет | Назначение |
|-------|-----------|
| `Aspid.MVVM.Analyzers` | Проверка ViewModel и View кода |
| `Aspid.MVVM.Unity.Generators` | Unity-специфичные проверки |

Анализаторы работают в IDE (Rider, Visual Studio, VS Code) и при компиляции Unity.

---

## Список диагностик

### ViewModel

| ID | Severity | Описание |
|----|----------|----------|
| `AMVVM001` | Warning | Класс с `[ViewModel]` должен быть `partial` |
| `AMVVM002` | Warning | Поле с `[Bind]` должно находиться в классе с `[ViewModel]` |
| `AMVVM003` | Warning | Метод с `[RelayCommand]` должен находиться в классе с `[ViewModel]` |
| `AMVVM004` | Info | Рекомендация использовать сгенерированное свойство вместо backing-поля |
| `AMVVM005` | Warning | `CanExecute` метод/свойство не найдено |
| `AMVVM006` | Warning | Несовпадение параметров `CanExecute` и команды |

### View

| ID | Severity | Описание |
|----|----------|----------|
| `AMVVM010` | Warning | Класс с `[View]` должен быть `partial` |
| `AMVVM011` | Warning | Класс с `[View]` должен наследовать `MonoView` |
| `AMVVM012` | Info | Поле биндера не имеет совпадающего свойства в ViewModel |

### Code Fixes

Для большинства диагностик доступны автоматические исправления:

| Диагностика | Code Fix |
|-------------|----------|
| `AMVVM001` | Добавить `partial` к классу |
| `AMVVM004` | Заменить `_field` на `Property` |
| `AMVVM010` | Добавить `partial` к классу |

---

## Установка

Анализаторы поставляются вместе с пакетом Aspid.MVVM. Если вы клонировали из исходного кода:

```bash
git submodule update --init --recursive
```

Убедитесь, что подмодуль `Aspid.MVVM.Analyzers` инициализирован.

---

## Настройка

### Отключение конкретных диагностик

В `.editorconfig`:

```ini
[*.cs]
# Отключить рекомендацию использовать свойство вместо поля
dotnet_diagnostic.AMVVM004.severity = none

# Понизить до информации
dotnet_diagnostic.AMVVM001.severity = suggestion
```

### В коде (для отдельных мест)

```csharp
#pragma warning disable AMVVM004
var text = _text; // Используем backing-поле намеренно
#pragma warning restore AMVVM004
```

---

## См. также

- [ViewModel](04-viewmodels.md) — атрибуты, проверяемые анализаторами
- [View](05-views.md) — правила для View
- [Лучшие практики](14-best-practices.md) — рекомендации по коду
