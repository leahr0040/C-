using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        public static bool DeleteUserDocument(DocumentDTO doc)
        {
            Document d = DocumentDTO.ToDAL(doc);
            return DocumentDAL.DeleteUserDocuments(d);
        }
        public static List<DocumentDTO> GetUserDocuments(int id,int type)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Document> documents = (from d in db.Documents where d.DocUser == id && d.type==type select d).ToList();
                List<DocumentDTO> docks = new List<DocumentDTO>();
                foreach (Document document in documents)
                {
                    docks.Add(new DocumentDTO(document));
                }
                return docks;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getUserDocumentsEror " + e.Message);
                return null;
            }

        }
        public static List<DocumentDTO> GetAllDocuments()
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<getAllDocuments_Result> documents = (from d in db.getAllDocuments() select d).ToList();
                List<DocumentDTO> docks = new List<DocumentDTO>();
                foreach (getAllDocuments_Result document in documents)
                {
                    docks.Add(new DocumentDTO(document));
                }
                return docks;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("getDocumentsEror " + e.Message);
                return null;
            }

        }
    }
}
