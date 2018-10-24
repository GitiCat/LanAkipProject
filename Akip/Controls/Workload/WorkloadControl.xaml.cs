using System.Windows.Controls;

namespace Akip
{
    /// <summary>
    /// Логика взаимодействия для WorkloadControl.xaml
    /// </summary>
    public partial class WorkloadControl : UserControl
    {
        public WorkloadControl(WorkloadControlViewModel controlViewModel)
        {
            InitializeComponent();
            DataContext = controlViewModel;
        }
    }
}
