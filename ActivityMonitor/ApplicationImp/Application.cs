using System;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using ActivityMonitor.Collections;
using System.IO;

namespace ActivityMonitor.Application
{
   

    public class Application : IApplication, INotifyPropertyChanged
    {
        private string _path;
        private string _exeName;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged("TotalUsageTime");
        }


        public Application(ApplicationUsages usage = null)
        {

            Usage = usage ?? new ApplicationUsages();

        }

        public Application()
        {
            Usage = new ApplicationUsages();
        }

        public Application(string name, string path)
        {
            Path = path;
            Name = name;
            Usage = new ApplicationUsages();
        }

        [XmlAttribute]
        public string Path
        {
            get => _path;

            set {
                FileInfo fi = new FileInfo(value);
                _exeName = fi.Name;
                _path = value;
            }
        }

        public string ExeName
        {
            get => _exeName;
        }

        [XmlAttribute]
        public string Name { get; set; }

        public long ApplicationID { get; set; }

        public TimeSpan TotalUsageTime
        {
            get { return Usage.TotalUsageTime(); }
        }

        public double TotalTimeInMinutes { get { return TotalUsageTime.TotalMinutes; } }

        private ApplicationUsages _usage;

        public ApplicationUsages Usage
        {
            get { return _usage; }
            set
            {
                _usage = value;
                NotifyPropertyChanged("Usage");
            }
        }

        [XmlIgnore]
        public Icon Icon { get; set; }

        public ApplicationDetails Details { get; set; }

    }
}
