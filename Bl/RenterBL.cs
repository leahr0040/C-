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
        public static List<UserDTO> ConvertListToDTO(List<User> renters)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<UserDTO> udto = new List<UserDTO>();
                foreach (User u in renters)
                    udto.Add(new UserDTO(u));
                return udto;
            }
            return null;
        }
        public static List<UserDTO> Search(string FirstName, string LastName, string SMS, string Email, string Phone, string UserName, string Password)
        {
            List<User> users = RenterDAL.Search(FirstName,LastName,SMS, Email, Phone, UserName, Password);
            return ConvertListToDTO(users);
        }
        public static List<UserDTO> GetAllRenters()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<User> renters = (from r in db.Users where r.UserRole.RoleName == "שוכר" select r).ToList();
                return ConvertListToDTO(renters);
            }
        }
        public static UserDTO GetRenterByID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                User renter = db.Users.Find(id );
                return new UserDTO(renter);
            }
        }
        public static List<RentalDTO> getRentalsbyRenterID(int id)//פרטי השכרה לפי איידי
        {
            List<Rental> rentals = RenterDAL.getRentalsbyRenterID(id);
            return Bl.RentalBL.ConvertListToDTO(rentals);
        }

        public static List<PropertyDTO> getPropertiesbyRenterID(int id)//דירות ששוכר לפי איידי
        {
            List<Property> properties = RenterDAL.getPropertiesbyRenterID(id);
            return Bl.PropertyBL.ConvertListToDTO(properties);
            
        }
    }
}
