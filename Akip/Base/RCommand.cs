using System;
using System.Windows.Input;

namespace Akip
{
    public class RCommand : ICommand
    {
        #region Private members
        private Action mAction;
        #endregion

        #region Public events
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        #region Command methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
        #endregion
    }
}
