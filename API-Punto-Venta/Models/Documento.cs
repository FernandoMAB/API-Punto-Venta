using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Punto_Venta.Models
{
    public class Documento
    {
        /// <summary>
        /// Código del documento.
        /// </summary>
        /// <example>12</example>
        public int DocId { get; set; }
        
        /// <summary>
        /// Nombre del documento.
        /// </summary>
        /// <example>Documento Ejemplo</example>
        [MaxLength(100)]
        public string DocName { get; set; }
        
        /// <summary>
        /// Extensión del documento.
        /// </summary>
        /// <example>pdf</example>
        [MaxLength(10)]
        public string DocExtension { get; set; }
        
        /// <summary>
        /// Documento transformado a base 64.
        /// </summary>
        /// <example>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</example>
        public string DocBase64 { get; set; }
        
        /// <summary>
        /// Estado del Catálogo, V: Vigente  -  E: Eliminado.
        /// </summary>
        /// <example>V</example>
        [MaxLength(1)]
        [MinLength(1)]
        public string DocStatus { get; set; }

        /// <summary>
        /// Código Id usuario que agrego el documento.
        /// </summary>
        /// <example>1</example>
        public int DocIdUploader { get; set; }
        
        /// <summary>
        /// Código Id del cliente del documento.
        /// </summary>
        /// <example>12</example>
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
