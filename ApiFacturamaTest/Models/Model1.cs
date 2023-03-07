using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ApiFacturamaTest.Models
{
	public partial class Model1 : DbContext
	{
		public Model1()
			: base("name=ModelFactura")
		{
		}

		public virtual DbSet<tblMRGFECFDIs> tblMRGFECFDIs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIID)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIFOLIOFISCAL)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDISERIE)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIRSOCEMISOR)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIRFCEMISOR)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIRSOCRECEPTOR)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIRFCRECEPTOR)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDITOTAL)
				.HasPrecision(19, 4);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIEMAIL)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIESACTIVO)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIURLPDF)
				.IsUnicode(false);

			modelBuilder.Entity<tblMRGFECFDIs>()
				.Property(e => e.CFDIURLXML)
				.IsUnicode(false);
		}

		public System.Data.Entity.DbSet<ApiFacturamaTest.Models.FacturaModel> FacturaModels { get; set; }
	}
}
