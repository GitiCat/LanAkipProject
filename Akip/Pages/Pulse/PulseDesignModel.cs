using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Akip
{
    /// <summary>
    ///     Класс для создания инструкции нагрузки
    /// </summary>
    public class PulseDesignModel : PulseViewModel
    {
        /// <summary>
        ///     Экземплям класса для доступа к статическим методам
        /// </summary>
        public static PulseDesignModel Instance => new PulseDesignModel();

        /// <summary>
        ///     Конструктор класса <see cref="PulseDesignModel"/>
        /// </summary>
        public PulseDesignModel()
        {
            PulseCollection = new ObservableCollection<PulseViewModel>();
            PulseCollection.CollectionChanged += PulseColleciton_CollectionChanged;
            TotalTimeInTable = "0";
            TotalStageCount = "0";
            CommandInitialization();
        }

        /// <summary>
        ///     Предоставляем метод инициализации команд управления
        /// </summary>
        private void CommandInitialization()
        {
            AddStage = new RCommand(() => { AddingNewStage(); });
            DeleteStage = new RCommand(() => { DeleteSelectedStage(); });
            DeleteAllStages = new RCommand(() => { DeleteAllStagesInTable(); });
            SetRepetitions = new RCommand(() => { SetEnteredNumber(); });
        }

        private void PulseColleciton_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    ScoreCollectionItem();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    ScoreCollectionItem();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    ScoreCollectionItem();
                    break;
                default:
                    break;
            }
        }

        private void ScoreCollectionItem()
        {
            TotalStageCount = PulseCollection.Count.ToString();
            TotalTimeInTable = ConversionTotalTimeToString();
        }

        private string ConversionTotalTimeToString()
        {
            double tempTime = 0.0;
            TimeSpan tempSpan;

            for(int index = 0; index < PulseCollection.Count; index++)
            {
                tempSpan = TimeSpan.Parse(PulseCollection[index].T_Time);
                tempTime += tempSpan.TotalSeconds;
            }

            tempSpan = TimeSpan.FromSeconds(tempTime);
            return tempSpan.ToString("hh':'mm':'ss'.'fff");
        }

        /// <summary>
        ///     Предоставляет метод дабавления данных в таблицу этапов
        /// </summary>
        private void AddingNewStage()
        {
            if(TimePusle == null || TimePusle == TimeSpan.Zero)
            {
                MessageBox.Show("Невозможно добавить новый этап..." +
                    "\nВведите время выполнения этапа.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TypePulse))
            {
                MessageBox.Show("Невозможно добавить новый этап...\n" +
                    "Выберите тип этапа.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PulseCollection.Add(new PulseViewModel()
            {
                T_Time = TimePusle.ToString("hh':'mm':'ss'.'fff"),
                T_Type = TypePulse
            });
        }

        /// <summary>
        ///     Предоставляем метод удаления выбранного этапа в таблице
        /// </summary>
        private void DeleteSelectedStage()
        {
            PulseCollection.RemoveAt(PulseCollectionSelectIndex);
        }

        /// <summary>
        ///     Предоставляет метод очистки коллекции этапов нагрузки
        /// </summary>
        private void DeleteAllStagesInTable()
        {
            PulseCollection.Clear();
        }

        /// <summary>
        ///     Предоставляет метод ввода числа повторений программы
        /// </summary>
        private void SetEnteredNumber()
        {
            if (!string.IsNullOrEmpty(InputNumberRepetitions))
            {
                NumberRepetitions = InputNumberRepetitions;
            }
        }
        
        /// <summary>
        ///     Возвращает тип работы
        /// </summary>
        private string TypePulse
        {
            get {
                if (IsLoadOn)
                    return "Разряд";
                else if (IsLoadOff)
                    return "Пауза";
                else
                    return null;
            }
        }

        /// <summary>
        ///     Возвращает время работу нагрузки
        /// </summary>
        public TimeSpan TimePusle
        {
            get {
                TimeSpan tempTime;
                string temp = ActiveTime;

                if(temp.Contains("."))
                    temp = temp.Replace(".", ",");

                double timeConvert = Convert.ToDouble(temp);
                tempTime = TimeSpan.FromSeconds(timeConvert);

                return tempTime;
            }
        }

        /// <summary>
        ///     Возвращает число повторов заданной программы
        /// </summary>
        public int RepetitionCount
        {
            get {
                return Convert.ToInt32(NumberRepetitions);
            }
        }
    }
}