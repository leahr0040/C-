using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Dal
{
    public class DocumentDAL
    {
        public static bool AddUserDocuments(Document doc)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
               
                db.Documents.Add(doc);
                db.SaveChanges();
                return true;
            }
            }
            catch(Exception e) {
               Trace.TraceInformation("addDocument "+e.Message);
                return false;
            }
        }
        public static bool DeleteUserDocuments(Document doc)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Document d = db.Documents.Find(doc.DocID);
                db.Documents.Remove(d);
                db.SaveChanges();
                return true;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("addDocument " + e.Message);
                return false;
            }
        }
        public static List<Document> GetUserDocuments(int id, int type)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<Document> documents = (from d in db.Documents where d.DocUser == id && d.type == type select d).ToList();
                    return documents;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getUserDocumentsEror " + e.Message);
                return null;
            }
        }
        public static List<getAllDocuments_Result> GetAllDocuments()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllDocuments_Result> documents = (from d in db.getAllDocuments() select d).ToList();
                    return documents;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getDocumentsEror " + e.Message);
                return null;
            }

        }
    }
}
