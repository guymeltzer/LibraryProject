using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
     class Customer : AUser, ICustomerPermissions
    {
      public void ViewItem() { }
      public void BorrowItem() { }

        public Customer(string Username, string Password, bool isAdmin) : base ( Username,  Password, isAdmin)
        {

        }
        public List<Permissions> GetPermissions()
        {
            return new List<BookLib.Permissions>() { Permissions.Borrow, Permissions.Return, Permissions.View };
        }

    }

}

