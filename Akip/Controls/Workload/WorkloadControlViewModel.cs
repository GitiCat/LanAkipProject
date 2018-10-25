using Akip.NetStream;
using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Akip
{
    public class WorkloadControlViewModel : WorkloadControlParam
    {
        private ObservableCollection<PulseViewModel> _loadCollection;
        public ObservableCollection<PulseViewModel> LoadColleciton
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
        
        public WorkloadControlViewModel()
        {
            CommandInitialization();
            TimerInitialize();
        }

        /// <summary>
        ///     Предоставляет метод инициализации команд 
        ///     управления запущенным процессом
        /// </summary>
        private void CommandInitialization()
        {
            RunningTest = new RCommand(() => { StartTimer(DateTime.Now); });
            StopTest = new RCommand(() => { StopTimer(); });
            SuspendTest = new RCommand(() => { PauseTimer(); });
            ResumeTest = new RCommand(() => { ReleaseTimer(); });
        }
        
        #region Реализация таймера работы
        //  Время запуска таймера
        private DateTime _startCountdown;
        //  Начальное время до окончания таймера
        private TimeSpan _startTimeSpan;
        //  Время до окончания таймера
        private TimeSpan _timeToEnd;
        //  Интервал таймера
        private TimeSpan _interval = TimeSpan.FromMilliseconds(1);
        //  Время паузы таймера
        private DateTime _pauseTime;

        //  Объект таймера
        private DispatcherTimer _timer;

        private int SelectStageIndex = 0;

        /// <summary>
        ///     Инициализация нового таймера
        /// </summary>
        public void TimerInitialize()
        {
            TimeSpan _timeTemp = TimeSpan.Parse(LoadColleciton[0].T_Time);
            double _timeToDouble = _timeTemp.TotalSeconds;
            _startTimeSpan = TimeSpan.FromSeconds(_timeToDouble);


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
                    SelectStageIndex += 1;
                    if(SelectStageIndex != LoadColleciton.Count - 1)
                    {
                        ChangedStage(SelectStageIndex);
                        if (LoadColleciton[SelectStageIndex].T_Type == "Разряд")
                        {
                            Request.SetSystemValue(Network, SystemCommands.LoadOn);
                        }
                        else
                        {
                            Request.SetSystemValue(Network, SystemCommands.LoadOff);
                        }
                        StartTimer(DateTime.Now);
                    }
                    else
                    {
                        Request.SetSystemValue(Network, SystemCommands.LoadOff);
                        MessageBox.Show("Программа завершена");
                    }
                }

                OnPropertyChanged("StringCountdown");
            }
        }

        //TODO: Приделать после залет на репит
        private int RepetitionIndex = 1;

        TimeSpan tempTime;
        double tempTimeToDouble;

        private void ChangedStage(int index)
        {
            tempTime = TimeSpan.Parse(LoadColleciton[index].T_Time);
            tempTimeToDouble = tempTime.TotalSeconds;
            _startTimeSpan = TimeSpan.FromSeconds(tempTimeToDouble);
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

        /// <summary>
        ///     Предоставляет метод возобновления 
        ///     работы таймера после паузы
        /// </summary>
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
