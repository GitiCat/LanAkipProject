using Akip.LanConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class ConnectionViewModel : Base
    {
        public string ConnectedIPString { get; set; }
        private int _connectionIPIndex;
        public int ConnectionIPIndex
        {
            get { return _connectionIPIndex; }
            set { _connectionIPIndex = value; OnPropertyChanged( nameof( ConnectionIPIndex ) ); }
        }

        public ObservableCollection<ConnectionViewModel> ConnectedIP { get; set; }
        private List<TcpClient> TcpClientColleciton { get; set; }
        public static List<NetworkStream> NetworkStreamCollection { get; set; }

        public ObservableCollection<LanConnectionModel> LanCollection { get; set; }
        public LanConnectionModel SelectLanCollection { get; set; }
        
        public ICommand UpdateLanCollection { get; set; }
        public ICommand ConnectionToLan { get; set; }
        public ICommand DisconnectionFromLan { get; set; }
        public ICommand RemoteLan { get; set; }
        public ICommand LocalLan { get; set; }

        public ConnectionViewModel()
        {
            ConnectedIP = new ObservableCollection<ConnectionViewModel>();

            TcpClientColleciton = new List<TcpClient>();
            NetworkStreamCollection = new List<NetworkStream>();

            M_UpdateLanConnection();
            UpdateLanCollection = new RCommand( () => {
                M_UpdateLanConnection();
            } );

            ConnectionToLan = new RCommand( () => {
                string ip = SelectLanCollection.ConnectionIP;
                int port = 4001;
                ConnectionMethod( ip, port );
                ip = null;
            } );
            DisconnectionFromLan = new RCommand( () => { DisconnectionMethod(); } );
        }

        private void M_UpdateLanConnection()
        {
            LanCollection = LanConnectionModel.Instance.GetLanCollection();
            if (LanCollection.Count != 0) {
                SelectLanCollection = LanCollection[0];
            }
        }
        
        private TcpClient ObjTcpConnect = null;

        private void ConnectionMethod(string ip, int port)
        {
            if (ObjTcpConnect == null)
                ObjTcpConnect = new TcpClient();

            if(ObjTcpConnect.Connected) {
                MessageBox.Show( $"Соединение с {ip} уже установлено..." );
                return;
            }

            try {
                ObjTcpConnect = new TcpClient();
                ObjTcpConnect.Connect( ip, port );
                
            } catch (Exception objException) {
                MessageBox.Show( objException.ToString() );
                return;
            } finally {
                if (ObjTcpConnect.Connected) {

                    ConnectedIP.Add( new ConnectionViewModel() {
                        ConnectedIPString = ip
                    } );

                    TcpClientColleciton.Add( ObjTcpConnect );
                    NetworkStreamCollection.Add( ObjTcpConnect.GetStream() );

                    ConnectedPage.ConnectedPageCollection.Add( new ConnectedPage() {
                        Name = ip,
                        RefPage = new ProgramPage()
                    } );

                    MainViewModel.ConnectedButtonCollection.Add(
                        new ConnectedIPButtonViewModel {
                            ConnectedString = ip,
                            OpenConnectedIPProgramPage = new RCommand( () => {
                                var target = ConnectedPage.ConnectedPageCollection
                                    .Where( x => x.Name == ip ).FirstOrDefault() as ConnectedPage;
                                IoC.Get<CollectionViewModels>().CurrentPage 
                                    = ConnectedPage.ConnectedPageCollection[ConnectedPage.ConnectedPageCollection.IndexOf( target )].RefPage;
                            } )
                        } );
                }
            }
        }

        private void DisconnectionMethod()
        {
            if (TcpClientColleciton[ConnectionIPIndex].Connected) {
                NetworkStreamCollection[ConnectionIPIndex].Close();
                TcpClientColleciton[ConnectionIPIndex].Close();
                
                TcpClientColleciton[ConnectionIPIndex] = null;
                TcpClientColleciton.RemoveAt( ConnectionIPIndex );

                NetworkStreamCollection[ConnectionIPIndex].Dispose();
                NetworkStreamCollection[ConnectionIPIndex] = null;
                NetworkStreamCollection.RemoveAt( ConnectionIPIndex );

                MainViewModel.ConnectedButtonCollection.RemoveAt( ConnectionIPIndex );
                ConnectedIP.RemoveAt( ConnectionIPIndex );
            }
        }
    }
}