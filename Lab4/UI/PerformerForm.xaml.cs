using System;
using System.Windows;
using Lab4.Models;

namespace Lab4.UI
{
    public partial class PerformerForm : Window
    {
        public Performer ResultPerformer { get; private set; }

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

                this.DialogResult = true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}