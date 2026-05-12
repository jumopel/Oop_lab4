using System;
using System.Windows;
using System.Windows.Controls;
using Lab4.Models;

namespace Lab4.UI
{
    public partial class MainForm : Window
    {
        private Concert _concert;

        public MainForm(Concert concert)
        {
            InitializeComponent();
            _concert = concert;

            txtOrganizer.Text = _concert.Organizer;
            dtpDate.SelectedDate = _concert.Date;
            RefreshUI();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new PerformanceForm(new Performance()) { Owner = this };
            if (form.ShowDialog() == true)
            {
                _concert.AddPerformance(form.ResultPerformance);
                RefreshUI();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstPerformances.SelectedItem is Performance selected)
            {
                var form = new PerformanceForm(selected) { Owner = this };
                if (form.ShowDialog() == true)
                {
                    RefreshUI();
                }
            }
        }

        private void RefreshUI()
        {
            lstPerformances.ItemsSource = null;
            lstPerformances.ItemsSource = _concert.Performances;

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
            try
            {
                if (txtOrganizer.Text.Trim().Length >= 3)
                    _concert.Organizer = txtOrganizer.Text;

                _concert.Date = dtpDate.SelectedDate ?? DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
                e.Cancel = true; 
            }
        }
    }
}