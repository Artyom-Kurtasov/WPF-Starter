using MahApps.Metro.Controls;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.ViewModels;

namespace WPF_Starter
{
    public partial class MainWindow : MetroWindow
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly PagingSettings _pagingSettings;
        private readonly DataGridManager _gridManager;
        private readonly AppDbContext _dataBase;
        private readonly Search _search;

        public MainWindow(
            PagingSettings pagingSettings,
            DataGridManager dataGridManager,
            Search search,
            AppDbContext appDbContext,
            MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            _pagingSettings = pagingSettings;
            _gridManager = dataGridManager;
            _dataBase = appDbContext;
            _search = search;
            _viewModel = mainWindowViewModel;

            DataContext = _viewModel;

            Loaded += async (s, e) =>
            {
               
                Loaded -= (sender, args) => { }; 

                await _viewModel.LoadStartupDataAsync();
            };
        }
    }
}