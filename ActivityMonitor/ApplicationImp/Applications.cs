using System;
using System.Collections.ObjectModel;
using System.Linq;
using ActivityMonitor.Collections;

namespace ActivityMonitor.Application
{
    public class Applications : ObservableCollection<Application>
    {
        public bool Contains(string application)
        {
            return (from app in this
                    where app.Name == application
                    select app).FirstOrDefault() != null;
        }

        public bool Contains(string application, string path)
        {
            return (from app in this
                    where app.Name == application && app.Path == path
                    select app).FirstOrDefault() != null;
        }

        public IApplication this[string applicationName]
        {
            get
            {
                return (from app in this
                        where app.Name == applicationName
                        select app).FirstOrDefault();


            }

        }

        public TimeSpan TotalTime
        {
            get
            {
                return (from app in this
                        select app.TotalUsageTime).Aggregate(TimeSpan.Zero, (subtotal,
                                                                             t) => subtotal.Add(t));
            }
        }

        public void Refresh()
        {

            foreach (var application in this)
            {
                application.Refresh();
            }

            
        }
        
    }
}