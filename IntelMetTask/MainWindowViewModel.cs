using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using OxyPlot;

namespace IntelMetTask
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            this.OpenFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "SlabMeasure0132981.json"));
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
            }
        }

        private bool CanOpenFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public string Title { get; private set; }

        public IList<DataPoint> Original { get; private set; } = new List<DataPoint>();

        public IList<DataPoint> NoNoise { get; private set; } = new List<DataPoint>();
    }
}