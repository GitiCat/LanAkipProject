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
        #region Режимы нагрузки

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

        public bool IsCVModeActive
        {
            get { return (bool)GetValue(IsCVModeActiveProperty); }
            set { SetValue(IsCVModeActiveProperty, value); }
        }

        public static readonly DependencyProperty IsCVModeActiveProperty =
            DependencyProperty.Register("IsCVModeActive", 
                typeof(bool),
                typeof(ProgramViewModel),
                new UIPropertyMetadata(false));

        public bool IsCPModeActive
        {
            get { return (bool)GetValue(IsCPModeActiveProperty); }
            set { SetValue(IsCPModeActiveProperty, value); }
        }

        public static readonly DependencyProperty IsCPModeActiveProperty =
            DependencyProperty.Register("IsCPModeActive",
                typeof(bool),
                typeof(ProgramViewModel),
                new UIPropertyMetadata(false));

        #endregion

        #region Тип этапа

        public bool IsTypeDischargeActive
        {
            get { return (bool)GetValue(IsTypeDischargeActiveProperty); }
            set { SetValue(IsTypeDischargeActiveProperty, value); }
        }

        public static readonly DependencyProperty IsTypeDischargeActiveProperty =
            DependencyProperty.Register("IsTypeDischargeActive", 
                typeof(bool), 
                typeof(ProgramViewModel), 
                new UIPropertyMetadata(false));
        
        public bool IsTypePauseActive
        {
            get { return (bool)GetValue(IsTypePauseActiveProperty); }
            set { SetValue(IsTypePauseActiveProperty, value); }
        }

        public static readonly DependencyProperty IsTypePauseActiveProperty =
            DependencyProperty.Register("IsTypePauseActive", 
                typeof(bool), 
                typeof(ProgramViewModel), 
                new UIPropertyMetadata(false));

        #endregion

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