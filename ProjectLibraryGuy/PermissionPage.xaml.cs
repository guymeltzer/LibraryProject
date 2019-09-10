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
using BookLib;
using BusinessLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectLibraryGuy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PermissionPage : Page
    {
        DataHelperClass data = new DataHelperClass();

        public PermissionPage()
        {
            this.InitializeComponent();
            //data.Users = UserCollection.InitializeUserCollection();
            //data.AbstractItems = LiteraryCollection.InitializeLiteraryCollection();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DataHelperClass)
            {
                data = (DataHelperClass)e.Parameter;
            }

            else
            {
                throw new Exception("was expecting to get data helper class from login page");
            }
        }

        private void UserMGMT_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserManagementPage), data);

        }

        private void LibraryMGMT_System_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LibrarianPage), data);

        }

        private void Logout_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), data);
        }

        private void Exit_BTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
