using DocumentFormat.OpenXml.Office2010.CustomUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Starter.Config;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.SearchServices;

namespace WPF_Starter
{
    /// <summary>
    /// Логика взаимодействия для DataGridWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PagingSettings _pagingSettings;
        private readonly DataGridManager _gridManager;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        public MainWindow(PagingSettings pagingSettings, DataGridManager dataGridManager, Search search, AppDbContext appDbContext)
        {
            InitializeComponent();
            _pagingSettings = pagingSettings;
            _gridManager = dataGridManager;
            _search = search;
            _dataBase = appDbContext;
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_pagingSettings.Page > 0)
                _pagingSettings.Page--;

            _gridManager.SetDataGrid(peoplesGrid, _dataBase, _pagingSettings, _search);
            UpdatePageIndicator();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            _pagingSettings.Page++;
            _gridManager.SetDataGrid(peoplesGrid, _dataBase, _pagingSettings, _search);
            UpdatePageIndicator();
        }

        private void UpdatePageIndicator()
        {
            PageIndicator.Text = $"Страница {_pagingSettings.Page + 1}";
        }
    }
}
