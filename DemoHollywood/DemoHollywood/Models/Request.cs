﻿namespace DemoHollywood.Models
{
    public abstract class Request
    {
        public string Author { get; set; }
        public string TargetTable { get; set; }
        public string Key { get; set; }
    }

    public sealed class ReueqstAppointments : Request
    {
        public Appointment Appointment { get; set; }

    }

    public sealed class RequestRemoveAppointment : Request
    {
        public string AppointKey { get; set; }
    }

    public sealed class RequestChangeUserProfile : Request
    {
        public UserProfile UserProfile { get; set; }
    }
}
