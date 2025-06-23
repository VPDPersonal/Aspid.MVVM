using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class MomentInputSpeakerView : MonoBehaviour
    {
        public event UnityAction<string> TextChanged
        {
            add => _inputField.onValueChanged.AddListener(value);
            remove => _inputField.onValueChanged.RemoveListener(value);
        }
        
        [SerializeField] private TMP_InputField _inputField;
        
        public string Text
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }
    }
}