using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Dal;

namespace Bl
{
   public  class DocumentBL
    {
        public static bool AddUserDocuments(DocumentDTO doc)
        {
            Document d = DocumentDTO.ToDAL(doc);
            return DocumentDAL.AddUserDocuments(d);
        }
        public static string[] GetUserDocuments(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                string[] documents = (from d in db.Documents where d.DocUser == id select d.DocCoding).ToArray();
                return documents;
            }

        }
    }
}
