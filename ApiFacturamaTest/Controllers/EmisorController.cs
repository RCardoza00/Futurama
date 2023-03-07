using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Facturama;
using Facturama.Models.Request;
using System.Configuration;
using ApiFacturamaTest.Models;
using Facturama.Models.Retentions;

namespace ApiFacturamaTest.Controllers
{
	public class EmisorController : ApiController
	{
		string conString = ConfigurationManager.ConnectionStrings["CfdiModel"].ToString();

		public dynamic Get()
		{
			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFEEmisorRecuperar", con);
				SqlDataAdapter ada = new SqlDataAdapter(command);
				ada.SelectCommand.CommandType = CommandType.StoredProcedure;
				ada.SelectCommand.Parameters.AddWithValue("@accion", 1);
				DataTable dt = new DataTable();
				ada.Fill(dt);
				List<EmisorModel> Emisores = new List<EmisorModel>();
				con.Open();
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						EmisorModel emisor = new EmisorModel();
						emisor.EmisorRfc = dt.Rows[i]["EMISORRFC"].ToString();
						emisor.RazonSocial = dt.Rows[i]["EMISORRAZSOCIAL"].ToString();
						emisor.EmisorStatus = dt.Rows[i]["EMISORESTATUS"].ToString();
						emisor.EmisorRegimFiscal = dt.Rows[i]["EMISORREGFISCAL"].ToString();
						emisor.EmisorCorreo = dt.Rows[i]["EMISORCORREO"].ToString();
						emisor.EmisorLogoUrl = dt.Rows[i]["EMISORLOGOURL"].ToString();
						emisor.EmisorCodPostal = dt.Rows[i]["EMISORCODPOSTAL"].ToString();
						emisor.EmisorMunicipio = dt.Rows[i]["EMISORMUNICIPIO"].ToString();
						emisor.EmisorEstado = dt.Rows[i]["EMISORESTADO"].ToString();
						emisor.EmisorColonia = dt.Rows[i]["EMISORCOLONIA"].ToString();
						emisor.EmisorCalle = dt.Rows[i]["EMISORCALLE"].ToString();
						emisor.EmisorNoExterior = dt.Rows[i]["EMISORNOEXTERIOR"].ToString();
						emisor.EmisorNoInterior = dt.Rows[i]["EMISORNOINTERIOR"].ToString();
						emisor.EmisorFolioInic = dt.Rows[i]["EMISORFOLIOINIC"].ToString();

						Emisores.Add(emisor);
					}
				}
				return Emisores;
			}
			
		}
		public dynamic PostEmisor([FromBody] EmisorModel emisor)
		{
			var facturama = new FacturamaApiMultiemisor("pruebas", "pruebas2011");

			int idi = 0;

			using (SqlConnection con = new SqlConnection(conString))
			{
				SqlCommand command = new SqlCommand("procMRGFEEmisorCrear", con);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@EMISORRFC", emisor.EmisorRfc);
				command.Parameters.AddWithValue("@EMISORRAZSOCIAL", emisor.RazonSocial);
				command.Parameters.AddWithValue("@EMISORESTATUS", emisor.EmisorStatus);
				command.Parameters.AddWithValue("@EMISORREGFISCAL", emisor.EmisorRegimFiscal);
				command.Parameters.AddWithValue("@EMISORCORREO", emisor.EmisorCorreo);
				command.Parameters.AddWithValue("@EMISORLOGOURL", emisor.EmisorLogoUrl);
				command.Parameters.AddWithValue("@EMISORCODPOSTAL", emisor.EmisorCodPostal);
				command.Parameters.AddWithValue("@EMISORMUNICIPIO", SqlDbType.VarChar).Value = emisor.EmisorMunicipio;
				command.Parameters.AddWithValue("@EMISORESTADO", SqlDbType.VarChar).Value = emisor.EmisorEstado;
				command.Parameters.AddWithValue("@EMISORCOLONIA", SqlDbType.VarChar).Value = emisor.EmisorColonia;
				command.Parameters.AddWithValue("@EMISORCALLE", SqlDbType.VarChar).Value = emisor.EmisorCalle;
				command.Parameters.AddWithValue("@EMISORNOEXTERIOR", SqlDbType.VarChar).Value = emisor.EmisorNoExterior;
				command.Parameters.AddWithValue("@EMISORNOINTERIOR", SqlDbType.VarChar).Value = emisor.EmisorNoInterior;
				command.Parameters.AddWithValue("@EMISORFOLIOINIC", SqlDbType.VarChar).Value = emisor.EmisorFolioInic;
				//
				con.Open();
				//if query is executed succesfully we'll get value 1 otherwise we'll get 0
				idi = command.ExecuteNonQuery();
				con.Close();
			}
			return emisor;
		}
	}
}

