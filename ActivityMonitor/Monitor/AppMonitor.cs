using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
//using System.Windows.Data;
//using System.Windows.Media;
//using System.Windows.Threading;
using Microsoft.Win32;
using ActivityMonitor.Application;
//using AppMonitor.SettingsManager;

namespace ActivityMonitor.ApplicationMonitor
{
    public class AppMonitor : INotifyPropertyChanged
    {
        private readonly AppUpdater _appUpdater;
        private string _currentApplicationName;
        private TimeSpan _currentApplicationTotalUsageTime;
        private string _currentApplicationPath;
        private Icon _currentApplicationIcon;
        private int _idleTime;
        private int _idleInterval;
        private bool _requestStop;
        private bool _started;
        private int _pollInterval;

        public UserSession Session;
        public Applications Applications
        {
            get { return Data; }
            set
            {
                Data = value;
                AppUpdater.Applications = value;
            }
        }

        public int IdleTime => _idleTime;

        private readonly DateTime _startTime = DateTime.Now;

        public AppMonitor()//Dispatcher dispatcher)
        {
            Data = new Applications();
            _appUpdater = new AppUpdater(Data);
            _idleInterval = 30;

            Session = new UserSession();
            
            SystemEvents.SessionSwitch += SystemEventsSessionSwitch;
        }

        private bool _sessionStopped;
        public void SystemEventsSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    _sessionStopped = true;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    _sessionStopped = false;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void Start(int pollInterval = 1000)
        {
            if (!_started) { 
                _pollInterval = pollInterval;
                Thread thread = new Thread(new ThreadStart(this.ApplicationsUpdater));
                if (this._started)
                    return;
            
                _requestStop = false;
                _started = true;
                thread.Start();
            }
        }

        public void EndSession()
        {
            Session.EndSession();
            Stop();
        }
        public void Stop()
        {
            //Trigger Thread Stop
            _requestStop = true;
        }
        private void ApplicationsUpdater()
        {
            _started = true;
            while (!_requestStop) { 
            try
            {
                var handle = WinApi.GetForegroundWindow();
                //var handle = WinApi.GetActiveWindow();
                int processId;
             
                //todo write result to trace and add try catch
                WinApi.GetWindowThreadProcessId(new HandleRef(null, handle), out processId);
                //var process = Process.GetProcessById(processId);
                var process = Process.GetProcessById(WinApi.GetRealProcessID(handle));

                // checking if the user is in idle mode - if so, dont update process and sum to Idle time
                // todo refactor
                var inputInfo = new WinApi.Lastinputinfo();
                inputInfo.cbSize = (uint)Marshal.SizeOf(inputInfo);
                WinApi.GetLastInputInfo(ref inputInfo);
                var idleTime = (Environment.TickCount - inputInfo.dwTime) / 1000;
                if (idleTime < _idleInterval && _sessionStopped == false)
                { // If idle time is less than _idleInterval then update process

                    var currentApplication = _appUpdater.Update(process);
                    if (currentApplication != null)
                    {
                        CurrentApplicationName = currentApplication.Name;
                        CurrentApplicationTotalUsageTime = currentApplication.TotalUsageTime;
                        CurrentApplicationPath = currentApplication.Path;
                        CurrentApplicationIcon = currentApplication.Icon;
                    }
                }
                else
                {
                     //Update User Idle Time
                     Session.AddIdleSeconds(_pollInterval / 1000);
                     NotifyPropertyChanged("IdleTime");
                     _appUpdater.Stop(process);
                }
            }
            catch (Exception ex)
            {
                    // todo logging
                    Console.WriteLine("EXC:" + ex.Message);
            }

            NotifyPropertyChanged("TotalTimeRunning");
            NotifyPropertyChanged("TotalTimeSpentInApplications");

            Thread.Sleep(_pollInterval);
            }
            
            //Stop Thread
            _requestStop = false;
            _started = false;
        }


        private Applications _data;
        public Applications Data
        {
            get { return _data; }
            private set
            {
                _data = value;                
            }
        }


        
        public string CurrentApplicationName
        {
            get { return _currentApplicationName; }
            private set
            {
                if (value == null || value == _currentApplicationName) return;
                _currentApplicationName = value;
                NotifyPropertyChanged("CurrentApplicationName");
            }
        }


        public TimeSpan CurrentApplicationTotalUsageTime
        {
            get { return _currentApplicationTotalUsageTime; }
            private set
            {
                if (value == _currentApplicationTotalUsageTime) return;
                _currentApplicationTotalUsageTime = value;
                NotifyPropertyChanged("CurrentApplicationTotalUsageTime");
            }
        }

        public string CurrentApplicationPath
        {
            get { return _currentApplicationPath; }
            private set
            {
                if (value == _currentApplicationPath) return;
                _currentApplicationPath = value;
                NotifyPropertyChanged("CurrentApplicationPath");
            }
        }

        public Icon CurrentApplicationIcon
        {
            get { return _currentApplicationIcon; }
            private set
            {
                if (value == _currentApplicationIcon) return;
                _currentApplicationIcon = value;
                NotifyPropertyChanged("CurrentApplicationIcon");
            }
        }

        public TimeSpan TotalTimeSpentInApplications
        {
            get
            {
                var totalTime = Applications.Sum(s => s.TotalTimeInMinutes);
                return TimeSpan.FromMinutes(totalTime);
            }


        }

        public TimeSpan TotalTimeRunning
        {
            get { return DateTime.Now.Subtract(_startTime); }
        }


    }
}