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
        public ICommand ProcessesInitialization
        {
            get { return _processesInitialization; }
            set { _processesInitialization = value; OnPropertyChanged(nameof(ProcessesInitialization)); }
        }
        private ObservableCollection<WorkloadControlViewModel> _processControlCollection;
        public ObservableCollection<WorkloadControlViewModel> ProcessControlColleciton
        {
            get { return _processControlCollection; }
            set { _processControlCollection = value; OnPropertyChanged(nameof(ProcessControlColleciton)); }
        }

        private ObservableCollection<PulseViewModel> _loadCopyCollection;
        public ObservableCollection<PulseViewModel> LoadCopyCollection
        {
            get { return _loadCopyCollection; }
            set { _loadCopyCollection = value; OnPropertyChanged(nameof(LoadCopyCollection)); }
        }

        public JobPageViewModel()
        {
            ProcessControlColleciton = new ObservableCollection<WorkloadControlViewModel>();
            ProcessesInitialization = new RCommand(() => { ProcessesInitializationMethod(); });
        }

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
    }
}
