using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Akip
{
    public class ProgramDesignModel : ProgramViewModel
    {
        public static ProgramDesignModel Instance => new ProgramDesignModel();

        private int _selectedIndexStageCollection;
        public int SelectedIndexStageCollection
        {
            get { return _selectedIndexStageCollection; }
            set { _selectedIndexStageCollection = value;
                OnPropertyChanged( nameof( SelectedIndexStageCollection ) );
            }
        }


        public ProgramDesignModel()
        {
            StageCollection = new ObservableCollection<ProgramViewModel>();

            AddStage = new RCommand( () => { M_AddStage(); } );
            DeleteStage = new RCommand( () => { M_DeleteStage(); } );
            CleanStageTable = new RCommand( () => { M_CreanStageTable(); } );
            ImportStageTable = new RCommand( () => { } );
            ExportStageTable = new RCommand( () => { } );
            SetMaxLoadValue = new RCommand( () => { } );
        }

        private string Temp_NameModeActive = null;
        private string Temp_NameTypeActive = null;
        private float Temp_AmperageValue;
        private TimeSpan Temp_TimeValue = TimeSpan.Zero;

        #region Добавление нового этапа нагрузки
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

            Temp_AmperageValue = GetAmperageValue();
            if (float.IsNaN( Temp_AmperageValue )) {
                MessageBox.Show( "Невозможно добавить новый этап!\nВведите значение тока импульса на этапе...",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error );
                return;
            }

            Temp_TimeValue = GetPulseTimeValue();
            if(Temp_TimeValue == TimeSpan.Zero) {
                MessageBox.Show( "Невожможно добавить новый этап!\nВведите значение времени импульса на этапе...",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error );
                return;
            }

            // Добавление нового элемента коллекции
            StageCollection.Add( new ProgramViewModel() {
                NameMode = Temp_NameModeActive,
                NameType = Temp_NameTypeActive,
                AmperageValue = Temp_AmperageValue,
                TimeValue = Temp_TimeValue
            } );
        }

        /// <summary>
        ///     Возвращает имя выбранного режима нагрузки
        /// </summary>
        /// <returns></returns>
        private string GetNameModeActive()
        {
            if (IsCCModeActive) return "CC";
            else if (IsCRModeActive) return "CR";
            else if (IsCVModeActive) return "CV";
            else if (IsCPModeActive) return "CP";
            else return string.Empty;
        }

        /// <summary>
        ///     Возвращает имя выбранного типа этапа
        /// </summary>
        /// <returns></returns>
        private string GetNameTypeActive()
        {
            if (IsTypeDischargeActive) return "Разряд";
            else if (IsTypePauseActive) return "Пауза";
            else return string.Empty;
        }

        /// <summary>
        ///     Возвращает значение тока импульса на этапе типа <see cref="float"/>
        /// </summary>
        /// <returns></returns>
        private float GetAmperageValue()
        {
            return ConvertToNumberFromStringValue("Ток импульса", Amperage);
        }
        
        /// <summary>
        ///     Возвращает значение времени продолжительности импульса 
        ///     на этапе типа <see cref="TimeSpan"/>
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetPulseTimeValue()
        {
            return ConvertToTimeFromStringValue( "Время импульса", PulseTime );
        }
        #endregion

        #region Удаление элемента коллекции
        /// <summary>
        ///     Предоставляет метод удаления выбранного 
        ///     элемента коллекции.
        /// </summary>
        private void M_DeleteStage()
        {
            if (StageCollection.Count == 0)
                return;

            StageCollection.RemoveAt( SelectedIndexStageCollection );
            MessageBox.Show( "Этап удален.", "Оповещение",
                MessageBoxButton.OK, MessageBoxImage.Information );
        }

        #endregion

        #region Очистка коллекции
        /// <summary>
        ///     Предоставляет метод очистки всех
        ///     элементов коллекции.
        /// </summary>
        private void M_CreanStageTable()
        {
            if(StageCollection.Count == 0) {
                MessageBox.Show( "Не удалось провести очистку данных..." +
                    "Таблица пуста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show( "Вы действительно хотите безвозвратно очистить таблицу этапов?" +
                "После удаления востановить данные будет невозможно...", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation ) == MessageBoxResult.Yes) {
                StageCollection.Clear();
            }
        }
        #endregion

        private void M_ImportStageTable()
        {

        }

        private void M_ExportStageTable()
        {

        }

        private void M_SetMaxLoadValue()
        {
            if (!string.IsNullOrWhiteSpace( MaxLoadValue )) {
                MessageBox.Show( "Невозможно задать верхнее значение нагрузки..." +
                    "\nПожалуйста введите значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error );
                return;
            }

        }

        #region Конвертеры
        /// <summary>
        ///     Возвращает преобразованную строку типа <see cref="string"/>
        ///     в число с плавающей запятой типа <see cref="float"/>
        /// </summary>
        /// <param name="valueName">Имя параметра преобразования</param>
        /// <param name="param">Входной параметр для преобразования</param>
        /// <returns></returns>
        private float ConvertToNumberFromStringValue(string valueName, string param)
        {
            string interim_param = string.Empty;

            try {
                if (( string.IsNullOrWhiteSpace( param ) )
                    || ( param.Any( c => char.IsLetter( c ) ) )) {
                    MessageBox.Show( $"Строка '{valueName}' не может быть пустой или иметь буквы," +
                        $" либо содержать в себе различные символы...",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation );
                    throw new ArgumentException();
                }

                if (param.Count( c => c == '.' ) > 1 || param.Count( c => c == ',' ) > 1) {
                    MessageBox.Show( $"Входная строка '{valueName}' имеет неверный формат..." +
                        $"\n Формат строки должен быть приведен к типу '0.0000'" );
                    throw new ArgumentException();
                }

                if (param.Contains( "." ) || param.Contains( "," )) {
                    if (param.Contains( "," )) {
                        interim_param = string.Format( "{0:f4}", Convert.ToDouble( param ) );
                    } else {
                        param.Replace( ".", "," );
                        interim_param = string.Format( "{0:f4}", Convert.ToDouble( param ) );
                    }
                } else {
                    interim_param = string.Format( "{0:f4}", Convert.ToInt32( param ) );
                }
            }
            catch(Exception objException) {
                MessageBox.Show( $"Произошла ошибка преобразования строк..." +
                    $"\nОшибка: {objException.ToString()}" );
            }

            return float.Parse(interim_param);
        }

        /// <summary>
        ///     Возвращает преобразованную строку типа <see cref="string"/>
        ///     в временной интервал в формате переменной <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="valueName">Имя параметра преобразования</param>
        /// <param name="param">Входной параметр для преобразования</param>
        /// <returns></returns>
        public TimeSpan ConvertToTimeFromStringValue(string valueName, string param)
        {
            TimeSpan result = TimeSpan.Zero;
            string format = "G";
            CultureInfo culture = CultureInfo.CurrentCulture;

            try {

                if ((string.IsNullOrWhiteSpace( param )) 
                    || ( param.Any( c => char.IsLetter( c ) ) )) {
                    MessageBox.Show( $"Входная строка '{valueName}' имеет неверный формат..." +
                        $"Строка не можеть быть пустой или иметь буквы, либо содержать в себе различные символы" +
                        $"Допустимый формат: 0.000 (секунды.миллисекунды" );
                    throw new ArgumentException();
                }
                
                if (TimeSpan.TryParseExact( param, format, culture, out TimeSpan interim )) {
                    result = interim;
                }

            } catch (Exception objException) {
                MessageBox.Show( $"Произошла ошибка преобразования временного интервала..." +
                    $"\nОшибка: {objException.ToString()}" );
            }

            return result;
        }
        #endregion
    }
}