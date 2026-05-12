using System;
using System.Windows;
using Lab4.Models;

namespace Lab4.UI
{
    public partial class PerformerForm : Window
    {
        public Performer ResultPerformer { get; private set; }
        private bool _isSaved = false;

        public PerformerForm(Performer performer)
        {
            InitializeComponent();
            ResultPerformer = performer;

            txtFirstName.Text = performer.FirstName;
            txtLastName.Text = performer.LastName;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResultPerformer.FirstName = txtFirstName.Text.Trim();
                ResultPerformer.LastName = txtLastName.Text.Trim();

                _isSaved = true;
                this.DialogResult = true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _isSaved = true;
            this.DialogResult = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_isSaved)
            {
                var res = MessageBox.Show("Зберегти зміни?", "Увага", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    btnOk_Click(sender, new RoutedEventArgs());
                    if (this.DialogResult != true)
                    {
                        e.Cancel = true; 
                    }
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}