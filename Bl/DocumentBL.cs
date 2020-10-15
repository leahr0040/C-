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
        public static List<DocumentDTO> GetUserDocuments(int id,int type)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Document> documents = (from d in db.Documents where d.DocUser == id select d).ToList();
                List<DocumentDTO> docks = new List<DocumentDTO>();
                foreach (Document document in documents)
                {
                    docks.Add(new DocumentDTO(document));
                }
                return docks;
            }

        }
    }
}
