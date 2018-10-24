using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class JobPageViewModel : Base
    {
        public ICommand ProcessesInitialization { get; set; }
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
            for(int index = 0; index < ConnectedPage.ConnectedPageCollection.Count; index++)
            {
                var pageObject = ConnectedPage.ConnectedPageCollection[index].RefPage.DataContext as PulseDesignModel;
                LoadCopyCollection = pageObject.PulseCollection;
                ProcessControlColleciton.Add(new WorkloadControlViewModel()
                {
                    NumberLoad = (index + 1).ToString(),
                    IpLoad = ConnectedPage.ConnectedPageCollection[index].Name,
                    LoadColleciton = LoadCopyCollection,
                    NumberRepetitions = Convert.ToInt32(pageObject.NumberRepetitions)
                });
            }
        }
    }
}
