using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using OxyPlot;

namespace IntelMetTask
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            // this.OpenFile(Path.Combine(Constants.DataPath, "SlabMeasure0132981.json"));
        }

        public ICommand OpenFileCommand => new RelayCommand(OpenFile, CanOpenFile);

        private void OpenFile(string filePath)
        {
            using (var file = File.OpenText(filePath))
            {
                var serializer = new JsonSerializer();
                var data = (SlabMeasure)serializer.Deserialize(file, typeof(SlabMeasure));

                if (data?.Distances is null)
                    throw new FileFormatException("Invalid file format");

                this.Title = Path.GetFileName(filePath);
                this.Original.Clear();
                foreach (var measure in data.Distances)
                {
                    this.Original.Add(new DataPoint(measure.Value, measure.Speed));
                }

                this.OnPropertyChanged(nameof(this.Title));
                this.OnPropertyChanged(nameof(this.Original));
            }
        }

        private bool CanOpenFile(string filePath)
        {
            return File.Exists(filePath);
        }

        private string _title;
        public string Title
        {
            get => this._title;
            private set
            {
                this._title = value;
                this.OnPropertyChanged(nameof(this.Title));
            }
        }

        private IList<DataPoint> _original = new List<DataPoint>();
        public IList<DataPoint> Original
        {
            get => this._original;
            private set
            {
                this._original = value;
                this.OnPropertyChanged(nameof(this.Original));
            }
        }

        // Implement the OnPropertyChanged method
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IList<DataPoint> NoNoise { get; private set; } = new List<DataPoint>();
    }
}