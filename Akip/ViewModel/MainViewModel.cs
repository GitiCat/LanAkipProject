using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class MainViewModel : Base
    {
        /// <summary>
        ///     Ссылка на экземпляр окна <see cref="MainWindow"/>
        /// </summary>
        Window MainWindowExample = Application.Current.MainWindow;

        public static ObservableCollection<ConnectedIPButtonDesignModel> ConnectedButtonCollection { get; set; }
            = new ObservableCollection<ConnectedIPButtonDesignModel>();

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
        #endregion 

        public MainViewModel()
        {
            ConnectedButtonCollection.Add( new ConnectedIPButtonDesignModel { ConnectedString = "192.168.0.100" } );

            FramePageCollection = new ObservableCollection<Page> {
                new HomePage(),
                new JobPage(),
                new ProgramPage(),
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
            } );
            OpenJobPage = new RCommand( () => {
                CurrentPage = FramePageCollection[1];
            } );
            OpenProgramPage = new RCommand( () => {
                CurrentPage = FramePageCollection[2];
            } );
            OpenConnecitonPage = new RCommand( () => {
                CurrentPage = FramePageCollection[3];
            } );
            OpenInfoPage = new RCommand( () => {
                CurrentPage = FramePageCollection[4];
            } );
            OpenSettingPage = new RCommand( () => {
                CurrentPage = FramePageCollection[5];
            } );
            #endregion

            CurrentPage = new CommunicationPage();
        }
    }
}