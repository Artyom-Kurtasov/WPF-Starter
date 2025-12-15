using MahApps.Metro.Controls;
using WPF_Starter.Services.Notifiers;

namespace WPF_Starter.View
{
    /// <summary>
    /// Логика взаимодействия для ExportToExcel.xaml
    /// </summary>
    public partial class Export : MetroWindow
    {
        private readonly ExportNotifyer _exportNotifyer;
        public Export(ExportNotifyer exportNotifyer)
        {
            InitializeComponent();

            _exportNotifyer = exportNotifyer;
        }
    }
}
