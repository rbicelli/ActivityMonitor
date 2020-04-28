using System;
using System.Xml.Serialization;
using ActivityMonitor.Collections;

namespace ActivityMonitor.Application
{
    public class ApplicationUsage : IApplicationUsage
    {
        public ApplicationUsage()
        {
        }

        public ApplicationUsage(DateTime beginTime,
                                DateTime endTime,
                                string detailedName = "")
        {
            BeginTime = beginTime;
            EndTime = endTime;
            DetailedName = detailedName;
        }

        [XmlAttribute]
        public string DetailedName { get; set; }


        [XmlAttribute]
        public DateTime BeginTime { get; set; }

        [XmlAttribute]
        public DateTime EndTime { get; set; }


        [XmlAttribute]
        public bool IsClosed { get; set; }

        public void Start()
        {
            BeginTime = DateTime.Now;
            IsClosed = false;
        }

        public void End()
        {
            EndTime = DateTime.Now;
            IsClosed = true;
        }

        public TimeSpan Total
        {
            get
            {
                return IsClosed ? EndTime.Subtract(BeginTime) : DateTime.Now.Subtract(BeginTime);
            }
        }



    }
}