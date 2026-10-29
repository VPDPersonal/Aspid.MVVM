import React from 'react';
import Link from '@docusaurus/Link';
import CodeBlock from '@theme/CodeBlock';
import ThemedImage from '@theme/ThemedImage';
import useBaseUrl from '@docusaurus/useBaseUrl';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import styles from './styles.module.css';

/*
 * The tutorials overview: one card per sample, the learning path first. A sample with `capture` shows its recording
 * per site theme, `static/img/samples/<id>.gif` and `<id>-light.gif`, as the card's preview; a sample without a scene
 * shows an excerpt of its code (`code`); the others show their step on the site's dot grid.
 */
const samples = [
  { id: 'counter', capture: true, step: '01', feature: 'Counter',
    en: 'The smallest possible Aspid.MVVM setup: a button increments a number shown in a text.',
    ru: 'Первый пример обучающего маршрута. Кнопка увеличивает счётчик — число отображается в тексте.' },
  { id: 'greeter', capture: true, step: '02', feature: 'Greeter',
    en: 'The user types a name and the greeting updates on every keystroke. No buttons, no bootstrap code.',
    ru: 'Пользователь вводит имя — приветствие обновляется в реальном времени.' },
  { id: 'bind-modes', capture: true, step: '03', feature: 'Bind Modes',
    en: 'Every BindMode on one screen, plus a converter of your own in a binder slot.',
    ru: 'Все четыре режима биндинга на одном экране и конвертер в слоте биндера.' },
  { id: 'stats', capture: true, step: '04', feature: 'Stats',
    en: 'Commands with a parameter, CanExecute, and a draft that reaches the model only on Confirm.',
    ru: 'Команда с параметром, CanExecute и черновик, который попадает в модель только по Confirm.' },
  { id: 'todo-list', capture: true, step: '05', feature: 'Todo List',
    en: 'A list of tasks with add, edit, delete, search and completion. Collections, child ViewModels and a dialog spawned at runtime.',
    ru: 'Коллекции, синхронизация, фильтрация и диалоги на примере списка задач.' },
  { id: 'custom-binder', capture: true, step: '06', feature: 'Custom Binder',
    en: 'How to bind a component the StarterKit does not know about.',
    ru: 'Биндер для компонента, о котором StarterKit не знает.' },
  { id: 'virtualized-list', capture: true, feature: 'Virtualized List',
    en: 'Large collections without a GameObject per item: only visible rows exist, filtered and ordered from the Inspector.',
    ru: 'Виртуализация, FilteredList, фильтры и компараторы.' },
  { id: 'dynamic-view-model', capture: true, feature: 'Dynamic ViewModel',
    en: 'MVVM without source generation, for data whose shape is only known at runtime.',
    ru: 'MVVM без генерации кода, для данных, форма которых известна только в рантайме.' },
  { id: 'di-integration', capture: true, feature: 'DI Integration',
    en: 'The container owns the model and the ViewModel; ViewInitializer pulls the ViewModel out of it.',
    ru: 'Контейнер владеет моделью и ViewModel, ViewInitializer забирает ViewModel из него.' },
  { id: 'example-scripts', feature: 'API Reference Scripts',
    // Scripts/ViewModels/Commands/Ex1CommandViewModel.cs, shortened.
    code: `[ViewModel]
public partial class Ex1CommandViewModel
{
    [Bind] private string _text;
    [Bind] private readonly IRelayCommand<int> _command2;

    public Ex1CommandViewModel() =>
        _command2 = new RelayCommand<int>(Do2, CanDo2);

    private void Do2(int arg1) => Text = $"Command2 {arg1}";
}`,
    en: 'Minimal compilable snippets, one attribute or feature per file. There are no scenes here; read the code.',
    ru: 'Минимальные компилируемые примеры, по одному атрибуту или возможности на файл. Сцен нет, только код.' },
];

function Capture({sample, index}) {
  const dark = useBaseUrl(`/img/samples/${sample.id}.gif`);
  const light = useBaseUrl(`/img/samples/${sample.id}-light.gif`);
  return (
    <div className={`${styles.preview} ${styles.scene}`}>
      <ThemedImage sources={{dark, light}} alt={sample.feature} width="1024" height="576" loading={index < 2 ? 'eager' : 'lazy'} />
    </div>
  );
}

function Preview({sample, ru, index}) {
  if (sample.capture) return <Capture sample={sample} index={index} />;
  if (sample.code) {
    return (
      <div className={`${styles.preview} ${styles.code}`}>
        <CodeBlock language="csharp">{sample.code}</CodeBlock>
      </div>
    );
  }
  return (
    <div className={`${styles.preview} ${styles.placeholder}`} aria-hidden="true">
      <span className={styles.step}>{sample.step ?? '✦'}</span>
      <span className={styles.kind}>{sample.step ? (ru ? 'Маршрут' : 'Path') : (ru ? 'Возможность' : 'Feature')}</span>
    </div>
  );
}

function Card({sample, index, ru}) {
  return (
    <Link to={`/tutorials/${sample.id}`} className={styles.card}>
      <Preview sample={sample} ru={ru} index={index} />
      <div className={styles.content}>
        <p className={styles.feature}>{sample.feature}</p>
        <p>{sample[ru ? 'ru' : 'en']}</p>
      </div>
    </Link>
  );
}

export default function SamplesGallery() {
  const {i18n} = useDocusaurusContext();
  const ru = i18n.currentLocale === 'ru';
  return (
    <div className={styles.page}>
      <header className={styles.header}>
        <h1>{ru ? 'Уроки' : 'Tutorials'}</h1>
        <p>{ru
          ? 'Примеры из Package Manager: шесть шагов маршрута, по одной идее на шаг, и отдельные возможности. README каждого примера и есть урок.'
          : 'Samples from the Package Manager: a six-step path that teaches one concept per step, and standalone features. Each sample\'s README is the tutorial.'}</p>
      </header>
      <div className={styles.grid}>
        {samples.map((sample, index) => <Card key={sample.id} sample={sample} index={index} ru={ru} />)}
      </div>
    </div>
  );
}
