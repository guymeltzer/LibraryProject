using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class Librarian : AUser, ICustomerPermissions
    {
        public void ViewItem() { }
        public void BorrowItem() { }

        public Librarian(string Username,string  Password, bool isAdmin) : base(Username, Password, isAdmin)
        {

        }
        public List<Permissions> GetPermissions()
        {
            //List < BookLib.Permissions > p = 
            return new List<BookLib.Permissions>() { Permissions.Borrow, Permissions.Return, Permissions.View, Permissions.Add, Permissions.Edit, Permissions.Remove }; 
        }
    }
}
