using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class AUser
    {

        public AUser(string Name, string Pass, bool Admin)
        {
            Username = Name;
            Password = Pass;
            Borrowed = new List<AbstractItem>();
            isAdmin = Admin;
        }

        public bool isAdmin { get ; set; }

        public string Username { get; set; }
       

        public string Password { get; set; }


        public List<AbstractItem> Borrowed { get; set; }

    }
}
