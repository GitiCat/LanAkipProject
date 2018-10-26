using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows.Controls;

namespace Akip
{
    public class CollectionViewModels : Base
    {
        public ObservableCollection<Page> ConnectionPageIPAddress { get; set; }

        public CollectionViewModels()
        {
            ConnectionPageIPAddress = new ObservableCollection<Page>();
            ConnectedNetworkStream = new ObservableCollection<NetworkStream>();
        }

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set {
                _currentPage = value;
                OnPropertyChanged( nameof( CurrentPage ) );
            }
        }

        private int _selectedIndexCButtonCollection;
        public int SelectedIndexCButtonCollection
        {
            get { return _selectedIndexCButtonCollection; }
            set {
                _selectedIndexCButtonCollection = value;
                OnPropertyChanged( nameof( SelectedIndexCButtonCollection ) );
            }
        }

        private ObservableCollection<NetworkStream> _connectedNetworkStream;
        public ObservableCollection<NetworkStream> ConnectedNetworkStream
        {
            get { return _connectedNetworkStream; }
            set {
                _connectedNetworkStream = value;
                OnPropertyChanged( nameof( ConnectedNetworkStream ) );
            }
        }
    }
}
