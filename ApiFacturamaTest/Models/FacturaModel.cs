using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiFacturamaTest.Models
{
	public class FacturaModel
	{
		public string CfdiId { get; set; }

		public string CfdiFolioFiscal { get; set; }

		public string CfdiSerie { get; set; }

		public string CfdiRSocEmisor { get; set; }

		public string CfdiRfcEmisor { get; set; }

		public string CfdiRSocReceptor { get; set; }

		public string CfdiRfcReceptor { get; set; }

		public DateTime CfdiFecha { get; set; }

		public double CfdiTotal { get; set; }

		public string CfdiEmail { get; set; }

		public string CfdiEsActivo { get; set; }

		public byte CfdiEmailEnviado { get; set; }

		public byte[] CfdiIPdf { get; set; }

		public byte[] CfdiIXml { get; set; }

		public byte CfdiProcesado1Pdf { get; set; }

		public byte CfdiProcesado1Xml { get; set; }

		public string CfdiUrlPdf { get; set; }

		public string CfdiUrlXml { get; set; }

		public DateTime CfdiFechaProcesadoPdf { get; set; }

		public DateTime CfdiFechaProcesadoXml { get; set; }
	}
}