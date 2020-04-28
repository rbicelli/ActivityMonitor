using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using ActivityMonitor.Application;
using ActivityMonitor.ApplicationMonitor;

namespace ActMon.Database
{
    public class DB
    {
        private SqlConnection conn;
        private string _dbServer;
        private string _dbUsername;
        private string _dbDatabase;
        private string _dbPassword;

        private bool _leaveConnectionOpen;
        
        public DB(int connectionTimeout = 1)
        {
            ConnectionTimeout = connectionTimeout;

            conn = new SqlConnection();
        }

        public int ConnectionTimeout {get; set;}

        public string Server
        {
            get => _dbServer;
            
            set
            {
                _dbServer = value;
                updateConnection();
            }
        }

        public string Database
        {
            get => _dbDatabase;
            
            set
            {
                _dbDatabase = value;
                updateConnection();
            }
        }

        public string Username
        {
            get =>_dbUsername;
            
            set
            {
                _dbUsername = value;
                updateConnection();
            }
        }

        public string Password
        {
            get => _dbPassword;
            
            set
            {
                _dbPassword = value;
                updateConnection();
            }
        }

        private void updateConnection()
        {
            conn.ConnectionString = "Connect Timeout = " + ConnectionTimeout.ToString() +  ";" +
                                    "Data Source=" + _dbServer +
                                    ";Initial Catalog=" + _dbDatabase +
                                    ";User id=" + _dbUsername +
                                    ";Password=" + _dbPassword;
        }
        public bool Connect()
        {                        
            conn.ConnectionString = "Connect Timeout = " + ConnectionTimeout.ToString() + ";" +
                                    "Data Source=" + _dbServer +
                                    ";Initial Catalog=" + _dbDatabase +
                                    ";User id=" + _dbUsername +
                                    ";Password=" + _dbPassword;
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        private void closeConnection()
        {
            
            if (conn.State == ConnectionState.Open && _leaveConnectionOpen==false)
                conn.Close();
        }

        public long getIdentity(string TableName)
        {
            long _ret;
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                openConnection();
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = "SELECT CAST(IDENT_CURRENT('" + TableName + "') AS BIGINT) as ID_VALUE";

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                _ret = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ID_VALUE"));
                sqlDataReader.Close();
                closeConnection();

                return _ret;
            }
        }
        public long RecordUser(UserSession uSession)
        {
            long lUserID;

            string sQuery = "SELECT userID FROM Users WHERE userSID=@userSID";

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = sQuery;
                sqlCommand.Parameters.AddWithValue("@userSID", (object)uSession.UserSID);
                try
                {
                    openConnection();
                    SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();

                    if (sqlDataReader1.HasRows)
                    {
                        sqlDataReader1.Read();
                        lUserID = sqlDataReader1.GetInt64(sqlDataReader1.GetOrdinal("userId"));
                        sqlDataReader1.Close();
                    }
                    else
                    {
                        sqlDataReader1.Close();

                        sqlCommand.CommandText = "INSERT INTO Users(userSID,userName,userDomain) VALUES (@userSID,@userName,@userDomain)";

                        //sqlCommand.Parameters.AddWithValue("@userSID", (object)uSession.UserSID);
                        sqlCommand.Parameters.AddWithValue("@userName", (object)uSession.UserName);
                        sqlCommand.Parameters.AddWithValue("@userDomain", (object)uSession.UserDomain);

                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.CommandText = "SELECT CAST(IDENT_CURRENT('Users') AS BIGINT) as ID_VALUE";
                        sqlCommand.Parameters.Clear();
                        SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
                        sqlDataReader2.Read();
                        lUserID = sqlDataReader2.GetInt64(sqlDataReader2.GetOrdinal("ID_VALUE"));
                        sqlDataReader2.Close();
                    }
                    closeConnection();
                    uSession.UserID = lUserID;
                    return lUserID;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public bool RecordSession(AppMonitor appMon)
        {
            _leaveConnectionOpen = true;

            RecordUserSession(appMon.Session);

            try
            {
                foreach (Application lApp in appMon.Applications)
                {
                    RecordApplication(lApp);
                    RecordApplicationSession(appMon.Session.SessionID, lApp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
            _leaveConnectionOpen = false;
            closeConnection();
            return true;
        }

        public bool RecordApplicationSession(long SessionID, Application sApp)
        {
            string sqlStr;

            sqlStr = @"begin tran
                     if exists(select * from SessionApplicationsUsage with (updlock, serializable) where sessionID=@sessionID AND applicationID = @applicationID)
                     begin
                      update SessionApplicationsUsage set usageTime=@usageTime
                      where sessionID=@sessionID AND applicationID = @applicationID
                     end
                     else
                     begin
                     insert into SessionApplicationsUsage(sessionID, applicationID, usageTime)
                     values(@sessionID, @applicationID, @usageTime)
                     end
                     commit tran";

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Parameters.AddWithValue("@sessionID", (object)SessionID);
                sqlCommand.Parameters.AddWithValue("@applicationID", (object)sApp.ApplicationID);
                sqlCommand.Parameters.AddWithValue("@usageTime", (long)sApp.TotalUsageTime.TotalSeconds);

                sqlCommand.Connection = conn;
                sqlCommand.CommandText = sqlStr;

                try
                {
                    openConnection();
                    sqlCommand.ExecuteNonQuery();
                    closeConnection();
                    return true;
                }
                catch (SqlException ex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }



        public bool RecordUserSession(UserSession uSession)
        {
            string sqlStr;

            if (uSession.UserID == 0)
            {
                if (RecordUser(uSession) == 0) return false;
            }

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                if (uSession.SessionID == 0)
                {
                    sqlStr = "INSERT INTO userSessions (userID, machineName, machineDomain, timeStarted) VALUES (@userID, @machineName, @machineDomain, @timeStarted)";
                    sqlCommand.Parameters.AddWithValue("@userID", (object)uSession.UserID);
                    sqlCommand.Parameters.AddWithValue("@machineName", (object)uSession.ComputerName);
                    sqlCommand.Parameters.AddWithValue("@machineDomain", (object)uSession.ComputerDomain);
                    sqlCommand.Parameters.AddWithValue("@timeStarted", (object)uSession.SessionStarted);
                }
                else
                {
                    sqlStr = "UPDATE userSessions SET timeEnded=@timeEnded, idleTime=@idleTime, timeLastUpdate=@timeLastUpdate WHERE sessionID=@sessionID";
                    sqlCommand.Parameters.AddWithValue("@timeLastUpdate", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@sessionID", (object)uSession.SessionID);
                    if (uSession.SessionEnded != null)
                        sqlCommand.Parameters.AddWithValue("@timeEnded", (object)uSession.SessionEnded);
                    else
                        sqlCommand.Parameters.AddWithValue("@timeEnded", DBNull.Value);

                    sqlCommand.Parameters.AddWithValue("@idleTime", (object)uSession.IdleTime.TotalSeconds);
                }

                sqlCommand.Connection = conn;
                sqlCommand.CommandText = sqlStr;

                try
                {
                    openConnection();
                    sqlCommand.ExecuteNonQuery();
                    closeConnection();
                    if (uSession.SessionID == 0) uSession.SessionID = getIdentity("UserSessions");
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool RecordApplication(Application lApp)
        {

            string sqlStr;
            byte[] imageData;
            Bitmap iconBitmap;

            if (lApp.ApplicationID == 0)
            {
                //get Application by exename
                lApp.ApplicationID = GetApplicationID(lApp);

                //create Application
                if (lApp.ApplicationID == 0)
                {
                    //Save Icon to PNG
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        iconBitmap = lApp.Icon.ToBitmap();
                        iconBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        imageData = stream.ToArray();
                    }
                    sqlStr = "INSERT INTO Applications(exeName,applicationTitle,applicationIcon) VALUES (@exeName,@applicationTitle,@applicationIcon)";

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = conn;
                        sqlCommand.CommandText = sqlStr;
                        sqlCommand.Parameters.AddWithValue("@exeName", (object)lApp.ExeName);
                        sqlCommand.Parameters.AddWithValue("@applicationTitle", (object)lApp.Name);
                        sqlCommand.Parameters.AddWithValue("@applicationIcon", imageData);
                        try
                        {
                            openConnection();
                            sqlCommand.ExecuteNonQuery();
                            closeConnection();
                            lApp.ApplicationID = getIdentity("Applications");
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return false;
                        }
                    }
                }

            }

            return false;
        }

        public long GetApplicationID(Application sApp)
        {
            long lRet = 0;
            string sQuery = "SELECT ApplicationID FROM Applications WHERE exeName=@exeName";

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = sQuery;
                sqlCommand.Parameters.AddWithValue("@exeName", (object)sApp.ExeName);
                try
                {
                    openConnection();
                    SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();

                    if (sqlDataReader1.HasRows)
                    {
                        sqlDataReader1.Read();
                        lRet = sqlDataReader1.GetInt64(sqlDataReader1.GetOrdinal("ApplicationId"));    
                    }

                    sqlDataReader1.Close();
                    closeConnection();

                }
                catch (Exception ex)
                {
                    return 0;
                }
                return lRet;
            }
        }
    }
}
