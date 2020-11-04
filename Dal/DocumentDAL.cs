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
    }
}
