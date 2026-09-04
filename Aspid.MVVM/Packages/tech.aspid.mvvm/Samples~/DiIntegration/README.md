# DI Integration

The container owns the model and the ViewModel; `ViewInitializer` pulls the ViewModel out of it.

**You learn:** constructor injection into a ViewModel, `ViewInitializer` with `Resolve Type = Di`, `Initialize Stage = DiConstructor`, integration define symbols.

**Assumes:** [Counter](../01.%20Counter/README.md), scene `Counter (ViewInitializer)`.

A wallet: a coin count and an "Earn" button. Two identical scenes, one per container.

| Scene | Container | Registration |
|---|---|---|
| `Wallet (Zenject)` | Zenject `SceneContext` | `Scripts/Zenject/WalletInstaller.cs` |
| `Wallet (VContainer)` | VContainer `LifetimeScope` | `Scripts/VContainer/WalletLifetimeScope.cs` |

## ViewModel with a constructor dependency

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

No `[Inject]`. The ViewModel is an ordinary class with a constructor, and the container builds it like any other service.

## Registration

Zenject, a `MonoInstaller` on `SceneContext`:

```csharp
public override void InstallBindings()
{
    Container.Bind<Wallet>().AsSingle();
    Container.Bind<WalletViewModel>().AsSingle();
}
```

VContainer, a `LifetimeScope` in the scene:

```csharp
protected override void Configure(IContainerBuilder builder)
{
    builder.Register<Wallet>(Lifetime.Singleton);
    builder.Register<WalletViewModel>(Lifetime.Singleton);
}
```

Both files are wrapped in `#if ASPID_MVVM_ZENJECT_INTEGRATION` / `#if ASPID_MVVM_VCONTAINER_INTEGRATION`, so the sample compiles with either container alone.

## ViewInitializer

On the View object:

| Field | Value |
|---|---|
| **Views → Resolve Type** | `Component`, reference to `WalletView` |
| **ViewModel → Resolve Type** | `Di`, type `WalletViewModel` |
| **Initialize Stage** | `DiConstructor` |

`DiConstructor` initializes at the moment the container injects `ViewInitializer`: before `Awake`, with the container already built. Zenject needs only a `SceneContext`. For VContainer the View object must be in **Auto Inject Game Objects** on the `LifetimeScope`, otherwise injection never happens.

## Define symbols

- VContainer is the UPM package `jp.hadashikick.vcontainer`; `versionDefines` in the `.asmdef` turns on `ASPID_MVVM_VCONTAINER_INTEGRATION` automatically.
- Zenject is not a package, so add `ASPID_MVVM_ZENJECT_INTEGRATION` to **Scripting Define Symbols** yourself.

> [!NOTE]
> A scene for a container you have not installed opens with missing scripts. That is expected.

More in [DI Integration](../../Documentation/12-di-integration.md) and [View Initializers](../../Documentation/11-view-initializers.md).

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
