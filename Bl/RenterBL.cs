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
            ud.RoleID = 3;
            User u = UserDTO.ToDal(ud);
            int id= UserDAL.AddUser(u);
            if (id != 0)
            {
                if (ud.Dock != null)
                {
                    Document doc = new Document();
                    doc.DocCoding = ud.Dock;
                    doc.DocUser = id;
                    doc.type = 4;
                    doc.DocName = ud.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));
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
                Document doc = new Document();
                doc.DocCoding = ud.Dock;
                doc.DocUser = ud.UserID;
                doc.type = 4;
                doc.DocName = ud.DocName;
                DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                
            }
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
        public static List<UserDTO> ConvertListToDTO(List<getAllUsers_Result> renters)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<UserDTO> udto = new List<UserDTO>();
                foreach (getAllUsers_Result u in renters)
                    udto.Add(new UserDTO(u));
                return udto;
            }
            return null;
        }
        public static List<UserDTO> Search(string FirstName, string LastName, string SMS, string Email, string Phone)
        {
            List<User> users = RenterDAL.Search(FirstName,LastName,SMS, Email, Phone);
            return ConvertListToDTO(users);
        }
        public static List<UserDTO> GetAllRenters()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<getAllUsers_Result> renters = (from r in db.getAllUsers() where r.RoleID==3  select r).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();
                return ConvertListToDTO(renters);
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
            return Bl.PropertyBL.ConvertListToDTO(properties);
            
        }
    }
}
