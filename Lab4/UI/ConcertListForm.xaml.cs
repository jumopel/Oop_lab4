using Lab4.Models;
using Lab4.Services;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab4.UI
{
    public partial class ConcertListForm : Window
    {
        private ObservableCollection<Concert> _allConcerts;
        private bool _isClosingConfirmed = false;

        public ConcertListForm()
        {
            InitializeComponent();
            _allConcerts = new ObservableCollection<Concert>(SerializationService.LoadList());
            lstConcerts.ItemsSource = _allConcerts;
        }

        private void btnAddConcert_Click(object sender, RoutedEventArgs e)
        {
            var newConcert = new Concert { Organizer = "Новий організатор" };
            var editor = new MainForm(newConcert);
            editor.Owner = this;
            editor.ShowDialog();
            if (editor.IsSaved) _allConcerts.Add(newConcert);
            lstConcerts.Items.Refresh();
        }

        private void btnEditConcert_Click(object sender, RoutedEventArgs e)
        {
            if (lstConcerts.SelectedItem is Concert selected)
            {
                var editor = new MainForm(selected);
                editor.Owner = this;
                editor.ShowDialog();
                lstConcerts.Items.Refresh();
            }
        }

        private void lstConcerts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnEditConcert.IsEnabled = lstConcerts.SelectedIndex != -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isClosingConfirmed) return;

            var res = MessageBox.Show("Зберегти всі зміни та вийти з програми?", "Увага", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                SerializationService.SaveList(new List<Concert>(_allConcerts));
                _isClosingConfirmed = true;
            }
            else if (res == MessageBoxResult.No)
            {
                _isClosingConfirmed = true;
            }
            else if (res == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}