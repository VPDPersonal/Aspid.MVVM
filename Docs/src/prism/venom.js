/** Prism themes in the Ayu palette (Light and Dark). Code blocks sit on the article surface inside their frame, like the admonitions. */

const light = {
  plain: { color: '#2a2c31', backgroundColor: 'transparent' },
  // Ayu Light's hues darkened only to 4.6:1 on the inline-code chip (5.3:1 on white), so the code keeps the dark theme's
  // clear colours instead of a muted, earthy set. Strings lean a little greener and properties a little softer than
  // Ayu, which would otherwise darken into olive and alarm red.
  styles: [
    { types: ['comment', 'prolog', 'doctype', 'cdata'], style: { color: '#686d76' } },
    { types: ['punctuation'], style: { color: '#2a2c31' } },
    // A namespace nested in a type name (`System.` of `System.Type`) overrides the type's colour.
    { types: ['namespace'], style: { color: '#2a2c31' } },
    { types: ['keyword', 'operator', 'important'], style: { color: '#b24d05' } },
    { types: ['builtin', 'class-name', 'maybe-class-name', 'return-type'], style: { color: '#1670b0' } },
    { types: ['function'], style: { color: '#916204' } },
    { types: ['string', 'char', 'attr-value', 'inserted'], style: { color: '#3d7a00' } },
    { types: ['number', 'boolean', 'constant', 'symbol'], style: { color: '#8752bd' } },
    { types: ['regex'], style: { color: '#2b785f' } },
    { types: ['tag', 'selector', 'deleted'], style: { color: '#24748f' } },
    { types: ['attr-name', 'property', 'variable'], style: { color: '#c73246' } },
    { types: ['annotation', 'decorator', 'attribute'], style: { color: '#1670b0' } },
  ],
};

const dark = {
  plain: { color: '#bfbdb6', backgroundColor: 'transparent' },
  styles: [
    { types: ['comment', 'prolog', 'doctype', 'cdata'], style: { color: '#89919d' } },
    { types: ['punctuation'], style: { color: '#bfbdb6' } },
    // A namespace nested in a type name (`System.` of `System.Type`) overrides the type's colour.
    { types: ['namespace'], style: { color: '#bfbdb6' } },
    { types: ['keyword', 'operator', 'important'], style: { color: '#f29750' } },
    { types: ['builtin', 'class-name', 'maybe-class-name', 'return-type'], style: { color: '#73c0f8' } },
    { types: ['function'], style: { color: '#ffb454' } },
    { types: ['string', 'char', 'attr-value', 'inserted'], style: { color: '#b0d860' } },
    { types: ['number', 'boolean', 'constant', 'symbol'], style: { color: '#d2a6ff' } },
    { types: ['regex'], style: { color: '#95e6cb' } },
    { types: ['tag', 'selector', 'deleted'], style: { color: '#39bae6' } },
    { types: ['attr-name', 'property', 'variable'], style: { color: '#f07178' } },
    { types: ['annotation', 'decorator', 'attribute'], style: { color: '#73c0f8' } },
  ],
};

export default { light, dark };
