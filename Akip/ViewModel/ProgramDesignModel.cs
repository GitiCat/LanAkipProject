using System.Windows;

namespace Akip
{
    public class ProgramDesignModel : ProgramViewModel
    {
        public static ProgramDesignModel Instance => new ProgramDesignModel();
        public ProgramDesignModel()
        {
            AddStage = new RCommand( () => { } );
            DeleteStage = new RCommand( () => { } );
            CleanStageTable = new RCommand( () => { } );
            ImportStageTable = new RCommand( () => { } );
            ExportStageTable = new RCommand( () => { } );
            SetMaxLoadValue = new RCommand( () => { } );
        }

        private string Temp_NameModeActive = null;
        private string Temp_NameTypeActive = null;
        private double? Temp_AmperageValue = null;

        private void M_AddStage()
        {
            Temp_NameModeActive = GetNameModeActive();
            if (string.IsNullOrWhiteSpace( Temp_NameModeActive )) {
                MessageBox.Show( "Невозможно добавить новый этап!\nВыберите режим нагрузки...",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error );
                return;
            }

            Temp_NameTypeActive = GetNameTypeActive();
            if (string.IsNullOrWhiteSpace( Temp_NameTypeActive )) {
                MessageBox.Show( "Невозможно добавить новый этап!\nВыберите тип нового этапа...",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error );
                return;
            }

            
        }

        private string GetNameModeActive()
        {
            if (IsCCModeActive) return "CC";
            else if (IsCRModeActive) return "CR";
            else if (IsCVModeActive) return "CV";
            else if (IsCPModeActive) return "CP";
            else return string.Empty;
        }

        private string GetNameTypeActive()
        {
            if (IsTypeDischargeActive) return "Разряд";
            else if (IsTypePauseActive) return "Пауза";
            else return string.Empty;
        }
    }
}
