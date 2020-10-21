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
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SMS { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Dock { get; set; }
        public string DocName { get; set; }
        public bool status { get; set; }
        public UserDTO()
        {

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
            status = true;
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
}
