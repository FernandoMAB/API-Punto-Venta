using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Documento
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string DocExtension { get; set; }
        public string DocBase64 { get; set; }
        public string DocStatus { get; set; }

        public int DocIdUploader { get; set; }
        public int DocIdClient { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }
        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }

        public Documento (int docId, string docName, string docExtension, string docBase64, string docStatus, int docIdUploader, int docIdClient)
        {
            DocId = docId;
            DocName = docName;
            DocExtension = docExtension;
            DocBase64 = docBase64;
            DocStatus = docStatus;
            DocIdUploader = docIdUploader;
            DocIdClient = docIdClient;
        }
    }
}
