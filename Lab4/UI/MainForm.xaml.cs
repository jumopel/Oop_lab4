using Lab4.Models;
using System.Collections.ObjectModel;
using System;
using System.Windows;
using System.Windows.Controls;
using Lab4.Models;

namespace Lab4.UI
{
    public partial class MainForm : Window
    {
        private Concert _concert;
        private ObservableCollection<Performance> _performances;
        private bool _isClosingConfirmed = false;
        public bool IsSaved { get; private set; } = false;
        public MainForm(Concert concert)
        {
            InitializeComponent();
            _concert = concert;
            txtOrganizer.Text = _concert.Organizer;
            dtpDate.SelectedDate = _concert.Date;
            _performances = new ObservableCollection<Performance>(_concert.Performances);
            lstPerformances.ItemsSource = _performances;
            UpdateShortInfo();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new PerformanceForm(new Performance()) { Owner = this };
            if (form.ShowDialog() == true)
            {
                _concert.AddPerformance(form.ResultPerformance);
                _performances.Add(form.ResultPerformance); 
                UpdateShortInfo();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstPerformances.SelectedItem is Performance selected)
            {
                var form = new PerformanceForm(selected) { Owner = this };
                if (form.ShowDialog() == true)
                {
                    lstPerformances.Items.Refresh();
                    UpdateShortInfo();
                }
            }
        }

        private void UpdateShortInfo()
        {
            if (txtOrganizer.Text.Trim().Length >= 3)
            {
                _concert.Organizer = txtOrganizer.Text;
            }
            txtShortInfo.Text = _concert.ToShortString();
        }

        private void lstPerformances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEdit.IsEnabled = lstPerformances.SelectedIndex != -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isClosingConfirmed) return;

            var res = MessageBox.Show("Зберегти зміни в деталях концерту?", "Увага", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    if (txtOrganizer.Text.Trim().Length >= 3)
                        _concert.Organizer = txtOrganizer.Text;

                    _concert.Date = dtpDate.SelectedDate ?? DateTime.Now;

                    IsSaved = true; 
                    _isClosingConfirmed = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                    e.Cancel = true;
                }
            }
            else if (res == MessageBoxResult.No)
            {
                IsSaved = false; 
                _isClosingConfirmed = true;
            }
            else if (res == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}