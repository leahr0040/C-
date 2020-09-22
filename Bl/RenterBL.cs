using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;


namespace Bl
{
    public class RenterBL
    {
        public static bool AddRenter(UserDTO ud)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                ud.RoleID = (from r in db.UserRoles where r.RoleName == 'שוכר' select r.RoleID).FirstOrDefault();
            }
            User u = UserDTO.ToDal(ud);
            return UserDAL.AddUser(u);
        }
        public static bool UpdateRenter(UserDTO ud)
        {
            return UserBL.UpdateUser(ud);
        }
        public static List<UserDTO> Search(string FirstName, string LastName, string SMS, string Email, string Phone, string UserName, string Password)
        {
            List<User> users = RenterDAL.Search(FirstName,LastName,SMS, Email, Phone, UserName, Password);
            List<UserDTO> udto = new List<UserDTO>();
            foreach (User u in users)
                udto.Add(new UserDTO(u));
            return udto;
        }
    }
}
