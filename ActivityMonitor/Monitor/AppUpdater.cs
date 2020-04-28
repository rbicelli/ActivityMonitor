using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using ActivityMonitor.Collections;
using ActivityMonitor.Application;

namespace ActivityMonitor.ApplicationMonitor
{
    public class AppUpdater
    {
        private static Applications _applications;

        private string _previousApplicationName = string.Empty;
        public static Applications Applications
        {
            set { _applications = value; }
        }

        public static double GetMaxValue
        {
            get
            {
                return (from app in _applications
                        select app.TotalUsageTime.TotalSeconds).Max();
            }
        }

        public AppUpdater(Applications applications)
        {
            _applications = applications;
        }


        public void Stop(Process process)
        {
            try
            {

                if (_applications[_previousApplicationName] != null &&
                        _applications[_previousApplicationName].Usage.FindLast(u => !u.IsClosed) != null)
                {
                    _applications[_previousApplicationName].Usage.FindLast(u => !u.IsClosed).End();
                }

                var currentProcess = process.MainModule.FileVersionInfo.FileDescription;
                if (_applications[currentProcess] != null &&
                            _applications[currentProcess].Usage.FindLast(u => !u.IsClosed) != null)
                {
                    _applications[currentProcess].Usage.FindLast(u => !u.IsClosed).End();
                }
            }
            catch (Exception)
            {

                // todo logging
            }


        }

        private string DetectFilePath(Process lProcess)
        {
            // Detect File Path: Sometimes MainModule.FileVersionInfo is not reliable
            // so we switch detection to Win API 
            
            string strFileName = "";
            try
            {
                strFileName = lProcess.MainModule.FileVersionInfo.FileName;
            }
            catch (Exception)
            {
                strFileName = WinApi.GetProcessFilename(lProcess);
            }
            
            return strFileName;
        }

        private string DetectFileDescription(Process lProcess)
        {
            // Detect File Descriptiom: dealing with file description is a little time consuming
            // so for now that's rely on process name 

            string strFileDescription;
            try
            {
                strFileDescription = lProcess.MainModule.FileVersionInfo.FileDescription;
            }
            catch (Exception)
            {
                strFileDescription = lProcess.ProcessName;
            }
            return strFileDescription;
        }
        public IApplication Update(Process process)
        {
            //Reliably detect the process file name:
            string strFileName = DetectFilePath(process);
            string strFileDescription = DetectFileDescription(process);

            try
            {                

                if (_previousApplicationName != strFileDescription)
                {

                    if (_applications[_previousApplicationName] != null &&
                        _applications[_previousApplicationName].Usage.FindLast(u => !u.IsClosed) != null)
                    {
                        _applications[_previousApplicationName].Usage.FindLast(u => !u.IsClosed).End();
                    }

                    _previousApplicationName = strFileDescription;

                    if (
                        !_applications.Contains(strFileDescription,
                                                strFileName))
                    {
                        try
                        {
                            using (new MemoryStream())
                            {                            
                                var AppIcon = Icon.ExtractAssociatedIcon(strFileName);                                                                                            
                                _applications.Add(
                                new ActivityMonitor.Application.Application(strFileDescription,strFileName) { Icon = AppIcon });                                                                                           
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.Message);
                            //todo logging
                        }
                    }

                    var usage = new ApplicationUsage { DetailedName = process.MainWindowTitle };
                    usage.Start();
                    _applications[strFileDescription].Usage.Add(usage);

                }

                // if resume - no usages are present
                var currentProcess = strFileDescription;
                if (_applications[_previousApplicationName] != null &&
                    _applications[_previousApplicationName].Usage.FindLast(u => !u.IsClosed) == null &&
                    _applications[currentProcess] != null &&
                    _applications[currentProcess].Usage.FindLast(u => !u.IsClosed) == null)
                {
                    var usage = new ApplicationUsage { DetailedName = process.MainWindowTitle };
                    usage.Start();
                    _applications[strFileDescription].Usage.Add(usage);
                }

            }
            catch (Exception ex)
            {
                //todo logging
                Console.WriteLine(ex.Message);
            }
            return _applications[_previousApplicationName];
        }

        protected IApplication CurrentApplication
        {
            get;
            private set;
        }

    }
}