using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class SubPropertyDTO
    {
        
        public int SubPropertyID { get; set; }
        public int PropertyID { get; set; }
        public int num { get; set; }
        public bool IsRented { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<double> RoomsNum { get; set; }
        public string Dock { get; set; }
        public string DocName { get; set; }
        public Nullable<bool> status { get; set; }
        public SubPropertyDTO(SubProperty sp)
        {
            this.SubPropertyID =sp.SubPropertyID;
            this.PropertyID =sp.PropertyID;
            this.num = sp.num;
            this.IsRented = sp.IsRented;
            this.Size = sp.Size;
            this.RoomsNum = sp.RoomsNum;
            status = true;
        }
        public static SubProperty ToDal(SubPropertyDTO sp)
        {
            return new SubProperty
            {
                SubPropertyID = sp.SubPropertyID,
                PropertyID = sp.PropertyID,
                num = sp.num,
                IsRented = sp.IsRented,
                Size = sp.Size,
                RoomsNum = sp.RoomsNum
            };
        }
    }
}
