using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class StringFormatConverter : IConverter<string, string>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private string _format;

        public StringFormatConverter()
        {
            _format = string.Empty;
        }
        
        public StringFormatConverter(string format)
        {
            _format = format;
        }

        public string Convert(string value) =>
            string.IsNullOrEmpty(_format) ? value : string.Format(_format, value);
    }
}