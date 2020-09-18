using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApiConsumerDemo.Commands
{
    /// <summary>
    /// Async Command with automatic firing of CanExecuteChanged
    /// 
    /// Based on John Thiriet AsynCommand
    /// Source : https://johnthiriet.com/mvvm-going-async-with-async-command/
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    class AsyncCommand : IAsyncCommand
    {
        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #region ICommand methods implementation
        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            /// We ignore the warning, since this way of executing
            /// is the sync way
            ExecuteAsync();
        }
        #endregion
    }
}
