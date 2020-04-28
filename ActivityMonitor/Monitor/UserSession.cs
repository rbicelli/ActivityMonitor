using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.ApplicationMonitor
{
    public class UserSession
    {
        private long _sessionID;
        private int _sessionType;
        private DateTime _sessionStarted;
        private DateTime? _sessionEnded;
        private long _idleTime;
        private long _userID;
        private string _userName;
        private string _userSID;
        private string _userDomain;
        private string _computerName;
        private string _computerDomain;

        private TimeSpan _idleTimeSpan;

        public long SessionID
        {
            get => _sessionID;

            set { _sessionID = value; }
        }

        public long UserID
        {
            get => _userID;

            set { _userID = value; }
        }

        public DateTime SessionStarted => _sessionStarted;

        public DateTime? SessionEnded => _sessionEnded;
        public string UserSID => _userSID;
        public string UserName => _userName;
        public string UserDomain => _userDomain;
        public string ComputerName => _computerName;
        public string ComputerDomain => _computerDomain;

        public long IdleSeconds {
            get => _idleTime;
            
            set
            {
                _idleTime = value;
            }
        }
        
        public void AddIdleSeconds(int Seconds)
        {
            _idleTime += Seconds;
        }
        public TimeSpan IdleTime
        {
            get {
                return TimeSpan.FromSeconds(_idleTime);
             }        
        }
        public UserSession()
        {
            _sessionStarted = DateTime.Now;
            _sessionEnded = null;
            //Gather Facts
            System.Security.Principal.WindowsIdentity currentUser;

            currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            
            _userSID = currentUser.User.ToString();
            _userName = System.Environment.UserName;
            _userDomain = System.Environment.UserDomainName;

            _computerName = System.Environment.MachineName;
            
            
            try
            {
                _computerDomain = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().ToString();
            }
            catch (Exception ex)
            {
                _computerDomain = "";
            }
            
        }

        public void EndSession()
        {
            _sessionEnded = DateTime.Now;
        }

    }
}
