using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
   public class Book : AbstractItem
    {
        public BookCategory bookCategory { get; set; }

        public enum BookCategory
        {
            Biography = 1, 
            Business = 2,
            Kids = 3,
            Comics = 4, 
            Cooking = 5,
            Fiction = 6
        }

        public Book(Guid isbn, string name, string author, int publishingyear, int copynumber, BookCategory bookCategory) : base(isbn, name, author, publishingyear, copynumber)
        {
            this.bookCategory = bookCategory;
        }

        public override string ToString()
        {
            return base.ToString() + " " + bookCategory;
        }
        
    }
}
