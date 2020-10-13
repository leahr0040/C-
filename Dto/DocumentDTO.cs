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
        public string DocCoding { get; set; }
        public DocumentDTO(Document d)
        {
            this.DocID = d.DocID;
            this.DocUser = d.DocUser;
            this.DocCoding = d.DocCoding;
        }
        public static Document ToDAL(DocumentDTO d)
        {
            return new Document
            {
                DocID = d.DocID,
                DocUser = d.DocUser,
                DocCoding = d.DocCoding
            };
        }
    }
}
