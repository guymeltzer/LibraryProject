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
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectLibraryGuy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DataHelperClass data = new DataHelperClass();
        public MainPage()
        {
            this.InitializeComponent();

            if (data.Users == null)
            {
                data.Users = UserCollection.InitializeUserCollection();

            }
            data.AbstractItems = LiteraryCollection.InitializeLiteraryCollection();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DataHelperClass)
            {
                data = (DataHelperClass)e.Parameter;
            }

            //else
            //{
            //    throw new Exception("was expecting to get data helper class from login page");
            //}
        }

        private async void Login_BTN_Click(object sender, RoutedEventArgs e)
        {
            //UserCollection.UserType u = data.Users.VerifyUser(UserName_TextBox.Text, Password_TextBox.Password);
            try
            {
                data.CurrentUser = data.Users.VerifyUser(UserName_TextBox.Text, Password_TextBox.Password);
                if (data.CurrentUser.isAdmin)
                {
                    this.Frame.Navigate(typeof(PermissionPage), data);
                }

                else
                {
                    this.Frame.Navigate(typeof(CustomerPage), data);
                }
            }

            catch (NullReferenceException)
            {
                await new MessageDialog("Credentials not recognized, please try again!").ShowAsync();
            }

            //else
            //{
            //   // Console.WriteLine("insert error here");
            //}

           // LibrarianPage adminPage = new LibrarianPage();
            //this.Frame.Navigate(typeof(LibrarianPage));
        }

        private void Clear_BTN_Click(object sender, RoutedEventArgs e)
        {
            UserName_TextBox.Text = "";
            Password_TextBox.Password = ""; 
        }

        
    }
}
