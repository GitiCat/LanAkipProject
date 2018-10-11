using System.Windows.Controls;

namespace Akip
{
    /// <summary>
    /// Логика взаимодействия для ProgramPage.xaml
    /// </summary>
    public partial class ProgramPage : Page
    {
        public ProgramPage(ProgramDesignModel designModel)
        {
            InitializeComponent();
            DataContext = designModel;
        }
    }
}
