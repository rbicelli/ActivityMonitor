using System;
using ActivityMonitor.Application;
using Microsoft.Win32;

namespace ActMon.SettingsManager
{
    public class Settings
    {

        private const string regRootHKLM = @"HKEY_LOCAL_MACHINE";
        private const string regRootHKCU = @"HKEY_CURRENT_USER";
        private const string regKey = @"Software\ActMon\Settings";
        private const string regKeyGPO = @"Software\Policies\Sequence Software\ActMon";
        private const string regKeyRun = @"Software\Microsoft\Windows\CurrentVersion\Run";

        private bool _lockSettings;
        private bool _hideMenuExit;
        private bool _runHidden;

        private string _dbServer;
        private string _dbDatabase;
        private string _dbUsername;
        private string _dbPassword;

        private string _regBase;
        private string _regBaseGPO;

        private string _appExe;

        private int _dbDumpRate;
        public bool lockSettings
        {
            get => _lockSettings;
        }

        public bool Autostart
        {
            get; set;
        }

        public string DBServer
        {
            get => _dbServer;
            set
            {
                _dbServer = value;
            }
        }

        public string DBDatabase
        {
            get => _dbDatabase;
            set
            {
                _dbDatabase = value;
            }
        }

        public string DBUsername
        {
            get => _dbUsername;
            set
            {
                _dbUsername = value;
            }
        }

        public string DBPassword
        {
            get => _dbPassword;
            set
            {
                _dbPassword = value;
            }
        }

        public bool LockSettings
        {
            get => _lockSettings;
        }
        public bool RunHidden
        {
            get => _runHidden;
        }

        public bool HideMenuExit
        {
            get => _hideMenuExit;
        }

        public int DBDumprate
        {
            get => _dbDumpRate;
        }
        public Settings()
        {
            _regBase = regRootHKCU + "\\" + regKey;
            _regBaseGPO = regRootHKCU + "\\" + regKeyGPO;
            _appExe = new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            ReadSettings();
        }
        
        
        /// <summary>
        /// Read Settings from Registry
        /// Settings are controllable from Group Policy
        /// </summary>
        public void ReadSettings()
        {

            _dbDumpRate = 30;
            string strRegBaseKey = _regBase;
 
            try { 
                //Settings Locked Via GPO
                _lockSettings = regBool(Registry.GetValue(_regBaseGPO, "LockSettings", 0));
                _hideMenuExit = regBool(Registry.GetValue(_regBaseGPO, "HideMenuExit", 0));
                _runHidden = regBool(Registry.GetValue(_regBaseGPO, "RunHidden", 0));

                if (_lockSettings) strRegBaseKey = _regBaseGPO;

                _dbServer = (string)Registry.GetValue(strRegBaseKey, "DatabaseServer", "");
                _dbDatabase = (string)Registry.GetValue(strRegBaseKey, "DatabaseCatalog", "");
                _dbUsername = (string)Registry.GetValue(strRegBaseKey, "DatabaseUsername", "");
                _dbPassword = (string)Registry.GetValue(strRegBaseKey, "DatabasePassword", "");
                _dbDumpRate = regInt(Registry.GetValue(strRegBaseKey, "DatabaseDumpInterval", 30));

                string regValue = (string)Registry.GetValue(regRootHKCU + "\\" + regKeyRun, "ActMon", "ActMon");
                if (regValue!=null) {
                 
                if (regValue.ToLower() == _appExe.ToLower())
                    Autostart = true;
                }

            } 
            catch (NullReferenceException)
            {
                Console.WriteLine("Error reading configuration.");
            }
        }

        private int regInt(object intValue)
        {
                return (int)intValue;
        }
        private bool regBool(object intValue)
        {
            if (intValue == null) intValue = 0;
            bool boolvalue = (int)intValue != 0;
            return boolvalue;
        }
        public void WriteSettings()
        {
            try { 
                Registry.SetValue(_regBase, "DatabaseServer", _dbServer);
                Registry.SetValue(_regBase, "DatabaseCatalog", _dbDatabase);
                Registry.SetValue(_regBase, "DatabaseUsername", _dbUsername);
                Registry.SetValue(_regBase, "DatabasePassword", _dbPassword);

                if (Autostart == false) {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(regKeyRun, true))
                    {
                        if (key != null)
                            key.DeleteValue("ActMon");
                    }
                } else
                {
                    Registry.SetValue(regRootHKCU + "\\" + regKeyRun, "ActMon", _appExe);
                }

            } catch
            {

            }
            
        }

    }
}
