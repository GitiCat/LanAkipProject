using System.Windows.Input;

namespace Akip
{
    public class ConnectedIPButtonViewModel : Base
    {
        private string _connectedString;
        public string ConnectedString
        {
            get {
                return _connectedString;
            }
            set {
                _connectedString = value;
                OnPropertyChanged( nameof( ConnectedString ) );
            }
        }

        public ICommand OpenConnectedIPProgramPage { get; set; }
    }
}
