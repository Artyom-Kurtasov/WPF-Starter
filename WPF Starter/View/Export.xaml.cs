using System.Windows;
using WPF_Starter.Services.Export;

namespace WPF_Starter.View
{
    /// <summary>
    /// Логика взаимодействия для ExportToExcel.xaml
    /// </summary>
    public partial class Export : Window
    {
        private readonly ExportNotifyer _exportNotifyer;
        public Export(ExportNotifyer exportNotifyer)
        {
            InitializeComponent();

            _exportNotifyer = exportNotifyer;
        }
    }
}
