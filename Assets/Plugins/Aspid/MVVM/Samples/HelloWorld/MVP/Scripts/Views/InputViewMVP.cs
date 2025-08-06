using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class InputViewMVP : MonoBehaviour
    {
        public event UnityAction Clicked
        {
            add => _sayButton.onClick.AddListener(value);
            remove => _sayButton.onClick.RemoveListener(value);       
        }

        [SerializeField] private Button _sayButton;
        [SerializeField] private TMP_InputField _inputField;

        public string Text
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }
    }
}