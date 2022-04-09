using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHollywood.Models
{
    public class UserProfile
    {
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }


        public static bool operator==(UserProfile left,UserProfile right)
        {
            return left.DisplayName == right.DisplayName && left.IsAdmin == right.IsAdmin && left.PhoneNumber == right.PhoneNumber;
        }
        public static bool operator !=(UserProfile left, UserProfile right)
        {
            return left.DisplayName != right.DisplayName || left.IsAdmin != right.IsAdmin || left.PhoneNumber != right.PhoneNumber;
        }
    }
}
