using System.Diagnostics;
using System.Windows;

namespace ezShutdown
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HourTextBox.Focus();
        }

        private void HighlightText(object sender, RoutedEventArgs e)
        {
            if (HourTextBox.IsFocused) HourTextBox.SelectAll();
            else if (MinTextBox.IsFocused) MinTextBox.SelectAll();
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!byte.TryParse(HourTextBox.Text, out byte h)) h = 0;
            if (!byte.TryParse(MinTextBox.Text, out byte m)) m = 0;

            int time = (h * 3600) + (m * 60);
            if (time == 0) return;
			
            string option = "Shutdown";
            switch (TypeComboBox.SelectedIndex)
            {
                case 0:
                    option = "Shutdown";
                    break;
                case 1:
                    option = "Restart";
                    break;
            }

            string confirmPrompt = option + " in:  ";
            if (h != 0) confirmPrompt += h + "h ";
            if (m != 0) confirmPrompt += m + "m";

            MessageBoxResult result = MessageBox.Show(
                confirmPrompt, "Confirm", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            Process.Start("shutdown.exe", "-" + option.ToLower()[0] + " -t " + time);

            Close();
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown.exe", "-a");
			HourTextBox.Focus();
		}

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Subtract) Close();
        }
    }
}
