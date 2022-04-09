using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoHollywood.Models
{
    public class AppointGroup
    {
        public AppointGroup(string tableName,string mainKey)
        {
            this.tableName = tableName;
            this.mainKey = mainKey;
            appoints = new Dictionary<string, Appointment>();
        }


        private string tableName;
        private Dictionary<string, Appointment> appoints;
        private string mainKey;



        public Dictionary<string, Appointment> Appoints => appoints;
        public string MainKey => mainKey;
        public string TableName => tableName;
        public string DisplayTime
        {
            get
            {
                if (appoints.Count > 1)
                    return $"c {appoints.Values.ToList()[0].DisplayTime} по {appoints.Values.ToList()[appoints.Count - 1].EndTime}";
                else if (appoints.Count == 1)
                    return $"с {appoints.Values.ToList()[0].DisplayTime} по {appoints.Values.ToList()[0].EndTime}";
                else return string.Empty;
            }
        }
        public string DisplayDate => TableName;
        public string DisplayService
        {
            get
            {
                if (appoints.Count > 0)
                    return $"Услуга:{appoints.Values.ToList()[0].AppointedService}";
                else
                    return string.Empty;
            }
        }

        public void AddAppointKeys(string rootKey,Appointment appoint)
        {
            appoints.Add(rootKey, appoint);
        }

    }
}
