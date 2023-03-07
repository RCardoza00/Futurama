using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiFacturamaTest.Models;
using Facturama;
using Facturama.Models;
using Facturama.Models.Complements;
using Facturama.Models.Complements.Payroll;
using Facturama.Models.Request;
using Facturama.Models.Response;
using Issuer = Facturama.Models.Request.Issuer;
using Item = Facturama.Models.Request.Item;
using Receiver = Facturama.Models.Request.Receiver;
using Facturama.Services;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace ApiFacturamaTest.Controllers
{
    public class CdfiController : ApiController
    {
		string conString = ConfigurationManager.ConnectionStrings["CfdiModel"].ToString();
		private byte[] pdf;
		private byte[] xml;

		//get datos
		public dynamic Get()
        {
			//var facturama = new FacturamaApiMultiemisor("pruebas", "pruebas2011");

			//var cdfis = facturama.Cfdis.List();


			//return cdfis;
			var result = new List<string>();
			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFECFDIsRecuperacionCFDIs", con);
				SqlDataAdapter ada= new SqlDataAdapter(command);
				//command.CommandType = CommandType.StoredProcedure;
				//command.Connection = con;
				//command.Parameters.AddWithValue("@accion", SqlDbType.VarChar).Value = 3;
				//command.Parameters.AddWithValue("@CFDIFECHAFIN", SqlDbType.VarChar).Value = "20230702";
				ada.SelectCommand.CommandType= CommandType.StoredProcedure;
				ada.SelectCommand.Parameters.AddWithValue("@accion", 1);

				DataTable dt = new DataTable();
				ada.Fill(dt);
				List<FacturaModel> facturaModels = new List<FacturaModel>();
				con.Open();
				//command.ExecuteNonQuery();
				if(dt.Rows.Count > 0)
				{
					for(int i=0; i < dt.Rows.Count; i++)
					{
						FacturaModel facturita = new FacturaModel();
						facturita.CfdiId = dt.Rows[i]["CFDIID"].ToString();
						facturita.CfdiFolioFiscal = dt.Rows[i]["CFDIFOLIOFISCAL"].ToString() ;
						facturita.CfdiSerie = dt.Rows[i]["CFDISERIE"].ToString();
						facturita.CfdiRSocEmisor = dt.Rows[i]["CFDIRSOCEMISOR"].ToString();
						facturita.CfdiRfcEmisor = dt.Rows[i]["CFDIRFCEMISOR"].ToString();
						facturita.CfdiRSocReceptor = dt.Rows[i]["CFDIRSOCRECEPTOR"].ToString();
						facturita.CfdiRfcReceptor = dt.Rows[i]["CFDIRFCRECEPTOR"].ToString();
						facturita.CfdiFecha = (DateTime)dt.Rows[i]["CFDIFECHA"];
						facturita.CfdiTotal = Convert.ToDouble(dt.Rows[i]["CFDITOTAL"]);



						facturaModels.Add(facturita);
					}
				}
				command.ExecuteNonQuery();
				command.ExecuteReader();
				return facturaModels;


			}
		
		}

		//obtener cdfi por id
		/// <summary>
		/// Looks up some data by ID.
		/// </summary>
		/// <param name="id">The ID of the data.</param>
		public dynamic asd(string id)
        {
			var facturama = new FacturamaApiMultiemisor("pruebas", "pruebas2011");
			var Cdfi = facturama.Cfdis.Retrieve(id);
			//SqlConnection con = new SqlConnection(conString);
			//con.Open();
			//SqlCommand com = new SqlCommand("SELECT  PDF,XML FROM Facturas where id=@id ", con);
			//com.Parameters.AddWithValue("@id", id);
			////
			//using (SqlDataReader oReader = com.ExecuteReader())
			//{
			//	while (oReader.Read())
			//	{
			//		pdf = (byte[])oReader["PDF"];
			//		xml = (byte[])oReader["XML"];
			//	}

			//	con.Close();
			//}
			////


			////if query is executed succesfully we'll get value 1 otherwise we'll get 0
			////byte[] asd = com.ExecuteScalar() as Byte[];
			
			//string folioFiscal=Cdfi.Complement.TaxStamp.Uuid.ToString();
			
			//	//byte[] stream = (byte[])Encoding.ASCII.GetBytes(str);
				
			//File.WriteAllBytes("C:\\Users\\esnip/"+folioFiscal+".pdf", pdf);
			//File.WriteAllBytes("C:\\Users\\esnip/" + folioFiscal + ".xml", xml);
			



			return Cdfi;
		}


		[System.Web.Http.HttpGet, System.Web.Http.Route("api/cdfiById/{id}")]
		public dynamic getFacturaporId(string id)
		{
			FacturaModel facturaFolio = new FacturaModel();
			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFECFDIsRecuperacionCFDIs", con);
				SqlDataAdapter ada = new SqlDataAdapter(command);
				ada.SelectCommand.CommandType = CommandType.StoredProcedure;
				ada.SelectCommand.Parameters.AddWithValue("@accion", 2);
				ada.SelectCommand.Parameters.AddWithValue("@CFDIID", id);
				DataTable dt = new DataTable();
				ada.Fill(dt);

				if (dt.Rows.Count > 0)
				{



					facturaFolio.CfdiId = dt.Rows[0]["CFDIID"].ToString();
					facturaFolio.CfdiFolioFiscal = dt.Rows[0]["CFDIFOLIOFISCAL"].ToString();
					facturaFolio.CfdiSerie = dt.Rows[0]["CFDISERIE"].ToString();
					facturaFolio.CfdiRSocEmisor = dt.Rows[0]["CFDIRSOCEMISOR"].ToString();
					facturaFolio.CfdiRfcEmisor = dt.Rows[0]["CFDIRFCEMISOR"].ToString();
					facturaFolio.CfdiRSocReceptor = dt.Rows[0]["CFDIRSOCRECEPTOR"].ToString();
					facturaFolio.CfdiRfcReceptor = dt.Rows[0]["CFDIRFCRECEPTOR"].ToString();
					facturaFolio.CfdiFecha = (DateTime)dt.Rows[0]["CFDIFECHA"];
					facturaFolio.CfdiTotal = Convert.ToDouble(dt.Rows[0]["CFDITOTAL"]);

					return facturaFolio;



				}
				else
				{
					return "no se encontraron registros";
				}

			}
		}
		[System.Web.Http.HttpGet, System.Web.Http.Route("api/CdfiByFolio/{folio}")]
		public dynamic getFacturaPorFolio(string folio)
		{
			FacturaModel facturaFolio = new FacturaModel();
			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFECFDIsRecuperacionCFDIs", con);
				SqlDataAdapter ada = new SqlDataAdapter(command);
				ada.SelectCommand.CommandType = CommandType.StoredProcedure;
				ada.SelectCommand.Parameters.AddWithValue("@accion", 3);
				ada.SelectCommand.Parameters.AddWithValue("@CFDIFOLIOFISCAL", folio);
				DataTable dt = new DataTable();
				ada.Fill(dt);
				
				if (dt.Rows.Count > 0)
				{



					facturaFolio.CfdiId = dt.Rows[0]["CFDIID"].ToString();
					facturaFolio.CfdiFolioFiscal = dt.Rows[0]["CFDIFOLIOFISCAL"].ToString();
					facturaFolio.CfdiSerie = dt.Rows[0]["CFDISERIE"].ToString();
					facturaFolio.CfdiRSocEmisor = dt.Rows[0]["CFDIRSOCEMISOR"].ToString();
					facturaFolio.CfdiRfcEmisor = dt.Rows[0]["CFDIRFCEMISOR"].ToString();
					facturaFolio.CfdiRSocReceptor = dt.Rows[0]["CFDIRSOCRECEPTOR"].ToString();
					facturaFolio.CfdiRfcReceptor = dt.Rows[0]["CFDIRFCRECEPTOR"].ToString();
					facturaFolio.CfdiFecha = (DateTime)dt.Rows[0]["CFDIFECHA"];
					facturaFolio.CfdiTotal = Convert.ToDouble(dt.Rows[0]["CFDITOTAL"]);





				}
				else
				{
					return "no se encontraron registros";
				}

			}

			
			return facturaFolio;
		}

		[System.Web.Http.HttpGet, System.Web.Http.Route("api/CdfiMultiFiltro/")]
		public dynamic GetFacturaFiltros(string fechaInicio,string fechaFin,string rfcReceptor="",string rfcEmisor = "")
		{
			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFECFDIsRecuperacionCFDIs", con);
				SqlDataAdapter ada = new SqlDataAdapter(command);
				//command.CommandType = CommandType.StoredProcedure;
				//command.Connection = con;
				//command.Parameters.AddWithValue("@accion", SqlDbType.VarChar).Value = 3;
				//command.Parameters.AddWithValue("@CFDIFECHAFIN", SqlDbType.VarChar).Value = "20230702";
				ada.SelectCommand.CommandType = CommandType.StoredProcedure;
				ada.SelectCommand.Parameters.AddWithValue("@accion", 4);
				ada.SelectCommand.Parameters.AddWithValue("@CFDIFECHAINICIO", fechaInicio);
				ada.SelectCommand.Parameters.AddWithValue("@CFDIFECHAFIN", fechaFin);
				ada.SelectCommand.Parameters.AddWithValue("@CFDIRFCRECEPTOR", rfcReceptor);
				ada.SelectCommand.Parameters.AddWithValue("@CFDIRFCEMISOR", rfcEmisor); 
				DataTable dt = new DataTable();
				ada.Fill(dt);
				List<FacturaModel> facturaModels = new List<FacturaModel>();
				con.Open();
				//command.ExecuteNonQuery();
				command.ExecuteNonQuery();
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						FacturaModel facturita = new FacturaModel();
						facturita.CfdiId = dt.Rows[i]["CFDIID"].ToString();
						facturita.CfdiFolioFiscal = dt.Rows[i]["CFDIFOLIOFISCAL"].ToString();
						facturita.CfdiSerie = dt.Rows[i]["CFDISERIE"].ToString();
						facturita.CfdiRSocEmisor = dt.Rows[i]["CFDIRSOCEMISOR"].ToString();
						facturita.CfdiRfcEmisor = dt.Rows[i]["CFDIRFCEMISOR"].ToString();
						facturita.CfdiRSocReceptor = dt.Rows[i]["CFDIRSOCRECEPTOR"].ToString();
						facturita.CfdiRfcReceptor = dt.Rows[i]["CFDIRFCRECEPTOR"].ToString();
						facturita.CfdiFecha = (DateTime)dt.Rows[i]["CFDIFECHA"];
						facturita.CfdiTotal = Convert.ToDouble(dt.Rows[i]["CFDITOTAL"]);



						facturaModels.Add(facturita);
						
					}
					return facturaModels;
				}
				else
				{
					return "No registros";
				}
				
			}
		}



		//generar factura
		public dynamic PostFactura([FromBody] CfdiMulti factura)
        {

			var facturama = new FacturamaApiMultiemisor("pruebas", "pruebas2011");
			
			var cfdiCreated = facturama.Cfdis.Create(factura);
			var savepdf = facturama.Cfdis.GetFile(cfdiCreated.Id, CfdiLiteService.FileFormat.Pdf);
			var saveXml = facturama.Cfdis.GetFile(cfdiCreated.Id, CfdiLiteService.FileFormat.Xml);
			//date of the processed files
			DateTime now = DateTime.Now;

			int idi = 0;
			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFECFDIsCrear", con);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@CFDIID", cfdiCreated.Id);
				command.Parameters.AddWithValue("@CFDIFOLIOFISCAL", cfdiCreated.Complement.TaxStamp.Uuid);
				command.Parameters.AddWithValue("@CFDISERIE", cfdiCreated.Serie);
				command.Parameters.AddWithValue("@CFDIRSOCEMISOR", cfdiCreated.Issuer.TaxName);
				command.Parameters.AddWithValue("@CFDIRFCEMISOR", cfdiCreated.Issuer.Rfc);
				command.Parameters.AddWithValue("@CFDIRSOCRECEPTOR", cfdiCreated.Receiver.Name);
				command.Parameters.AddWithValue("@CFDIRFCRECEPTOR", cfdiCreated.Receiver.Rfc);
				command.Parameters.AddWithValue("@CFDIFECHA", cfdiCreated.Date);
				command.Parameters.AddWithValue("@CFDITOTAL", cfdiCreated.Total);
				command.Parameters.AddWithValue("@CFDIEMAIL", cfdiCreated.Receiver.Email);
				command.Parameters.AddWithValue("@CFDIESACTIVO", cfdiCreated.Status);
				command.Parameters.AddWithValue("@CFDIEMAILENVIADO", cfdiCreated.SendMail);
				command.Parameters.AddWithValue("@CFDIPDF", Convert.FromBase64String(savepdf.Content));
				command.Parameters.AddWithValue("@CFDIXML", Convert.FromBase64String(saveXml.Content));
				command.Parameters.AddWithValue("@CFDIPROCESADO1PDF", 1);
				command.Parameters.AddWithValue("@CFDIPROCESADO1XML", 1);
				command.Parameters.AddWithValue("@CFDIURLPDF", "TESTURLPDF");
				command.Parameters.AddWithValue("@CFDIURLXML", "TESTURLXML");
				command.Parameters.AddWithValue("@CFDIFECHAPROCESADOPDF", now);
				command.Parameters.AddWithValue("@CFDIFECHAPROCESADOXML", now);
				//open connectionasd
				con.Open();
				//if query is executed succesfully we'll get value 1 otherwise we'll get 0
				idi = command.ExecuteNonQuery();
				con.Close();
			}
			var path = "C:\\Users\\esnip/factura.pdf";

			var base64Pdf = Convert.FromBase64String(savepdf.Content);
			File.WriteAllBytes(path, base64Pdf);

			return cfdiCreated;
		}

		
        
    }
}
