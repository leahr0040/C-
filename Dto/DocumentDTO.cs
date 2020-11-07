using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class DocumentDTO
    {
        public int DocID { get; set; }
        public int DocUser { get; set; }
        public Nullable<int> type { get; set; }
        public string DocCoding { get; set; }
        public string DocName { get; set; }
        public DocumentDTO()
        {
            
        }
        public DocumentDTO(Document d)
        {
            this.DocID = d.DocID;
            this.DocUser = d.DocUser;
            this.DocCoding = d.DocCoding;
           this.type = d.type;
            this.DocName = d.DocName;
        }
        public static Document ToDAL(DocumentDTO d)
        {
            return new Document
            {
                DocID = d.DocID,
                DocUser = d.DocUser,
                DocCoding = d.DocCoding,
                type=d.type,
               DocName=d.DocName
            };
        }
    }
}
