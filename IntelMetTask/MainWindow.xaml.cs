using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace IntelMetTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ShowFilePicker_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files|*.json";
            openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            
            if(openFileDialog.ShowDialog() == true && viewModel.OpenFileCommand.CanExecute(openFileDialog.FileName))
            {
                viewModel.OpenFileCommand.Execute(openFileDialog.FileName);
            }
        }
    }
}