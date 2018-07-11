using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace Akip.LanConnection
{
    public class LanConnectionModel : SingletonBase<LanConnectionModel>
    {
        public string ConnectionIP { get; set; }

        public ObservableCollection<LanConnectionModel> GetLanCollection()
        {
            ObservableCollection<LanConnectionModel> lan_collection = new ObservableCollection<LanConnectionModel>();

            try {
                for (int i = 2; i <= 100; i++) {

                    IPAddress iP = IPAddress.Parse( $"192.168.0.{i}" );

                    if (_TryPing( iP.ToString(), 4001, 25 )) {

                        lan_collection.Add( new LanConnectionModel() {
                            ConnectionIP = iP.ToString()
                        } );

                    }
                }

                if (lan_collection.Count == 0) {
                    lan_collection.Add( new LanConnectionModel() {
                        ConnectionIP = "Не найдено подключенных устройств..."
                    } );
                }
            }
            catch(Exception objException) {
                MessageBox.Show( objException.ToString() );
            }

            return lan_collection;
        }

        private static bool _TryPing(string strIpAddress, int intPort, int nTimeoutMsec)
        {
            Socket socket = null;
            try {
                socket = new Socket( AddressFamily.InterNetwork, 
                    SocketType.Stream, 
                    ProtocolType.Tcp );

                socket.SetSocketOption( SocketOptionLevel.Socket, 
                    SocketOptionName.DontLinger, 
                    false );

                IAsyncResult result = socket.BeginConnect( strIpAddress, 
                    intPort, 
                    null, 
                    null );
                bool success = result.AsyncWaitHandle.WaitOne( 
                    nTimeoutMsec, 
                    true );

                return socket.Connected;
            } catch {
                return false;
            } finally {
                if (null != socket)
                    socket.Close();
            }
        }
    }
}
