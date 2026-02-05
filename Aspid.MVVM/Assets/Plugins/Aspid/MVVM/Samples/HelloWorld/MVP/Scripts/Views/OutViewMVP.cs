using TMPro;
using System.Linq;
using UnityEngine;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class OutViewMVP : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _texts;

        public string Text
        {
            get => _texts.FirstOrDefault()?.text ?? string.Empty;
            set
            {
                foreach (var text in _texts)
                    text.text = value;
            }
        }
    }
}