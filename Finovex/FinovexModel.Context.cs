﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finovex
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FinovexEntities : DbContext
    {
        public FinovexEntities()
            : base("name=FinovexEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DeleteHistory> DeleteHistories { get; set; }
        public virtual DbSet<DownloadHistory> DownloadHistories { get; set; }
        public virtual DbSet<FileList> FileLists { get; set; }
        public virtual DbSet<UpdateHistory> UpdateHistories { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<UploadHistory> UploadHistories { get; set; }
    }
}
