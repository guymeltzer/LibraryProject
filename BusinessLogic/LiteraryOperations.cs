using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLib;
using Windows.UI.Popups;

namespace BusinessLogic
{
    public static class LiteraryOperations
    {



        public static List<AbstractItem> FilterItems(DataHelperClass data, bool BookRadio, bool JournalRadio, string Name_Text,
                string Author_Text, string PublishingYear_Text, string JournalType_Text,
                string JournalCategory_Text, string BookCategory_Text, string CopyNumber_Text)
        {
            List<AbstractItem> filteredItems = new List<AbstractItem>();

            if (BookRadio == JournalRadio)
            {
                throw new Exception("literary cannot be both book and Journal or none");
            }

            bool filterByName = Name_Text != "";
            bool filterByAuthor = Author_Text != "";
            bool filterByYear = PublishingYear_Text != "";
            bool filterByCopyNumber = CopyNumber_Text != "";
            bool filterByJournalType = JournalType_Text != "";
            bool filterByJournalCategory = JournalCategory_Text != "";
            bool filterByBookCategory = BookCategory_Text != "";

            foreach (AbstractItem ai in data.AbstractItems.GetAllLiteraryItems())
            {
                if ((!filterByName || ai.Name.Equals(Name_Text)) && (!filterByAuthor || ai.Author.Equals(Author_Text)) &&
                     (!filterByYear || ai.PublishingYear == int.Parse(PublishingYear_Text)) && (!filterByCopyNumber || ai.CopyNumber == (int.Parse(CopyNumber_Text))))
                {
                    if ((BookRadio) && (ai is Book))
                    {
                        Book b = (Book)ai;
                        if (!filterByBookCategory || b.bookCategory.Equals(BookCategory_Text))
                        {
                            filteredItems.Add(ai);
                        }
                    }
                    else if ((JournalRadio) && (ai is Journal))
                    {
                        Journal j = (Journal)ai;
                        if (!filterByJournalType || j.journalcategory.Equals((Journal.JournalType)int.Parse(JournalType_Text)) &&
                            (!filterByJournalCategory || j.PublishingYear.Equals((Journal.JournalCategory)int.Parse(JournalCategory_Text))))
                        {
                            filteredItems.Add(ai);
                        }
                    }
                }
            }

            return filteredItems;
        }


        public async static void AddItem(DataHelperClass data, bool BookRadio, bool JournalRadio, string Name_Text,
                 string Author_Text, string PublishingYear_Text, string JournalType_Text,
                 string JournalCategory_Text, string BookCategory_Text, string CopyNumber_Text)
        {        
            if (BookRadio == JournalRadio)
            {
                throw new Exception("Item cannot be both book and Journal or none");
            }

            bool filterByName = Name_Text != "";
            bool filterByAuthor = Author_Text != "";
            bool filterByYear = PublishingYear_Text != "";
            bool filterByJournalType = JournalType_Text != "";
            bool filterByJournalCategory = JournalCategory_Text != "";
            bool filterByBookCategory = BookCategory_Text != "";
            bool filterByCopyNumber = CopyNumber_Text != "";

            if (!filterByName || !filterByAuthor || !filterByYear || !filterByCopyNumber)
            {
                await new MessageDialog("You Must Fill All Fields.").ShowAsync();

            }
            if ( (BookRadio && !filterByBookCategory) || (JournalRadio && (!filterByJournalType || !filterByJournalCategory)) )
            {
                return;
                //TODO add message to user
            }

            //try to see if book already exists
            List<AbstractItem> filteredItems = FilterItems(data, BookRadio, JournalRadio, Name_Text,
                  Author_Text, PublishingYear_Text, CopyNumber_Text, JournalType_Text,
                  JournalCategory_Text, BookCategory_Text);

            if(filteredItems.Count > 1)
            {
                throw new Exception("Found more than one book with the same parameters!");
            }
            else if(filteredItems.Count == 1)
            {
                //TODO inform user that there that book was already addded before
            }
            else
            {
                try
                {
                    AbstractItem ai;

                    if (BookRadio)
                    {
                        ai = new Book(Guid.NewGuid(), Name_Text, Author_Text, int.Parse(PublishingYear_Text), int.Parse(CopyNumber_Text), (Book.BookCategory)int.Parse(BookCategory_Text));
                    }
                    else
                    {
                        ai = new Journal(Guid.NewGuid(), Name_Text, Author_Text, int.Parse(PublishingYear_Text), int.Parse(CopyNumber_Text),
                            (Journal.JournalType)int.Parse(JournalType_Text), (Journal.JournalCategory)int.Parse(JournalCategory_Text));
                    }
                    data.AbstractItems.AddItem(ai);
                }

                catch (FormatException)
                {
                    await new MessageDialog("Please put in your required information only in ENGLISH!").ShowAsync();
                }
                
            }
        }
    }
}



