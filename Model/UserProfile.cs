using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMusicLibrary.Model
{
    public enum UserMenu
    {
        UserProfile,
        LogOut

    }
    public class UserProfile
    {
        public string UserName { get; set; }
        public double BonusPoints { get; set; }
        public UserMenu usermenu { get; set; }

    }
}
