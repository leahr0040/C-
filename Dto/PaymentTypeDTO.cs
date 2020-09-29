using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class PaymentTypeDTO
    {
        public int PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
        public PaymentTypeDTO()
        {

        }
        public PaymentTypeDTO(PaymentType pt)
        {
            this.PaymentTypeID = pt.PaymentTypeID;
            this.PaymentTypeName = pt.PaymentTypeName;
        }
        public static PaymentType ToDAL(PaymentTypeDTO ptDTO)
        {
            return new PaymentType
            {
                PaymentTypeID = ptDTO.PaymentTypeID,
                PaymentTypeName = ptDTO.PaymentTypeName
            };
        }
    }
}
