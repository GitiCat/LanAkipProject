using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Akip
{
    public class ProgramViewModel : Base
    {
        public bool IsCCModeActive
        {
            get { return (bool)GetValue(IsCCModeActiveProperty); }
            set { SetValue(IsCCModeActiveProperty, value); }
        }

        public static readonly DependencyProperty IsCCModeActiveProperty =
            DependencyProperty.Register("IsCCModeActive", 
                typeof(bool),
                typeof(ProgramViewModel), 
                new UIPropertyMetadata(false));

        
        public bool IsCRModeActive
        {
            get { return (bool)GetValue(IsCRModeActiveProperty); }
            set { SetValue(IsCRModeActiveProperty, value); }
        }

        public static readonly DependencyProperty IsCRModeActiveProperty =
            DependencyProperty.Register("IsCRModeActive", 
                typeof(bool), 
                typeof(ProgramViewModel), 
                new UIPropertyMetadata(false));

        //TODO: Дописать оставшиеся параметры bool с DependencyProperty
        
        private bool _isCV;
        internal bool IsCVModeActive
        {
            get { return _isCV; }
            set { _isCV = value; OnPropertyChanged( nameof( IsCVModeActive ) );  }
        }

        private bool _isCP;
        internal bool IsCPModeActive
        {
            get { return _isCP; }
            set { _isCP = value; OnPropertyChanged( nameof( IsCPModeActive ) ); }
        }

        private bool _IsTypeDischarge;
        internal bool IsTypeDischargeActive
        {
            get { return _IsTypeDischarge; }
            set { _IsTypeDischarge = value; OnPropertyChanged( nameof( IsTypeDischargeActive ) ); }
        }

        private bool _isTypePauseActive;
        internal bool IsTypePauseActive
        {
            get { return _isTypePauseActive; }
            set { _isTypePauseActive = value; OnPropertyChanged( nameof( IsTypeDischargeActive ) ); }
        }
        
        // --------- Определение команд----*
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
        private ICommand _cleanStageTable;
        public ICommand CleanStageTable
        {
            get { return _cleanStageTable; }
            set { _cleanStageTable = value; OnPropertyChanged(nameof(CleanStageTable)); }
        }
        private ICommand _importStageTable;
        public ICommand ImportStageTable
        {
            get { return _importStageTable; }
            set { _importStageTable = value; OnPropertyChanged(nameof(ImportStageTable)); }
        }
        private ICommand _exportStageTable;
        public ICommand ExportStageTable
        {
            get { return _exportStageTable; }
            set { _exportStageTable = value; OnPropertyChanged(nameof(ExportStageTable)); }
        }
        private ICommand _setMaxLoadValue;
        public ICommand SetMaxLoadValue
        {
            get { return _setMaxLoadValue; }
            set { _setMaxLoadValue = value; OnPropertyChanged(nameof(SetMaxLoadValue)); }
        }

        // --------- Определение строк----*
        private string _amperage;
        public string Amperage
        {
            get { return _amperage; }
            set { _amperage = value; OnPropertyChanged(nameof(Amperage)); }
        }
        private string _pulseTime;
        public string PulseTime
        {
            get { return _pulseTime; }
            set { _pulseTime = value; OnPropertyChanged(nameof(PulseTime)); }
        }
        private string _minLoadValue;
        public string MinLoadValue
        {
            get { return _minLoadValue; }
            set { _minLoadValue = value; OnPropertyChanged(nameof(MinLoadValue)); }
        }
        private string _maxLoadValue;
        public string MaxLoadValue
        {
            get { return _maxLoadValue; }
            set { _maxLoadValue = value; OnPropertyChanged(nameof(MaxLoadValue)); }
        }
        private string _currentUpperValue;
        public string CurrentUpperValue
        {
            get { return _currentUpperValue; }
            set { _currentUpperValue = value; OnPropertyChanged(nameof(CurrentUpperValue)); }
        }
        private string _totalNumberStage;
        public string TotalNumberStage
        {
            get { return _totalNumberStage; }
            set { _totalNumberStage = value; OnPropertyChanged(nameof(TotalNumberStage)); }
        }
        private string _totalProgramTime;
        public string TotalProgramTime
        {
            get { return _totalProgramTime; }
            set { _totalProgramTime = value; OnPropertyChanged(nameof(TotalProgramTime)); }
        }

        // ---------- Определение коллекций----*
        internal string NameMode { get; set; }
        internal string NameType { get; set; }
        internal float AmperageValue { get; set; }
        internal TimeSpan TimeValue { get; set; }

        private ObservableCollection<ProgramViewModel> _stageCollection;
        internal ObservableCollection<ProgramViewModel> StageCollection
        {
            get { return _stageCollection; }
            set { _stageCollection = value;
                OnPropertyChanged( nameof( StageCollection ) );
            }
        }
    }
}