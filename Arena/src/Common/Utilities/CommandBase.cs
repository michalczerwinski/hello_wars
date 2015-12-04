using System;
using System.Windows.Input;

namespace Common.Utilities
{
    public abstract class CommandBase : ICommand
    {
        private event EventHandler CanExecuteChangedInternal;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public void OnCanExecuteChanged()
        {
            var handler = CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual bool CanExecute(object parameter = null)
        {
            return true;
        }

        public abstract void Execute(object parameter = null);
    }
}
