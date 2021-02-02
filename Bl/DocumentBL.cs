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
    public class DocumentBL
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
        public static List<DocumentDTO> GetUserDocuments(int id, int type)
        {
            try
            {
               
                    List<Document> documents = DocumentDAL.GetUserDocuments(id, type);
                    List<DocumentDTO> docks = new List<DocumentDTO>();
                    foreach (Document document in documents)
                    {
                        docks.Add(new DocumentDTO(document));
                    }
                    return docks;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getUserDocumentsblEror " + e.Message);
                return null;
            }

        }
        public static List<DocumentDTO> GetAllDocuments()
        {
            try
            {
                {
                    List<getAllDocuments_Result> documents = DocumentDAL.GetAllDocuments();
                    List<DocumentDTO> docks = new List<DocumentDTO>();
                    foreach (getAllDocuments_Result document in documents)
                    {
                        docks.Add(new DocumentDTO(document));
                    }
                    return docks;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getDocumentsblEror " + e.Message);
                return null;
            }

        }
    }
}
