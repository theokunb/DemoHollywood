using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHollywood.Models
{
    public class AppointParser
    {
        public AppointParser()
        {

        }

        public List<AppointGroup> Parse(List<KeyValuePair<string, Appointment>> appoints)
        {
            List<AppointGroup> result = new List<AppointGroup>();
            foreach(var element in appoints)
            {
                if(result.Exists(param=>param.MainKey == element.Value.ReferenceKey))
                {
                    result.Find(param => param.MainKey == element.Value.ReferenceKey).AddAppointKeys(element.Key, element.Value);
                }
                else
                {
                    result.Add(new AppointGroup(element.Value.DisplayDate.Replace('.', ':'), element.Value.ReferenceKey));
                    result[result.Count - 1].AddAppointKeys(element.Key, element.Value);
                }
            }


            return result;
        }
    }
}
