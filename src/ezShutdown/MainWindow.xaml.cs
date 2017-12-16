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
            else if (SecTextBox.IsFocused) SecTextBox.SelectAll();
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Byte.TryParse(HourTextBox.Text, out byte h)
                || !Byte.TryParse(MinTextBox.Text, out byte m)
                || !Byte.TryParse(SecTextBox.Text, out byte s))
                return;
            
            int time = (h * 3600) + (m * 60) + s;
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
                case 2:
                    type_Char = 'l';
                    type_String = "Logoff";
                    break;
            }

            string confirmPrompt = type_String + " in:  ";
            if (h != 0) confirmPrompt += h + "h ";
            if (m != 0) confirmPrompt += m + "m ";
            if (s != 0) confirmPrompt += s + "s";

            MessageBoxResult result = MessageBox.Show(
                confirmPrompt, "Confirm", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            Process.Start("shutdown.exe", "-" + type_Char + " -t " + time);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown.exe", "-a");
        }
    }
}
