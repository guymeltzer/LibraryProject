using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public abstract class AbstractItem
    {
        private Guid _isbn;
        private string _name, _author;
        private int _publishingYear, _copyNumber, _availableCopies;
        private bool isBorrowed;

        public Guid ISBN { get => _isbn; set => _isbn = value; }
        public string Name { get => _name; set => _name = value; }
        public string Author { get => _author; set => _author = value; }
        public int PublishingYear { get => _publishingYear; set => _publishingYear = value; }
        public int AvailableCopies { get => _availableCopies; set => _availableCopies = value; }
        public int CopyNumber { get => _copyNumber; set => _copyNumber = value; }
        public bool IsBorrowed { get => isBorrowed; set => isBorrowed = value; }

        public AbstractItem(Guid ISBN, string Name, string Author, int PublishingYear, int CopyNumber)
        {
            _isbn = ISBN;
            _name = Name;
            _author = Author;
            _publishingYear = PublishingYear;
            _availableCopies = AvailableCopies;
            _copyNumber = CopyNumber;
            this.isBorrowed = false;
        }

        public override string ToString()
        {
            return _name + " " + _author + isBorrowed;
        }


    }
}
