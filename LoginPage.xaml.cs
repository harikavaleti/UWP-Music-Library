using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
   
    public sealed partial class LoginPage : Page
    {
        public static bool Islogin { get; set; }
        public static string UserName { get; set; }
        
        public LoginPage()
        {
            this.InitializeComponent();
            Islogin = true;

        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\User";
            string ps = passwordBox.Password.ToString();
            UserName = username.Text;
            string[] VerifyUsers = Directory.GetDirectories(path);
            foreach (string user in VerifyUsers)
            {
                string un = Path.GetFileNameWithoutExtension(user);
               if((UserName == un) && (ps == "rules"))
               {
                    this.Frame.Navigate(typeof(MainPage));
               }
                else
                {
                    ErrorMessage.Text = "Invalid Credentials";
                }
            }
  
        }
    }
}
