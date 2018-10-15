using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Akip.ViewModel.Additional
{
    public class WndTimer : Base
    {
        private DateTime StartCountdown;
        private TimeSpan StartTimeSpan;
        private TimeSpan TimeToEnd;
        private DateTime PauseTime;

        private DispatcherTimer Timer;

        public WndTimer(TimeSpan timerInterval)
        {
            Timer = new DispatcherTimer
            {
                Interval = timerInterval
            };

            Timer.Tick += delegate
            {
                var now = DateTime.Now;
                var elapsed = now.Subtract(StartCountdown);
                TimeToEnd = StartTimeSpan.Subtract(elapsed);
            };
        }
    }
}
