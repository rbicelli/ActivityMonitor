using System;
using System.Windows.Forms;
using ActivityMonitor.ApplicationMonitor;
using ActMon.Properties;

namespace ActMon.Forms
{

    public partial class FormActivity : Form
    {
        private AppMonitor _appMon;
        private ApplicationView _idleCtl;        
        public FormActivity(AppMonitor AppMonitor)
        {
            InitializeComponent();

            _appMon = AppMonitor;

            _idleCtl = new ApplicationView(_appMon);
            _idleCtl.ApplicationText = ResFiles.GlobalRes.caption_IdleTime;
            _idleCtl.Icon = Resources.IdleIcon.ToBitmap();
            flApplicationsUsage.Controls.Add(_idleCtl);
            flApplicationsUsage.SetFlowBreak(_idleCtl, true);
            ResizeControls();
            updateView();
        }

        private void FormActivity_Load(object sender, EventArgs e)
        {

        }

        private void gBoxSessionInfo_Enter(object sender, EventArgs e)
        {

        }

        private void updateView()
        {
            foreach (ActivityMonitor.Application.Application lApp in _appMon.Applications)
            {
                addFlControlIfNotExists(lApp);
            }

            int pbvalue;
            double totaltime;

            totaltime = _appMon.TotalTimeSpentInApplications.TotalSeconds + _appMon.Session.IdleTime.TotalSeconds;
            if (totaltime == 0)
                pbvalue = 0;
            else
                pbvalue = (int)(_appMon.Session.IdleTime.TotalSeconds * 100 / totaltime);

            _idleCtl.AppUsageText = _appMon.Session.IdleTime.ToString(@"hh\:mm\:ss") + " (" + pbvalue.ToString() + "%)";

            _idleCtl.PctValue = pbvalue;
        }

        private void addFlControlIfNotExists(ActivityMonitor.Application.Application lApp)
        {
            ApplicationView ctl;

            foreach (ApplicationView Av in flApplicationsUsage.Controls)
            {
                if (Av.Key == lApp.ExeName) return;
            }

            ctl = new ApplicationView(lApp, _appMon);            
            flApplicationsUsage.Controls.Add(ctl);
            flApplicationsUsage.SetFlowBreak(ctl, true);
            ctl.TriggerResize();           

        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            updateView();
        }

        private void flApplicationsUsage_OnResize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void ResizeControls()
        {
            if (flApplicationsUsage.HorizontalScroll.Visible)
            {
                flApplicationsUsage.AutoScroll = false;
                flApplicationsUsage.HorizontalScroll.Visible = false;
                flApplicationsUsage.Width -= (SystemInformation.VerticalScrollBarWidth + 10);
                flApplicationsUsage.AutoScroll = true;
                flApplicationsUsage.Width += (SystemInformation.VerticalScrollBarWidth + 10);
            }
            foreach (ApplicationView c in flApplicationsUsage.Controls)
            {
                c.TriggerResize();
            }
        }
    }
}