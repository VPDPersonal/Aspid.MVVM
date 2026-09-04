# Туториал: DI Integration

Разбор сэмпла `Feature: DI Integration` — контейнер владеет моделью и ViewModel, `ViewInitializer` забирает ViewModel из него.

**Предполагается знание:** [Counter](../01.%20Counter/README.ru.md) — сцена `Counter (ViewInitializer)`.

---

## Что строим

Кошелёк: число монет и кнопка «Earn». Две одинаковые сцены — для Zenject и для VContainer.

Файлы: `Samples~/DiIntegration/`.

---

## ViewModel с зависимостью в конструкторе

```csharp
[ViewModel]
public sealed partial class WalletViewModel : IDisposable
{
    [OneWayBind] private int _coins;

    private readonly Wallet _wallet;

    public WalletViewModel(Wallet wallet)
    {
        _wallet = wallet;
        _coins = wallet.Coins;
        _wallet.CoinsChanged += SetCoins;
    }

    [RelayCommand]
    private void Earn() => _wallet.Add(10);

    public void Dispose() => _wallet.CoinsChanged -= SetCoins;
}
```

Никаких `[Inject]`: ViewModel — обычный класс с конструктором, и контейнер создаёт его как любой другой сервис.

---

## Регистрация

Zenject, `MonoInstaller` на `SceneContext`:

```csharp
public override void InstallBindings()
{
    Container.Bind<Wallet>().AsSingle();
    Container.Bind<WalletViewModel>().AsSingle();
}
```

VContainer, `LifetimeScope` в сцене:

```csharp
protected override void Configure(IContainerBuilder builder)
{
    builder.Register<Wallet>(Lifetime.Singleton);
    builder.Register<WalletViewModel>(Lifetime.Singleton);
}
```

Оба файла обёрнуты в `#if ASPID_MVVM_ZENJECT_INTEGRATION` / `#if ASPID_MVVM_VCONTAINER_INTEGRATION`, чтобы сэмпл компилировался с любым одним контейнером.

---

## ViewInitializer

На объекте View стоит `ViewInitializer`:

| Поле | Значение |
|---|---|
| **Views → Resolve Type** | `Component`, ссылка на `WalletView` |
| **ViewModel → Resolve Type** | `Di`, тип `WalletViewModel` |
| **Initialize Stage** | `DiConstructor` |

`DiConstructor` означает: инициализация происходит в момент, когда контейнер инжектит `ViewInitializer`, — раньше `Awake`, но уже с готовым контейнером. Для Zenject достаточно `SceneContext`; для VContainer объект View добавлен в **Auto Inject Game Objects** у `LifetimeScope`, иначе инжект не случится.

---

## Define-символы

- VContainer — UPM-пакет `jp.hadashikick.vcontainer`; `versionDefines` в `.asmdef` включает `ASPID_MVVM_VCONTAINER_INTEGRATION` автоматически.
- Zenject не пакет, поэтому `ASPID_MVVM_ZENJECT_INTEGRATION` добавляется в **Scripting Define Symbols** руками.

Сцена под неустановленный контейнер откроется с потерянными скриптами — это ожидаемо.

Подробнее — в [DI-интеграции](../../Documentation/ru/12-di-integration.md) и [View Initializers](../../Documentation/ru/11-view-initializers.md).
