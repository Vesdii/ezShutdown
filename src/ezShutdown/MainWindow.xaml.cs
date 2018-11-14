using System;
using System.Windows;
using System.Diagnostics;

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
            if (!Byte.TryParse(HourTextBox.Text, out byte h)) h = 0;
            if (!Byte.TryParse(MinTextBox.Text, out byte m)) m = 0;
            
            int time = (h * 3600) + (m * 60);
            if (time == 0) return;

            char type_Char = 's';
            string type_String = "Shutdown";
            switch (TypeComboBox.SelectedIndex)
            {
                case 0:
                    type_Char = 's';
                    type_String = "Shutdown";
                    break;
                case 1:
                    type_Char = 'r';
                    type_String = "Restart";
                    break;
            }

            string confirmPrompt = type_String + " in:  ";
            if (h != 0) confirmPrompt += h + "h ";
            if (m != 0) confirmPrompt += m + "m";

            MessageBoxResult result = MessageBox.Show(
                confirmPrompt, "Confirm", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            Process.Start("shutdown.exe", "-" + type_Char + " -t " + time);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown.exe", "-a");
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape) Close();
        }
    }
}
