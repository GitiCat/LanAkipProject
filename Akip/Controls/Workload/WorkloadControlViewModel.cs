using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;

namespace Akip
{
    public class WorkloadControlViewModel : WorkloadControlParam
    {
        private ObservableCollection<ProgramDesignModel> _loadCollection;
        public ObservableCollection<ProgramDesignModel> LoadColleciton
        {
            get { return _loadCollection; }
            set { _loadCollection = value; OnPropertyChanged(nameof(LoadColleciton)); }
        }

        private int _numberRepetitions;
        public int NumberRepetitions
        {
            get { return _numberRepetitions; }
            set { _numberRepetitions = value; OnPropertyChanged(nameof(NumberRepetitions)); }
        }

        private double _minVoltageValue;
        public double MinVoltageValue
        {
            get { return _minVoltageValue; }
            set { _minVoltageValue = value; OnPropertyChanged(nameof(MinVoltageValue)); }
        }

        public WorkloadControlViewModel() { }

        private int LoadIndex = 0;
        private Thread LoadBackgroundThread = null;

        public void Setup(NetworkStream netStream)
        {
            
        }

        private void Process()
        {

        }

        #region Реализация таймера работы
        //  Время запуска таймера
        private DateTime _startCountdown;
        //  Начальное время до окончания таймера
        private TimeSpan _startTimeSpan = TimeSpan.FromMinutes(20);
        //  Время до окончания таймера
        private TimeSpan _timeToEnd;
        //  Интервал таймера
        private TimeSpan _interval = TimeSpan.FromMilliseconds(1);
        //  Время паузы таймера
        private DateTime _pauseTime;

        //  Объект таймера
        private DispatcherTimer _timer;

        /// <summary>
        ///     Инициализация нового таймера
        /// </summary>
        public void TimerInitialize()
        {
            _timer = new DispatcherTimer
            {
                Interval = _interval
            };
            _timer.Tick += delegate
            {
                var now = DateTime.Now;
                var elapsed = now.Subtract(_startCountdown);
                TimeToEnd = _startTimeSpan.Subtract(elapsed);
            };

            StopTimer();
        }

        /// <summary>
        ///     Предоставляет параметр завершения времени работы таймера
        /// </summary>
        public TimeSpan TimeToEnd
        {
            get {
                return _timeToEnd;
            }

            set {
                _timeToEnd = value;
                if (value.TotalMilliseconds <= 0)
                {
                    StopTimer();
                    //TODO: Действия при завершении работы таймера на участке времени
                }

                OnPropertyChanged("StringCountdown");
            }
        }

        /// <summary>
        ///     Возвращает строку, представляющую собой текущее время работы таймера
        /// </summary>
        public string StringCountdown
        {
            get {
                var frmt = TimeToEnd.Minutes < 1 ? "ss\\.fff" : "mm\\:ss";
                return _timeToEnd.ToString(frmt);
            }
        }

        /// <summary>
        ///     Возвращает значение состяния таймера
        /// </summary>
        public bool TimerIsEnabled
        {
            get { return _timer.IsEnabled; }
        }

        /// <summary>
        ///     Предоставляет метод завершения работы таймера
        /// </summary>
        private void StopTimer()
        {
            if (TimerIsEnabled)
                _timer.Stop();
            TimeToEnd = _startTimeSpan;
        }

        /// <summary>
        ///     Предоставляет метод запуска работы таймера
        /// </summary>
        /// <param name="sDate">Время начала работы</param>
        private void StartTimer(DateTime sDate)
        {
            _startCountdown = sDate;
            _timer.Start();
        }

        /// <summary>
        ///     Предоставляет метод паузы текущего рабочего таймера
        /// </summary>
        private void PauseTimer()
        {
            _timer.Stop();
            _pauseTime = DateTime.Now;
        }

        private void ReleaseTimer()
        {
            var now = DateTime.Now;
            var elapsed = now.Subtract(_pauseTime);
            _startCountdown = _startCountdown.Add(elapsed);
            _timer.Start();
        }
        #endregion
    }
}
