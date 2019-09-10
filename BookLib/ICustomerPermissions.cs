using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    interface ICustomerPermissions
    {
        void ViewItem();
        void BorrowItem();
        List<Permissions> GetPermissions();

    }

    public enum Permissions
    {
        View,
        Edit,
        Borrow,
        Return,
        Add,
        Remove
    }

}
