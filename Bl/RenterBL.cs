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
                ud.RoleID = (from r in db.UserRoles where r.RoleName == "שוכר" select r.RoleID).FirstOrDefault();
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
        public static List<RentalDTO> getRentalsbyRenterID(int id)//פרטי השכרה לפי איידי
        {
            List<Rental> renters = RenterDAL.getRentalsbyRenterID(id);
            List<RentalDTO> rdto = new List<RentalDTO>();
            foreach (Rental r in renters)
                rdto.Add(new RentalDTO(r));
            return rdto;
        }

        public static List<PropertyDTO> getPropertiesbyRenterID(int id)//דירות ששוכר לפי איידי
        {
            List<Property> properties = RenterDAL.getPropertiesbyRenterID(id);
            List<PropertyDTO> pdto = new List<PropertyDTO>();
            foreach (Property p in properties)
                pdto.Add(new PropertyDTO(p));
            return pdto;
            
        }
    }
}
