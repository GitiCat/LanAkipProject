using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Akip
{
    public class PulseViewModel : Base
    {
        private bool _isLoadOn;
        public bool IsLoadOn
        {
            get { return _isLoadOn; }
            set { _isLoadOn = value; OnPropertyChanged(nameof(IsLoadOn)); }
        }

        private bool _isLoadOff;
        public bool IsLoadOff
        {
            get { return _isLoadOff; }
            set { _isLoadOff = value; OnPropertyChanged(nameof(IsLoadOff)); }
        }

        private string _activeTime;
        public string ActiveTime
        {
            get { return _activeTime; }
            set { _activeTime = value; OnPropertyChanged(nameof(ActiveTime)); }
        }

        private string _numberRepetitions;
        public string NumberRepetitions
        {
            get { return _numberRepetitions; }
            set { _numberRepetitions = value; OnPropertyChanged(nameof(NumberRepetitions)); }
        }

        private string _inputNumberRepetitions;
        public string InputNumberRepetitions
        {
            get { return _inputNumberRepetitions; }
            set { _inputNumberRepetitions = value; OnPropertyChanged(nameof(InputNumberRepetitions)); }
        }

        //********Список импульсов********

        private string _t_Type;
        public string T_Type
        {
            get { return _t_Type; }
            set { _t_Type = value; OnPropertyChanged(nameof(T_Type)); }
        }

        private string _t_Time;
        public string T_Time
        {
            get { return _t_Time; }
            set { _t_Time = value; OnPropertyChanged(nameof(T_Time)); }
        }

        private ObservableCollection<PulseViewModel> _pulseCollection;
        public ObservableCollection<PulseViewModel> PulseCollection
        {
            get { return _pulseCollection; }
            set { _pulseCollection = value; OnPropertyChanged(nameof(PulseCollection)); }
        }

        private int _pulseCollectionSelectIndex;
        public int PulseCollectionSelectIndex
        {
            get { return _pulseCollectionSelectIndex; }
            set { _pulseCollectionSelectIndex = value; OnPropertyChanged(nameof(PulseCollectionSelectIndex)); }
        }

        private string _totalTimeInTable;
        public string TotalTimeInTable
        {
            get { return _totalTimeInTable; }
            set { _totalTimeInTable = value; OnPropertyChanged(nameof(TotalTimeInTable)); }
        }

        private TimeSpan _conversionTotalTime;
        public TimeSpan ConversionTotalTime
        {
            get { return _conversionTotalTime; }
            set { _conversionTotalTime = value; OnPropertyChanged(nameof(ConversionTotalTime)); }
        }

        private string _totalStageCount;
        public string TotalStageCount
        {
            get { return _totalStageCount; }
            set { _totalStageCount = value; OnPropertyChanged(nameof(TotalStageCount)); }
        }

        //********Команды управления********
        private ICommand _addStage;
        public ICommand AddStage
        {
            get { return _addStage; }
            set { _addStage = value; OnPropertyChanged(nameof(AddStage)); }
        }

        private ICommand _deleteStage;
        public ICommand DeleteStage
        {
            get { return _deleteStage; }
            set { _deleteStage = value; OnPropertyChanged(nameof(DeleteStage)); }
        }

        private ICommand _deleteAllStages;
        public ICommand DeleteAllStages
        {
            get { return _deleteAllStages; }
            set { _deleteAllStages = value; OnPropertyChanged(nameof(DeleteAllStages)); }
        }

        private ICommand _setRepetitions;
        public ICommand SetRepetitions
        {
            get { return _setRepetitions; }
            set { _setRepetitions = value; OnPropertyChanged(nameof(SetRepetitions)); }
        }
    }
}
