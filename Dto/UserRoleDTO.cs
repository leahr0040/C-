using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class UserRoleDTO
    {
       
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public UserRoleDTO()
        {
        }
        public UserRoleDTO(UserRole ur)
        {
            RoleID =ur.RoleID;
            RoleName =ur.RoleName;
        }
        public static UserRole ToDAL(UserRoleDTO urDTO)
        {
            return new UserRole
            {
                RoleID = urDTO.RoleID,
                RoleName = urDTO.RoleName
            };
        }
    }
}
