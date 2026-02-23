using NasaPhoto_WinApp.Application.Interfaces;
using NasaPhoto_WinApp.Application.UseCases;
using NasaPhoto_WinApp.Domain.Entities;
using NasaPhoto_WinApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NasaPhoto_WinApp.Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GetApodsByMonthUseCase _useCase;

        public MainViewModel(GetApodsByMonthUseCase useCase)
        {
            _useCase = useCase;

            var currentYear = DateTime.Now.Year;

            Years = Enumerable.Range(1995, currentYear - 1995 + 1)
                              .Reverse()
                              .ToList();

            Months = new List<string>
            {
                "January","February","March","April",
                "May","June","July","August",
                "September","October","November","December"
            };

            SelectedYear = currentYear;
            SelectedMonth = DateTime.Now.Month;

            LoadCommand = new RelayCommand(async _ => await LoadAsync());
        }

        public ObservableCollection<Apod> Apods { get; set; } = new();
        public List<int> Years { get; }

        public List<string> Months { get; }
        private int _selectedMonthIndex;
        public int SelectedMonthIndex
        {
            get => _selectedMonthIndex;
            set
            {
                _selectedMonthIndex = value;
                SelectedMonth = value + 1;
                OnPropertyChanged();
            }
        }

        private int _selectedYear = DateTime.Now.Year;
        public int SelectedYear
        {
            get => _selectedYear;
            set { _selectedYear = value; OnPropertyChanged(); }
        }

        private int _selectedMonth = DateTime.Now.Month;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set { _selectedMonth = value; OnPropertyChanged(); }
        }
        public int PageSize { get; } = 6;

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                UpdatePagedItems();
            }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public int TotalPages { get; private set; }

        private List<Apod> _allApods = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ICommand LoadCommand { get; }
        public ICommand NextPageCommand => new RelayCommand(_ =>
        {
            if (CurrentPage < TotalPages)
                CurrentPage++;
            return Task.CompletedTask;
        });

        public ICommand PrevPageCommand => new RelayCommand(_ =>
        {
            if (CurrentPage > 1)
                CurrentPage--;
            return Task.CompletedTask;
        });

        private async Task LoadAsync()
        {
            try
            {
                IsLoading = true;
                Apods.Clear();

                var result = await _useCase.ExecuteAsync(SelectedYear, SelectedMonth);

                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage!,
                        "Invalid Date",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                _allApods = result.Data!;
                TotalPages = (int)Math.Ceiling((double)_allApods.Count / PageSize);

                CurrentPage = 1;
                UpdatePagedItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private void UpdatePagedItems()
        {
            Apods.Clear();

            var items = _allApods
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);

            foreach (var item in items)
                Apods.Add(item);

            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(TotalPages));
        }


    }
}
