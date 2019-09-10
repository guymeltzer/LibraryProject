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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using BookLib;
using BusinessLogic;
using Windows.UI.Popups;
using ProjectLibraryGuy;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectLibraryGuy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LibrarianPage : Page
    {
        DataHelperClass data;
        List<AbstractItem> lastSearch;

        public LibrarianPage()
        {
            this.InitializeComponent();


            ClearAll();
            //Image image = new Image();
            //image.Source = new SvgImageSource(new Uri(@"https://www.shareicon.net/download/2015/12/19/690041_arrows.svg"));
            //BackToMain_BTN.Content = image;
            //BackToMain_BTN.Content = image.Stretch
            //AddItem_BTN.Visibility = Visibility.Collapsed;
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
                throw new Exception("was expecting to get data helper class from login page");

            BookRadioButton.IsChecked = true;
            
        }



        private void BackToMain_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PermissionPage), data);
        }

        private void Clear_BTN_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            BookRadioButton.IsChecked = true;
        }

        private void HideGeneralFields()
        {
            Author_TextBox.Visibility = Visibility.Collapsed;
            Author_TextBox.Text = "";
            Name_TextBox.Visibility = Visibility.Collapsed;
            Name_TextBox.Text = "";
            PublishingYear_TextBox.Visibility = Visibility.Collapsed;
            PublishingYear_TextBox.Text = "";
            ISBN_TextBox.Visibility = Visibility.Collapsed;
            ISBN_TextBox.Text = "";
            CopyNumber_TextBox.Visibility = Visibility.Collapsed;
            CopyNumber_TextBox.Text = "";
            Author_TextBlock.Visibility = Visibility.Collapsed;
            Name_TextBlock.Visibility = Visibility.Collapsed;
            PublishingYear_TextBlock.Visibility = Visibility.Collapsed;
            ISBN_TextBlock.Visibility = Visibility.Collapsed;
            CopyNumber_TextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowGeneralFields()
        {
            Author_TextBox.Visibility = Visibility.Visible;
            Name_TextBox.Visibility = Visibility.Visible;
            PublishingYear_TextBox.Visibility = Visibility.Visible;
            ISBN_TextBox.Visibility = Visibility.Visible;
            CopyNumber_TextBox.Visibility = Visibility.Visible;
            Author_TextBlock.Visibility = Visibility.Visible;
            Name_TextBlock.Visibility = Visibility.Visible;
            PublishingYear_TextBlock.Visibility = Visibility.Visible;
            ISBN_TextBlock.Visibility = Visibility.Visible;
            CopyNumber_TextBlock.Visibility = Visibility.Visible;
        }


        private void HideJournalFields()
        {
            JournalRadioButton.IsChecked = false;
            JournalType_TextBox.Visibility = Visibility.Collapsed;
            JournalCategory_TextBox.Visibility = Visibility.Collapsed;
            JournalType_TextBlock.Visibility = Visibility.Collapsed;
            JournalCategory_TextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowJournalFields()
        {

            JournalType_TextBox.Visibility = Visibility.Visible;
            JournalCategory_TextBox.Visibility = Visibility.Visible;
            JournalType_TextBlock.Visibility = Visibility.Visible;
            JournalCategory_TextBlock.Visibility = Visibility.Visible;
        }

        private void HideBookFields()
        {
            BookCategory_TextBlock.Visibility = Visibility.Collapsed;
            BookCategory_TextBox.Visibility = Visibility.Collapsed;
            BookCategory_TextBox.Text = "";
            BookRadioButton.IsChecked = false;

        }

        private void ShowBookFields()
        {
            BookCategory_TextBlock.Visibility = Visibility.Visible;
            BookCategory_TextBox.Visibility = Visibility.Visible;
        }

        private void ClearAll()
        {
            HideBookFields();
            HideGeneralFields();
            HideJournalFields();
            NameListView.Items.Clear();
            TypeListView.Items.Clear();
            IsBorrowedListView.Items.Clear();
        }

        private void BookRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            HideJournalFields();
            ShowGeneralFields();
            ShowBookFields();
        }

        private void JournalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            HideBookFields();
            ShowGeneralFields();
            ShowJournalFields();
        }

        public void FillLists()
        {
            NameListView.Items.Clear();
            TypeListView.Items.Clear();
            IsBorrowedListView.Items.Clear();
            foreach (var item in data.AbstractItems.Library)
            {
                NameListView.Items.Add(item.Name);
                if (item is Book)
                {
                    TypeListView.Items.Add("Book");
                }

                else
                {
                    TypeListView.Items.Add("Journal");
                }
                IsBorrowedListView.Items.Add(item.IsBorrowed ? "Yes" : "No");
            }
        }

            private async void FilterAndUpdateLastSearchList()
        {
            NameListView.Items.Clear();
            TypeListView.Items.Clear();
            IsBorrowedListView.Items.Clear();

             if (BookRadioButton.IsChecked == false && JournalRadioButton.IsChecked == false)
            {
                MessageDialog msg = new MessageDialog("You must choose either Book or Journal to continue. ");
                await msg.ShowAsync();
            }


            lastSearch = LiteraryOperations.FilterItems(data, (BookRadioButton.IsChecked == true), (JournalRadioButton.IsChecked == true), Name_TextBox.Text,
                Author_TextBox.Text, PublishingYear_TextBox.Text, CopyNumber_TextBox.Text, JournalType_TextBox.Text,
                JournalCategory_TextBox.Text, BookCategory_TextBox.Text);


            foreach (AbstractItem ai in lastSearch)
            {
                NameListView.Items.Add(ai.Name);
                TypeListView.Items.Add((ai is Book) ? "Book" : "Journal");
                IsBorrowedListView.Items.Add((ai.IsBorrowed) ? "Yes" : "No");

            }
        }

        private void LibSearch_BTN_Click(object sender, RoutedEventArgs e)
        {
            FilterAndUpdateLastSearchList();
        }

        private async void RemoveItem_BTN_Click(object sender, RoutedEventArgs e)
        {
            int nameListIdx = NameListView.SelectedIndex;
            if (nameListIdx >= 0)
            {
                data.AbstractItems.RemoveItem(lastSearch[nameListIdx]);
            }
            
            else
            {
                await new MessageDialog("You need to pick a library item to remove it.").ShowAsync();
            }

            FilterAndUpdateLastSearchList();
        }

        private void AddItem_BTN_Click(object sender, RoutedEventArgs e)
        {
            LiteraryOperations.AddItem(data, (BookRadioButton.IsChecked == true), (JournalRadioButton.IsChecked == true), Name_TextBox.Text,
                Author_TextBox.Text, PublishingYear_TextBox.Text, JournalType_TextBox.Text,
                JournalCategory_TextBox.Text, BookCategory_TextBox.Text, CopyNumber_TextBox.Text);

            FilterAndUpdateLastSearchList();

        }

        private void ShowAllDetails_BTN_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllDetailsPage), data);
        }
    }
}
