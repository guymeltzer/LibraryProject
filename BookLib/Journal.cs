using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class Journal : AbstractItem
    {
        public enum JournalType
        {
            Daily = 1,
            Weekly = 2,
            Monthly = 3
        }

        public enum JournalCategory
        {
            Animals = 1,
            Art = 2,
            Hobbies = 3,
            Travelling = 4
        }

        public JournalType journaltype { get; set; }
        public JournalCategory journalcategory { get; set; }

        public Journal(Guid ISBN, string Name, string Author, int PublishingYear, int CopyNumber
                , JournalType journaltype, JournalCategory journalcategory) : base(ISBN, Name, Author, PublishingYear, CopyNumber)
        {
            this.journaltype = journaltype;
            this.journalcategory = journalcategory;
        }
    }
}
