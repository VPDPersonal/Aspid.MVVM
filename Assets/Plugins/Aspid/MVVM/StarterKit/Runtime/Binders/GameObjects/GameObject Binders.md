# GameObject Binder

## Content
* **Simple**
  * [GameObject Binder - Tag](#gameobject-binder---tag)
  * [GameObject Binder - Tag Switcher](#gameobject-binder---tag-switcher)
  * [GameObject Binder - Visible](#gameobject-binder---visible)
* **MonoBehaviour**
  * [GameObject MonoBinder - Tag](#gameobject-monobinder---tag)
  * [GameObject MonoBinder - Tag Enum](#gameobject-monobinder---tag-enum)
  * [GameObject MonoBinder - Tag Enum Group](#gameobject-monobinder---tag-enum-group)
  * [GameObject MonoBinder - Tag Switcher](#gameobject-monobinder---tag-switcher)
  * [GameObject MonoBinder - Visible](#gameobject-monobinder---visible)
  * [GameObject MonoBinder - Visible Enum](#gameobject-monobinder---visible-enum)
  * [GameObject MonoBinder - Visible Enum Group](#gameobject-monobinder---visible-enum-group)

***
## GameObject Binder - Tag

**GameObjectTagBinder** связывает `string` значение из **ViewModel** со свойством `tag` у **GameObject**.
Поддерживает конвертацию значения.

### Examples

``` csharp
[View]
public partial class View : MonoView
{
    // Иницилизация через инспектор
    [SerializeField] private GameObjectTagBinder _binder1;
    
    // Иницизилация через свойство для чтения
    private GameObjectTagBinder Binder2 => new(gameObject, value => $"Converted {value}");
}
```

### Definition

**Namespace: Aspid.MVVM.StarterKit.Binders**

``` csharp
public class GameObjectTagBinder : Binder, IBinder<string>
```

**Inheritance: Object -> Binder -> GameObjectTagBinder**

**Implements: IBinder\<string\>**

### Constructors

| Parameters                                | Description                                                                                                                                                                            |
|-------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| GameObject, Func<string?, string?>        | Создает **binder**, который связывает `string` значение из **ViewModel** со свойство `tag` у **GameObject** с возможностью конвертации значения из **ViewModel**.                      |
| GameObject, IConverter<string?, string?>? | Создает **binder**, который связывает `string` значение из **ViewModel** со свойство `tag` у **GameObject** с возможностью конвертации значения из **ViewModel** через **IConverter**. |
| GameObject                                | Создает **binder**, который связывает `string` значение из **ViewModel** со свойство `tag` у **GameObject**.                                                                           |

### SerializeFields

| Type                          | Name        | Description                                                                                               |
|-------------------------------|-------------|-----------------------------------------------------------------------------------------------------------|
| GameObject                    | _gameObject | **GameObject**, у которого будет связываться свойство `tag`. **Обязательное поле**.                       |
| IConverter<string?, string?>? | _converter  | Конвертер исходного значения в новое значение для свойства tag у **GameObject**. **Необязательное поле**. |

### Methods

| Definition              | Description                                                                                                               |
|-------------------------|---------------------------------------------------------------------------------------------------------------------------|
| SetValue(string? value) | Устанавливает значение для свойства `tag` у "GameObject". Вызывается автоматически при изменении значения во "ViewModel". |

Метод "SetValue" отвечает за обновление свойство `tag` у "GameObject" в соответствии с изменениями во "ViewModel". Если используется конвертер, значение будет преобразовано перед присвоением.

***

## GameObject Binder - Tag Switcher

**GameObjectTagSwitcherBinder** связывает `bool` значение из **ViewModel** со свойством `tag` у **GameObject**.

### Examples

``` csharp
[View]
public partial class View : MonoView
{
    // Иницилизация через инспектор
    [SerializeField] private GameObjectTagSwitcherBinder _binder1;

    // Иницизилация через свойство для чтения
    private GameObjectTagSwitcherBinder Binder2 => new(gameObject, "TrueTag", "FalseTag");
}
```

### Definition

**Namespace: Aspid.MVVM.StarterKit.Binders**

``` csharp
public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<string>
```

**Inheritance: Object -> Binder -> SwitcherBinder\<string\> -> GameObjectTagSwitcherBinder**

### Constructors

| Parameters                 | Description                                                                                                |
|----------------------------|------------------------------------------------------------------------------------------------------------|
| GameObject, string, string | Создает **binder**, который связывает `bool` значение из **ViewModel** со свойство `tag` у **GameObject**. |

### SerializeFields

| Type       | Name        | Description                                                                       |
|------------|-------------|-----------------------------------------------------------------------------------|
| GameObject | _gameObject | GameObject, у которого будет переключаться значение `tag`. **Обязательное поле**. |
| string     | _trueValue  | Значение, которое будет устанавливаться в свойство `tag` при значение `true`      |
| string     | _falseValue | Значение, которое будет устанавливаться в свойство `tag` при значение `false`     |

***

## GameObject Binder - Visible

**GameObjectVisibleBinder** связывает `bool` из **ViewModel** с видимостью (активностью) **GameObject**.
Поддерживает инвертирование значения.

### Examples

``` csharp
[View]
public partial class View : MonoView
{
    // Иницилизация через инспектор
    [SerializeField] private GameObjectVisibleBinder _binder1;
    
    // Иницизилация через свойство для чтения
    private GameObjectVisibleBinder Binder2 => new(gameObject, true);
}
```

### Definition

**Namespace: Aspid.MVVM.StarterKit.Binders**

``` csharp
public class GameObjectVisibleBinder : Binder, IBinder<bool>
```

**Inheritance: Object -> Binder -> GameObjectVisibleBinder**

**Implements: IBinder\<bool\>**

### Constructors

| Parameters       | Description                                                                                                                               |
|------------------|-------------------------------------------------------------------------------------------------------------------------------------------|
| GameObject       | Создает "binder", который связывает `bool` с видимостью (активностью) **GameObject**                                                      |
| GameObject, bool | Создает "binder", который связывает `bool` с видимостью (активностью) **GameObject**. Можно включить инвертирование получаемого значения. |

### SerializeFields

| Type       | Name        | Description                                                                           |
|------------|-------------|---------------------------------------------------------------------------------------|
| GameObject | _gameObject | GameObject, у которого будет изменятся видимость (активность). **Обязательное поле**. |
| bool       | _isInvert   | Если `true`, то инвертирует получаемое значение. По умолчанию `false`.                |

### Methods

| Definition           | Description                                                                                                      |
|----------------------|------------------------------------------------------------------------------------------------------------------|
| SetValue(bool value) | Устанавливает видимость (активность) "GameObject". Вызывается автоматически при изменении значения во ViewModel. |

Метод "SetValue" отвечает за обновление видимости (активности) "GameObject" в соответствии с изменениями во "ViewModel". Если `_isInvert = true`, значение будет инвертировано перед использованием.

***

## GameObject MonoBinder - Tag


## GameObject MonoBinder - Tag Enum
## GameObject MonoBinder - Tag Enum Group
## GameObject MonoBinder - Tag Switcher
## GameObject MonoBinder - Visible
## GameObject MonoBinder - Visible Enum
## GameObject MonoBinder - Visible Enum Group

