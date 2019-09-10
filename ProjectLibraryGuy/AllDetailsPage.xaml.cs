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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectLibraryGuy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllDetailsPage : Page
    {
        DataHelperClass data;

        public AllDetailsPage()
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

            FillLists();
        }

        private void AllDetailsBack_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (data.CurrentUser.isAdmin)
                Frame.Navigate(typeof(PermissionPage), data);

            else
                Frame.Navigate(typeof(CustomerPage), data);
        }

        private void FillLists()
        {
            List<Journal> journals = data.AbstractItems.OnlyJournals();
            List<Book> books = data.AbstractItems.OnlyBooks();
            Name_Listview.Items.Clear();
            Author_Listview.Items.Clear();
            ISBN_Listview.Items.Clear();
            PublishingYear_Listview.Items.Clear();
            CopyNumber_Listview.Items.Clear();
            Category_Listview.Items.Clear();
            JournalType_Listview.Items.Clear();


            foreach (var item in books)
            {
                Name_Listview.Items.Add(item.Name);
                Author_Listview.Items.Add(item.Author);
                ISBN_Listview.Items.Add(item.ISBN);
                PublishingYear_Listview.Items.Add(item.PublishingYear);
                CopyNumber_Listview.Items.Add(item.CopyNumber);
                Category_Listview.Items.Add(item.bookCategory);
                JournalType_Listview.Items.Add("not applicable");
            }

            foreach (var item in journals)
            {
                Name_Listview.Items.Add(item.Name);
                Author_Listview.Items.Add(item.Author);
                ISBN_Listview.Items.Add(item.ISBN);
                PublishingYear_Listview.Items.Add(item.PublishingYear);
                CopyNumber_Listview.Items.Add(item.CopyNumber);
                Category_Listview.Items.Add(item.journalcategory);
                JournalType_Listview.Items.Add(item.journaltype);
            }
        }
    }
}
