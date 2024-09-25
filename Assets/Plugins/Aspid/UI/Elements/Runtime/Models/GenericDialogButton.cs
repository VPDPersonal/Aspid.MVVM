using System;

namespace Aspid.UI.Elements.Models
{
    public class GenericDialogButton
    {
        public string Text { get; }
            
        public Action CallBack { get; }

        public GenericDialogButton(string text, Action callBack)
        {
            Text = text;
            CallBack = callBack;
        }
    }
}