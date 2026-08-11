// Объявление живёт в тестовой сборке намеренно: оно и есть проверка того, что генератор работает
// внутри Unity, а не только в собственных юнит-тестах на Roslyn. Свойство выбрано такое,
// для которого в пакете биндера нет, — иначе рукописный класс победил бы генерируемый.
[assembly: Aspid.MVVM.StarterKit.GenerateBinders(
    typeof(UnityEngine.UI.CanvasScaler), "referencePixelsPerUnit",
    Prefix = "GeneratedCanvasScalerReferencePixels",
    Menu = "Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Reference Pixels Per Unit",
    SerializedName = "m_ReferencePixelsPerUnit")]
