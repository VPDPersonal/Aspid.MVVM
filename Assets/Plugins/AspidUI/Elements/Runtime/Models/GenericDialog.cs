using System;
using UnityEngine;

namespace Plugins.AspidUI.Elements.Runtime.Models
{
    public sealed class GenericDialog : IDisposable
    {
        public Action Closed;
        public Action<int> Clicked;

        public Sprite Icon { get; private set; }
        
        public string Title { get; private set; }
        
        public string Message { get; private set; }
        
        public bool IsIsAutoClose { get; private set; }
        
        public GenericDialogButton[] Buttons { get; private set; }
        
        public GenericDialog(string title, string message, params GenericDialogButton[] buttons) 
            : this(null, title, message, true, buttons) { }
        
        public GenericDialog(string title, string message, bool isAutoClose, params GenericDialogButton[] buttons) 
            : this(null, title, message, isAutoClose, buttons) { }
        
        public GenericDialog(Sprite icon, string title, string message, params GenericDialogButton[] buttons) 
            : this(null, title, message, true, buttons) { }
        
        public GenericDialog(Sprite icon, string title, string message, bool isAutoClose, params GenericDialogButton[] buttons)
        {
            if (buttons.Length == 0) throw new ArgumentException("Buttons cannot be empty.", nameof(buttons));
            
            Icon = icon;
            Title = title;
            Message = message;
            Buttons = buttons;
            IsIsAutoClose = isAutoClose;
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