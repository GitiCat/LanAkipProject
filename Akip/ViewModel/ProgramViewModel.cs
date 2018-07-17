using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Akip
{
    public class ProgramViewModel : Base
    {
        // --------- Определение активных модулей----*
        private bool _isCC;
        internal bool IsCCModeActive
        {
            get { return _isCC; }
            set { _isCC = value; OnPropertyChanged( nameof( IsCCModeActive ) ); }
        }

        private bool _isCR;
        internal bool IsCRModeActive
        {
            get { return _isCR; }
            set { _isCR = value; OnPropertyChanged( nameof( IsCRModeActive ) ); }
        }

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
        internal ICommand AddStage { get; set; }
        internal ICommand DeleteStage { get; set; }
        internal ICommand CleanStageTable { get; set; }
        internal ICommand ImportStageTable { get; set; }
        internal ICommand ExportStageTable { get; set; }
        internal ICommand SetMaxLoadValue { get; set; }

        // --------- Определение строк----*
        internal string Amperage { get; set; }
        internal string PulseTime { get; set; }
        internal string MinLoadValue { get; set; }
        internal string MaxLoadValue { get; set; }
        internal string CurrentUpperValue { get; set; }
        internal string TotalNumberStage { get; set; }
        internal string TotalProgramTime { get; set; }

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