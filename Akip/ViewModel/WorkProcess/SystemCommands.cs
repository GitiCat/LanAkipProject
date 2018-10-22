namespace Akip
{
    public class SystemCommands
    {
        /// <summary>
        ///     Команда ввода устройства в режим дистанционного управления
        /// </summary>
        public const string Remote = "REMOTE;";
        /// <summary>
        ///     Команда ввода устройства в режим локального (ручного) управеления
        /// </summary>
        public const string Local = "LOCAL;";
        /// <summary>
        ///     Команда включения нагрузки на приборе
        /// </summary>
        public const string LoadOn = "LOAD ON;";
        /// <summary>
        ///     Команда выключения нагрузки на приборе
        /// </summary>
        public const string LoadOff = "LOAD OFF;";

        /// <summary>
        ///     Команда ввода верхнего значения напряжения на оборудовании
        /// </summary>
        /// <param name="value">Значение предельного напряжения</param>
        /// <returns>Возвращает строкове представление команды
        /// ввода верхнего (предельного) значения напряжения</returns>
        public static string AmperageUpper(float value)
        {
            return $"CC:HIGH {value};";
        }
        
        /// <summary>
        ///     Команда ввода рабочего значения напряжения на оборудовании
        /// </summary>
        /// <param name="value">Значение рабочего напряжения</param>
        /// <returns>Возвращает строкое представление команды 
        /// ввода рабочего напряжения</returns>
        public static string LoadAmperage(float value)
        {
            return $"CC:LOW {value};";
        }
    }
}
