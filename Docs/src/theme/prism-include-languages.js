import prismIncludeLanguages from '@theme-original/prism-include-languages';

// Root namespaces of the code on this site: a name starting with one of them is a namespace, not a type.
const ROOT_NAMESPACES = 'System|UnityEngine|UnityEditor|Unity|Aspid|Microsoft';

export default function includeLanguages(Prism) {
  prismIncludeLanguages(Prism);

  // A namespace stays in the normal text colour: `using Aspid.MVVM.StarterKit;` (the theme leaves `namespace` plain) and
  // the qualifier of a type, which Prism otherwise folds into the class name — in `System.Type` only `Type` is coloured.
  // The shared inside grammar of every `class-name` pattern is rebuilt in place, the qualifier first.
  const typeInside = Prism.languages.csharp['class-name'][0].inside;
  const typeTokens = {...typeInside};
  Object.keys(typeInside).forEach((key) => delete typeInside[key]);
  Object.assign(typeInside, {
    'namespace-qualifier': {
      pattern: new RegExp(`\\b(?:${ROOT_NAMESPACES})(?:\\s*\\.\\s*[A-Za-z_]\\w*)*(?=\\s*\\.\\s*[A-Za-z_])`),
      alias: 'namespace',
      inside: {punctuation: /\./},
    },
  }, typeTokens);

  const preprocessor = Prism.languages.csharp.preprocessor;
  // Keep directive arguments in the normal text color and include # in the keyword.
  delete preprocessor.alias;
  delete preprocessor.inside.directive.lookbehind;

  // Prism colours an event's delegate type only before `;` or `=`, and only the first part of `class Outer.Inner`.
  Prism.languages.insertBefore('csharp', 'class-name', {
    'nested-type-declaration': {
      pattern: /(\b(?:class|enum|interface|record|struct)\s+)[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)+/,
      lookbehind: true,
      alias: 'class-name',
      inside: {punctuation: /\./},
    },
    // Prism leaves a type used for static access plain (`EditorApplication.delayCall`, `Resources.Load`, `DamageType.Fire`).
    // A PascalCase name followed by `.` counts as a type unless a `.` precedes it, so member chains keep their colours.
    // Root namespaces stay plain, as in `System.Type`, which InlineCode colours the same way.
    'static-type-access': {
      pattern: new RegExp(`(^|[^.\\w])(?!(?:${ROOT_NAMESPACES})\\b)[A-Z]\\w*(?=\\s*\\.\\s*[A-Za-z_])`),
      lookbehind: true,
      alias: 'class-name',
    },
    'event-type': {
      pattern: /(\bevent\s+)[A-Za-z_][\w.]*(?:<[^<>;{}]*>)?(?=\s+[A-Za-z_]\w*)/,
      lookbehind: true,
      alias: 'class-name',
      inside: {punctuation: /[.<>,]/},
    },
  });
}
