using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class CityDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public CityDTO()
        {

        }
        public CityDTO(City c)
        {
            CityId = c.CityId;
            CityName = c.CityName;
        }
        public CityDTO(getAllCities_Result c)
        {
            CityId = c.CityId;
            CityName = c.CityName;
        }
        public static City ToDAL(CityDTO cDTO)
        {
            return new City
            {
                CityId = cDTO.CityId,
                CityName = cDTO.CityName
            };
        }
    }
}
