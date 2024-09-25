using System;
using UnityEngine;

namespace Aspid.UI.Elements.Models
{
    public class GenericDialog : IDisposable
    {
        public Action Closed;
        public Action<int> Clicked;

        public Sprite Icon { get; private set; }
        
        public string Title { get; private set; }
        
        public string Message { get; private set; }
        
        public bool IsIsAutoClose { get; private set; }
        
        public GenericDialogButton Button { get; private set; }
        
        public GenericDialogButton[] Buttons { get; private set; }
        
        public GenericDialog(string title, string message, GenericDialogButton button, params GenericDialogButton[] buttons) 
            : this(null, title, message, true, button, buttons) { }
        
        public GenericDialog(string title, string message, bool isAutoClose, GenericDialogButton button, params GenericDialogButton[] buttons) 
            : this(null, title, message, isAutoClose, button, buttons) { }
        
        public GenericDialog(Sprite icon, string title, string message, GenericDialogButton button, params GenericDialogButton[] buttons) 
            : this(null, title, message, true, button, buttons) { }
        
        public GenericDialog(Sprite icon, string title, string message, bool isAutoClose, GenericDialogButton button, params GenericDialogButton[] buttons)
        {
            if (buttons.Length == 0) throw new ArgumentException("Buttons cannot be empty.", nameof(buttons));
            
            Icon = icon;
            Title = title;
            Message = message;
            Buttons = buttons;
            IsIsAutoClose = isAutoClose;
        }

        public void Click()
        {
            Button.CallBack.Invoke();
            if (IsIsAutoClose) Close();
        }

        public void Click(int index)
        {
            Buttons[index].CallBack.Invoke();
            Clicked?.Invoke(index);
            
            if (IsIsAutoClose) Close();
        }

        private void Close()
        {
            Icon = null;
            Title = null;
            Message = null;
            Buttons = null;
            
            Closed?.Invoke();
        }

        public void Dispose() => Close();
    }
}