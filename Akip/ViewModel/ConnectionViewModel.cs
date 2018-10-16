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
        
        /// <summary>
        /// Предоставляет коллекцию адресов активных подключений
        /// </summary>
        public ObservableCollection<ConnectionViewModel> ConnectedIP { get; set; }
        /// <summary>
        ///     Предоставлят список активных подлкючений
        /// </summary>
        private List<TcpClient> TcpClientColleciton { get; set; }
        /// <summary>
        ///     Предоставляем список потоков для активных подключений
        /// </summary>
        public List<NetworkStream> NetworkStreamCollection { get; set; }

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

                    ConnectedPage.ConnectedPageCollection.Add(new ConnectedPage() {
                        Name = ip,
                        RefPage = new ProgramPage(new ProgramDesignModel())
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

        /// <summary>
        ///     Предоставляет метод отключения связи с выбранным оборудованием
        /// </summary>
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

                ConnectedPage.ConnectedPageCollection.RemoveAt(ConnectionIPIndex);

                MainViewModel.ConnectedButtonCollection.RemoveAt( ConnectionIPIndex );
                ConnectedIP.RemoveAt( ConnectionIPIndex );
            }
        }
    }
}