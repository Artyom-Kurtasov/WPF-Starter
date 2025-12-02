using System.Windows;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.ViewModels;

namespace WPF_Starter
{
    /// <summary>
    /// Логика взаимодействия для DataGridWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly PagingSettings _pagingSettings;
        private readonly DataGridManager _gridManager;
        private readonly AppDbContext _dataBase;
        private readonly Search _search;
        public MainWindow(PagingSettings pagingSettings, DataGridManager dataGridManager, Search search, AppDbContext appDbContext,
            MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _pagingSettings = pagingSettings;
            _viewModel = mainWindowViewModel;
            _gridManager = dataGridManager;
            _dataBase = appDbContext;
            _search = search;
            DataContext = _viewModel;
        }
    }
}
