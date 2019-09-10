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
using BusinessLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectLibraryGuy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        DataHelperClass data;
        AbstractItem CurrentItem;
        List<AbstractItem> lastSearch;

        public CustomerPage()
        {
            this.InitializeComponent();
            ClearAll();
            lastSearch = new List<AbstractItem>();
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
            CustomerBookRadioButton.IsChecked = true;
            FillLists();
        }

        private void HideGeneralFields()
        {
            CustomerAuthor_TextBox.Visibility = Visibility.Collapsed;
            CustomerAuthor_TextBox.Text = "";
            CustomerName_TextBox.Visibility = Visibility.Collapsed;
            CustomerName_TextBox.Text = "";
            CustomerPublishingYear_TextBox.Visibility = Visibility.Collapsed;
            CustomerPublishingYear_TextBox.Text = "";
            CustomerISBN_TextBox.Visibility = Visibility.Collapsed;
            CustomerISBN_TextBox.Text = "";
            CustomerCopyNumber_TextBox.Visibility = Visibility.Collapsed;
            CustomerCopyNumber_TextBox.Text = "";
            CustomerAuthor_TextBlock.Visibility = Visibility.Collapsed;
            CustomerName_TextBlock.Visibility = Visibility.Collapsed;
            CustomerPublishingYear_TextBlock.Visibility = Visibility.Collapsed;
            CustomerISBN_TextBlock.Visibility = Visibility.Collapsed;
            CustomerCopyNumber_TextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowGeneralFields()
        {
            CustomerAuthor_TextBox.Visibility = Visibility.Visible;
            CustomerName_TextBox.Visibility = Visibility.Visible;
            CustomerPublishingYear_TextBox.Visibility = Visibility.Visible;
            CustomerISBN_TextBox.Visibility = Visibility.Visible;
            CustomerCopyNumber_TextBox.Visibility = Visibility.Visible;
            CustomerAuthor_TextBlock.Visibility = Visibility.Visible;
            CustomerName_TextBlock.Visibility = Visibility.Visible;
            CustomerPublishingYear_TextBlock.Visibility = Visibility.Visible;
            CustomerISBN_TextBlock.Visibility = Visibility.Visible;
            CustomerCopyNumber_TextBlock.Visibility = Visibility.Visible;
        }

        private void HideBookFields()
        {
            CustomerBookRadioButton.IsChecked = false;
            CustomerBookCategory_TextBlock.Visibility = Visibility.Collapsed;
            CustomerBookCategory_TextBox.Visibility = Visibility.Collapsed;
            CustomerBookCategory_TextBox.Text = "";
        }

        private void ShowBookFields()
        {
            CustomerBookCategory_TextBlock.Visibility = Visibility.Visible;
            CustomerBookCategory_TextBox.Visibility = Visibility.Visible;
        }

        private void HideJournalFields()
        {
            CustomerJournalRadioButton.IsChecked = false;
            CustomerJournalType_TextBox.Visibility = Visibility.Collapsed;
            CustomerJournalCategory_TextBox.Visibility = Visibility.Collapsed;
            CustomerJournalType_TextBlock.Visibility = Visibility.Collapsed;
            CustomerJournalCategory_TextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowJournalFields()
        {
            CustomerJournalType_TextBox.Visibility = Visibility.Visible;
            CustomerJournalCategory_TextBox.Visibility = Visibility.Visible;
            CustomerJournalType_TextBlock.Visibility = Visibility.Visible;
            CustomerJournalCategory_TextBlock.Visibility = Visibility.Visible;
        }

        private void ClearAll()
        {
            HideBookFields();
            HideGeneralFields();
            HideJournalFields();
            Name_LIstView.Items.Clear();
            Type_ListView.Items.Clear();
            IsBorrowed_ListView.Items.Clear();
        }

        private void CustomerJournalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            HideBookFields();
            ShowGeneralFields();
            ShowJournalFields();
        }

        private void CustomerBookRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ShowBookFields();
            HideJournalFields();
            ShowGeneralFields();
        }

        private void FilterAndUpdateLastSearchList()
        {
            Name_LIstView.Items.Clear();
            Type_ListView.Items.Clear();
            IsBorrowed_ListView.Items.Clear();

            lastSearch = LiteraryOperations.FilterItems(data, (CustomerBookRadioButton.IsChecked == true), (CustomerJournalRadioButton.IsChecked == true), CustomerName_TextBox.Text,
                CustomerAuthor_TextBox.Text, CustomerPublishingYear_TextBox.Text, CustomerCopyNumber_TextBox.Text, CustomerJournalType_TextBox.Text,
                CustomerJournalCategory_TextBox.Text, CustomerBookCategory_TextBox.Text);

            foreach (AbstractItem ai in lastSearch)
            {
                Name_LIstView.Items.Add(ai.Name);
                Type_ListView.Items.Add((ai is Book) ? "Book" : "Journal");
                //IsBorrowed_ListView.Items.Add((data.CurrentUser.Borrowed.Contains(CurrentItem) ? "Yes" : "No"));
                IsBorrowed_ListView.Items.Add((ai.IsBorrowed) ? "Yes" : "No");

            }
        }

        private void Back_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void CustomerClear_BTN_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            CustomerBookRadioButton.IsChecked = true;
        }

        private void LibSearch_BTN_Click(object sender, RoutedEventArgs e)
        {
        //    if (CustomerName_TextBox.Text = "" && CustomerAuthor_TextBox.Text = ""
            FilterAndUpdateLastSearchList();
        }

        private async void BorrowItem_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.AbstractItems.FindByName(Name_LIstView.SelectedItem.ToString()) is Book)
                {
                    CurrentItem = (Book)data.AbstractItems.FindByName(Name_LIstView.SelectedItem.ToString());
                }

                else
                {
                    CurrentItem = (Journal)data.AbstractItems.FindByName(Name_LIstView.SelectedItem.ToString());
                }
                data.AbstractItems.UserBorrowItem(CurrentItem, data.CurrentUser);
                FillLists();

            }

            catch (NullReferenceException)
            {
                await new MessageDialog("You must choose an item to remove before trying to borrow it!").ShowAsync();
            }
            
        }


        private void FillLists()
        {
            Name_LIstView.Items.Clear();
            Type_ListView.Items.Clear();
            IsBorrowed_ListView.Items.Clear();
            foreach (var item in data.AbstractItems.Library)
            {
                Name_LIstView.Items.Add(item.Name);
                if (item is Book)
                {
                    Type_ListView.Items.Add("Book");
                }
                
                else
                {
                    Type_ListView.Items.Add("Journal");
                }
                IsBorrowed_ListView.Items.Add(item.IsBorrowed ? "Yes" : "No");
            }
        }

        private async void ReturnItem_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.AbstractItems.FindByName(Name_LIstView.SelectedItem.ToString()) is Book)
                {
                    CurrentItem = (Book)data.AbstractItems.FindByName(Name_LIstView.SelectedItem.ToString());
                }

                else
                {
                    CurrentItem = (Journal)data.AbstractItems.FindByName(Name_LIstView.SelectedItem.ToString());
                }
                data.AbstractItems.UserReturnItem(CurrentItem, data.CurrentUser);
                FillLists();
            }

            catch (NullReferenceException)
            {
                await new MessageDialog("You must choose an item to remove before trying to return it!").ShowAsync();
            }


        }

        private void ShowAllDetails_BTN_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllDetailsPage), data);
        }
    }
}
