using System;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows;
using Akip.NetStream;
using System.Net.Sockets;

namespace Akip
{
    /// <summary>
    ///     Класс, предоставляющий методы для проведения 
    ///     испытания, включая системныю нагрузку
    /// </summary>
    public class Workload : INotifyPropertyChanged
    {
        /// <summary>
        ///     Объект коллекции, хранящий данные копию данных 
        ///     коллекции программы нагрузки для быстрого доступа
        /// </summary>
        private ObservableCollection<ProgramDesignModel> LoadCollection;
        /// <summary>
        ///     Количество элементов в коллекции
        /// </summary>
        private int LoadCollecitonElementCount;

        /// <summary>
        ///     Конструктор класса <see cref="Workload"/>
        /// </summary>
        /// <param name="_loadCollection">Коллецкия программы нагрузки</param>
        public Workload(ObservableCollection<ProgramDesignModel> _loadCollection)
        {
            LoadCollection = _loadCollection;
            LoadCollecitonElementCount = LoadCollection.Count;
        }

        //  Имя режима нагрузки
        private string Mode = string.Empty;
        //  Имя типа нагрузка (Разряд/Пауза)
        private string Type = string.Empty;
        //  Значение нагрузки на этапе
        private float Amperage = 0.0F;
        //  Премя этапа
        private double Time = 0.0;
        // Количество повторений программы нагрузки
        private int NumberRepetitions;

        /// <summary>
        ///     Предоставляет метод инициализации начальный 
        ///     параметров на старте нагрузки
        /// </summary>
        private void InitializeValue()
        {
            if (LoadCollection != null)
            {
                if (LoadCollecitonElementCount < 0)
                {
                    SetInitialValues(0);
                }
                else
                {
                    MessageBox.Show("Во время процесса запуска инициализации параметров возникла ошибка." +
                        "\nНе удалось загрузить данные коллекции, так как она не может быть пустой...",
                        "Initialize Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Во время запуска инициализации параметров возникла критическая ошибка." +
                    "\nНе удалось загрузить данные коллекции нагрузки...",
                    "Initialize Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        ///     Предоставляет метод записи начальный 
        ///     значений нагрузки из коллекции
        /// </summary>
        /// <param name="index">Индекс элемента в коллецкии</param>
        private void SetInitialValues(int index)
        {
            Mode = LoadCollection[index].NameMode;
            Type = LoadCollection[index].NameType;
            Amperage = LoadCollection[index].AmperageValue;
            Time = LoadCollection[index].TimeValue;
        }

        private Thread LoadThread = null;
        public void StartBackgroundThread()
        {
            if (LoadThread == null)
            {
                LoadThread = new Thread(new ThreadStart(LoadProcess))
                {
                    Name = "Akip Load Background Thread",
                    Priority = ThreadPriority.Highest
                };
            }
            else
            {
                MessageBox.Show("Произошла ошибка при попытке запуска процесса начала испытания." +
                    "\nНевозможно создать более одного процесса нагрузки для одного элемента...",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (LoadThread.ThreadState != ThreadState.Running)
                LoadThread.Start();
            else
            {
                MessageBox.Show("Произошла ошибка при попытке запуску процесса начала испытания." +
                    "\nПроцесс находится в режиме работу, невозможно запустить более одной копии для одного элемента...",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        ///     Предоставляем метод остановки фонового процесса нагрузки
        /// </summary>
        public void StopBackgroundThread()
        {
            if (LoadThread != null)
            {
                if (LoadThread.ThreadState == ThreadState.Running)
                {
                    LoadThread.Abort();
                }
                LoadThread = null;
            }
        }

        private int collection_index = 0;

        /// <summary>
        ///     Предоставляем метод, выполняющий 
        ///     инструкции проведения нагрузки
        /// </summary>
        private void LoadProcess()
        {
            
        }

        /// <summary>
        ///     Предоствлят метод, выполняющий 
        ///     инструкции по остановки процесса нагрузки
        /// </summary>
        private void LoadStoppingProcess(NetworkStream stream)
        {
            Request.SetSystemValue(stream, SystemCommands.LoadOff);

            if(TimerIsEnabled)
            {
                StopTimer();
            }

            LoadThread.Abort();
        }

        /// <summary>
        ///     Предоставляет метод ввода рабочего напряжения на нагрузке
        /// </summary>
        /// <param name="stream">Поток подключения передачи данных</param>
        /// <param name="value">Значение рабочего напряжения</param>
        private void SetLoadValue(NetworkStream stream, float value)
        {
            Request.SetSystemValue(stream, SystemCommands.LoadAmperage(value));
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

        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
