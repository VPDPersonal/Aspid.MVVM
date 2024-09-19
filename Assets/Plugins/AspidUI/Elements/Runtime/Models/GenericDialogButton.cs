using System;

namespace Plugins.AspidUI.Elements.Runtime.Models
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