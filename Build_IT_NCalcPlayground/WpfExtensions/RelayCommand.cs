using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Build_IT_NCalcPlayground.WpfExtensions
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Predicate<T> _canExecute = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
             Execute((T)parameter);
        }

         public bool CanExecute(T parameter)
         {
             return _canExecute is null || _canExecute(parameter);
         }
         
         public void Execute(T parameter)
         {
             _execute(parameter);
         }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute = null;
        private readonly Func<bool> _canExecute = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute) : this(execute, null)
        {
        }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        bool ICommand.CanExecute(object parameter = null)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter = null)
        {
            Execute();
        }

        public bool CanExecute()
        {
            return _canExecute is null || _canExecute();
        }

        public void Execute()
        {
            _execute();
        }
    }
}
