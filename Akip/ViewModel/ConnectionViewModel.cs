using Akip.LanConnection;
using Akip.NetStream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class ConnectionViewModel : Base
    {
        public static ConnectionViewModel Instance => new ConnectionViewModel();

        public string ConnectedIPString { get; set; }
        private int _connectionIPIndex;
        public int ConnectionIPIndex
        {
            get { return _connectionIPIndex; }
            set { _connectionIPIndex = value; OnPropertyChanged( nameof( ConnectionIPIndex ) ); }
        }
        
        /// <summary>
        /// Предоставляет коллекцию адресов активных подключений
        /// </summary>
        public ObservableCollection<ConnectionViewModel> ConnectedIP { get; set; }
        /// <summary>
        ///     Предоставлят список активных подлкючений
        /// </summary>
        public List<TcpClient> TcpClientColleciton { get; set; }
        /// <summary>
        ///     Предоставляем список потоков для активных подключений
        /// </summary>
        public static List<NetworkStream> NetworkStreamCollection { get; set; }

        /// <summary>
        ///     Предоставляет коллекцию активных подключений с оборудованием
        /// </summary>
        public ObservableCollection<LanConnectionModel> LanCollection { get; set; }
        /// <summary>
        ///     Предоставляет объект подключения, выбранный пользователем
        /// </summary>
        public LanConnectionModel SelectLanCollection { get; set; }
        
        /// <summary>
        ///     Предоставляет команду обновления списка доступных устройств
        /// </summary>
        public ICommand UpdateLanCollection { get; set; }
        /// <summary>
        ///     Предоставляет команду соединения с устройством
        /// </summary>
        public ICommand ConnectionToLan { get; set; }
        /// <summary>
        ///     Предоставляет метод для разрыва соединения с устройством
        /// </summary>
        public ICommand DisconnectionFromLan { get; set; }
        public ICommand RemoteLan { get; set; }
        public ICommand LocalLan { get; set; }

        /// <summary>
        ///     Конструктор класса
        /// </summary>
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

        /// <summary>
        ///     Предоставляем метод для обновления списка устройств
        ///     доступных для подключения
        /// </summary>
        private void M_UpdateLanConnection()
        {
            LanCollection = LanConnectionModel.Instance.GetLanCollection();
            if (LanCollection.Count != 0) {
                SelectLanCollection = LanCollection[0];
            }
        }
        
        /// <summary>
        ///     Объект для подключения
        /// </summary>
        private TcpClient ObjTcpConnect = null;

        private IPEndPoint objEndPointConnect = null;
        private IPAddress objClientIpAddress = null;

        /// <summary>
        ///     Объект страницы для подключения
        /// </summary>
        private Page ObjConnectPage = null;

        private ProgramDesignModel ObjPageModel = null;

        /// <summary>
        ///     Предоставляет метод для соединения оборудования 
        ///     по локальному подключению
        /// </summary>
        /// <param name="ip">Адрес устройства для подключения</param>
        /// <param name="port">Порт для подключения</param>
        private void ConnectionMethod(string ip, int port)
        {
            //if (ObjTcpConnect == null)
            //    ObjTcpConnect = new TcpClient();

            objClientIpAddress = IPAddress.Parse(ip);
            foreach(var item in TcpClientColleciton)
            {
                objEndPointConnect = (IPEndPoint)item.Client.RemoteEndPoint;

                if(objEndPointConnect.Address == objClientIpAddress)
                {
                    MessageBox.Show($"Соединение с утройством по адресу {objEndPointConnect.Address} уже произведено...",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }

            TcpClientColleciton.Add(new TcpClient() );
            
            try {
                //ObjTcpConnect.Connect( ip, port );
                TcpClientColleciton[TcpClientColleciton.Count - 1].Connect(ip, port);
                IoC.Get<CollectionViewModels>().ConnectedNetworkStream.Add( TcpClientColleciton[TcpClientColleciton.Count - 1].GetStream() );
                
            } catch (Exception objException) {
                MessageBox.Show( objException.ToString() );
                return;
            } finally {
                if (TcpClientColleciton[TcpClientColleciton.Count - 1].Connected) {

                    ConnectedIP.Add( new ConnectionViewModel() {
                        ConnectedIPString = ip
                    } );
                    
                    ConnectedPage.ConnectedPageCollection.Add(new ConnectedPage() {
                        Name = ip,
                        RefPage = new PulsePage(new PulseDesignModel())
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
                    Request.SetSystemValue(TcpClientColleciton[TcpClientColleciton.Count - 1].GetStream(), SystemCommands.Remote);
                }
            }
        }

        /// <summary>
        ///     Предоставляет метод отключения связи с выбранным оборудованием
        /// </summary>
        private void DisconnectionMethod()
        {
            if (TcpClientColleciton[ConnectionIPIndex].Connected) {

                Request.SetSystemValue(TcpClientColleciton[ConnectionIPIndex].GetStream(), SystemCommands.LoadOff);
                Request.SetSystemValue(TcpClientColleciton[ConnectionIPIndex].GetStream(), SystemCommands.Local);

                TcpClientColleciton[ConnectionIPIndex].GetStream().Close();
                TcpClientColleciton[ConnectionIPIndex].Close();
                
                TcpClientColleciton[ConnectionIPIndex] = null;
                TcpClientColleciton.RemoveAt( ConnectionIPIndex );
                
                ConnectedPage.ConnectedPageCollection.RemoveAt(ConnectionIPIndex);

                MainViewModel.ConnectedButtonCollection.RemoveAt( ConnectionIPIndex );
                ConnectedIP.RemoveAt( ConnectionIPIndex );
            }
        }
    }
}