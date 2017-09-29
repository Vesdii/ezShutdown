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
        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            if (HourTextBox.Text.Length == 0
                || !Byte.TryParse(HourTextBox.Text, out byte h)
                || !Byte.TryParse(MinTextBox.Text, out byte m)
                || !Byte.TryParse(SecTextBox.Text, out byte s))
            { return; }

            byte hour = Convert.ToByte(HourTextBox.Text);
            byte min = Convert.ToByte(MinTextBox.Text);
            byte sec = Convert.ToByte(SecTextBox.Text);
            int time = (hour * 3600) + (min * 60) + sec;

            char type_C = 's';
            string type_S = "Shutdown";
            switch (TypeComboBox.SelectedIndex)
            {
                case 1:
                    type_C = 'r';
                    type_S = "Restart";
                    break;
            }

            MessageBoxResult result = MessageBox.Show(
                String.Format("{0} in:  {1}h {2}m {3}s", type_S, hour, min, sec),
                "Confirm", MessageBoxButton.OKCancel
                );
            if (result == MessageBoxResult.Cancel)
            { return; }

            Process.Start("shutdown.exe", "-" + type_C + " -t " + time);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown.exe", "-a");
        }

        private void HourTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            HourTextBox.SelectAll();
        }

        private void MinTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MinTextBox.SelectAll();
        }

        private void SecTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SecTextBox.SelectAll();
        }
    }
}
