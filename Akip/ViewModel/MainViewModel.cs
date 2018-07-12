using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class MainViewModel : Base
    {
        public static MainViewModel Instance => new MainViewModel();

        /// <summary>
        ///     Ссылка на экземпляр окна <see cref="MainWindow"/>
        /// </summary>
        Window MainWindowExample = Application.Current.MainWindow;

        public static ObservableCollection<ConnectedIPButtonViewModel> ConnectedButtonCollection { get; set; }

        private Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value)
                    return;
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        #region Инициализация команд для управления главным окном
        public ICommand Exit { get; set; }
        public ICommand Resize { get; set; }
        public ICommand Hidden { get; set; }
        #endregion


        #region Инициализация команд для управления меню приложения
        public ICommand OpenHomePage { get; set; }
        public ICommand OpenJobPage { get; set; }
        public ICommand OpenProgramPage { get; set; }
        public ICommand OpenConnecitonPage { get; set; }
        public ICommand OpenInfoPage { get; set; }
        public ICommand OpenSettingPage { get; set; }

        private ObservableCollection<Page> FramePageCollection;
        public ObservableCollection<Page> ConnectionIpPage { get; set; } 
            = new ObservableCollection<Page>();
        #endregion 

        public MainViewModel()
        {
            ConnectedButtonCollection = new ObservableCollection<ConnectedIPButtonViewModel>();
            ConnectedPage.ConnectedPageCollection = new ObservableCollection<ConnectedPage>();
            FramePageCollection = new ObservableCollection<Page> {
                new HomePage(),
                new JobPage(),
                new CommunicationPage(),
                new HelpAndInfoPage(),
                new SettingPage()
            };

            #region Создание команд для управления главным окном

            Exit = new RCommand( () => {
                if(MainWindowExample != null) {
                    MainWindowExample.Close();
                }
            } );

            Resize = new RCommand( () => {
                if(MainWindowExample != null) {
                    if(MainWindowExample.WindowState == WindowState.Normal) {
                        MainWindowExample.WindowState = WindowState.Maximized;
                    } else {
                        MainWindowExample.WindowState = WindowState.Normal;
                    }
                }
            } );

            Hidden = new RCommand( () => {
                if(MainWindowExample != null) {
                    MainWindowExample.WindowState = WindowState.Minimized;
                }
            } );

            #endregion

            #region Создание команд для управления меню приложения
            OpenHomePage = new RCommand( () => {
                CurrentPage = FramePageCollection[0];
                IoC.Get<CollectionViewModels>().CurrentPage = FramePageCollection[0];
            } );
            OpenJobPage = new RCommand( () => {
                CurrentPage = FramePageCollection[1];
                IoC.Get<CollectionViewModels>().CurrentPage = FramePageCollection[1];
            } );
            OpenProgramPage = new RCommand( () => {
                //CurrentPage = FramePageCollection[];
            } );
            OpenConnecitonPage = new RCommand( () => {
                CurrentPage = FramePageCollection[2];
                IoC.Get<CollectionViewModels>().CurrentPage = FramePageCollection[2];
            } );
            OpenInfoPage = new RCommand( () => {
                CurrentPage = FramePageCollection[3];
                IoC.Get<CollectionViewModels>().CurrentPage = FramePageCollection[3];
            } );
            OpenSettingPage = new RCommand( () => {
                CurrentPage = FramePageCollection[4];
                IoC.Get<CollectionViewModels>().CurrentPage = FramePageCollection[4];
            } );
            #endregion

            CurrentPage = new CommunicationPage();
        }
    }

    public class ConnectedPage : Base
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged( nameof( Name ) );
            }
        }

        private Page _refPage;
        public Page RefPage
        {
            get { return _refPage; }
            set {
                _refPage = value;
                OnPropertyChanged( nameof( RefPage ) );
            }
        }

        public static ObservableCollection<ConnectedPage> ConnectedPageCollection { get; set; }
    }
}