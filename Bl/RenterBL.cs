using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;
using System.Diagnostics;


namespace Bl
{

    public class RenterBL
    {
        public static bool AddRenter(UserDTO ud)
        {
            ud.RoleID = 3;
            User u = UserDTO.ToDal(ud);
            int id= UserDAL.AddUser(u);
            if (id != 0)
            {
                if (ud.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO( id,ud.Dock,4,ud.DocName));
                }
                return true;
            }
            return false;
        }
        public static bool DeleteRenter(int id)
        {
            return UserBL.DeleteUser(id);
        }
        public static bool UpdateRenter(UserDTO ud)
        {
            if ( ud.Dock != null)
            {
                DocumentBL.AddUserDocuments(new DocumentDTO( ud.UserID,ud.Dock,4,ud.DocName));
                
            }
            return UserBL.UpdateUser(ud);
        }
        
        public static List<UserDTO> Search(string FirstName, string LastName, string SMS, string Email, string Phone)
        {
            List<User> users = RenterDAL.Search(FirstName,LastName,SMS, Email, Phone);
            return UserDTO. ConvertListToDTO(users);
        }
        public static List<UserDTO> GetAllRenters()
        {
            try { 
            List<getAllUsers_Result> renters = RenterDAL.GetAllRenters();
                return UserDTO.ConvertListToDTO(renters);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllRentersEror " + e.Message);
                return null;
            }
        }
        //public static UserDTO GetRenterByID(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        User renter = db.Users.Find(id );
        //        return new UserDTO(renter);
        //    }
        //}
        //public static List<RentalDTO> getRentalsbyRenterID(int id)//פרטי השכרה לפי איידי
        //{
        //    List<Rental> rentals = RenterDAL.getRentalsbyRenterID(id);
        //    return Bl.RentalBL.ConvertListToDTO(rentals);
        //}

        public static List<PropertyDTO> getPropertiesbyRenterID(int id)//דירות ששוכר לפי איידי
        {
            List<Property> properties = RenterDAL.getPropertiesbyRenterID(id);
            return PropertyDTO.ConvertListToDTO(properties);
            
        }
    }
}
