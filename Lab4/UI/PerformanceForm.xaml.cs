using System;
using System.ComponentModel;
using System.Windows;
using Lab4.Models;

namespace Lab4.UI
{
    public partial class PerformanceForm : Window
    {
        public Performance ResultPerformance { get; private set; }
        private bool _isSaved = false;

        public PerformanceForm(Performance performance)
        {
            InitializeComponent();
            ResultPerformance = performance;

            cmbWorkType.ItemsSource = Enum.GetValues(typeof(WorkType));

            txtTitle.Text = performance.Title;
            txtDuration.Text = performance.Duration > 0 ? performance.Duration.ToString() : "";
            cmbWorkType.SelectedItem = performance.WorkType;
            UpdatePerformerLabel();
        }

        private void btnSelectPerformer_Click(object sender, RoutedEventArgs e)
        {
            var performer = ResultPerformance.Performer ?? new Performer();
            var form = new PerformerForm(performer) { Owner = this };

            if (form.ShowDialog() == true)
            {
                ResultPerformance.Performer = form.ResultPerformer;
                UpdatePerformerLabel();
            }
        }

        private void UpdatePerformerLabel()
        {
            if (ResultPerformance.Performer != null)
                txtPerformerName.Text = $"{ResultPerformance.Performer.FirstName} {ResultPerformance.Performer.LastName}";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ResultPerformance.Performer == null)
                    throw new ArgumentException("Спочатку оберіть виконавця!");

                if (!int.TryParse(txtDuration.Text, out int duration))
                    throw new ArgumentException("Тривалість має бути числом!");

                ResultPerformance.Title = txtTitle.Text;
                ResultPerformance.Duration = duration;
                ResultPerformance.WorkType = (WorkType)cmbWorkType.SelectedItem;

                _isSaved = true;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _isSaved = true;
            this.DialogResult = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_isSaved)
            {
                var res = MessageBox.Show("Зберегти зміни?", "Увага", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Yes) btnSave_Click(sender, null);
                else if (res == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }
    }
}