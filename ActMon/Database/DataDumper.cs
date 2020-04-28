using System.Threading;

using ActivityMonitor.ApplicationMonitor;

namespace ActMon.Database
{
    class DataDumper
    {
        private AppMonitor _appMonitor;
        private DB _database;

        private int _dumpInterval;

        private bool _started;
        private bool _requestStop;
        public DataDumper(AppMonitor AppMonitor, DB Database, int DumpInterval = 60)
        {
            _appMonitor = AppMonitor;
            _database = Database;

            _dumpInterval = DumpInterval;
            _started = false;
        }

        public void Start()
        {
            if (!_started)
            {
                Thread thread = new Thread(new ThreadStart(this._runDumper));
                if (this._started)
                    return;

                _requestStop = false;
                _started = true;
                thread.Start();
            }
        }

        public void Stop()
        {
            _requestStop = true;
        }
        private void _runDumper()
        {
            int dumpCycles;
            dumpCycles = 0;
            while (!_requestStop)
            {
                if (dumpCycles >= _dumpInterval)
                {
                    _database.RecordSession(_appMonitor);
                    dumpCycles = 0;
                }

                Thread.Sleep(1000);
                dumpCycles++;
            }

            //Dump Last Time
            _database.RecordSession(_appMonitor);

            _started = false;
            _requestStop = false;
        }


    }
}
