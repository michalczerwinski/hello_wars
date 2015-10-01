using System;
using System.Windows.Input;

namespace Common.Utilities
{
    public class CommandBase : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private event EventHandler CanExecuteChangedInternal;

        public CommandBase()
            : this(DefaultCanExecute)
        {
        }

        public CommandBase(Predicate<object> canExecute)
        {
          if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            _canExecute = canExecute;
        }

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

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        public virtual void Execute(object parameter = null)
        {
        }

        public void OnCanExecuteChanged()
        {
            var handler = CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
