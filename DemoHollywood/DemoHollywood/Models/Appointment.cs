using System;

namespace DemoHollywood.Models
{
    public class Appointment
    {
        public Appointment(bool isBusy = false, string username = "", string appointdedService = "", string referenceKey = "")
        {
            IsBusy = isBusy;
            AppointedUser = username;
            AppointedService = appointdedService;
            ReferenceKey = referenceKey;
        }

        public string AppointKey { get; set; }
        public string ReferenceKey { get; set; }
        public string AppointedUser { get; set; }
        public string AppointedService { get; set; }
        public  TimeSpan TimeOfAppointment { get; set; }
        public DateTime Date { get; set; }
        public  bool IsBusy { get; set; }

        public string DisplayTime => TimeOfAppointment.ToString();
        public string DisplayIsBusy => IsBusy ? "занято" : "свободно";
        public string DisplayDate => Date.ToShortDateString();
        public bool IsEnabled => !IsBusy;

        public string EndTime
        {
            get
            {
                return TimeOfAppointment.Add(new TimeSpan(0, 30, 0)).ToString();
            }
        }
        public void Reserve(string username, string service)
        {
            IsBusy = true;
            AppointedUser = username;
            AppointedService = service;
        }

    }

    public class AppointmentBig : Appointment
    {
        public AppointmentBig(Appointment appointment,UserProfile userProfile)
        {
            this.appointment = appointment;
            this.userProfile = userProfile;
        }

        private UserProfile userProfile;
        private Appointment appointment;
        public UserProfile UserProfile => userProfile;
        public Appointment Appointment => appointment;
    }
}
