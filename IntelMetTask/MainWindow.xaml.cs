using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using OxyPlot.Series;

namespace IntelMetTask
{
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
            openFileDialog.InitialDirectory = Constants.DataPath;
            
            if(openFileDialog.ShowDialog() == true && viewModel.OpenFileCommand.CanExecute(openFileDialog.FileName))
            {
                viewModel.OpenFileCommand.Execute(openFileDialog.FileName);
                this.Plot.InvalidatePlot();
            }
        }
    }
}