using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class JobPageViewModel : Base
    {
        private ICommand _processesInitialization;
        /// <summary>
        ///     Команда, предназначенная для начала процесса инициализации процессов нагрузки
        /// </summary>
        public ICommand ProcessesInitialization
        {
            get { return _processesInitialization; }
            set { _processesInitialization = value; OnPropertyChanged(nameof(ProcessesInitialization)); }
        }

        private ICommand _startAllLoadProcesses;
        /// <summary>
        ///     Команда, предназначенная для остановки всех запущенных ранее процессов нагрузки
        /// </summary>
        public ICommand StartAllLoadProcesses
        {
            get { return _startAllLoadProcesses; }
            set { _startAllLoadProcesses = value; OnPropertyChanged(nameof(StartAllLoadProcesses)); }
        }

        private ICommand _stopAllLoadProcesses;
        /// <summary>
        ///     Предоставет команду для остановки всех ранее запущенных процессов нагрузки
        /// </summary>
        public ICommand StopAllLoadProcesses
        {
            get { return _stopAllLoadProcesses; }
            set { _stopAllLoadProcesses = value; OnPropertyChanged(nameof(StopAllLoadProcesses)); }
        }

        private ObservableCollection<WorkloadControlViewModel> _processControlCollection;
        /// <summary>
        ///     Возвращает или задает коллекцию процессов нагрузки
        /// </summary>
        public ObservableCollection<WorkloadControlViewModel> ProcessControlColleciton
        {
            get { return _processControlCollection; }
            set { _processControlCollection = value; OnPropertyChanged(nameof(ProcessControlColleciton)); }
        }

        private ObservableCollection<PulseViewModel> _loadCopyCollection;
        /// <summary>
        ///     Возвращает или задает коллекцию нагрузки для процесса
        /// </summary>
        public ObservableCollection<PulseViewModel> LoadCopyCollection
        {
            get { return _loadCopyCollection; }
            set { _loadCopyCollection = value; OnPropertyChanged(nameof(LoadCopyCollection)); }
        }

        /// <summary>
        ///     Конструктов класса <see cref="JobPageViewModel"/>
        /// </summary>
        public JobPageViewModel()
        {
            ProcessControlColleciton = new ObservableCollection<WorkloadControlViewModel>();
            ProcessesInitialization = new RCommand(() => { ProcessesInitializationMethod(); });
            StartAllLoadProcesses = new RCommand(() => { LaunchProcessCollection(); });
            StopAllLoadProcesses = new RCommand(() => { StopingProcessCollection(); });
        }

        /// <summary>
        ///     Предоставяет метод инициализации процессов нагрузки
        /// </summary>
        private void ProcessesInitializationMethod()
        {
            if (ProcessControlColleciton.Count > 0)
                ProcessControlColleciton.Clear();
            for(int index = 0; index < ConnectedPage.ConnectedPageCollection.Count; index++)
            {
                var pageObject = ConnectedPage.ConnectedPageCollection[index].RefPage.DataContext as PulseDesignModel;
                LoadCopyCollection = pageObject.PulseCollection;
                var netPageObject = IoC.Get<CollectionViewModels>().ConnectedNetworkStream[index];
                ProcessControlColleciton.Add(new WorkloadControlViewModel()
                {
                    NumberLoad = (index + 1).ToString(),
                    IpLoad = ConnectedPage.ConnectedPageCollection[index].Name,
                    LoadColleciton = LoadCopyCollection,
                    NumberRepetitions = Convert.ToInt32(pageObject.NumberRepetitions),
                    StageCount = pageObject.TotalStageCount,
                    TotalTime = pageObject.TotalTimeInTable,
                    Network = netPageObject
                });
            }
        }

        /// <summary>
        ///     Предоставляет метод запуска всех созданных процессов нагрузки
        /// </summary>
        private void LaunchProcessCollection()
        {
            foreach(var control in ProcessControlColleciton)
            {
                control.RunningNewTimer();
            }
        }

        /// <summary>
        ///     Предоставляет метод оставноки всех запущенных процессов нагрузки
        /// </summary>
        private void StopingProcessCollection()
        {
            foreach(var control in ProcessControlColleciton)
            {
                control.StopTimer();
            }
        }
    }
}
