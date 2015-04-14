using System;
using System.Windows;
using System.Windows.Input;
using Youtube2mp3.Views;

namespace Youtube2mp3
{
    public class MinimizeCommand :ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow window = MainWindow.handler;
            if (window.WindowState == WindowState.Minimized)
            {
                window.ShowInTaskbar = true;
                window.WindowState = WindowState.Normal;
            }
            else if (window.WindowState == WindowState.Normal)
            {
                window.ShowInTaskbar = false;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}