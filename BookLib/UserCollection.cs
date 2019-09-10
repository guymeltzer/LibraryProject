using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
   public class UserCollection
    {
        public enum UserType
        {
            LIBRARIAN,
            CUSTOMER,
            NONE,
        }
        public List<AUser> Users = new List<AUser>();

        public UserCollection()
        {

        }

        public static UserCollection InitializeUserCollection()
        {
            UserCollection usercollection = new UserCollection();
            AUser AdminUser = new AUser("guy", "library", true);
            AUser CustomerUser = new AUser("sela", "sela123", false);
            AUser CustomerUser2 = new AUser("user", "Aa123456", false);

            usercollection.Users.Add(AdminUser);
            usercollection.Users.Add(CustomerUser);
            return usercollection;
            
        }

        public void AddUser(AUser user)
        {
            Users.Add(user);
        }

        public AUser FindUser (string username)
        {
            foreach (AUser user in Users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return null;
        }

        public AUser VerifyUser (string username, string password)
        {
            AUser loginUser = FindUser(username);
            return loginUser;
        }
    }
}
