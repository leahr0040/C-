using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class UserDTO
    {
        public int UserID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string SMS { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int RoleID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Dock { set; get; }
        public string DocName { set; get; }
        public bool status { set; get; }
        public UserDTO()
        {

        }
        public UserDTO(int UserID, string FirstName, string LastName, string SMS, string Email, string Phone, int RoleID, string UserName, string Password, string Dock, string DocName, bool status)
        {
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.SMS = SMS;
            this.Email = Email;
            this.Phone = Phone;
            this.RoleID = RoleID;
            this.UserName = UserName;
            this.Password = Password;
            this.status = status;
        }
        public UserDTO(User u)
        {
            this.UserID = u.UserID;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.SMS = u.SMS;
            this.Email = u.Email;
            this.Phone = u.Phone;
            this.RoleID = u.RoleID;
            this.UserName = u.UserName;
            this.Password = u.Password;
            this.status = true;
        }
        public static User ToDal(UserDTO u)
        {
            return new User
            {
                UserID = u.UserID,
                FirstName = u.FirstName,
                LastName = u.LastName,
                SMS = u.SMS,
                Email = u.Email,
                Phone = u.Phone,
                RoleID = u.RoleID,
                UserName = u.UserName,
                Password = u.Password
            };
        }

    }

    public class IdDto
    {
        public int id { set; get; }

        public IdDto() { }
    }

    public class NameDto
    {
            public string name { set; get; }
        public NameDto() { }
    }

    public class DoubleDto
    {
        public double dob { set; get; }
        public DoubleDto() { }
    }


    public class DtoRent
    {
        public int PropertyID { set; get; }
        public string Owner { set; get; }
        public string User { set; get; }
        public DateTime EnteryDate { set; get; }
        public DateTime EndDate { set; get; }

        public DtoRent() { }
    }
    public class Dtostrstr
    {
        public string username { set; get; }
        public string passemail { set; get; }
        
        public Dtostrstr() { }
    }

    public class Dtointint
    {
        public int id { set; get; }
        public int type { set; get; }

        public Dtointint() { }
    }
}
