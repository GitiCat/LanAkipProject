using Akip.LanConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Windows;
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
        private List<NetworkStream> NetworkStreamCollection { get; set; }

        public ObservableCollection<LanConnectionModel> LanCollection { get; set; }
        public LanConnectionModel SelectLanCollection { get; set; }

        public ICommand ConnectionToLan { get; set; }
        public ICommand DisconnectionFromLan { get; set; }

        public ICommand RemoteLan { get; set; }
        public ICommand LocalLan { get; set; }

        public ConnectionViewModel()
        {
            ConnectedIP = new ObservableCollection<ConnectionViewModel>();
            TcpClientColleciton = new List<TcpClient>();
            NetworkStreamCollection = new List<NetworkStream>();

            LanCollection = LanConnectionModel.Instance.GetLanCollection();
            if(LanCollection.Count != 0) {
                SelectLanCollection = LanCollection[0];
            }

            ConnectionToLan = new RCommand( () => {
                string ip = SelectLanCollection.ConnectionIP;
                int port = 4001;
                ConnectionMethod( ip, port );
                ip = null;
            } );
            DisconnectionFromLan = new RCommand( () => { DisconnectionMethod(); } );

            RemoteLan = new RCommand( () => {
                byte[] res_b = Encoding.ASCII.GetBytes( "REMOTE;" );
                int count = NetworkStreamCollection.Count;

                for (int i = 0; i < count; i++) {
                    NetworkStreamCollection[i].Write( res_b, 0, res_b.Length );
                }
            } );

            LocalLan = new RCommand( () => {
                byte[] res_b = Encoding.ASCII.GetBytes( "LOCAL;" );
                int count = NetworkStreamCollection.Count;

                for (int i = 0; i < count; i++) {
                    NetworkStreamCollection[i].Write( res_b, 0, res_b.Length );
                }
            } );
        }

        private TcpClient ObjTcpConnect = null;

        private void ConnectionMethod(string ip, int port)
        {
            if (ObjTcpConnect == null)
                ObjTcpConnect = new TcpClient();

            if(ObjTcpConnect.Connected) {
                MessageBox.Show( $"Соединение с {ObjTcpConnect.Client} уже установлено..." );
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
                    
                }
            }
        }

        private void DisconnectionMethod()
        {
            if (TcpClientColleciton[ConnectionIPIndex].Connected) {
                NetworkStreamCollection[ConnectionIPIndex].Close();
                TcpClientColleciton[ConnectionIPIndex].Close();

                TcpClientColleciton[ConnectionIPIndex].Dispose();
                TcpClientColleciton[ConnectionIPIndex] = null;
                TcpClientColleciton.RemoveAt( ConnectionIPIndex );

                NetworkStreamCollection[ConnectionIPIndex].Dispose();
                NetworkStreamCollection[ConnectionIPIndex] = null;
                NetworkStreamCollection.RemoveAt( ConnectionIPIndex );

                ConnectedIP.RemoveAt( ConnectionIPIndex );
            }
        }
    }
}