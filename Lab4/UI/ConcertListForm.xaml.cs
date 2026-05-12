using System;
using System.Collections.Generic;
using System.Windows;
using Lab4.Models;
using Lab4.Services;

namespace Lab4.UI
{
    public partial class ConcertListForm : Window
    {
        private List<Concert> _allConcerts;

        public ConcertListForm()
        {
            InitializeComponent();
            _allConcerts = SerializationService.LoadList(); 
            lstConcerts.ItemsSource = _allConcerts;
        }

        private void btnAddConcert_Click(object sender, RoutedEventArgs e)
        {
            var newConcert = new Concert { Organizer = "Новий організатор" };
            _allConcerts.Add(newConcert);
            var editor = new MainForm(newConcert);
            editor.Owner = this;
            editor.ShowDialog();

            RefreshList();
        }

        private void btnEditConcert_Click(object sender, RoutedEventArgs e)
        {
            if (lstConcerts.SelectedItem is Concert selected)
            {
                var editor = new MainForm(selected);
                editor.Owner = this;
                editor.ShowDialog();
                RefreshList();
            }
        }

        private void RefreshList()
        {
            lstConcerts.ItemsSource = null;
            lstConcerts.ItemsSource = _allConcerts;
        }

        private void lstConcerts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnEditConcert.IsEnabled = lstConcerts.SelectedIndex != -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SerializationService.SaveList(_allConcerts); 
        }
    }
}