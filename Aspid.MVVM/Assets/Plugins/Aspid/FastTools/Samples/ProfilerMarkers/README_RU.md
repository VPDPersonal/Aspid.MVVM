# Пример ProfilerMarkers

Демонстрирует три поддерживаемые формы вызова `this.Marker()`. В каждой
форме source-генератор Aspid.FastTools заменяет место вызова уникальным
`ProfilerMarker`, привязанным к `(type, method, line)`. `.WithName(...)`
не обязателен — если его не указать, генератор автоматически назовёт
маркер по имени содержащего типа и метода.

## Поддерживаемые формы

1. **Блок `using`-statement с явным именем.** Область маркера — блок;
   полное отображаемое имя имеет вид `"{TypeName}.{WithName} ({line})"`,
   поэтому в `WithName` передавайте только короткий суффикс.
   ```csharp
   using (this.Marker().WithName("Physics")) // Profiler: "FrameProfiler.Physics (<line>)"
       SimulatePhysics();
   ```
2. **`using`-declaration без `WithName`.** Область маркера — остаток
   содержащего блока; генератор именует маркер по имени содержащего
   метода.
   ```csharp
   using var _ = this.Marker(); // Profiler: "FrameProfiler.SimulateInput (<line>)"
   ```
3. **Комбинированная форма.** Метод-широкий `using`-declaration в паре с
   вложенным `using`-statement — удобно, когда нужен один внешний маркер
   на весь метод и более узкий — на «горячий» подэтап. Оба маркера
   получают автоимя по методу; разные номера строк дают разные записи в
   Profiler.
   ```csharp
   using var _ = this.Marker(); // Profiler: "FrameProfiler.SimulateAudio (<outer-line>)"
   using (this.Marker())        // Profiler: "FrameProfiler.SimulateAudio (<inner-line>)"
   {
       // Некоторый код.
   }
   ```

## Как запустить

1. Откройте `Scenes/ProfilerMarkers.unity` — в сцене уже есть GameObject с
   `FrameProfiler`. Вариант `Prefabs/ProfilerMarkers.prefab` можно добавить
   в собственные сцены.
2. Откройте `Window → Analysis → Profiler`.
3. Войдите в Play Mode и осмотрите CPU-трек на наличие именованных маркеров.

## Где смотреть

- `Scripts/FrameProfiler.cs:18,21,24` — три маркера верхнего уровня с явными
  именами в `Update` (`FrameProfiler.Physics`, `FrameProfiler.AI`,
  `FrameProfiler.Render`).
- `Scripts/FrameProfiler.cs:43` — вложенный маркер `FrameProfiler.AI.Agent`
  в `SimulateAI`, генерируемый раз за итерацию цикла, отображается под
  областью `AI`.
- `Scripts/FrameProfiler.cs:67` — форма `using`-declaration без `WithName`
  в `SimulateInput`; генератор именует маркер по имени метода.
- `Scripts/FrameProfiler.cs:83,86` — комбинированная форма в
  `SimulateAudio`: внешний метод-широкий `using`-declaration плюс
  вложенный `using`-statement вокруг `MixAudio()`.

Каждый блок `using` (statement или declaration) автоматически запускает и
завершает сгенерированный маркер, поэтому Profiler показывает точное
self-time для каждой фазы.
