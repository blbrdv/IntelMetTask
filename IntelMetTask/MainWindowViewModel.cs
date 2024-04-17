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
            this.OpenFile(Path.Combine(Constants.DataPath, "SlabMeasure0132981.json"));
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
                this.NoNoise.Clear();
                foreach (var distance in NoiseRemover.CleanData(data.Distances))
                {
                    this.NoNoise.Add(new DataPoint(distance.Value, distance.Speed));
                }

                this.OnPropertyChanged(nameof(this.Title));
                this.OnPropertyChanged(nameof(this.Original));
                this.OnPropertyChanged(nameof(this.NoNoise));
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

        private IList<DataPoint> _noNoise = new List<DataPoint>();
        public IList<DataPoint> NoNoise
        {
            get => this._noNoise;
            private set
            {
                this._original = value;
                this.OnPropertyChanged(nameof(this.NoNoise));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}