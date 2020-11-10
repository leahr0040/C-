using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class StreetDTO
    {
        public int StreetID { get; set; }
        public string StreetName { get; set; }
        public Nullable<int> CityId { get; set; }
        public StreetDTO()
        {
        }
        public StreetDTO(Street s)
        {
            StreetID = s.StreetID;
            StreetName = s.StreetName;
            CityId = s.CityId;
        }
        public StreetDTO(getStreets_Result s)
        {
            StreetID = s.StreetID;
            StreetName = s.StreetName;
            CityId = s.CityId;
        }
        public static Street ToDAL(StreetDTO sDTO)
        {
            return new Street
            {
                StreetID = sDTO.StreetID,
                StreetName = sDTO.StreetName,
                CityId = sDTO.CityId
            };
        }
    }
}
