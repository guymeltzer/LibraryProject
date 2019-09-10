using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookLib
{
    interface ILibrary
    {
         void AddItem(AbstractItem item);
         void RemoveItem(AbstractItem item);
         void ModifyItem(AbstractItem newItem, AbstractItem oldItem);
         void BorrowItem(AbstractItem item);
         void ReturnItem(AbstractItem item);
         
    }
}
