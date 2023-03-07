namespace ApiFacturamaTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblMRGFECFDIs
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string CFDIID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string CFDIFOLIOFISCAL { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string CFDISERIE { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string CFDIRSOCEMISOR { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string CFDIRFCEMISOR { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string CFDIRSOCRECEPTOR { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string CFDIRFCRECEPTOR { get; set; }

        [Key]
        [Column(Order = 7)]
        public DateTime CFDIFECHA { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "money")]
        public decimal CFDITOTAL { get; set; }

        [StringLength(50)]
        public string CFDIEMAIL { get; set; }

        [StringLength(50)]
        public string CFDIESACTIVO { get; set; }

        public bool? CFDIEMAILENVIADO { get; set; }

        public byte[] CFDIPDF { get; set; }

        public byte[] CFDIXML { get; set; }

        public bool? CFDIPROCESADO1PDF { get; set; }

        public bool? CFDIPROCESADO1XML { get; set; }

        [StringLength(8000)]
        public string CFDIURLPDF { get; set; }

        [StringLength(8000)]
        public string CFDIURLXML { get; set; }

        public DateTime? CFDIFECHAPROCESADOPDF { get; set; }

        public DateTime? CFDIFECHAPROCESADOXML { get; set; }
    }
}
