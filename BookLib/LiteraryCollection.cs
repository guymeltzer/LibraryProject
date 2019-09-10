using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BookLib
{
    public class LiteraryCollection : ILibrary
    {
        public List<AbstractItem> Library;

        public LiteraryCollection()
        {
            Library = new List<AbstractItem>();

            AddItem(new Book(Guid.NewGuid(), "Fear And Loathing In Las Vegas", "Hunter S. Thompson", 1972, 5, Book.BookCategory.Biography));
            AddItem(new Book(Guid.NewGuid(), "Cat's Cradle", "Kurt Vonnegut", 1963, 2, Book.BookCategory.Fiction));
            AddItem(new Book(Guid.NewGuid(), "The Andromeda Strain", "Michael Crichton", 1969, 9, Book.BookCategory.Cooking));
            AddItem(new Book(Guid.NewGuid(), "The Yiddish Policemen's Union", "Michael Chabon", 2007, 6, Book.BookCategory.Comics));
            AddItem(new Book(Guid.NewGuid(), "Fight Club", "Chuck Palahniuk", 1996, 54, Book.BookCategory.Fiction));
            AddItem(new Book(Guid.NewGuid(), "The Catcher in the Rye", "J.D. Salinger", 1951, 70, Book.BookCategory.Kids));
            AddItem(new Book(Guid.NewGuid(), "Nineteen Eighty-Four", "Georeg Orwell", 1949, 23, Book.BookCategory.Comics));
            AddItem(new Book(Guid.NewGuid(), "Watchmen", "Alan Moore", 1987, 101, Book.BookCategory.Comics));
            AddItem(new Book(Guid.NewGuid(), "Snoop & Martha", "Martha Stewart", 2011, 400, Book.BookCategory.Cooking));
            AddItem(new Book(Guid.NewGuid(), "The Life Of David Gale", "Kevin Spacy", 1999, 66, Book.BookCategory.Biography));


            AddItem(new Journal(Guid.NewGuid(), "National Geographic", "Lisa Thomas", 2017, 3491, Journal.JournalType.Weekly, Journal.JournalCategory.Animals));
            AddItem(new Journal(Guid.NewGuid(), "Time", "Henry Luce", 2001, 1003, Journal.JournalType.Monthly, Journal.JournalCategory.Hobbies));
            AddItem(new Journal(Guid.NewGuid(), "Wanderlust", "Chloe Esposito", 1995, 140, Journal.JournalType.Weekly, Journal.JournalCategory.Travelling));
            AddItem(new Journal(Guid.NewGuid(), "Aperture", "Chris Boot", 2010, 33, Journal.JournalType.Monthly, Journal.JournalCategory.Art));
            AddItem(new Journal(Guid.NewGuid(), "Rotor Drone", "John Reid", 2015, 19, Journal.JournalType.Daily, Journal.JournalCategory.Hobbies));

        }


        public static LiteraryCollection InitializeLiteraryCollection()
        {
            LiteraryCollection literaryCollection = new LiteraryCollection();
            return literaryCollection;
        }

        public void AddItem(AbstractItem item)
        {
            if (item is Book)
            {
                if (item.Name == "")
                {
                    MessageDialog showDialog = new MessageDialog("Did not receive input, please try again. ");
                }
                Library.Add(item);
            }

            else if (item is Journal)
            {
                if (item.Name == "")
                {
                }                    MessageDialog showDialog = new MessageDialog("Did not receive input, please try again. ");

                Library.Add(item);
            }
        }

        public void RemoveItem(AbstractItem item)
        {
            if (!(Library.Contains(item)))
            {
                throw new Exception("Fatal error: item" + item.Name + "does not appear in Library!");
            }
            Library.Remove(item);
        }
        public void ModifyItem(AbstractItem newItem, AbstractItem oldItem)
        {
            if (Library.Contains(oldItem))
            {
                Library.Remove(oldItem);
                Library.Add(newItem);
            }
        }
        public void BorrowItem(AbstractItem item)
        {
            if (!(Library.Contains(item)))
            {
                throw new Exception("Fatal error: item" + item + "does not appear in Library!");
            }
            if (item.IsBorrowed)
            {
                throw new Exception("Fatal error: item" + item + "is already borrowed");
            }
            item.IsBorrowed = true;
        }

        public async void UserBorrowItem(AbstractItem item, AUser user)
        {
            try
            {
                if (item.IsBorrowed)
                    throw new InvalidOperationException("Item is already borrowed! try a different one.");
                user.Borrowed.Add(item);
                item.IsBorrowed = true;
            }
            
            catch (InvalidOperationException)
            {
                await new MessageDialog("Item is already borrowed! try a different one.").ShowAsync();
            }
        }

        public async void UserReturnItem(AbstractItem item, AUser user)
        {
            try
            {
                if (!item.IsBorrowed)
                    throw new InvalidOperationException("Item is already returned and in library!");
                else
                {
                    user.Borrowed.Remove(item);
                    item.IsBorrowed = false;
                }
            }
            
            catch (InvalidOperationException)
            {
                await new MessageDialog("Item is already returned and in library!").ShowAsync();
            }

        }

        public async void ReturnItem(AbstractItem item)
        {
            try
            {
                if (!(Library.Contains(item)))
                {
                    throw new InvalidOperationException("Error: item" + item + "does not appear in Library!");
                }
                if (!item.IsBorrowed)
                {
                    throw new InvalidOperationException("Error: item" + item + "is not borrowed");
                }
                item.IsBorrowed = false;
            }

            catch (InvalidOperationException)
            {
                await new MessageDialog("Error : cannot return this item, please try another action.").ShowAsync();
            }

        }

        public List<AbstractItem> GetAllLiteraryItems()
        {
            return Library;
        }

        public List<Book> OnlyBooks()
        {
            List<Book> list = new List<Book>();
            foreach (var item in Library)
            {
                if (item is Book)
                {
                    list.Add((Book)item);
                }
            }
            return list;
        }

        public List<Journal> OnlyJournals()
        {
            List<Journal> list = new List<Journal>();
            foreach (var item in Library)
            {
                if (item is Journal)
                {
                    list.Add((Journal)item);
                }
            }
            return list;
        }

        public AbstractItem FindByName(string name)
        {
            foreach (var item in Library)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
