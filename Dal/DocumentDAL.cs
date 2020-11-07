using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DocumentDAL
    {
        public static bool AddUserDocuments(Document doc)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
               
                db.Documents.Add(doc);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool DeleteUserDocuments(Document doc)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Document d = db.Documents.Find(doc.DocID);
                db.Documents.Remove(d);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
