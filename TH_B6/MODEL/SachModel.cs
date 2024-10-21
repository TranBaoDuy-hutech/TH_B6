using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TH_B6.MODEL
{
    public partial class SachModel : DbContext
    {
        public SachModel()
            : base("name=SachModel")
        {
        }

        public virtual DbSet<LoaiSach> LoaiSach { get; set; }
        public virtual DbSet<Sach> Sach { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sach>()
                .Property(e => e.MaSach)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
