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
using ProjectLibraryGuy;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectLibraryGuy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserManagementPage : Page
    {
        DataHelperClass data;
        AUser tempuser;

        public UserManagementPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DataHelperClass)
            {
                data = (DataHelperClass)e.Parameter;
            }

            else
                throw new Exception("was expecting to get data helper class from login page");

            FillList();
        }

        private void FillList()
        {
            UserMGMT_ListView.Items.Clear();
            IsAdmin_ListView.Items.Clear();
            foreach (var item in data.Users.Users)
            {
                UserMGMT_ListView.Items.Add(item.Username);
                IsAdmin_ListView.Items.Add(item.isAdmin ? "Yes" : "No");

            }
        }

        private async void UserRemove_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tempuser = data.Users.FindUser(UserMGMT_ListView.SelectedItem.ToString());
                {
                    if (tempuser.isAdmin)
                    {
                        throw new InvalidDataException("You cannot remove youserlf from the user list!");
                    }
                        data.Users.Users.Remove(tempuser);
                        FillList();
                }
            }

            catch (NullReferenceException)
            {
                await new MessageDialog("No input received, please try again!").ShowAsync();
            }

            catch (InvalidDataException)
            {
                await new MessageDialog("You cannot remove youserlf from the user list!").ShowAsync();

            }

        }

        private void UserBack_BTN_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PermissionPage), data);
        }

        private async void UserAdd_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tempuser = new AUser(Username_TextBox.Text, Password_PasswordBox.Password, IsAdmin_ToggleSwitch.IsOn);
                if (Username_TextBox.Text != string.Empty || Password_PasswordBox.Password != string.Empty)
                {
                    data.Users.Users.Add(tempuser);
                    FillList();
                }

                if (Username_TextBox.Text == "")
                    throw new NullReferenceException("Wrong input please try again!");


                if (Password_PasswordBox.Password == "")
                {
                    data.Users.Users.Remove(tempuser);
                    throw new NullReferenceException("Wrong input please try again!");
                }

            }

            catch (NullReferenceException)
            {
                await new MessageDialog("No input received, please try again!").ShowAsync();
            }

        }
    }
}
