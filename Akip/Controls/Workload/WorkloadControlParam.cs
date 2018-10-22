namespace Akip
{
    public class WorkloadControlParam : Base
    {
        private string _numberLoad;
        public string NumberLoad
        {
            get { return _numberLoad; }
            set { _numberLoad = value; OnPropertyChanged(nameof(NumberLoad)); }
        }

        private string _ipLoad;
        public string IpLoad
        {
            get { return _ipLoad; }
            set { _ipLoad = value; OnPropertyChanged(nameof(IpLoad)); }
        }

        private string _stageCount;
        public string StageCount
        {
            get { return _stageCount; }
            set { _stageCount = value; OnPropertyChanged(nameof(StageCount)); }
        }

        private string _totalTime;
        public string TotalTime
        {
            get { return _totalTime; }
            set { _totalTime = value; OnPropertyChanged(nameof(TotalTime)); }
        }

        private string _minAmperageValue;
        public string MinAmperageValue
        {
            get { return _minAmperageValue; }
            set { _minAmperageValue = value; OnPropertyChanged(nameof(MinAmperageValue)); }
        }

        private string _currentStageNumber;
        public string CurrentStageNumber
        {
            get { return _currentStageNumber; }
            set { _currentStageNumber = value; OnPropertyChanged(nameof(CurrentStageNumber)); }
        }

        private string _stageTime;
        public string StageTime
        {
            get { return _stageTime; }
            set { _stageTime = value; OnPropertyChanged(nameof(StageTime)); }
        }

        private string _countdown;
        public string Countdown
        {
            get { return _countdown; }
            set { _countdown = value; OnPropertyChanged(nameof(Countdown)); }
        }

        private string _currentAmperage;
        public string CurrentAmperage
        {
            get { return _currentAmperage; }
            set { _currentAmperage = value; OnPropertyChanged(nameof(CurrentAmperage)); }
        }

        private string _currentThreadStat;
        public string CurrentThreadStat
        {
            get { return _currentThreadStat; }
            set { _currentThreadStat = value; OnPropertyChanged(nameof(CurrentThreadStat)); }
        }

        private string _currentThreadPriory;
        public string CurrentThreadPriory
        {
            get { return _currentThreadPriory; }
            set { _currentThreadPriory = value; OnPropertyChanged(nameof(CurrentThreadPriory)); }
        }
    }
}