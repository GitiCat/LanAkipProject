using System.Windows.Input;

namespace Akip
{
    public class ConnectedIPButtonViewModel : Base
    {
        public string ConnectedString { get; set; }
        public ICommand OpenConnectedIPProgramPage { get; set; }

        public ConnectedIPButtonViewModel()
        {

        }
    }
}
