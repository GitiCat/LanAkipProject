using System.Windows.Input;

namespace Akip
{
    public class ProgramViewModel : Base
    {
        // --------- Определение активных модулей----*
        private bool _isCC;
        public bool IsCCModeActive
        {
            get { return _isCC; }
            set { _isCC = value; OnPropertyChanged( nameof( IsCCModeActive ) ); }
        }

        private bool _isCR;
        public bool IsCRModeActive
        {
            get { return _isCR; }
            set { _isCR = value; OnPropertyChanged( nameof( IsCRModeActive ) ); }
        }

        private bool _isCV;
        public bool IsCVModeActive
        {
            get { return _isCV; }
            set { _isCV = value; OnPropertyChanged( nameof( IsCVModeActive ) );  }
        }

        private bool _isCP;
        public bool IsCPModeActive
        {
            get { return _isCP; }
            set { _isCP = value; OnPropertyChanged( nameof( IsCPModeActive ) ); }
        }

        private bool _IsTypeDischarge;
        public bool IsTypeDischargeActive
        {
            get { return _IsTypeDischarge; }
            set { _IsTypeDischarge = value; OnPropertyChanged( nameof( IsTypeDischargeActive ) ); }
        }

        private bool _isTypePauseActive;
        public bool IsTypePauseActive
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

    }
}
